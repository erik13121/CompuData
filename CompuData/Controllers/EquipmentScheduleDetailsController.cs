using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class EquipmentScheduleDetailsController : Controller
    {
        // GET: EquipmentScheduleDetails
        public ActionResult Index(string scheduleID)
        {
            Models.EquipmentSchedule myModel = new Models.EquipmentSchedule();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (scheduleID != null)
            {
                var intScheduleID = Int32.Parse(scheduleID);
                var mySchedule = db.Equipment_Schedule_Line.Where(i => i.LineID == intScheduleID).FirstOrDefault();
                var myEquipment = db.Equipments.Where(i => i.EquipmentID == mySchedule.EquipmentID).FirstOrDefault();

                myModel.LineID = mySchedule.LineID;
                myModel.ManufacturerName = myEquipment.ManufacturerName;
                myModel.ModelNumber = myEquipment.ModelNumber;
                myModel.Date = mySchedule.Date;
                myModel.TimeStart = mySchedule.TimeStart;
                myModel.TimeEnd = mySchedule.TimeEnd;
                myModel.Status = mySchedule.Status;
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToEquipmentScheduleDetails(string scheduleID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "EquipmentScheduleDetails", new { scheduleID = scheduleID });
            return Json(new { Url = redirectUrl });
        }
    }
}