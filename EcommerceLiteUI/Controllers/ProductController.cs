﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EcommerceLiteBLL.Repository;
using EcommerceLiteBLL.Settings;
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
        ProductPictureRepo myProductPictureRepo = new ProductPictureRepo();
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
                //Mapleme yapıldı.
                Product newProduct = model.Adapt<Product>();
                int insertResult = myProductRepo.Insert(newProduct);
                if (insertResult>0)
                {
                    //Ürünün fotoğrafları da eklensin.
                    if (model.Files.Any())
                    {
                        ProductPicture productPicture = new ProductPicture();
                        productPicture.ProductId = newProduct.Id;
                        productPicture.RegisterDate = DateTime.Now;
                        int counter = 1;
                        foreach (var item in model.Files)
                        {
                            if (item != null && item.ContentType.Contains("image") && item.ContentLength > 0)
                            {

                                string filename = SiteSettings.UrlFormatConverter(model.ProductName).ToLower().Replace("-", "");
                                string extName = Path
                                    .GetExtension(item.FileName);

                                string guid = Guid.NewGuid()
                                    .ToString().Replace("-", "");
                                var directoryPath = Server.MapPath($"~/ProductPictures/{filename}/{model.ProductCode}");
                                var filePath = Server.MapPath($"~/ProductPictures/{filename}/{model.ProductCode}/") + filename + counter + "-" + guid + extName;
                                if (!Directory.Exists(directoryPath))
                                {
                                    Directory.CreateDirectory(directoryPath);
                                }
                                item.SaveAs(filePath);
                                if (counter==1)
                                {
                                    productPicture.ProductPicture1 = $"/ProductPictures/{filename}/{model.ProductCode}/" +filename + counter + "-" + guid + extName;
                                }
                                if (counter == 2)
                                {
                                    productPicture.ProductPicture2 = $"/ProductPictures/{filename}/{model.ProductCode}/" + filename + counter + "-" + guid + extName;
                                }
                                if (counter == 3)
                                {
                                    productPicture.ProductPicture3 = $"/ProductPictures/{filename}/{model.ProductCode}/" + filename + counter + "-" + guid + extName;
                                }
                                if (counter == 4)
                                {
                                    productPicture.ProductPicture4 = $"/ProductPictures/{filename}/{model.ProductCode}/" + filename + counter + "-" + guid + extName;
                                }
                                if (counter == 5)
                                {
                                    productPicture.ProductPicture5 = $"/ProductPictures/{filename}/{model.ProductCode}/" + filename + counter + "-" + guid + extName;
                                }
                            }
                            counter++;
                        }
                        int pictureInsertResult = myProductPictureRepo.Insert(productPicture);
                        if (pictureInsertResult>0)
                        {
                            return RedirectToAction("ProductList", "Product");
                        }
                        else
                        {
                            ModelState.AddModelError("", "Ürün eklendi ama ürüne ait fotoğraflar eklenirken bir hata oluştu. Fotoğraf eklemek için tekrar deneyiniz!");
                            return View(model);
                        }
                    }
                    else
                    {
                        return RedirectToAction("ProductList", "Product");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Ürün ekleme işleminde bir hata oluştu tekrar deneyiniz!");
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Beklenmedik hata oluştu!");
                return View(model);
            }
        }
    }
}