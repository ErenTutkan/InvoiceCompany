using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace EInvoice.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public string UserLogin(string username,string password)
        {
            string res;
            try
            {
                if (DatabaseFunction.User.Login(username, password) )
                {
                    res = "Succesfull";
                    FormsAuthentication.SetAuthCookie(username, false);
                    var user = DatabaseFunction.User.getUserId(username);
                    Session["Username"] = username;
                    Session["UserID"] = user.ID;
                }
                else
                    res = "Failed";
            }
            catch (Exception)
            {
                res = "Failed";
                throw;
            }
            return res;
        }
    }
}