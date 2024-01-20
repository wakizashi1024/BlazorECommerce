﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorECommerce.Shared;

public class Product
{
    public int Id { get; set; }
    [Required]
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    //[Column(TypeName = "decimal(18, 2)")]
    //public decimal Price { get; set; }
    public bool Featured { get; set; } = false;
    public bool Visible { get; set; } = true;
    public bool Deleted { get; set; } = false;
    [NotMapped]
    public bool Editing { get; set; } = false;
    [NotMapped]
    public bool IsNew { get; set; } = false;

    public Category? Category { get; set; }
    public int CategoryId { get; set; }
    public List<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
}
