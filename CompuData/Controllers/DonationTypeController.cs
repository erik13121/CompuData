using CompuData.Models;
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class DonationTypeController : Controller
    {
        // GET: DonationType
        public ActionResult Index()
        {
            DonationType myModel = new DonationType();
            if (TempData["model"] != null)
            {
                myModel = (DonationType)TempData["model"];
                TempData.Remove("model");
            }
            return View(myModel);
        }

        public ActionResult PageData(IDataTablesRequest request)
        {
            // Nothing important here. Just creates some mock data.
            var data = Models.DonationType.GetData();

            // Global filtering.
            // Filter is being manually applied due to in-memmory (IEnumerable) data.
            // If you want something rather easier, check IEnumerableExtensions Sample.
            var filteredData = data.Where(_item =>
            _item.TypeID.ToString().Contains(request.Search.Value) ||
            _item.TypeName.ToUpper().Contains(request.Search.Value.ToUpper())
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
        public void SetTempData()
        {
            TempData["js"] = "";
        }

        [HttpPost]
        public ActionResult Delete(string typeID)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();
                var intTypeID = int.Parse(typeID);
                var type = db.Donation_Type.Where(t => t.TypeID == intTypeID).FirstOrDefault();
                db.Donation_Type.Remove(type);
                db.SaveChanges();

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "DonationType");
                return Json(new { Url = redirectUrl });
            }
            catch
            {
                return Json(new { Url = "Cascading error!" });
            }
        }
    }
}