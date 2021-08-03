using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EInvoice.Controllers
{
    [Authorize]
    public class CaseTransactionController : Controller
    {
        // GET: CaseTransaction
        public ActionResult Index()
        {
            
            List<CasingTransaction> transaction = DatabaseFunction.CaseTransaction.GetTransactions();
           
            if (transaction != null)
            {
                return View(transaction);
            }
            else { return View(); }
         
        }
        public ActionResult Entry()
        {
            return View();
        }
        public ActionResult Out()
        {
            return View();
        }
        [HttpPost]
        public string Add(string name,string desc,float paid,int transactiontype)
        {
            string result;
            try
            {
                if (transactiontype == 2)
                {
                    paid = -paid;
                }
                else
                {
                    
                }
                var userid = int.Parse(Session["UserID"].ToString());
                if (DatabaseFunction.CaseTransaction.Add(name, transactiontype, desc, userid, paid))
                {
                    result = "Successfull";
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