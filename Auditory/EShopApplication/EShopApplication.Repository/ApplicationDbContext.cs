using EShopApplication.Domain.IdentityModels;
using EShopApplication.Domain.DomainModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
namespace EShopApplication.Repository;

public class ApplicationDbContext : IdentityDbContext<EShopApplicationUser> //dodadov generic 
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public virtual DbSet<ProductInShoppingCart> ProductInShoppingCarts { get; set; }

    //Fluent API 
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); // Ensure Identity entities are configured
        
        builder.Entity<Product>()
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        builder.Entity<ShoppingCart>()
            .Property(x => x.Id)
            .ValueGeneratedOnAdd();
        
        //composite key 
        builder.Entity<ProductInShoppingCart>()
            .HasKey(x => new { x.ProductID, x.ShoppingCartID });
        
        //relations
        builder.Entity<ProductInShoppingCart>()
            .HasOne(x => x.Product)
            .WithMany(x => x.ProductInShoppingCarts)
            .HasForeignKey(x => x.ShoppingCartID);
        
        builder.Entity<ProductInShoppingCart>()
            .HasOne(x => x.ShoppingCart)
            .WithMany(x => x.ProductsInShoppingCarts)
            .HasForeignKey(x => x.ProductID);
        
        //1-1 relacija pomegju User i ShoppingCart
        builder.Entity<ShoppingCart>()
            .HasOne(x => x.user)
            .WithOne(x => x.ShoppingCart)
            .HasForeignKey<ShoppingCart>(x => x.OwnerId);
    }
}