using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcommerceLiteBLL.Repository;
using EcommerceLiteEntity.Models;
using EcommerceLiteUI.Models;
using Mapster;

namespace EcommerceLiteUI.Controllers
{
    public class ProductController : Controller
    {
        //Global Alan
        ProductRepo myProductRepo = new ProductRepo();
        CategoryRepo myCategoryRepo = new CategoryRepo();
        public ActionResult ProductList()
        {
            var allProductList = myProductRepo.GetAll();
            return View(allProductList);
        }
        [HttpGet]
        public ActionResult Create()
        {
            List<SelectListItem> allCategories = new List<SelectListItem>();
            myCategoryRepo.GetAll().ToList().ForEach(x => allCategories.Add(new SelectListItem()
            {
                Text = x.CategoryName,
                Value = x.Id.ToString()
            }));
            ViewBag.CategoryList = allCategories;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ProductViewModel model)
        {
            try
            {
                List<SelectListItem> allCategories = new List<SelectListItem>();
                myCategoryRepo.GetAll().ToList().ForEach(x => allCategories.Add(new SelectListItem()
                {
                    Text = x.CategoryName,
                    Value = x.Id.ToString()
                }));
                ViewBag.CategoryList = allCategories;
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("", "Veri girişleri düzgün olmalıdır!");
                    return View(model);
                }
                Product newProduct = model.Adapt<Product>();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Beklenmedik hata oluştu!");
                return View(model);
            }
        }
    }
}