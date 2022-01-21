using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcommerceLiteBLL.Repository;

namespace EcommerceLiteUI.Controllers
{
    public class CategoryController : Controller
    {
        //Global Alan
        CategoryRepo myCategoryRepo = new CategoryRepo();
        public ActionResult CategoryList()
        {
            var allCategories = myCategoryRepo.Queryable().Where(x=> x.BaseCategoryId==null).ToList();
            ViewBag.CategoryCount = allCategories.Count;
            return View(allCategories);
        }
    }
}