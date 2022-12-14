using Microsoft.AspNetCore.Mvc;
using NWCodeFirstMVC.App.Contracts;
using NWCodeFirstMVCSacffold.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NWCodeFirstMVC.Infrastructure.Services
{    
    // Implements the inteface. This is the depndency inversion
    // Which states that higher level components dont depend on lower level ones
    // So the interface implementation doenst depend on the controller.
    // Instead there is an intermediary. The service implements the interface and is used
    // inside the contorller and keeps higher level functions

    public class ProductService:IProductService 
    {

        private readonly northwindContext _dc;

        public ProductService(northwindContext dc)
        {
            _dc = dc;
        }



        public List<Product> GetAllProduct()
        {
            IQueryable<Product> products = _dc.Products;

            var results = products.Select(x =>
            new Product
            {
                ProductId = x.ProductId,
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

            return results;
        }


        public List<Product> GetLuxuryUSProduct()
        {
            List<Product> luxproduct = _dc.Products.Join(_dc.Suppliers,
                              x => x.SupplierId,
                              y => y.SupplierId,
                              (x, y) => new { Product = x, Supplier = y })
                           .Where(z => z.Product.UnitPrice > 10 && z.Supplier.Country == "USA").Select(x => x.Product).ToList();
                            // What about defining a new object based on the join?

            return luxproduct;
        }
        public Product AddProduct(Product product)
        {
            _dc.Products.Add(product);
            _dc.SaveChanges();

            return product;

        }
    }
}
