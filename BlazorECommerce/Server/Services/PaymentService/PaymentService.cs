using BlazorECommerce.Shared;
using Stripe;
using Stripe.Checkout;
using static System.Net.WebRequestMethods;

namespace BlazorECommerce.Server.Services.PaymentService;

public class PaymentService : IPaymentService
{
    private readonly ICartService _cartService;
    private readonly IAuthService _authService;
    private readonly IOrderService _orderService;
    private readonly IShipService _shipService;
    private const string secret = "whsec_71f4b6646828cd8656236ac3a361c7074376f1addc2a8d0c5f0f669be8ce3855";

    public PaymentService(ICartService cartService, IAuthService authService, IOrderService orderService, IShipService shipService)
    {
        StripeConfiguration.ApiKey = "sk_test_51OYjTRAWLUSVP4FD3OHHmrAJTZQ3Pwd2Bt8pb6joAtuFFBKMRI15jyP7JuEKFlKporn6NDsTYmkqTI3apPwdVKdK00ZfWU3dJl";
        _cartService = cartService;
        _authService = authService;
        _orderService = orderService;
        _shipService = shipService;
    }

    public async Task<Session> CreateCheckoutSession()
    {
        var products = (await _cartService.GetDbCartProducts()).Data;
        var lineItems = products.Select(product => new SessionLineItemOptions
        {
            PriceData = new SessionLineItemPriceDataOptions
            {
                UnitAmountDecimal = product.Price * 100,
                Currency = "usd",
                ProductData = new SessionLineItemPriceDataProductDataOptions
                {
                    Name = product.Title,
                    Images = new List<string> 
                    {
                        product.ImageUrl,
                    }
                }
            },
            Quantity = product.Quantity,
        })
        .ToList();

        var shipInfo = (await _shipService.GetShipInfo()).Data;
        var createOpitons = new SessionCreateOptions
        {
            CustomerEmail = _authService.GetUserEmail(),
            //ShippingAddressCollection = new SessionShippingAddressCollectionOptions
            //{
            //    AllowedCountries = new List<string> { "CN", "GB", "TW", "US" },
            //},
            
            PaymentIntentData = new SessionPaymentIntentDataOptions
            {
                Shipping = shipInfo is not null 
                    ? new ChargeShippingOptions
                        {
                            Name = $"{shipInfo.FirstName} {shipInfo.LastName}",
                            Address = new AddressOptions
                            {
                                Country = shipInfo.Country,
                                State = shipInfo.State,
                                City = shipInfo.City,
                                Line1 = shipInfo.Line1,
                                Line2 = shipInfo.Line2,
                                PostalCode = shipInfo.PostalCode,
                            },
                            Phone = shipInfo.Phone,
                        }
                    : new ChargeShippingOptions(),
            },
            PaymentMethodTypes = new List<string>
            {
                "card",
            },
            LineItems = lineItems,
            Mode = "payment",
            SuccessUrl = "https://localhost:7269/order-success",
            CancelUrl = "https://localhost:7269/cart"
        };

        var service = new SessionService();
        Session session = service.Create(createOpitons);

        return session;
    }

    public async Task<ServiceResponse<bool>> FulfillOrder(HttpRequest request)
    {
        var json = await new StreamReader(request.Body).ReadToEndAsync();
        try
        {
            var stripeEvent = EventUtility.ConstructEvent(
                json,
                request.Headers["Stripe-Signature"],
                secret
            );

            if (stripeEvent.Type == Events.PaymentIntentSucceeded)
            {
                var paymentIntent = stripeEvent.Data.Object as PaymentIntent;
                var shipInfo = paymentIntent.Shipping;
                await _orderService.AddShipInfo(new OrderShipInfo
                {
                    Name = shipInfo.Name,
                    Country = shipInfo.Address.Country,
                    State = shipInfo.Address.State,
                    City = shipInfo.Address.City,
                    Line1 = shipInfo.Address.Line1,
                    Line2 = shipInfo.Address.Line2,
                    PostalCode = shipInfo.Address.PostalCode,
                    Phone = shipInfo.Phone,
                    Remark = paymentIntent.Id
                });
                Console.WriteLine(paymentIntent.Id);
            }
            else if (stripeEvent.Type == Events.CheckoutSessionCompleted)
            {
                var session = stripeEvent.Data.Object as Session;
                var user = await _authService.GetUserByEmail(session.CustomerEmail);
                await _orderService.PlaceOrder(user.Id,
                    session.PaymentIntentId
                );
                Console.WriteLine(session.PaymentIntentId);
            }

            return new ServiceResponse<bool>
            {
                Data = true,
            };
        } 
        catch(StripeException se)
        {
            return new ServiceResponse<bool>
            {
                Data = false,
                Success = false,
            };
        };
    }
}
