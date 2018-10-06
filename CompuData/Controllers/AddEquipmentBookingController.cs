using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddEquipmentBookingController : Controller
    {
        static int globalEquipmentID;
        // GET: AddEquipmentBooking
        public ActionResult Index(string equipmentID)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();

            globalEquipmentID = Int32.Parse(equipmentID);

            var equipment = db.Equipments.Where(e => e.EquipmentID == globalEquipmentID).FirstOrDefault();

            Session["type"] = equipment.Equipment_Type.TypeName;
            Session["manufacturer"] = equipment.ManufacturerName;
            Session["model"] = equipment.ModelNumber;

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
        public ActionResult RedirectToAddEquipmentBooking(string equipmentID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "AddEquipmentBooking", new { equipmentID = equipmentID });
            return Json(new { Url = redirectUrl });
        }

        [HttpGet]
        public JsonResult GetEvents()
        {
            using (CodeFirst.CodeFirst db = new CodeFirst.CodeFirst())
            {
                db.Configuration.LazyLoadingEnabled = false;
                var equipTypes = db.Equipment_Type.ToList();
                var events = db.Equipment_Booking_Line.ToList();
                var newData =
                    (from e in events
                     join p in db.Projects.ToList() on e.ProjectID equals p.ProjectID
                     join u in db.Users.ToList() on e.UserID equals u.UserID
                     join eq in db.Equipments.ToList() on e.EquipmentID equals eq.EquipmentID
                     select new
                     {
                         BookingID = e.BookingID,
                         EquipmentBookingID = e.EquipmentBookingID,
                         DateBooked = e.DateBooked.ToString("MM/dd/yyyy"),
                         StartTime = e.TimeIn,
                         EndTime = e.TimeOut,
                         PagesPrinted = e.PagesPrinted,
                         Manufacturer = eq.ManufacturerName,
                         Model = eq.ModelNumber,
                         EquipType = equipTypes.Where(e => e.TypeID == eq.TypeID).FirstOrDefault().TypeName,
                         ProjectName = p.ProjectName,
                         UserName = u.FirstName + " " + u.LastName,
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
                var events = db.Equipment_Schedule_Line
                    .Where(v => v.EquipmentID == globalEquipmentID)
                    .ToList();

                var newData =
                    (from e in events
                     select new
                     {
                         start = e.TimeStart,
                         end = e.TimeEnd,
                         dow = e.Date == "Sunday" ? 0 :
                         e.Date == "Monday" ? 1 :
                         e.Date == "Tuesday" ? 2 :
                         e.Date == "Wednesday" ? 3 :
                         e.Date == "Thursday" ? 4 :
                         e.Date == "Friday" ? 5 :
                         e.Date == "Saturday" ? 6 : -1
                     })
                     .ToList();

                var myResult = new JsonResult();
                myResult = new JsonResult { Data = newData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                db.Configuration.LazyLoadingEnabled = false;
                return Json(newData, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Add(Models.EquipmentBooking booking)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();

                db.Equipment_Booking.Add(new CodeFirst.Equipment_Booking
                {
                    EquipmentBookingID = booking.BookingID
                });

                db.Equipment_Booking_Line.Add(new CodeFirst.Equipment_Booking_Line
                {
                    BookingID = booking.BookingID,
                    EquipmentBookingID = booking.BookingID,
                    EquipmentID = globalEquipmentID,
                    PagesPrinted = booking.PagesPrinted,
                    DateBooked = DateTime.ParseExact(booking.DateBooked, "MM/dd/yyyy", CultureInfo.InvariantCulture),
                    TimeIn = booking.TimeIn,
                    TimeOut = booking.TimeOut,
                    ProjectID = booking.ProjectID,
                    UserID = booking.UserID
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
        public ActionResult Adjusted(Models.EquipmentBooking booking)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();

                var adjBooking = db.Equipment_Booking_Line.Where(v => v.BookingID == booking.BookingID).FirstOrDefault();
                adjBooking.PagesPrinted = booking.PagesPrinted;

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
        public ActionResult Edit(Models.EquipmentBooking booking)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();

                var adjBooking = db.Equipment_Booking_Line.Where(v => v.BookingID == booking.BookingID).FirstOrDefault();
                adjBooking.TimeOut = booking.TimeOut;
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
        public ActionResult RemovePages(string bookingID)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();

                var intBookingID = Int32.Parse(bookingID);
                var adjBooking = db.Equipment_Booking_Line.Where(v => v.BookingID == intBookingID).FirstOrDefault();
                adjBooking.PagesPrinted = 0;

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
                var theBookingID = db.Equipment_Booking.Where(t => t.EquipmentBookingID == intBookingID).FirstOrDefault();
                var booking = db.Equipment_Booking_Line.Where(t => t.BookingID == intBookingID).FirstOrDefault();
                db.Equipment_Booking_Line.Remove(booking);
                db.Equipment_Booking.Remove(theBookingID);
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