using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EInvoice.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        // GET: Company
        public ActionResult Index(int? comtype)
        {
            if (comtype != null)
            {
                
                DatabaseFunction.Model.CompanyAndType model = new DatabaseFunction.Model.CompanyAndType()
                {
                    companies = DatabaseFunction.Company.GetCompanies((int)comtype),
                    companytype = (int)comtype
                    
                };
                
                return View(model);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
          
        }
        public ActionResult New()
        {
            return View();
        }
        public ActionResult NewUnofficial()
        {
            return View();
        }
        public ActionResult Detail(int? id)
        {
            Companies company = new Companies();
            
            if (id!=null && DatabaseFunction.Company.getCompany((int)id) != null)
            {
                company = DatabaseFunction.Company.getCompany((int)id);
                return View(company);
            }
           
            else
            {
                return RedirectToAction("Index","Company");
            }
           
        }

        [HttpPost]
        public string Add(string companyname,string companydesc,string telno,string email,string companyaddress,int companytype)
        {
            string result;
            try
            {
                if (DatabaseFunction.Company.Add(companyname, companydesc, telno, email, companyaddress,companytype))
                {
                    result = "Success";
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
        public string Update(int id,string companyname, string companydesc, string telno, string email, string companyaddress)
        {
            string result;
            try
            {
                if (DatabaseFunction.Company.Update(id,companyname,companydesc,telno,email,companyaddress))
                    result = "Success";

                else
                    result = "Failed";
            }
            catch (Exception)
            {
                result = "Failed";
                throw;
            }
            return result;
        }
        public string Delete(int id)
        {
            string result;
            try
            {
                if (DatabaseFunction.Company.Delete(id))
                    result = "Success";
                else
                    result = "Failed";

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