using Microsoft.AspNetCore.Mvc;
using NWCodeFirstMVCSacffold.Models;
using NWCodeFirstMVC.Application.Contracts;

namespace NWCodeFirstMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly northwindContext _dc;
        public ProductsController(IProductService productService, northwindContext dc)
        {
            _productService = productService;
            _dc = dc;
        }

        [HttpGet]
        public JsonResult GetAllProducts()
        {
            return _productService.GetAllProducts();
        }

        public ActionResult Index()
        {
            return View(_dc.Products.ToList());
        }


        public ActionResult View()
        {
            Category model = new Category();
            model.Initialize(_dc);

            return View(model);
        }
    }
}
