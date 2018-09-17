using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class SuppliersDetailsController : Controller
    {
        // GET: SuppliersDetails
        public ActionResult Index(string supplierID)
        {
            Models.Supplier myModel = new Models.Supplier();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (supplierID != null)
            {
                var intSupplierID = Int32.Parse(supplierID);
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
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToSupplierDetails(string supplierID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "SuppliersDetails", new { supplierID = supplierID });
            return Json(new { Url = redirectUrl });
        }
    }
}