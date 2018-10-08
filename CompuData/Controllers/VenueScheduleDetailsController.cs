using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class VenueScheduleDetailsController : Controller
    {
        // GET: VenueScheduleDetails
        public ActionResult Index(string scheduleID)
        {
            Models.VenueSchedule myModel = new Models.VenueSchedule();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (scheduleID != null)
            {
                var intScheduleID = Int32.Parse(scheduleID);
                var mySchedule = db.Venue_Schedule_Line.Where(i => i.ScheduleID == intScheduleID).FirstOrDefault();
                var myVenue = db.Venues.Where(i => i.VenueID == mySchedule.VenueID).FirstOrDefault();
                var myBuilding = db.Buildings.Where(i => i.BuildingID == mySchedule.BuildingID).FirstOrDefault();

                myModel.ScheduleID = mySchedule.ScheduleID;
                myModel.Name = myVenue.Name;
                myModel.BuildingName = myBuilding.Name;
                myModel.DateAvailable = mySchedule.DateAvailable;
                myModel.StartTime = mySchedule.StartTime;
                myModel.EndTime = mySchedule.EndTime;
                myModel.Status = mySchedule.Status;
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToVenueScheduleDetails(string scheduleID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "VenueScheduleDetails", new { scheduleID = scheduleID });
            return Json(new { Url = redirectUrl });
        }
    }
}