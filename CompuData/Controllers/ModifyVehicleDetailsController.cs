using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ModifyVehicleDetailsController : Controller
    {
        // GET: ModifyVehicleDetails
        public ActionResult Index(string vehicleID)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            Models.Vehicle myModel = new Models.Vehicle();
            if (vehicleID != null)
            {
                var intVehicleID = Int32.Parse(vehicleID);
                var myVehicle = db.Vehicles.Where(i => i.VehicleID == intVehicleID).FirstOrDefault();
                var myTypeID = db.Vehicle_Type.Where(i => i.TypeID == myVehicle.TypeID).FirstOrDefault();

                myModel.VehicleID = myVehicle.VehicleID;
                myModel.Brand = myVehicle.Brand;
                myModel.Model = myVehicle.Model;
                myModel.NumberPlate = myVehicle.NumberPlate;
                myModel.DateofPurchase = myVehicle.DateOfPurchase;
                myModel.DateofLastRepair = myVehicle.DateofLastRepair;
                myModel.DateofLicencePurchase = myVehicle.DateofLicencePurchase;
                myModel.LicenseExpireDate = myVehicle.LicenseExpireDate;
                myModel.ServiceIntervalInMonths = myVehicle.ServiceIntervalInMonths;
                myModel.ServiceIntervalInKMs = myVehicle.ServiceIntervalInKMs;
                myModel.TypeID = myTypeID.TypeID;

                myModel.VehicleTypes = db.Vehicle_Type.ToList();
                return View(myModel);
            }

            myModel.VehicleTypes = db.Vehicle_Type.ToList();
            return View(myModel);
        }

        [HttpPost]
        public ActionResult Index(Models.Vehicle model)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            
            if (ModelState.IsValid)
            {
                Models.Vehicle myModel = new Models.Vehicle();
                
                var myVehicle = db.Vehicles.Where(i => i.VehicleID == model.VehicleID).FirstOrDefault();
                var myTypeID = db.Vehicle_Type.Where(i => i.TypeID == myVehicle.TypeID).FirstOrDefault();

                myModel.VehicleID = myVehicle.VehicleID;
                myModel.Brand = myVehicle.Brand;
                myModel.Model = myVehicle.Model;
                myModel.NumberPlate = myVehicle.NumberPlate;
                myModel.DateofPurchase = myVehicle.DateOfPurchase;
                myModel.DateofLastRepair = myVehicle.DateofLastRepair;
                myModel.DateofLicencePurchase = myVehicle.DateofLicencePurchase;
                myModel.LicenseExpireDate = myVehicle.LicenseExpireDate;
                myModel.ServiceIntervalInMonths = myVehicle.ServiceIntervalInMonths;
                myModel.ServiceIntervalInKMs = myVehicle.ServiceIntervalInKMs;
                myModel.TypeID = myTypeID.TypeID;

                myModel.VehicleTypes = db.Vehicle_Type.ToList();
                return View(myModel);
            }

            model.VehicleTypes = db.Vehicle_Type.ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult RedirectToModifyVehicleDetails(string vehicleID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ModifyVehicleDetails", new { vehicleID = vehicleID });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult Modify([Bind(Prefix = "")]Models.Vehicle model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                var vehicle = db.Vehicles.Where(v => v.VehicleID == model.VehicleID).SingleOrDefault();

                if (vehicle != null)
                {
                    vehicle.Brand = model.Brand;
                    vehicle.Model = model.Model;
                    vehicle.NumberPlate = model.NumberPlate;
                    vehicle.DateOfPurchase = model.DateofPurchase;
                    vehicle.DateofLastRepair = model.DateofLastRepair;
                    vehicle.DateofLicencePurchase = model.DateofLicencePurchase;
                    vehicle.LicenseExpireDate = model.LicenseExpireDate;
                    vehicle.ServiceIntervalInMonths = model.ServiceIntervalInMonths;
                    vehicle.ServiceIntervalInKMs = model.ServiceIntervalInKMs;
                    vehicle.TypeID = model.TypeID;
                    db.SaveChanges();
                }

                TempData["js"] = "myUpdateSuccess()";
                return RedirectToAction("Index", "Vehicles");
            }
            model.VehicleTypes = db.Vehicle_Type.ToList();
            return View("Index", model);
        }
    }
}