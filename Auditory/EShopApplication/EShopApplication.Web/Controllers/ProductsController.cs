using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using EShopApplication.Domain.DomainModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using EShopApplication.Domain.DTO;
using EShopApplication.Repository;
using EShopApplication.Services.Interface;

namespace EShopApplication.Web.Controllers
{
    public class ProductsController : Controller
    {
        // private readonly ApplicationDbContext _context;
        private readonly IProductService productService;

        public ProductsController(IProductService productService)
        {
            this.productService = productService;
        }

        // GET: Products/AddProductToCart
        public IActionResult AddProductToCart(Guid? Id)
        {
            var model = productService.GetShoppingCartInfo(Id);
            
            return View(model);
        }
        
        // POST: Products/AddProductToCart/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddProductToCart(AddProductToShoppingCartDTO model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get logged-in user ID

            if (string.IsNullOrEmpty(userId))
            {
                return RedirectToAction("Login", "Account"); 
            }

            var result = productService.AddToShoppingCart(model, userId);

            if (result)
            {
                return RedirectToAction("Index", "Products"); 
            }

            return View(model);
        }
        
        // GET: Products
        public IActionResult Index()
        {
            var allProducts = productService.GetAllProducts();
            
            return View(allProducts);
        }

        // GET: Products/Details/5
        public IActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            // var product =  _context.Products.FirstOrDefaultAsync(m => m.Id == id);
            var product = productService.GetDetailsForProducts(id);
            
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
        public IActionResult Create([Bind("Id,ProductName,ProductImage,ProductDescription,ProductPrice,Rating")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Id = Guid.NewGuid();
                productService.CreateNewProduct(product);
                return RedirectToAction(nameof(Index));
            }
    
            return View(product);
        }

        // GET: Products/Edit/5
        public IActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = productService.GetDetailsForProducts(id);
            
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
        public IActionResult Edit(Guid id, [Bind("Id,ProductName,ProductImage,ProductDescription,ProductPrice,Rating")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    productService.UpdateExistingProduct(product);
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
        public IActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = productService.GetDetailsForProducts(id);
            
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(Guid id)
        {
            var product = productService.GetDetailsForProducts(id);
            if (product != null)
            {
                productService.DeleteProduct(product);
            }

            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(Guid id)
        {
            return productService.GetDetailsForProducts(id) != null;
        }
    }
}
