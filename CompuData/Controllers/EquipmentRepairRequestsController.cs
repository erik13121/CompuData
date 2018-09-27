using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class EquipmentRepairRequestsController : Controller
    {
        // GET: EquipmentRepairRequests
        public ActionResult Index()
        {
            Models.EquipmentRepairRequest myModel = new Models.EquipmentRepairRequest();
            if (TempData["model"] != null)
            {
                myModel = (Models.EquipmentRepairRequest)TempData["model"];
                TempData.Remove("model");
            }
            return View(myModel);
        }

        public ActionResult PageData(IDataTablesRequest request)
        {
            // Nothing important here. Just creates some mock data.
            var data = Models.EquipmentRepairRequest.GetData();

            var db = new CodeFirst.CodeFirst();
            var newData = (from d in data
                           join e in db.Equipments.ToList() on d.EquipmentID equals e.EquipmentID
                           into equipments
                           from mE in equipments.DefaultIfEmpty()
                           join p in db.RepairPersons.ToList() on d.RepPersonID equals p.RepPersonID
                           select new
                           {
                               RequestID = d.RequestID,
                               Approved = d.Approved,
                               Repaired = d.Repaired,
                               Reason = d.Reason,
                               Equipment = mE != null ? mE.ManufacturerName + " " + mE.ModelNumber : "Not linked",
                               RepPerson = p.Name
                           }).ToList();

            // Global filtering.
            // Filter is being manually applied due to in-memmory (IEnumerable) data.
            // If you want something rather easier, check IEnumerableExtensions Sample.
            var filteredData = newData.Where(_item =>
            _item.RequestID.ToString().Contains(request.Search.Value) ||
            _item.Approved.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.Repaired.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.Reason.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            (_item.Equipment != null ? _item.Equipment.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) : false) ||
            _item.RepPerson.ToUpper().Contains(request.Search.Value.ToUpper())
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
        public ActionResult Delete(string requestID)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();
                var intRequestID = int.Parse(requestID);
                var request = db.Repair_Request.Where(v => v.RequestID == intRequestID).FirstOrDefault();
                db.Repair_Request.Remove(request);
                db.SaveChanges();

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "EquipmentRepairRequests");
                return Json(new { Url = redirectUrl });
            }
            catch
            {
                return Json(new { Url = "Cascading error!" });
            }
        }

        [HttpPost]
        public ActionResult Approve(string requestID)
        {
            var db = new CodeFirst.CodeFirst();
            var intRequestID = int.Parse(requestID);
            var request = db.Repair_Request.Where(r => r.RequestID == intRequestID).FirstOrDefault();
            request.Approved = true;
            db.SaveChanges();

            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "EquipmentRepairRequests");
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult Finalize(string requestID)
        {
            var db = new CodeFirst.CodeFirst();
            var intRequestID = int.Parse(requestID);
            var request = db.Repair_Request.Where(r => r.RequestID == intRequestID).FirstOrDefault();
            if (request.Approved == true)
            {
                request.Repaired = true;
            }
            else
            {
                return Json(new { ErrorMessage = "Error" });
            }
            
            db.SaveChanges();

            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "EquipmentRepairRequests");
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public void SetTempData()
        {
            TempData["js"] = "";
        }
    }
}