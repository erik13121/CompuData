using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class SupplierController : Controller
    {
        // GET: Supplier
        public ActionResult Index()
        {
            Models.Supplier myModel = new Models.Supplier();
            if (TempData["model"] != null)
            {
                myModel = (Models.Supplier)TempData["model"];
                TempData.Remove("model");
            }
            return View(myModel);
        }

        public ActionResult PageData(IDataTablesRequest request)
        {
            // Nothing important here. Just creates some mock data.
            var data = Models.Supplier.GetData();     

            // Global filtering.
            // Filter is being manually applied due to in-memmory (IEnumerable) data.
            // If you want something rather easier, check IEnumerableExtensions Sample.
            var filteredData = data.Where(_item =>
            _item.SupplierID.ToString().Contains(request.Search.Value) ||
            _item.Name.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.ContactNumber.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            (_item.VATNumber != null ? _item.VATNumber.ToString().Contains(request.Search.Value) : false) ||
             (_item.EmailAddress != null ? _item.EmailAddress.ToString().Contains(request.Search.Value) : false) ||
             (_item.Bank != null ? _item.Bank.ToString().Contains(request.Search.Value) : false) ||
            (_item.AccountNumber != null ? _item.AccountNumber.ToString().Contains(request.Search.Value) : false) ||
            (_item.BranchCode != null ? _item.BranchCode.ToString().Contains(request.Search.Value) : false) ||
             (_item.POAddress != null ? _item.POAddress.ToString().Contains(request.Search.Value) : false) ||
             (_item.POAreaCode != null ? _item.POAreaCode.ToString().Contains(request.Search.Value) : false) ||
             (_item.POCity != null ? _item.POCity.ToString().Contains(request.Search.Value) : false) 
            );

            // Paging filtered data.
            // Paging is rather manual due to in-memmory (IEnumerable) data.
            var dataPage = filteredData.Skip(request.Start).Take(request.Length);

            // Response creation. To create your response you need to reference your request, to avoid
            // request/response tampering and to ensure response will be correctly created.
            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), dataPage);

            // Easier way is to return a new 'DataTablesJsonResult', which will automatically convert your
            // response to a json-compatible content, so DataTables can read it when received.
            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(string SupplierID)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();
                var intSupplierID = int.Parse(SupplierID);
                var Supplier = db.Suppliers.Where(v => v.SupplierID == intSupplierID).FirstOrDefault();
                db.Suppliers.Remove(Supplier);
                db.SaveChanges();

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Supplier");
                return Json(new { Url = redirectUrl });
            }
            catch
            {
                return Json(new { Url = "Cascading error!" });
            }
        }

        [HttpPost]
        public void SetTempData()
        {
            TempData["js"] = "";
        }
    }
}