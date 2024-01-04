
namespace BlazorECommerce.Server.Data;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ProductVariant>()
            .HasKey(p => new { p.ProductId, p.ProductTypeId });
        modelBuilder.Entity<ProductType>().HasData(
            new ProductType { Id = 1, Name = "Default" },
            new ProductType { Id = 2, Name = "Paperback" },
            new ProductType { Id = 3, Name = "E-book" },
            new ProductType { Id = 4, Name = "Audiobook" },
            new ProductType { Id = 5, Name = "Stream" },
            new ProductType { Id = 6, Name = "Blu-ray" },
            new ProductType { Id = 7, Name = "VHS" },
            new ProductType { Id = 8, Name = "PC" },
            new ProductType { Id = 9, Name = "PlayStation" },
            new ProductType { Id = 10, Name = "Xbox" },
            new ProductType { Id = 11, Name = "Nintendo Switch" }
        );
        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                Id = 1,
                Name = "Books",
                Url = "books",
            },
            new Category
            {
                Id = 2,
                Name = "Movies",
                Url = "movies",
            },
            new Category
            {
                Id = 3,
                Name = "Video Games",
                Url = "video-games",
            }
        );
        modelBuilder.Entity<Product>().HasData(
             new Product
             {
                 Id = 1,
                 Title = "麥田捕手",
                 Description = "《麥田捕手》（英語：The Catcher in the Rye），中國大陸譯為《麥田裡的守望者》，為美國作家J.D.沙林傑於1951年發表的長篇小說。這部有爭議的作品原本是面向成年讀者的，但迅速因其青春期焦慮和隔絕的主題而在青少年讀者中流行。該書以主人公荷頓·考菲爾德第一人稱口吻講述自己被學校開除學籍後在紐約城遊蕩將近兩晝夜，企圖逃出虛偽的成人世界、去尋求純潔與真理的經歷與感受。",
                 ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/8/89/The_Catcher_in_the_Rye_%281951%2C_first_edition_cover%29.jpg/413px-The_Catcher_in_the_Rye_%281951%2C_first_edition_cover%29.jpg",
                 CategoryId = 1,
             },
            new Product
            {
                Id = 2,
                Title = "三體",
                Description = "《三體》是中國大陸作家劉慈欣於2006年5月至12月在《科幻世界》雜誌上連載的一部長篇科幻小說，出版後成為中國大陸最暢銷的科幻長篇小說之一。2008年，該書的單行本由重慶出版社出版。本書是「三體系列」（系列原名：地球往事三部曲）的第一部。該系列第二部《三體II：黑暗森林》於2008年5月出版；2010年11月，第三部《三體III：死神永生》出版發行。2011年，在台灣陸續出版。小說的英文版獲得美國科幻奇幻作家協會2014年度「星雲獎」提名，並榮獲2015年雨果獎最佳小說獎。",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/zh/0/0f/Threebody.jpg",
                CategoryId = 1,
            },
            new Product
            {
                Id = 3,
                Title = "安妮日記",
                Description = "《安妮的日記》（荷蘭語：Het Achterhuis）由安妮·弗蘭克所寫，此書發行版的內容摘錄自安妮在納粹佔領荷蘭的時期所寫的日記內容，並於戰後由她倖存的父親奧托·弗蘭克加以整理出版。",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/zh/4/44/First_edition.jpg",
                CategoryId = 1,
            },
            new Product
            {
                Id = 4,
                Title = "新上海灘",
                Description = "《新上海灘》（英語：Shanghai Grand）是一部1996年上映的香港電影，由潘文杰執導，徐克監製，劉德華、張國榮和寧靜主演。故事以1930年代民國上海為背景，講述中日民族矛盾、黑幫鬥爭、兄弟情義和兒女情長。香港收穫2083萬港幣，位列香港年度華語片第八位。",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/zh/b/b8/Shanghai_Grand_poster.jpg",
                CategoryId = 2,
            },
            new Product
            {
                Id = 5,
                Title = "刺激1995",
                Description = "《刺激1995》（英語：The Shawshank Redemption，中國大陸譯《肖申克的救贖》，香港譯《月黑高飛》）是1994年的美國劇情片，由法蘭克·戴瑞邦編劇並導演，根據史蒂芬·金1982年中篇小說《麗塔海華絲與鯊堡監獄的救贖》改編。影片講述銀行家安迪·杜佛倫（提姆·羅賓斯）因涉嫌謀殺夫人及其情夫被判無期徒刑，進入鯊堡州立監獄服刑後，他與能為獄友走私各種違禁商品的埃利斯·「瑞德」·雷丁（摩根·費里曼）成為朋友，同時利用金融才能為典獄長山繆·諾頓（鮑勃·岡頓）等監獄官員和看守洗錢逃稅的故事。其他演員包括威廉·湯瑪斯·桑德勒、克蘭西·布朗、吉爾·貝羅斯和詹姆士·惠特摩。",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/zh/a/af/Shawshank_Redemption_ver2.jpg",
                CategoryId = 2,
            },
            new Product
            {
                Id = 6,
                Title = "獅子王",
                Description = "《獅子王》（英語：The Lion King）是一部1994年由華特迪士尼長篇動畫製作、由華特迪士尼影片發行的動畫史詩歌舞電影，為第32部迪士尼經典動畫長片，也是迪士尼文藝復興的第五部作品，該故事從威廉·莎士比亞的《王子復仇記》取得靈感。",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/zh/3/3d/The_Lion_King_poster.jpg",
                CategoryId = 2,
            },
            new Product
            {
                Id = 7,
                Title = "仙境傳說",
                Description = "《仙境傳說》（韓語：라그나로크 온라인，英語：Ragnarok Online）是由韓國重力社製作發行的一款大型多人在線角色扮演遊戲（MMORPG）。《仙境傳說》最早自2002年8月31日起於韓國開始營運，其後在日本、北美、歐洲、中國大陸、台灣等多地營運。2010年8月30日，台灣營運方更名為「新仙境傳說」，2016年營運權轉移回Gravity原廠後更名回「仙境傳說」。遊戲基於南韓漫畫家李命進的作品《仙境傳說》的世界觀而創作，主要背景建立在北歐神話中，但也融合了亞洲、非洲、歐洲許多國家的神話元素等，由於人物插圖非常可愛而受到熱烈的歡迎。之後亦由台灣遊戲公司改編為仙境傳說網頁版。",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/f/fb/RO_icon_file.png",
                CategoryId = 3,
            },
            new Product
            {
                Id = 8,
                Title = "俠盜獵車手V",
                Description = "《俠盜獵車手V》（英語：Grand Theft Auto V）是由Rockstar North製作並由Rockstar Games發行的2013年開放世界動作冒險遊戲，這是繼2008年的《俠盜獵車手IV》之後，《俠盜獵車手》系列中的第七個主要作品，也是第十五部作品。單人遊戲故事設定在虛構的聖安地列斯，以南加州為基礎，講述了三位主角——退休的銀行搶劫犯麥可·迪聖塔、街頭黑幫富蘭克林·柯林頓以及毒販和槍手崔佛·菲利普。本作於2013年9月17日在PlayStation 3和Xbox 360主機上推出。由於本作是第七世代遊戲機末期推出的大型作品之一，因此在上市前便獲得了廣大的期待。",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/zh/0/0a/V_coverart_1024x768.jpg",
                CategoryId = 3,
            },
            new Product
            {
                Id = 9,
                Title = "魔物獵人 世界",
                Description = "《魔物獵人 世界》（日語：モンスターハンター：ワールド，英語：Monster Hunter: World，中國大陸譯作「怪物獵人 世界」）是一款由卡普空製作並在PlayStation 4、Xbox One和Windows平台上發行的動作角色扮演遊戲，是該系列繼2009年發售的《魔物獵人3》之後再次為家用主機平台製作的新作，也是系列首次在Windows上發售的本傳作品。本作也是首次支援繁簡中文的本傳遊戲，而騰訊的Wegame平台提前兩日（以北京時間計算）提供發售Windows版的簡體中文版本，但僅發售一周後即因被舉報而下架停售。",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/zh/6/62/Moster_hunter_world_jp_cover.jpg",
                CategoryId = 3,
            },
            new Product
            {
                Id = 10,
                Title = "超級瑪利歐派對",
                Description = "《超級瑪利歐派對》（日語：スーパー マリオ パーティ，英語：Super Mario Party）是由NDcube開發，任天堂發行的聚會遊戲。於2018年10月5日在任天堂Switch平台發售。本作是瑪利歐派對系列第十一部主遊戲，也是首次加入線上「聯網競獎」模式的作品\r\n本作無法透過任天堂Switch的掌機模式運行，必須取下Joy-Con控制器才可遊玩。",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/zh/6/6c/Super_Mario_Party.jpg",
                CategoryId = 3,
            },
            new Product
            {
                Id = 11,
                Title = "Xbox 360",
                Description = "Xbox 360 是美國的電腦軟體公司微軟所發行的第二部家用遊戲主機，為Xbox的後繼機種。Xbox 360 (簡稱 X360)為索尼電腦娛樂PlayStation 3和任天堂Wii的市場競爭者，同為第七世代遊戲主機。Xbox 360最早於2005年5月12日，在E3遊戲展前一星期首度曝光，2005年11月22日在北美地區上市，其後擴展至歐洲及日本地區，而香港及台灣也已經於2006年3月16日正式上市。",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/4/40/Xbox-360-Pro-wController.jpg/320px-Xbox-360-Pro-wController.jpg",
                CategoryId = 3,
            },
            new Product
            {
                Id = 12,
                Title = "任天堂64",
                Description = "任天堂64（日語：ニンテンドウ64，Nintendo 64，簡稱「N64」），是日本任天堂公司開發的家用電視遊戲機。於1996年6月23日在日本面世，而北美洲於1996年9月29日、歐洲和澳洲於1997年3月1日、法國於1997年公開發售。最大的創新之處在於首次在手把上加入了類比搖桿和震動功能。任天堂64上誕生了《薩爾達傳說 時之笛》和《超級瑪利歐64》等大作。任天堂64銷量為3293萬台。下一代產品為任天堂GameCube。另外任天堂當年為中國大陸市場，進行主機及遊戲中文化曾推出神遊機（iQue Player）作為特製版主機。",
                ImageUrl = "https://upload.wikimedia.org/wikipedia/commons/thumb/e/e9/Nintendo-64-wController-L.jpg/1024px-Nintendo-64-wController-L.jpg",
                CategoryId = 3,
            }
        );
        modelBuilder.Entity<ProductVariant>().HasData(
            new ProductVariant
            {
                ProductId = 1,
                ProductTypeId = 2,
                Price = 9.99m,
                OriginalPrice = 19.99m
            },
            new ProductVariant
            {
                ProductId = 1,
                ProductTypeId = 3,
                Price = 7.99m
            },
            new ProductVariant
            {
                ProductId = 1,
                ProductTypeId = 4,
                Price = 19.99m,
                OriginalPrice = 29.99m
            },
            new ProductVariant
            {
                ProductId = 2,
                ProductTypeId = 2,
                Price = 7.99m,
                OriginalPrice = 14.99m
            },
            new ProductVariant
            {
                ProductId = 3,
                ProductTypeId = 2,
                Price = 6.99m
            },
            new ProductVariant
            {
                ProductId = 4,
                ProductTypeId = 5,
                Price = 3.99m
            },
            new ProductVariant
            {
                ProductId = 4,
                ProductTypeId = 6,
                Price = 9.99m
            },
            new ProductVariant
            {
                ProductId = 4,
                ProductTypeId = 7,
                Price = 19.99m
            },
            new ProductVariant
            {
                ProductId = 5,
                ProductTypeId = 5,
                Price = 3.99m,
            },
            new ProductVariant
            {
                ProductId = 6,
                ProductTypeId = 5,
                Price = 2.99m
            },
            new ProductVariant
            {
                ProductId = 7,
                ProductTypeId = 8,
                Price = 19.99m,
                OriginalPrice = 29.99m
            },
            new ProductVariant
            {
                ProductId = 8,
                ProductTypeId = 9,
                Price = 59.99m
            },
            new ProductVariant
            {
                ProductId = 8,
                ProductTypeId = 10,
                Price = 59.99m,
                OriginalPrice = 49.99m
            },
            new ProductVariant
            {
                ProductId = 8,
                ProductTypeId = 8,
                Price = 9.99m,
                OriginalPrice = 24.99m,
            },
            new ProductVariant
            {
                ProductId = 9,
                ProductTypeId = 8,
                Price = 14.99m
            },
            new ProductVariant
            {
                ProductId = 10,
                ProductTypeId = 11,
                Price = 59.99m,
                OriginalPrice = 109m
            },
            new ProductVariant
            {
                ProductId = 11,
                ProductTypeId = 1,
                Price = 149.99m,
                OriginalPrice = 299m
            },
            new ProductVariant
            {
                ProductId = 12,
                ProductTypeId = 1,
                Price = 79.99m,
                OriginalPrice = 199m
            }
        );
    }

    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<ProductVariant> ProductVariants { get; set; }
}
