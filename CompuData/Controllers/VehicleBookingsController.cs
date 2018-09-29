using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

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
                db.Configuration.LazyLoadingEnabled = false;
                var events = db.Vehicle_Booking_Line.ToList();
                var newData =
                    (from e in events
                     join i in db.Services.ToList() on e.IntervalID equals i.IntervalID into interval
                     from inter in interval.DefaultIfEmpty()
                     join p in db.Projects.ToList() on e.ProjectID equals p.ProjectID
                     join u in db.Users.ToList() on e.UserID equals u.UserID
                     join v in db.Vehicles.ToList() on e.VehicleID equals v.VehicleID
                     select new
                     {
                         Brand = v.Brand,
                         Model = v.Model,
                         NumberPlate = v.NumberPlate,
                         Reason = e.Reason,
                         ProjectName = p.ProjectName,
                         OdoEnd = e.OdoEnd != null ? e.OdoEnd : 0,
                         StartTime = e.StartTime,
                         EndTime = e.EndTime,
                         VehicleBookingID = e.VehicleBookingID,
                         IntervalID = inter != null ? inter.IntervalID : 0,
                         UserName = u.FirstName + " " + u.LastName,
                         DateBooked = e.DateBooked
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