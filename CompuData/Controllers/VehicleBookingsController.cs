using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class VehicleBookingsController : Controller
    {
        // GET: VehicleBookings
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult GetEvents()
        {
            using (CodeFirst.CodeFirst db = new CodeFirst.CodeFirst())
            {
                var events = db.Vehicle_Booking_Line.ToList();
                var newData =
                    (from e in events
                     join p in db.Projects.ToList() on e.ProjectID equals p.ProjectID
                     join i in db.Services.ToList() on e.IntervalID equals i.IntervalID
                     join u in db.Users.ToList() on e.UserID equals u.UserID
                     join v in db.Vehicles.ToList() on e.VehicleID equals v.VehicleID
                     select new
                     {
                         Brand = v.Brand,
                         Model = v.Model,
                         NumberPlate = v.NumberPlate,
                         Reason = e.Reason,
                         ProjectName = p.ProjectName,
                         OdoEnd = e.OdoEnd,
                         StartTime = e.StartTime,
                         EndTime = e.EndTime,
                         VehicleBookingID = e.VehicleBookingID,
                         IntervalID = i.IntervalID,
                         UserName = u.FirstName + " " + u.LastName,
                         DateBooked = e.DateBooked
                     });

                var myResult = new JsonResult();
                myResult = new JsonResult { Data = newData, JsonRequestBehavior = JsonRequestBehavior.AllowGet };

                db.Configuration.LazyLoadingEnabled = false;
                return Json(newData, JsonRequestBehavior.AllowGet);
            }
        }
    }
}