using CompuData.CodeFirst;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddVehicleController : Controller
    {
        // GET: AddVehicle
        public ActionResult Index()
        {
            var db = new CodeFirst.CodeFirst();
            var vehicles = new Models.Vehicle();
            vehicles.VehicleTypes = db.Vehicle_Type.ToList();
            return View(vehicles);
        }

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "")]Models.Vehicle model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                if (db.Vehicles.Count() > 0)
                {
                    var item = db.Vehicles.OrderByDescending(a => a.VehicleID).FirstOrDefault();

                    db.Vehicles.Add(new Vehicle
                    {
                        VehicleID = item.VehicleID + 1,
                        Brand = model.Brand,
                        Model = model.Model,
                        NumberPlate = model.NumberPlate,
                        DateOfPurchase = model.DateofPurchase,
                        DateofLastRepair = model.DateofLastRepair,
                        DateofLicencePurchase = model.DateofLicencePurchase,
                        LicenseExpireDate = DateTime.ParseExact(model.LicenseExpireDate.Value.ToString("MM/dd/yyyy"), "MM/dd/yyyy", CultureInfo.InvariantCulture),
                        ServiceIntervalInMonths = model.ServiceIntervalInMonths,
                        ServiceIntervalInKMs = model.ServiceIntervalInKMs,
                        TypeID = model.TypeID
                    });
                }
                else
                {
                    db.Vehicles.Add(new Vehicle
                    {
                        VehicleID = 1,
                        Brand = model.Brand,
                        Model = model.Model,
                        NumberPlate = model.NumberPlate,
                        DateOfPurchase = model.DateofPurchase,
                        DateofLastRepair = model.DateofLastRepair,
                        DateofLicencePurchase = model.DateofLicencePurchase,
                        LicenseExpireDate = model.LicenseExpireDate,
                        ServiceIntervalInMonths = model.ServiceIntervalInMonths,
                        ServiceIntervalInKMs = model.ServiceIntervalInKMs,
                        TypeID = model.TypeID
                    });
                }
                
                db.SaveChanges();
                model.JavaScriptToRun = "mySuccess()";
                TempData["model"] = model;
                return RedirectToAction("Index", "Vehicles");
            }

            model.VehicleTypes = db.Vehicle_Type.ToList();
            return View("Index", model);
        }
    }
}