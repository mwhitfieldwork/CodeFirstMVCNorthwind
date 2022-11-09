using Microsoft.AspNetCore.Mvc;
using NWCodeFirstMVC.Application.Contracts;
using NWCodeFirstMVCSacffold.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NWCodeFirstMVC.Infrastructure.Services
{
    public class ProductsService : Controller, IProductService

    {
        private readonly northwindContext _dc;

        public ProductsService(northwindContext dc)
        {
            _dc = dc;
        }

        [HttpGet]
        public JsonResult GetAllProducts()
        {
            IQueryable<Product> products = _dc.Products;

            var results = products.Select(x =>
            new
            {
                productId = x.ProductId,
                ProductName = x.ProductName,
                SupplierId = x.SupplierId,
                CategoryId = x.CategoryId,
                QuantityPerUnit = x.QuantityPerUnit,
                UnitPrice = x.UnitPrice,
                UnitsInStock = x.UnitsInStock,
                UnitsOnOrder = x.UnitsOnOrder,
                ReorderLevel = x.ReorderLevel,
                Discontinued = x.Discontinued
            }).ToList();

            return Json(results);
        }
    }
}
