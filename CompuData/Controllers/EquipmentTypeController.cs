using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class EquipmentTypeController : Controller
    {
        // GET: EquipmentType
        public ActionResult Index()
        {
            Models.EquipmentType myModel = new Models.EquipmentType();
            if (TempData["model"] != null)
            {
                myModel = (Models.EquipmentType)TempData["model"];
                TempData.Remove("model");
            }
            return View(myModel);
        }

        public ActionResult PageData(IDataTablesRequest request)
        {
            // Nothing important here. Just creates some mock data.
            var data = Models.EquipmentType.GetData();

            // Global filtering.
            // Filter is being manually applied due to in-memmory (IEnumerable) data.
            // If you want something rather easier, check IEnumerableExtensions Sample.
            var filteredData = data.Where(_item =>
            _item.TypeID.ToString().Contains(request.Search.Value) ||
            _item.TypeName.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.TypeDescription.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.Removable.ToString().ToUpper().Contains(request.Search.Value)
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
        public ActionResult Delete(string TypeID)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();
                var intTypeID = int.Parse(TypeID);
                var EquipType = db.Suppliers.Where(v => v.SupplierID == intTypeID).FirstOrDefault();
                db.Suppliers.Remove(EquipType);
                db.SaveChanges();

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "EquipmentType");
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