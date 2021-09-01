using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EInvoice.Controllers
{
    [Authorize]
    public class InvoiceController : Controller
    {
        // GET: Invoice
        public ActionResult Index()
        {
            var invoices = DatabaseFunction.Invoice.GetInvoices();
            return View(invoices);
        }
        public ActionResult InvoiceList(int companyid)
        {
            var list = DatabaseFunction.Invoice.GetCompanyInvoices(companyid);
            return View(list);
        }
        public ActionResult Detail(int id) {
            var model = new DatabaseFunction.Model.InvoiceProductsModel() {
                invoice = DatabaseFunction.Invoice.getDetail(id),
                products = DatabaseFunction.Product.getProducts()
            };
         
            return View(model);
        }
        [HttpPost]
        public string Update(int id,int productid,float weight,float unitprice,float amount,string desc,float paid)
        {
            string result;
            try
            {
                var invoice = DatabaseFunction.Invoice.getDetail(id);
                var totalamount = invoice.Amount - invoice.Paid;
                var product = DatabaseFunction.Product.getProduct(productid);
                var currentproductid = invoice.ProductId;
                var currentproductstock = invoice.Weight;
                if (DatabaseFunction.Invoice.Update(id, productid, weight, unitprice, amount, desc, paid))
                {
                    result = "Succesfully";
                    
                    var cash = amount - paid;
                    var companyid = invoice.CompanyID;

                    if (invoice.InvoiceType == 1)
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
                        if (currentproductid != productid)
                        {
                            DatabaseFunction.Product.StockUpdate((int)currentproductid, -(float)currentproductstock);
                            DatabaseFunction.Product.StockUpdate(productid, weight);
                        }
                        else if(currentproductid==productid)
                        {
                            if (currentproductstock > weight)
                            {
                                float stockcount = (float)(currentproductstock - weight);
                                DatabaseFunction.Product.StockUpdate(productid, -(float)stockcount);
                            }
                            else if (weight > currentproductstock)
                            {
                                float stock = (float)(weight - currentproductstock);
                                DatabaseFunction.Product.StockUpdate(productid, stock);
                            }
                        }
                       
                    }
                    else
                    {
                        if (totalamount < cash)
                        {
                            float trans = (float)totalamount - cash;
                            DatabaseFunction.CompanyTransaction.CreateTransaction((int)companyid, trans);
                        }
                        else if (totalamount > cash)
                        {
                            float trans = (float)totalamount - cash;
                            DatabaseFunction.CompanyTransaction.CreateTransaction((int)companyid, trans);
                        }
                        if (currentproductid != productid)
                        {
                            DatabaseFunction.Product.StockUpdate((int)currentproductid, (float)currentproductstock);
                            DatabaseFunction.Product.StockUpdate(productid, -weight);
                        }
                        else if (currentproductid == productid)
                        {
                            if (currentproductstock > weight)
                            {
                                float stockcount = (float)(currentproductstock - weight);
                                DatabaseFunction.Product.StockUpdate(productid, (float)stockcount);
                            }
                            else if (weight > currentproductstock)
                            {
                                float stock = (float)(weight - currentproductstock);
                                DatabaseFunction.Product.StockUpdate(productid, -stock);
                            }
                        }
                    }
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
        public ActionResult Entry(int companyid)
        {
            DatabaseFunction.Model.CompanyProductsModel model = new DatabaseFunction.Model.CompanyProductsModel()
            {
                company = DatabaseFunction.Company.getCompany(companyid),
                products = DatabaseFunction.Product.getProducts()
            };
            return View(model);
        }
        public ActionResult Out(int companyid)
        {
            DatabaseFunction.Model.CompanyProductsModel model = new DatabaseFunction.Model.CompanyProductsModel()
            {
                company = DatabaseFunction.Company.getCompany(companyid),
                products = DatabaseFunction.Product.getProducts()
            };
            return View(model);
        }
        public string Add(int productid, float weight, float unitprice, float amount,string desc, float paid,int companyid,int invoicetype)
        {
            string result;
            try
            {
                var userid = int.Parse(Session["UserID"].ToString());
                
                if (DatabaseFunction.Invoice.Add(companyid, productid, weight, unitprice, amount, desc, paid,invoicetype,userid))
                {
                    if (invoicetype == 1)
                    {
                        var cash = amount - paid;
                        DatabaseFunction.CompanyTransaction.CreateTransaction(companyid, cash);
                        DatabaseFunction.Product.StockUpdate(productid, weight);
                        //DatabaseFunction.CompanyCase.CaseUpdate(paid);
                    }
                    else
                    {
                        var cash = amount - paid;
                        DatabaseFunction.CompanyTransaction.CreateTransaction(companyid, -paid);
                        DatabaseFunction.Product.StockUpdate(productid, -weight);
                        //   DatabaseFunction.CompanyCase.CaseUpdate(-paid);
                    }
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
        public string Delete(int id)
        {
            string res;
            try
            {
                var invoice = DatabaseFunction.Invoice.getDetail(id);
                if (DatabaseFunction.Invoice.Delete(id))
                {
                  
                    var totalamount = invoice.Amount - invoice.Paid;
                    var companyid = (int)invoice.CompanyID;
                    DatabaseFunction.CompanyTransaction.CreateTransaction((int)companyid, (float)totalamount);
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