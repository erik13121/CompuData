using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompuData.CodeFirst;
using CompuData.Models;

namespace CompuData.Controllers
{
    public class AddVehicleServiceController : Controller
    {
        // GET: AddVehicleService
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Index(string vehicleID)
        {
            VehicleService myService = new VehicleService();
            if (vehicleID != null)
            {
                myService.VehicleID = int.Parse(vehicleID);
            }

            return View(myService);            
        }

        [HttpPost]
        public ActionResult RedirectToAddVehicleServiceDetails(string vehicleID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "AddVehicleService", new { vehicleID = vehicleID });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "")]Models.VehicleService model)
        {
            if (ModelState.IsValid)
            {
                var db = new CodeFirst.CodeFirst();
                if (db.Services.Count() > 0)
                {
                    var item = db.Services.OrderByDescending(s => s.IntervalID).FirstOrDefault();

                    db.Services.Add(new Service
                    {
                        IntervalID = item.IntervalID + 1,
                        ServiceDate = model.ServiceDate,
                        Completed = model.Completed,
                        VehicleID = model.VehicleID
                    });
                }
                else
                {
                    db.Services.Add(new Service
                    {
                        IntervalID = 1,
                        ServiceDate = model.ServiceDate,
                        Completed = model.Completed,
                        VehicleID = model.VehicleID
                    });
                }
                
                db.SaveChanges();
                model.JavaScriptToRun = "mySuccess()";
                TempData["model"] = model;
                return RedirectToAction("Index", "VehicleServices");
            }

            return View("Index", model);
        }
    }
}