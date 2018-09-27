using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ModifyEquipmentRepairRequestController : Controller
    {
        // GET: ModifyEquipmentRepairRequest
        public ActionResult Index(string requestID)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            Models.EquipmentRepairRequest myModel = new Models.EquipmentRepairRequest();
            if (requestID != null)
            {
                var intRequestID = Int32.Parse(requestID);
                var myRequest = db.Repair_Request.Where(i => i.RequestID == intRequestID).FirstOrDefault();

                myModel.RequestID = myRequest.RequestID;
                myModel.Approved = myRequest.Approved;
                myModel.Repaired = myRequest.Repaired;
                myModel.Reason = myRequest.Reason;
                myModel.EquipmentID = myRequest.EquipmentID;
                myModel.RepPersonID = myRequest.RepPersonID;

                myModel.repairPeople = db.RepairPersons.ToList();
                return View(myModel);
            }

            myModel.repairPeople = db.RepairPersons.ToList();
            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToModifyEquipmentRepairRequestDetails(string requestID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ModifyEquipmentRepairRequest", new { requestID = requestID });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult Modify([Bind(Prefix = "")]Models.EquipmentRepairRequest model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                var request = db.Repair_Request.Where(v => v.RequestID == model.RequestID).SingleOrDefault();

                if (request != null)
                {
                    request.RequestID = model.RequestID;
                    request.Approved = model.Approved;
                    request.Repaired = model.Repaired;
                    request.Reason = model.Reason;
                    request.EquipmentID = model.EquipmentID;
                    request.RepPersonID = model.RepPersonID;
                    db.SaveChanges();
                }

                TempData["js"] = "myUpdateSuccess()";
                return RedirectToAction("Index", "EquipmentRepairRequests");
            }
            model.repairPeople = db.RepairPersons.ToList();
            return View("Index", model);
        }
    }
}