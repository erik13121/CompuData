 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ModifySupplierController : Controller
    {
        // GET: ModifySupplier
        public ActionResult Index(string SupplierID)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            Models.Supplier myModel = new Models.Supplier();
            if (SupplierID != null)
            {
                var intSupplierID = Int32.Parse(SupplierID);
                var mySupplier = db.Suppliers.Where(i => i.SupplierID == intSupplierID).FirstOrDefault();

                myModel.SupplierID = mySupplier.SupplierID;
                myModel.Name = mySupplier.Name;
                myModel.VATNumber = mySupplier.VATNumber;
                myModel.EmailAddress = mySupplier.EmailAddress;
                myModel.ContactNumber = mySupplier.ContactNumber;
                myModel.Bank = mySupplier.Bank;
                myModel.AccountNumber = mySupplier.AccountNumber;
                myModel.BranchCode = mySupplier.BranchCode;
                myModel.POAddress = mySupplier.POAddress;
                myModel.POCity = mySupplier.POCity;
                myModel.POAreaCode = mySupplier.POAreaCode;

                return View(myModel);
            }

            return View(myModel);
        }     

        [HttpPost]
        public ActionResult RedirectToModifySupplierDetails(string supplierID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ModifySupplier", new { supplierID = supplierID });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult Modify([Bind(Prefix = "")]Models.Supplier model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                var Supplier = db.Suppliers.Where(v => v.SupplierID == model.SupplierID).SingleOrDefault();

                if (Supplier != null)
                {
                    Supplier.SupplierID = model.SupplierID;
                    Supplier.Name = model.Name;
                    Supplier.VATNumber = model.VATNumber;
                    Supplier.EmailAddress = model.EmailAddress;
                    Supplier.ContactNumber = model.ContactNumber;
                    Supplier.Bank = model.Bank;
                    Supplier.AccountNumber = model.AccountNumber;
                    Supplier.BranchCode = model.BranchCode;
                    Supplier.POAddress = model.POAddress;
                    Supplier.POCity = model.POCity;
                    Supplier.POAreaCode = model.POAreaCode;
                    db.SaveChanges();
                }

                TempData["js"] = "myUpdateSuccess()";
                return RedirectToAction("Index", "Supplier");
            }

            return View("Index", model);
        }
    }
}