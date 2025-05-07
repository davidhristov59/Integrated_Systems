using System.ComponentModel.DataAnnotations;

namespace EShopApplication.Domain.DomainModels
{
    public class Product : BaseEntity
    {
        [Required]
        [Display(Name = "Product Name")]
        public string? ProductName { get; set; }
        
        [Required] 
        [Display(Name = "Product Image")]
        public string? ProductImage { get; set; }
        
        [Required] 
        [Display(Name = "Product Description")]
        public string? ProductDescription { get; set; }
        
        [Required] 
        [Display(Name = "Product Price")]
        public double ProductPrice { get; set; }
        public int Rating { get; set; }
        
        public virtual ICollection<ProductInShoppingCart>? ProductsInShoppingCarts { get; set; }
        public virtual ICollection<ProductInOrder>? ProductInOrders { get; set; }
        
        // public ICollection<ProductInShoppingCart> ProductInShoppingCarts { get; set; } =
        //     new List<ProductInShoppingCart>();
    }
}