using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddSupplierController : Controller
    {
        // GET: AddSupplier
        public ActionResult Index()
        {
            var db = new CodeFirst.CodeFirst();
            var supplier = new Models.Supplier();
            return View(supplier);
        }

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "")]Models.Supplier model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                if (db.Suppliers.Count() > 0)
                {
                    var item = db.Suppliers.OrderByDescending(a => a.SupplierID).FirstOrDefault();

                    db.Suppliers.Add(new CodeFirst.Supplier
                    {
                        SupplierID = item.SupplierID + 1,
                        Name = model.Name,
                        VATNumber = model.VATNumber,
                        EmailAddress = model.EmailAddress,
                        ContactNumber = model.ContactNumber,
                        Bank = model.Bank,
                        AccountNumber = model.AccountNumber,
                        BranchCode = model.BranchCode,
                        POAddress = model.POAddress,
                        POCity = model.POCity,
                        POAreaCode = model.POAreaCode
                });
                }
                else
                {
                    db.Suppliers.Add(new CodeFirst.Supplier
                    {
                        SupplierID = 1,
                        Name = model.Name,
                        VATNumber = model.VATNumber,
                        EmailAddress = model.EmailAddress,
                        ContactNumber = model.ContactNumber,
                        Bank = model.Bank,
                        AccountNumber = model.AccountNumber,
                        BranchCode = model.BranchCode,
                        POAddress = model.POAddress,
                        POCity = model.POCity,
                        POAreaCode = model.POAreaCode
                    });
                }

                db.SaveChanges();
                model.JavaScriptToRun = "mySuccess()";
                TempData["model"] = model;
                return RedirectToAction("Index", "Supplier");
            }

            return View("Index", model);
        }
    }
}