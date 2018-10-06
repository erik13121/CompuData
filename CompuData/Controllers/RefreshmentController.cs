using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class RefreshmentController : Controller
    {
        // GET: Refreshment
        public ActionResult Index()
        {
            Models.Refreshment myModel = new Models.Refreshment();
            if (TempData["model"] != null)
            {
                myModel = (Models.Refreshment)TempData["model"];
                TempData.Remove("model");
            }
            return View(myModel);
        }

        public ActionResult PageData(IDataTablesRequest request)
        {
            // Nothing important here. Just creates some mock data.
            var data = Models.Refreshment.GetData();

            // Global filtering.
            // Filter is being manually applied due to in-memmory (IEnumerable) data.
            // If you want something rather easier, check IEnumerableExtensions Sample.
            var filteredData = data.Where(_item =>
            _item.RefreshmentID.ToString().Contains(request.Search.Value) ||
            _item.Name.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.Description.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.UnitPrice.ToString().ToUpper().Contains(request.Search.Value.ToUpper())
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
        public ActionResult Delete(string refreshmentID)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();
                var intSupplierID = int.Parse(refreshmentID);
                var Refreshment = db.Refreshments.Where(v => v.RefreshmentID == intSupplierID).FirstOrDefault();
                db.Refreshments.Remove(Refreshment);
                db.SaveChanges();

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Refreshment");
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