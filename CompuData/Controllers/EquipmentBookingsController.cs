using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class EquipmentBookingsController : Controller
    {
        // GET: EquipmentBookings
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
                var events = db.Equipment_Schedule_Line.ToList();

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
    }
}