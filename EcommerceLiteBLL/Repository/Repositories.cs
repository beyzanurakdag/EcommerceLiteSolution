﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcommerceLiteEntity.Models;
using EcommerceLiteEntity.ViewModels;

namespace EcommerceLiteBLL.Repository
{
    public class Repositories
    {
    }
    public class CategoryRepo : RepositoryBase<Category, int> 
    {
        public List<ProductCountModel> GetBaseCategoriesProductCount()
        {
            List<ProductCountModel> list = new List<ProductCountModel>();
            dbContext = new EcommerceLiteDAL.MyContext();
            var categoryList = from c in dbContext.Categories
                               where c.BaseCategoryId == null
                               select c;
            foreach (var item in categoryList)
            {
                //sub categories
                var subCategoryList = from c in dbContext.Categories
                                      where c.BaseCategoryId == item.Id
                                      select c;
                int productCount = 0;
                foreach (var subitem in subCategoryList)
                {
                    var productList = from p in dbContext.Products
                                      where p.CategoryId == subitem.Id
                                      select p;
                    productCount += productList.ToList().Count;
                }
                list.Add(new ProductCountModel()
                { 
                    BaseCategory=item,
                    ProductCount=productCount
                });
            }
            return list;
        }
    }
    public class ProductRepo : RepositoryBase<Product, int> { }
    public class OrderRepo : RepositoryBase<Order, int> { }
    public class OrderDetailRepo : RepositoryBase<OrderDetail, int> { }
    public class CustomerRepo : RepositoryBase<Customer, string> { }
    public class AdminRepo : RepositoryBase<Admin, string> { }
    public class PassiveUserRepo : RepositoryBase<PassiveUser, string> { }
    public class ProductPictureRepo:RepositoryBase<ProductPicture,int> { }

}
