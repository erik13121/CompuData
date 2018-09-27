using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class EquipmentRepairRequestDetailsController : Controller
    {
        // GET: EquipmentRepairRequestDetails
        public ActionResult Index(string requestID)
        {
            Models.EquipmentRepairRequest myModel = new Models.EquipmentRepairRequest();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (requestID != null)
            {
                var intRequestID = Int32.Parse(requestID);
                var myRequest = db.Repair_Request.Where(r => r.RequestID == intRequestID).FirstOrDefault();
                var myEquipment = db.Equipments.Where(e => e.EquipmentID == myRequest.EquipmentID).FirstOrDefault();
                var myPerson = db.RepairPersons.Where(rp => rp.RepPersonID == myRequest.RepPersonID).FirstOrDefault();

                myModel.RequestID = myRequest.RequestID;
                myModel.Approved = myRequest.Approved;
                myModel.Repaired = myRequest.Repaired;
                myModel.Reason = myRequest.Reason;
                myModel.EquipmentID = myRequest.EquipmentID;
                myModel.EquipmentName = myEquipment.ManufacturerName + " " + myEquipment.ModelNumber;
                myModel.RepPersonID = myRequest.RepPersonID;
                myModel.RepairPersonName = myPerson.Name;
            }

            myModel.repairPeople = db.RepairPersons.ToList();
            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToEquipmentRepairRequestDetails(string requestID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "EquipmentRepairRequestDetails", new { requestID = requestID });
            return Json(new { Url = redirectUrl });
        }
    }
}