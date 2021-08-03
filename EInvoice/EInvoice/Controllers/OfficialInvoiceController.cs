using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EInvoice.Controllers
{
    [Authorize]
    public class OfficialInvoiceController : Controller
    {
        // GET: OfficialInvoice
        public ActionResult Index()
        {
           var officialinvoices= DatabaseFunction.OfficialInvoice.GetOfficialInvoices();
            return View(officialinvoices);
        }
        public ActionResult InvoiceList(int companyid)
        {
            var list = DatabaseFunction.OfficialInvoice.getCompanyOfficialInvoices(companyid);
            return View(list);
        }
        public ActionResult EntryAdd(int companyid)
        {
            if (DatabaseFunction.Company.getCompany(companyid) == null)
            {
                return RedirectToAction("Index","OfficialInvoice");
            }
           var company= DatabaseFunction.Company.getCompany(companyid);
            return View(company);
        }
        public ActionResult PrintOutAdd(int companyid)
        {
            if (DatabaseFunction.Company.getCompany(companyid) == null)
            {
                return RedirectToAction("Index", "OfficialInvoice");
            }
            var company = DatabaseFunction.Company.getCompany(companyid);
            return View(company);
        }
        public ActionResult Detail(int id)
        {
            return View(DatabaseFunction.OfficialInvoice.getDetail(id));
        }
        [HttpPost]
        public string Add(string waybillno,string invoiceno,float amount,string desc,float paid,int entrydata,int companyid)
        {
            string result;
            try
            {
                var userid = int.Parse(Session["UserID"].ToString());
                if (DatabaseFunction.OfficialInvoice.Add(companyid, waybillno, invoiceno, amount, desc, paid,userid, entrydata))
                {
                    if (entrydata == 1)
                    {
                        var cash = amount - paid;
                        DatabaseFunction.CompanyTransaction.CreateTransaction(companyid, cash);
                        //DatabaseFunction.CompanyCase.CaseUpdate(paid);

                    }
                    else
                    {
                        var cash = amount - paid;
                        DatabaseFunction.CompanyTransaction.CreateTransaction(companyid,-paid);
                     //   DatabaseFunction.CompanyCase.CaseUpdate(-paid);
                    }
                    result = "Succesfully";
                }
                else { result = "Failed"; }
                
               
            }
            catch (Exception)
            {
                result = "Failed";
                throw;
            }
            return result;
        }
        [HttpPost]
        public string Update(int id,string waybillno, string invoiceno, float amount, string desc, float paid)
        {
            string result;
            try
            {
                var officialinvoice = DatabaseFunction.OfficialInvoice.getDetail(id);
                var totalamount = officialinvoice.Amount - officialinvoice.Paid;
                if (DatabaseFunction.OfficialInvoice.Update(id,waybillno,invoiceno,amount,desc,paid))
                {
                  
                    var cash = amount - paid;
                    var companyid = officialinvoice.CompanyID;
                    if (officialinvoice.entry_printout == 1)
                    {

                        if (totalamount > cash)
                        {

                            float trans = (float)totalamount - cash;
                            DatabaseFunction.CompanyTransaction.CreateTransaction((int)companyid, -trans);
                        }
                        else if (totalamount < cash)
                        {
                            float trans = cash - (float)totalamount;
                            DatabaseFunction.CompanyTransaction.CreateTransaction((int)companyid, trans);
                        }
                    }
                    else
                    {
                        if (totalamount < cash)
                        {
                            float trans =(float)totalamount-cash;
                            DatabaseFunction.CompanyTransaction.CreateTransaction((int)companyid, trans);
                        }
                        else if (totalamount > cash)
                        {
                            float trans = (float)totalamount-cash;
                            DatabaseFunction.CompanyTransaction.CreateTransaction((int)companyid, trans);
                        }
                    
                    }
                    
                    result = "Succesfully";
                }
                else { result = "Failed"; }


            }
            catch (Exception)
            {
                result = "Failed";
                throw;
            }
            return result;
        }
        [HttpPost]
        public string Delete(int id)
        {
            string res;
            try
            {
                var invoice = DatabaseFunction.OfficialInvoice.getDetail(id);
                if (DatabaseFunction.OfficialInvoice.Delete(id))
                {
                    
                    var totalamount = invoice.Amount - invoice.Paid;
                    var companyid = invoice.CompanyID;
                    DatabaseFunction.CompanyTransaction.CreateTransaction((int)companyid,(float)-totalamount);
                    res = "Succesfull";
                }
                else
                {
                    res = "Failed";
                }
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