using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
namespace EInvoice.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var model = new DatabaseFunction.Model.CasingModel()
            {
                casing = DatabaseFunction.CompanyCase.getCash(),
                casings = DatabaseFunction.CompanyCase.GetCasings()
            };
            return View(model);
        }
        public ActionResult Logout()
        {
            Session["UserID"] = null;
            Session["Username"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
       
    }
}