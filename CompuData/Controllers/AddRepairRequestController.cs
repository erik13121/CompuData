using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddRepairRequestController : Controller
    {
        // GET: AddRepairRequest
        public ActionResult Index(string equipmentID)
        {
            var db = new CodeFirst.CodeFirst();
            var requests = new Models.EquipmentRepairRequest();
            requests.EquipmentID = Int32.Parse(equipmentID);
            requests.repairPeople = db.RepairPersons.ToList();
            return View(requests);
        }

        [HttpPost]
        public ActionResult RedirectToAddRepairRequest(string equipmentID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "AddRepairRequest", new { equipmentID = equipmentID });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "")]Models.EquipmentRepairRequest model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                if (db.Repair_Request.Count() > 0)
                {
                    var item = db.Repair_Request.OrderByDescending(a => a.RequestID).FirstOrDefault();

                    db.Repair_Request.Add(new CodeFirst.Repair_Request
                    {
                        RequestID = item.RequestID + 1,
                        Approved = false,
                        Repaired = false,
                        Reason = model.Reason,
                        EquipmentID = model.EquipmentID,
                        RepPersonID = model.RepPersonID
                    });
                }
                else
                {
                    db.Repair_Request.Add(new CodeFirst.Repair_Request
                    {
                        RequestID = 1,
                        Approved = false,
                        Repaired = false,
                        Reason = model.Reason,
                        EquipmentID = model.EquipmentID,
                        RepPersonID = model.RepPersonID
                    });
                }

                db.SaveChanges();
                model.JavaScriptToRun = "mySuccess()";
                TempData["model"] = model;
                return RedirectToAction("Index", "EquipmentRepairRequests");
            }

            model.repairPeople = db.RepairPersons.ToList();
            return View("Index", model);
        }
    }
}