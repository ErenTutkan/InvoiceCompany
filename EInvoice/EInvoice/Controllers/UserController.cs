using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EInvoice.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            var users = DatabaseFunction.User.getUserList();
            return View(users);
        }
        public ActionResult Register()
        {
            return View();
        }
        public ActionResult Detail(int userid)
        {
            var user = DatabaseFunction.User.Detail(userid);
            return View(user);
        }
        [HttpPost]
        public string Add(string username,string password)
        {
            string result;
            try
            {
                if (DatabaseFunction.User.Add(username, password))
                {
                    result = "Succesfull";
                }
                else{
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
        public string Update(int userid, string password)
        {
            string result;
            try
            {
                if (DatabaseFunction.User.Update(userid, password))
                {
                    result = "Succesfull";
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
        public string Delete(int userid)
        {
            string result;
            try
            {
                if (DatabaseFunction.User.Delete(userid))
                {
                    result = "Succesfull";
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
    }
}