﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class VenueBookingsController : Controller
    {
        // GET: VenueBookings
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetEvents()
        {
            using (CodeFirst.CodeFirst db = new CodeFirst.CodeFirst())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var events = db.Venue_Booking_Line.ToList();
                var newData =
                    (from e in events
                     join b in db.Buildings.ToList() on e.BuildingID equals b.BuildingID
                     join v in db.Venues.ToList() on e.VenueID equals v.VenueID
                     join p in db.Projects.ToList() on e.ProjectID equals p.ProjectID
                     join u in db.Users.ToList() on e.UserID equals u.UserID
                     select new
                     {
                         People = e.NumberofPeople,
                         DateBooked = e.DateBooked.ToString("MM/dd/yyyy"),
                         StartTime = e.StartTime,
                         EndTime = e.EndTime,
                         UserName = u.FirstName + " " + u.LastName,
                         Venue = v.Name,
                         Building = b.Name,
                         ProjectName = p.ProjectName
                     })
                     .ToList();

                var myResult = new JsonResult();
                myResult = new JsonResult { Data = newData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                db.Configuration.LazyLoadingEnabled = false;
                return Json(newData, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public JsonResult GetScheduleEvents()
        {
            using (CodeFirst.CodeFirst db = new CodeFirst.CodeFirst())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var events = db.Venue_Schedule_Line.ToList();

                var newData =
                    (from e in events
                     select new
                     {
                         start = e.StartTime,
                         end = e.EndTime,
                         dow = e.DateAvailable == "Sunday" ? 0 :
                         e.DateAvailable == "Monday" ? 1 :
                         e.DateAvailable == "Tuesday" ? 2 :
                         e.DateAvailable == "Wednesday" ? 3 :
                         e.DateAvailable == "Thursday" ? 4 :
                         e.DateAvailable == "Friday" ? 5 :
                         e.DateAvailable == "Saturday" ? 6 : -1
                     })
                     .ToList();

                var myResult = new JsonResult();
                myResult = new JsonResult { Data = newData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                db.Configuration.LazyLoadingEnabled = false;
                return Json(newData, JsonRequestBehavior.AllowGet);
            }
        }
    }
}