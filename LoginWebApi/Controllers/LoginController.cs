using LoginWebApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;

namespace LoginWebApi.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public void Put([FromBody] Product product)
        {
            var currentProduct = productList.Where(p => p.ProductID == product.ProductID).FirstOrDefault();
            if (currentProduct != null)
            {
                foreach (var item in productList)
                {
                    if (item.ProductID.Equals(currentProduct.ProductID))
                    {
                        item.ProductName = product.ProductName;
                        item.Price = product.Price;
                        item.Count = product.Count;
                        item.Description = product.Description;
                    }
                }
            }
        }
        
    }
}