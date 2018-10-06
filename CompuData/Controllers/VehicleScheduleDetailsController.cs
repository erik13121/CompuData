using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class VehicleScheduleDetailsController : Controller
    {
        // GET: VehicleScheduleDetails
        public ActionResult Index(string scheduleID)
        {
            Models.VehicleSchedule myModel = new Models.VehicleSchedule();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (scheduleID != null)
            {
                var intScheduleID = Int32.Parse(scheduleID);
                var mySchedule = db.Vehicle_Schedule_Line.Where(i => i.Veh_Schedule_ID == intScheduleID).FirstOrDefault();
                var myVehicle = db.Vehicles.Where(i => i.VehicleID == mySchedule.VehicleID).FirstOrDefault();

                myModel.Veh_Schedule_ID = mySchedule.Veh_Schedule_ID;
                myModel.Brand = myVehicle.Brand;
                myModel.Model = myVehicle.Model;
                myModel.NumberPlate = myVehicle.NumberPlate;
                myModel.Date = mySchedule.Date;
                myModel.StartTime = mySchedule.StartTime;
                myModel.EndTime = mySchedule.EndTime;
                myModel.Status = mySchedule.Status;
            }
            
            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToVehicleScheduleDetails(string scheduleID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "VehicleScheduleDetails", new { scheduleID = scheduleID });
            return Json(new { Url = redirectUrl });
        }
    }
}