using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class EquipmentsController : Controller
    {
        // GET: Equipments
        public ActionResult Index()
        {
            Models.Equipment myModel = new Models.Equipment();
            if (TempData["model"] != null)
            {
                myModel = (Models.Equipment)TempData["model"];
                TempData.Remove("model");
            }
            return View(myModel);
        }

        public ActionResult PageData(IDataTablesRequest request)
        {
            // Nothing important here. Just creates some mock data.
            var data = Models.Equipment.GetData();

            
            var db = new CodeFirst.CodeFirst();
            var newData = (from e in data
                           join u in db.Users.ToList() on e.UserID equals u.UserID
                           into users
                           from mU in users.DefaultIfEmpty()
                           join t in db.Equipment_Type.ToList() on e.TypeID equals t.TypeID
                           select new
                           {
                               EquipmentID = e.EquipmentID,
                               ManufacturerName = e.ManufacturerName,
                               ModelNumber = e.ModelNumber,
                               DatePurchased = e.DatePurchased.ToString("dd-MM-yyyy"),
                               ServiceIntervalMonths = e.ServiceIntervalMonths,
                               Status = e.Status,
                               UserName = mU != null ? mU.FirstName + " " + mU.LastName : "Not linked",
                               TypeName = t.TypeName,
                               Removable = t.Removable
                           }).ToList(); 
            
            // Global filtering.
            // Filter is being manually applied due to in-memmory (IEnumerable) data.
            // If you want something rather easier, check IEnumerableExtensions Sample.
            var filteredData = newData.Where(_item =>
            _item.EquipmentID.ToString().Contains(request.Search.Value) ||
            _item.ManufacturerName.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.ModelNumber.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            (_item != null ? _item.DatePurchased.ToString().Contains(request.Search.Value.ToUpper()) : false) ||
            _item.ServiceIntervalMonths.ToString().Contains(request.Search.Value) ||
            _item.Status.ToUpper().Contains(request.Search.Value) ||
            _item.UserName.ToUpper().Contains(request.Search.Value) ||
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
        public ActionResult RedirectToEquipments(string userID, string equipmentID)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();

                var inID = Int32.Parse(equipmentID);
                var equipment = db.Equipments.Where(e => e.EquipmentID == inID).FirstOrDefault();
                equipment.UserID = Int32.Parse(userID);
                db.SaveChanges();

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Equipments", new { equipmentID = equipmentID });
                return Json(new { Url = redirectUrl });
            }
            catch
            {
                return Json(new { Error = "Error" });
            }
        }

        [HttpPost]
        public ActionResult Delete(string equipmentID)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();
                var intEquipmentID = int.Parse(equipmentID);
                var equipment = db.Equipments.Where(v => v.EquipmentID == intEquipmentID).FirstOrDefault();
                equipment.Status = "Written-off";
                db.SaveChanges();

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Equipments");
                return Json(new { Url = redirectUrl });
            }
            catch
            {
                return Json(new { Url = "Error" });
            }
        }

        [HttpPost]
        public void SetTempData()
        {
            TempData["js"] = "";
        }
    }
}