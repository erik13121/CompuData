using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class VehicleDetailsController : Controller
    {
        // GET: VehicleDetails
        public ActionResult Index(string vehicleID)
        {
            Models.Vehicle myModel = new Models.Vehicle();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
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
                myModel.TypeName = db.Vehicle_Type.Where(i => i.TypeID == myTypeID.TypeID).FirstOrDefault().Name;
            }

            myModel.VehicleTypes = db.Vehicle_Type.ToList();
            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToVehicleDetails(string vehicleID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "VehicleDetails", new { vehicleID = vehicleID });
            return Json(new { Url = redirectUrl });
        }
    }
}