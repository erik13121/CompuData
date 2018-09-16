using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ModifyVehicleServiceDetailsController : Controller
    {
        // GET: ModifyVehicleServiceDetails
        public ActionResult Index(string intervalID)
        {
            Models.VehicleService myModel = new Models.VehicleService();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (intervalID != null)
            {
                var intIntervalID = Int32.Parse(intervalID);
                var myVehicle = db.Services.Where(i => i.IntervalID == intIntervalID).FirstOrDefault();

                myModel.IntervalID = myVehicle.IntervalID;
                myModel.ServiceDate = myVehicle.ServiceDate;
                myModel.Completed = myVehicle.Completed;
                myModel.VehicleID = myVehicle.VehicleID;
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult Index(Models.VehicleService model)
        {
            if (ModelState.IsValid)
            {
                CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
                
                var myVehicle = db.Services.Where(i => i.IntervalID == model.IntervalID).FirstOrDefault();

                model.IntervalID = myVehicle.IntervalID;
                model.ServiceDate = myVehicle.ServiceDate;
                model.Completed = myVehicle.Completed;
                model.VehicleID = myVehicle.VehicleID;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult RedirectToModifyVehicleServiceDetails(string intervalID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ModifyVehicleServiceDetails", new { intervalID = intervalID });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult Modify([Bind(Prefix = "")]Models.VehicleService model)
        {
            if (ModelState.IsValid)
            {
                var db = new CodeFirst.CodeFirst();
                var service = db.Services.Where(v => v.IntervalID == model.IntervalID).SingleOrDefault();

                if (service != null)
                {
                    service.IntervalID = model.IntervalID;
                    service.ServiceDate = model.ServiceDate;
                    service.Completed = model.Completed;
                    service.VehicleID = model.VehicleID;
                    db.SaveChanges();
                }

                TempData["js"] = "myUpdateSuccess()";
                return RedirectToAction("Index", "VehicleServices");
            }
            
            return View("Index", model);
        }
    }
}