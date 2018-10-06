using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddVenueBookingController : Controller
    {
        static int globalVenueID;
        // GET: AddVenueBooking
        public ActionResult Index(string venueID)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();

            globalVenueID = Int32.Parse(venueID);
            ViewBag.Users = db.Users
                    .AsEnumerable()
                    .Select(u => new SelectListItem
                    {
                        Value = u.UserID.ToString(),
                        Text = u.Initials + " " + u.LastName
                    }).ToList();

            ViewBag.Projects = db.Projects
                .AsEnumerable()
                .Select(p => new SelectListItem
                {
                    Value = p.ProjectID.ToString(),
                    Text = p.ProjectName
                }).ToList();

            return View();
        }

        [HttpPost]
        public ActionResult RedirectToAddVenueBooking(string venueID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "AddVenueBooking", new { venueID = venueID });
            return Json(new { Url = redirectUrl });
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
                         BookingID = e.BookingID,
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
                var events = db.Venue_Schedule_Line
                    .Where(v => v.VenueID == globalVenueID)
                    .ToList();

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

        [HttpPost]
        public ActionResult Add(Models.VenueBooking booking)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();

                db.Venue_Booking.Add(new CodeFirst.Venue_Booking
                {
                    VenueBookingID = booking.VenueBookingID
                });

                db.Venue_Booking_Line.Add(new CodeFirst.Venue_Booking_Line
                {
                    BookingID = booking.BookingID,
                    VenueBookingID = booking.BookingID,
                    VenueID = globalVenueID,
                    NumberofPeople = booking.NumberofPeople,
                    DateBooked = DateTime.ParseExact(booking.DateBooked, "MM/dd/yyyy", CultureInfo.InvariantCulture),
                    StartTime = booking.StartTime,
                    EndTime = booking.EndTime,
                    UserID = booking.UserID,
                    BuildingID = db.Venues.Where(v => v.VenueID == globalVenueID).FirstOrDefault().BuildingID,
                    ProjectID = booking.ProjectID
                });
                db.SaveChanges();

                return Json(new
                {
                    ResultValue = "Success"
                });
            }
            catch
            {
                return Json(new
                {
                    ResultValue = "Error"
                });
            }
        }

        [HttpPost]
        public ActionResult Edit(Models.VenueBooking booking)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();

                var adjBooking = db.Venue_Booking_Line.Where(v => v.BookingID == booking.BookingID).FirstOrDefault();
                adjBooking.EndTime = booking.EndTime;
                adjBooking.UserID = booking.UserID;
                adjBooking.ProjectID = booking.ProjectID;

                db.SaveChanges();

                return Json(new
                {
                    ResultValue = "Success"
                });
            }
            catch
            {
                return Json(new
                {
                    ResultValue = "Error"
                });
            }
        }

        [HttpPost]
        public ActionResult Remove(string bookingID)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();
                var intBookingID = int.Parse(bookingID);
                var theBookingID = db.Venue_Booking.Where(t => t.VenueBookingID == intBookingID).FirstOrDefault();
                var booking = db.Venue_Booking_Line.Where(t => t.BookingID == intBookingID).FirstOrDefault();
                db.Venue_Booking_Line.Remove(booking);
                db.Venue_Booking.Remove(theBookingID);
                db.SaveChanges();

                return Json(new
                {
                    ResultValue = "Success"
                });
            }
            catch
            {
                return Json(new
                {
                    ResultValue = "Fail"
                });
            }
        }
    }
}