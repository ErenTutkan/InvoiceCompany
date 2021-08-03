using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EInvoice
{
    public class DatabaseFunction
    {
        public class Company
        {
            
            public static List<Companies> GetCompanies(int companytype)
            {
                try
                {
                   using(InvoiceEntities entities =new InvoiceEntities())
                    {
                        var companies = entities.Companies.Where(x=>x.CompanyType==companytype &&x.Status==1).ToList();
                        return companies;
                    }
                }
                catch (Exception)
                {
                    return null;
                    throw;
                }
            }
            public static Companies getCompany(int id)
            {
                try
                {
                    using(InvoiceEntities entities =new InvoiceEntities())
                    {
                        var company = entities.Companies.Where(x => x.ID == id && x.Status==1).FirstOrDefault();
                        return company;
                    }
                }
                catch (Exception)
                {
                    return null;
                    throw;
                }
               
            }
            public static bool Add(string _companyName,string _companyDesc,string _companyTelNo,string _companyEmail,string _companyAddrss,int companytype)
            {
                bool result;
                try
                {
                    using(InvoiceEntities entities=new InvoiceEntities())
                    {
                        var company = new Companies();
                        company.CompanyName = _companyName;
                        company.CompanyDescription= _companyDesc;
                        company.CompanyEmail = _companyEmail;
                        company.CompanyTelNo = _companyTelNo;
                        company.CompanyAddress = _companyAddrss;
                        company.CompanyType = companytype;
                        company.Status = 1;
                        company.CreateDate = DateTime.Now;
                        entities.Companies.Add(company);
                        entities.SaveChanges();
                        result = true;
                    }
                }
                catch (Exception)
                {
                    result = false;
                    throw;
                }
                return result;
            }
            public static bool Update(int id,string _companyName, string _companyDesc, string _companyTelNo, string _companyEmail, string _companyAddrss)
            {
                bool result;
                try
                {
                    using (InvoiceEntities entities = new InvoiceEntities())
                    {
                        var company = entities.Companies.Where(x => x.ID == id && x.Status == 1).FirstOrDefault();
                        
                        company.CompanyName = _companyName;
                        company.CompanyDescription = _companyDesc;
                        company.CompanyEmail = _companyEmail;
                        company.CompanyTelNo = _companyTelNo;
                        company.CompanyAddress = _companyAddrss;
                        company.UpdateDate = DateTime.Now;
                        entities.SaveChanges();
                        result = true;
                    }
                }
                catch (Exception)
                {
                    result = false;
                    throw;
                }
                return result;
            }
            public static bool Delete(int id)
            {
                bool result;
                try
                {
                    using (InvoiceEntities entities = new InvoiceEntities())
                    {
                        var company = entities.Companies.Where(x => x.ID == id && x.Status == 1).FirstOrDefault();

                        company.Status = 0;
                        company.DeleteDate = DateTime.Now;
                        entities.SaveChanges();
                        result = true;
                    }
                }
                catch (Exception)
                {
                    result = false;
                    throw;
                }
                return result;
            }
        }
        public class CompanyCase
        {
            public static void CreateCase(DateTime date)
            {
                try
                {
                    using(InvoiceEntities entity=new InvoiceEntities())
                    {
                        var casing = new Casing()
                        {
                            Date = date,
                            cash = 0
                        };
                        entity.Casing.Add(casing);
                        entity.SaveChanges();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
            public static bool CaseUpdate(float _cash)
            {
                try
                {
                    using(InvoiceEntities entity=new InvoiceEntities())
                    {
                        var datetime = DateTime.Now.Date;
                        
                        
                        if ( entity.Casing.Where(x => x.Date == datetime).Any())
                        {
                            

                            var date = entity.Casing.Where(x => x.Date == datetime).FirstOrDefault();
                            var currentcash = date.cash;
                            date.cash = currentcash + _cash;
                            entity.SaveChanges();
                            return true;
                        }
                        else if (!entity.Casing.Where(x => x.Date == datetime).Any())
                        {
                            float previouscash;
                            Casing previousdate = getPreviousCasing();
                            if (previousdate != null)
                            {
                                previouscash = (float)previousdate.cash;
                            }
                            else
                            {
                                previouscash = 0;
                            }
                         
                            CreateCase(datetime);
                            var date =entity.Casing.Where(x => x.Date == datetime).FirstOrDefault();
                            date.cash += previouscash+_cash;
                            entity.SaveChanges();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                       
                    }
                }
                catch (Exception e)
                {
                    e.ToString();
                    return false;
                    throw;
                }
            }
            public static List<Casing> GetCasings()
            {
                try
                {
                    using(InvoiceEntities entity=new InvoiceEntities())
                    {
                        List<Casing> casings = entity.Casing.Where(x => x.Date >= DateTime.Today.AddDays(-30) && x.Date <= DateTime.Today.AddDays(-1)).OrderByDescending(x=>x.ID).ToList();
                        return casings;
                    }
                }
                catch (Exception)
                {
                    return null;
                    throw;
                }
            }
            static Casing getPreviousCasing()
            {
                try
                {
                    using (InvoiceEntities entity = new InvoiceEntities())
                    {
                        var casingdata = entity.Casing.OrderByDescending(x => x.ID).FirstOrDefault();
                        if (casingdata != null)
                        {
                            var data = entity.Casing.Where(x => x.ID == casingdata.ID).FirstOrDefault();
                            return data;
                        }
                        return null;
                    }
                }
                catch (Exception e)
                {
                    e.ToString();
                    throw;
                }
               
                
                
            }
            public static float getCash()
            {
                try
                {
                    using(InvoiceEntities entity=new InvoiceEntities())
                    {
                        var today = DateTime.Today;
                        var casing = entity.Casing.Where(x => DbFunctions.TruncateTime(x.Date) == today).FirstOrDefault();
                        if (casing != null)
                        {
                            float cash = (float)casing.cash;
                            return cash;

                        }
                        casing = getPreviousCasing();
                        if (casing != null)
                        {
                            float cash = (float)casing.cash;
                            return cash;
                        }
                        else
                        {
                            return 0;
                        }

                    }
                }
                catch (Exception e)
                {
                    e.ToString();
                    return 0;
                    throw;
                }
            }
        }
        public class CaseTransaction
        {
            public static List<CasingTransaction> GetTransactions()
            {
                try
                {
                    using(InvoiceEntities entity=new InvoiceEntities())
                    {
                        List<CasingTransaction> transaction = entity.CasingTransaction.Include("SystemUser").OrderByDescending(x=>x.ID).ToList();
                        return transaction;

                            
                    }
                }
                catch (Exception e)
                {
                    e.ToString();
                    return null;
                    throw;
                }
            }
            public static bool Add(string _name,int transaction_type,string _desc,int _create_at,float _paid) 
            {
                try
                {
                    using(InvoiceEntities entity=new InvoiceEntities())
                    {
                        CasingTransaction transaction = new CasingTransaction()
                        {
                            Date = DateTime.Now,
                            Name = _name,
                            TransactionType=transaction_type,
                            Description = _desc,
                            Create_At = _create_at,
                            Paid = _paid
                            
                        };
                        entity.CasingTransaction.Add(transaction);
                        entity.SaveChanges();
                    }
                    CompanyCase.CaseUpdate(_paid);
                    
                    return true;
                }
                catch (Exception e)
                {
                    e.ToString();
                    return false;
                    throw;
                }
            }
        }
        public class Product
        {
            public static List<Products> getProducts()
            {
                try
                {
                    using(InvoiceEntities entity=new InvoiceEntities())
                    {
                        var products = entity.Products.ToList();
                        return products;
                    }
                }
                catch (Exception)
                {
                    return null;
                    throw;
                }
            }
            public static Products getProduct(int id)
            {
                try
                {
                    using(InvoiceEntities entity=new InvoiceEntities())
                    {
                        var product = entity.Products.Where(x => x.ID == id).FirstOrDefault();
                        return product;
                    }
                }
                catch (Exception)
                {
                    return null;
                    throw;
                }
            }
            public static bool Add(string productName)
            {
                try
                {
                    using (InvoiceEntities entity = new InvoiceEntities())
                    {
                        var product = new Products()
                        {
                            ProductName = productName
                        };
                        entity.Products.Add(product);
                        entity.SaveChanges();
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
            }
            public static bool Update(int id,string productName)
            {
                try
                {
                    using (InvoiceEntities entity = new InvoiceEntities())
                    {
                        var product = entity.Products.Where(x => x.ID == id).FirstOrDefault();
                        product.ProductName = productName;
                        entity.SaveChanges();
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
            }
        }
        public class Invoice
        {
            public static List<Invoices> GetInvoices()
            {
                try
                {
                    using (InvoiceEntities entity = new InvoiceEntities())
                    {
                        var invoices = entity.Invoices.Include("Companies").Include("Products").Include("SystemUser").Where(x=>x.Status==1).ToList();
                        return invoices;
                    }
                }
                catch (Exception)
                {
                    return null;
                    throw;
                }
            }
            
            public static List<Invoices> GetCompanyInvoices(int companyid)
            {
                try
                {
                    using (InvoiceEntities entity = new InvoiceEntities())
                    {
                        var invoices = entity.Invoices.Include("Companies").Include("Products").Include("SystemUser").Where(x=>x.CompanyID==companyid && x.Status==1).ToList();
                        return invoices;
                    }
                }
                catch (Exception)
                {
                    return null;
                    throw;
                }
            }
            public static Invoices getDetail(int id)
            {
                try
                {
                    using (InvoiceEntities entity = new InvoiceEntities())
                    {
                        return entity.Invoices.Include("Companies").Include("Products").Include("SystemUser").Where(x => x.ID == id && x.Status == 1).FirstOrDefault();
                    }
                }
                catch (Exception)
                {
                    return null;
                    throw;
                }
            }
            public static bool Add(int companyid,int productid,float weight,float unitprice,float amount,string desc,float paid,int invoicetype,int create_at)
            {
                
                    try
                    {
                        using (InvoiceEntities entity = new InvoiceEntities())
                        {
                              var invoice = new Invoices()
                              {
                                Date=DateTime.Now,
                                CompanyID=companyid,
                                ProductId=productid,
                                Weight=weight,
                                UnitPrice=unitprice,
                                Amount=amount,
                                Description=desc,
                                Paid=paid,
                                InvoiceType=invoicetype,
                                Create_At=create_at,
                                Status=1,
                                CreateDate=DateTime.Now
                              };
                        entity.Invoices.Add(invoice); 
                        entity.SaveChanges();

                            return true;
                        }
                    }
                    catch (Exception)
                    {
                        return false;
                        throw;
                    }
                
            }
            public static bool Update(int id, int productid, float weight, float unitprice, float amount, string desc, float paid)
            {

                try
                {
                    using (InvoiceEntities entity = new InvoiceEntities())
                    {

                        var invoice = entity.Invoices.Where(x => x.ID == id && x.Status == 1).FirstOrDefault();
                        invoice.ProductId = productid;
                        invoice.Weight = weight;
                        invoice.UnitPrice = unitprice;
                        invoice.Amount = amount;
                        invoice.Description = desc;
                        invoice.Paid = paid;
                        invoice.UpdateDate = DateTime.Now;
                        entity.SaveChanges();
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }

            }
            public static bool Delete(int id)
            {

                try
                {
                    using (InvoiceEntities entity = new InvoiceEntities())
                    {

                        var invoice = entity.Invoices.Where(x => x.ID == id && x.Status == 1).FirstOrDefault();
                        invoice.DeleteDate = DateTime.Now;
                        invoice.Status = 0;
                        CompanyCase.CaseUpdate(-(float)invoice.Paid);
                        entity.SaveChanges();
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }

            }
        }
        public class OfficialInvoice
        {
            public static bool Add(int _companyid,string _waybillno,string _invoiceno,float _amount,string _desc, float _paid,int _createat,int _entry_print)
            {
                try
                {
                    using(InvoiceEntities entity=new InvoiceEntities())
                    {
                        OfficialInvoices invoice = new OfficialInvoices()
                        {
                            CompanyID = _companyid,
                            WaybillNo = _waybillno,
                            InvoiceNo = _invoiceno,
                            Amount = _amount,
                            Description = _desc,
                            Paid = _paid,
                            Create_At = 544,
                            entry_printout = _entry_print,
                            CreateDate = DateTime.Now,
                            Status = 1,
                            Date=DateTime.Now
                        };
                        entity.OfficialInvoices.Add(invoice);
                        entity.SaveChanges();
                        return true;
                    }
                }
                catch (Exception e)
                {
                    string excep = e.ToString();
                    return false;
                    throw;
                }
            }
            public static OfficialInvoices getDetail(int id)
            {
                try
                {
                    using(InvoiceEntities entity=new InvoiceEntities())
                    {
                        var detail = entity.OfficialInvoices.Where(x => x.ID == id && x.Status == 1).FirstOrDefault();
                        return detail;
                    }
                }
                catch (Exception)
                {
                    return null;
                    throw;
                }
            }
            public static List<OfficialInvoices> GetOfficialInvoices()
            {
                try
                {
                    using(InvoiceEntities entity=new InvoiceEntities())
                    {
                        return entity.OfficialInvoices.Include("Companies").Include("SystemUser").Where(x=>x.Status==1).ToList();
                    }
                }
                catch (Exception)
                {
                    return null;
                    throw;
                }
            }
            public static List<OfficialInvoices> getCompanyOfficialInvoices(int _companyid)
            {
                try
                {
                    using (InvoiceEntities entity = new InvoiceEntities())
                    {
                        return entity.OfficialInvoices.Include("SystemUser").Where(x=>x.CompanyID==_companyid && x.Status==1).ToList();
                    }
                }
                catch (Exception)
                {
                    return null;
                    throw;
                }
            }
            public static bool Update(int id, string _waybillno, string _invoiceno, float _amount, string _desc, float _paid)
            {
                try
                {
                    using(InvoiceEntities entity=new InvoiceEntities())
                    {
                        var officialinvoice = entity.OfficialInvoices.Where(x => x.ID == id && x.Status == 1).FirstOrDefault();
                        officialinvoice.WaybillNo = _waybillno;
                        officialinvoice.InvoiceNo = _invoiceno;
                        officialinvoice.Amount = _amount;
                        officialinvoice.Description = _desc;
                        officialinvoice.Paid = _paid;
                        officialinvoice.UpdateDate = DateTime.Now;
                        entity.SaveChanges();
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
            }
            public static bool Delete(int id)
            {
                try
                {
                    using (InvoiceEntities entity = new InvoiceEntities())
                    {
                        var officialinvoice = entity.OfficialInvoices.Where(x => x.ID == id && x.Status == 1).FirstOrDefault();
                        officialinvoice.Status = 0;
                        officialinvoice.DeleteDate = DateTime.Now;
                        entity.SaveChanges();
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
            }
        }
        public class User
        {
            public static SystemUser getUserId(string username)
            {
                try
                {
                    using(InvoiceEntities entity=new InvoiceEntities())
                    {
                        return entity.SystemUser.Where(x => x.Username == username && x.Status == 1).FirstOrDefault();
                    }
                }
                catch (Exception)
                {
                    return null;
                    throw;
                }
            }
            public static List<SystemUser> getUserList()
            {
                try
                {
                    using(InvoiceEntities entity=new InvoiceEntities())
                    {
                        List<SystemUser> systemUsers = entity.SystemUser.Include("Roles").Where(x => x.Status == 1).ToList();
                        return systemUsers;
                    }
                 
                }
                catch (Exception)
                {
                    return null;
                    throw;
                }
            }
            public static SystemUser getUser(int id)
            {
                try
                {
                    using (InvoiceEntities entity = new InvoiceEntities())
                    {
                        SystemUser systemUsers = entity.SystemUser.Where(x => x.Status == 1 && x.ID==id).FirstOrDefault();
                        return systemUsers;
                    }

                }
                catch (Exception)
                {
                    return null;
                    throw;
                }
            }
            public static bool Add(string username,string password)
            {
                
               
                SystemUser user = new SystemUser()
                {
                    Username = username,
                    Password = BCrypt.Net.BCrypt.HashPassword(password),
                    RoleID = 1,
                    Status=1,
                    Create_Date = DateTime.Now,
                    
                };
                try
                {
                    
                    using (InvoiceEntities entity=new InvoiceEntities())
                    {
                        if (entity.SystemUser.Where(x => x.Username == username).Any())
                        {
                            return false;
                        }
                        entity.SystemUser.Add(user);
                        entity.SaveChanges();
                        return true;
                    }
                }
                catch (Exception e)
                {
                    e.ToString();
                    e.InnerException.ToString();
                    return false;
                    throw;
                }
            }
            public static SystemUser Detail(int id)
            {
                try
                {
                    using(InvoiceEntities entity=new InvoiceEntities())
                    {
                        return entity.SystemUser.Where(x => x.ID == id && x.Status == 1).FirstOrDefault();
                    }
                }
                catch (Exception)
                {
                    return null;
                    throw;
                }
            }
            public static bool Update(int id,string password)
            {
                try
                {
                    using(InvoiceEntities entity=new InvoiceEntities())
                    {
                        var data = entity.SystemUser.Where(x => x.ID == id && x.Status == 1).FirstOrDefault();
                        data.Password = BCrypt.Net.BCrypt.HashPassword(password);
                        data.Update_Date = DateTime.Now;
                        entity.SaveChanges();
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
            }
            public static bool Delete(int userid)
            {
                try
                {
                    using (InvoiceEntities entity = new InvoiceEntities())
                    {
                        var data = entity.SystemUser.Where(x => x.ID == userid && x.Status == 1).FirstOrDefault();
                        data.Status = 0;
                        data.Delete_Date = DateTime.Now;
                        entity.SaveChanges();
                    }
                    return true;
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
            }
            public static bool Login(string username, string password)
            {
                try
                {
                    using (InvoiceEntities entity = new InvoiceEntities())
                    {
                        var user = entity.SystemUser.Where(x => x.Username == username && x.Status == 1).FirstOrDefault();
                        if (user != null)
                        {
                            bool isValidPassword = BCrypt.Net.BCrypt.Verify(password, user.Password);
                            if (isValidPassword == true)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false;
                        }
                    }

                }
                catch (Exception)
                {
                    return false;
                    throw;
                }

            }
        }
        public class CompanyTransaction
        {
            public static bool CreateTransaction(int companyid,float cash)
            {
                try
                {
                    using (InvoiceEntities entity = new InvoiceEntities())
                    {
                        float previouscash = 0;
                        if (entity.CompanyTransactions.Where(x => x.CompanyID == companyid).Any())
                        {
                            var previoustransaction = entity.CompanyTransactions.Where(x => x.CompanyID == companyid).OrderByDescending(x => x.ID).FirstOrDefault();
                            previouscash = (float)previoustransaction.Cash;
                        }
                        var transaction = new CompanyTransactions()
                        {
                            CompanyID = companyid,
                            Date = DateTime.Now,
                            Cash = cash + previouscash
                        };
                        entity.CompanyTransactions.Add(transaction);
                        entity.SaveChanges();
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                    throw;
                }
            }
            public static float GetTransaction(int companyid)
            {
                try
                {
                    using(InvoiceEntities entity=new InvoiceEntities())
                    {
                        var transaction = entity.CompanyTransactions.Where(x => x.CompanyID == companyid).OrderByDescending(x => x.ID).FirstOrDefault();
                        return (float)transaction.Cash;
                    }
                }
                catch (Exception)
                {
                    return 0;
                    throw;
                }
            }
        }
        public class Model
        {
            public class CompanyProductsModel
            {
                public Companies company { get; set; }
                public List<Products> products { get; set; }
            }
            public class CompanyAndType
            {
                public List<Companies> companies { get; set; }
                public int companytype { get; set; }
             
            }
            public class InvoiceProductsModel
            {
                public Invoices invoice { get; set; }
                public List<Products> products { get; set; }
            }
            public class CasingModel
            {
                public float casing { get; set; }
                public List<Casing> casings { get; set; }
            }
        }
    }
}