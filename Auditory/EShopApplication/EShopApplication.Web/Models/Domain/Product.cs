using System.ComponentModel.DataAnnotations;

namespace EShopApplication.Web.Models;

public class Product
{
    public Guid Id { get; set; }
    [Required]
    public string ProductName { get; set; }
    [Required]
    public string ProductImage { get; set; }
    [Required]
    public string ProductDescription { get; set; }
    [Required]
    public double ProductPrice { get; set; }
    public int Rating { get; set; }
    public ICollection<ProductInShoppingCart> ProductInShoppingCarts { get; set; } = new List<ProductInShoppingCart>();
}