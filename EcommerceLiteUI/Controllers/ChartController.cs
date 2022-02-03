using EcommerceLiteBLL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EcommerceLiteUI.Controllers
{
    public class ChartController : Controller
    {
        // Global Alan
        CategoryRepo myCategoryRepo = new CategoryRepo();
        public ActionResult VisualizePieChartResult()
        {
            //PieChartta göstermek istediğimiz datayı alacağız.Bu datayı dashboard'daki ajax işlemine gönderebilmek için return Json ile işlem yapacağız.
            var data = myCategoryRepo.GetBaseCategoriesProductCount();
            return Json(data,JsonRequestBehavior.AllowGet);
        }
    }
}