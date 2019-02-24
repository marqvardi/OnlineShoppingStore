using OnlineShoppingStore.Domain.Abstract;
using OnlineShoppingStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private IProductRepository repository;

        public AdminController(IProductRepository repo)
        {
            repository = repo;
        }

        // GET: Admin
        public ActionResult Index()
        {
            return View(repository.Products);
        }

        public ViewResult Edit(int ProductId)
        {
            Product product = new Product();
            product = repository.Products.FirstOrDefault(p => p.ProductID == ProductId);

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["Message"] = string.Format("{0} has been saved.",
                    product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                //Something wrong with values
                return View(product);
            }
        }

        public ViewResult Create()
        {
            return View(new Product());
        }

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                repository.SaveProduct(product);
                TempData["Message"] = string.Format("{0} has been saved.",
                    product.Name);
                return RedirectToAction("Index");
            }
            else
            {
                //Something wrong with values
                return View(product);
            }
        }

        [HttpPost]
        public ActionResult Delete(int productId)
        {
            Product deleteProduct = repository.DeleteProduct(productId);
            if (deleteProduct != null)
            {
                TempData["Message"] = string.Format("{0} was deleted.", deleteProduct.Name);
            }
            return RedirectToAction("Index");
        }
    }    
}