using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EInvoice.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index()
        {
            var products = DatabaseFunction.Product.getProducts();
            return View(products);
        }
        public ActionResult New()
        {
            return View();
        }
        public ActionResult StockAdd(int id)
        {
            var product = DatabaseFunction.Product.getProduct(id);
            return View(product);
        }
        public ActionResult StockTake(int id)
        {
            var product = DatabaseFunction.Product.getProduct(id);
            return View(product);
        }
        
        public ActionResult Detail(int id)
        {
            var product = DatabaseFunction.Product.getProduct(id);
            return View(product);
        }
        [HttpPost]
        public string Add(string productName)
        {
            string result;
            try
            {
                if (DatabaseFunction.Product.Add(productName))
                {
                    result = "Succesfully";
                }
                else
                {
                    result = "Failed";
                }
            }
            catch (Exception)
            {
                result = "Failed";
                throw;
            }
            return result;
        }
        [HttpPost]
        public string Update(int id,string productName)
        {
            string result;
            try
            {
                if (DatabaseFunction.Product.Update(id,productName))
                {
                    result = "Succesfully";
                }
                else
                {
                    result = "Failed";
                }
            }
            catch (Exception)
            {
                result = "Failed";
                throw;
            }
            return result;
        }
        [HttpPost]
        public string StockUpdate(int id,float stock)
        {
            try
            {
                if (DatabaseFunction.Product.StockUpdate(id, stock))
                {
                    return "Success";
                }
                else
                {
                    return "Failed";
                }
            }
            catch (Exception)
            {
                return "Failed";
                throw;
            }
        }
    }
}