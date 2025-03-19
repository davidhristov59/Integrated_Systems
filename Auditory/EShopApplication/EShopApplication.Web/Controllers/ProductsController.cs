using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EShopApplication.Web.Data;
using EShopApplication.Web.Models;
using EShopApplication.Web.Models.DTO;
using Microsoft.AspNetCore.Authorization;

namespace EShopApplication.Web.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }
        
        // GET: Products/AddProductToCart
        public async Task<IActionResult> AddProductToCart(Guid? Id)
        {
            var product = await _context.Products.Where(p => p.Id.Equals(Id)).FirstOrDefaultAsync();
            
            AddProductToShoppingCartDTO model = new AddProductToShoppingCartDTO
            {
                selectedProduct = product,
                ProductID = product.Id,
                Quantity = 1
            };
            
            return View(model);
        }
        
        // POST: Products/AddProductToCart/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> AddProductToCart([Bind("ProductId", "Quantity")] AddProductToShoppingCartDTO model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); //id-to na najaveniot korisnik
            
            var shoppingCartUser = await _context.ShoppingCarts.Where(s => s.OwnerId.Equals(userId)).FirstOrDefaultAsync();

            if (shoppingCartUser != null && model.ProductID != null)
            {
                var product = await _context.Products.Where(p => p.Id.Equals(model.ProductID)).FirstOrDefaultAsync();

                if (product != null)
                {
                    ProductInShoppingCart productInShoppingCart = new ProductInShoppingCart()
                    {
                        Product = product,
                        ProductID = model.ProductID,
                        ShoppingCart = shoppingCartUser,
                        ShoppingCartID = shoppingCartUser.Id,
                        Quantity = model.Quantity
                    };
                    _context.ProductInShoppingCarts.Add(productInShoppingCart);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction("Index", "Products");
            }
            return View(model);
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,ProductImage,ProductDescription,ProductPrice,Rating")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = Guid.NewGuid();
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
    
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,ProductName,ProductImage,ProductDescription,ProductPrice,Rating")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(Guid id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
