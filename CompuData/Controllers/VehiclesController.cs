using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompuData.Global;
using CompuData.Models;
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;

namespace CompuData.Controllers
{
    public class VehiclesController : Controller
    {
        // GET: Vehicles
        public ActionResult Index()
        {
            Models.Vehicle myModel = new Models.Vehicle();
            if (TempData["model"] != null)
            {
                myModel = (Models.Vehicle)TempData["model"];
                TempData.Remove("model");
            }
            return View(myModel);
        }

        public ActionResult PageData(IDataTablesRequest request)
        {
            // Nothing important here. Just creates some mock data.
            var data = Models.Vehicle.GetData();

            var db = new CodeFirst.CodeFirst();
            var vehicleTypes = db.Vehicle_Type.ToList();
            var newData = (from ta in data
                           join ti in vehicleTypes on ta.TypeID equals ti.TypeID
                           select new
                           {
                               VehicleID = ta.VehicleID,
                               Brand = ta.Brand,
                               Model = ta.Model,
                               NumberPlate = ta.NumberPlate,
                               DateofPurchase = ta.DateOfPurchase.Value.ToString("dd-MM-yyyy"),
                               DateofLastRepair = ta.DateofLastRepair.Value.ToString("dd-MM-yyyy"),
                               DateofLicencePurchase = ta.DateofLicencePurchase.Value.ToString("dd-MM-yyyy"),
                               LicenseExpireDate = ta.LicenseExpireDate.Value.ToString("dd-MM-yyyy"),
                               ServiceIntervalInMonths = ta.ServiceIntervalInMonths,
                               ServiceIntervalInKMs = ta.ServiceIntervalInKMs,
                               TypeName = ti.Name
                           }).ToList();

            // Global filtering.
            // Filter is being manually applied due to in-memmory (IEnumerable) data.
            // If you want something rather easier, check IEnumerableExtensions Sample.
            var filteredData = newData.Where(_item => 
            _item.VehicleID.ToString().Contains(request.Search.Value) ||
            _item.Brand.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.Model.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.NumberPlate.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            (_item.DateofPurchase != null ? _item.DateofPurchase.ToString().Contains(request.Search.Value) : false) ||
            (_item.DateofLastRepair != null ? _item.DateofLastRepair.ToString().Contains(request.Search.Value) : false) ||
            (_item.DateofLicencePurchase != null ? _item.DateofLicencePurchase.ToString().Contains(request.Search.Value) : false) ||
            (_item.LicenseExpireDate != null ? _item.LicenseExpireDate.ToString().Contains(request.Search.Value) : false) ||
            _item.ServiceIntervalInMonths.ToString().Contains(request.Search.Value) ||
            _item.ServiceIntervalInKMs.ToString().Contains(request.Search.Value) ||
            _item.TypeName.ToUpper().Contains(request.Search.Value.ToUpper())
            );

            // Paging filtered data.
            // Paging is rather manual due to in-memmory (IEnumerable) data.
            var dataPage = filteredData.Skip(request.Start).Take(request.Length);

            // Response creation. To create your response you need to reference your request, to avoid
            // request/response tampering and to ensure response will be correctly created.
            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), dataPage);

            // Easier way is to return a new 'DataTablesJsonResult', which will automatically convert your
            // response to a json-compatible content, so DataTables can read it when received.
            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(string vehicleID)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();
                var intVehicleID = int.Parse(vehicleID);
                var vehicle = db.Vehicles.Where(v => v.VehicleID == intVehicleID).FirstOrDefault();
                db.Vehicles.Remove(vehicle);
                db.SaveChanges();

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Vehicles");
                return Json(new { Url = redirectUrl });
            }
            catch
            {
                return Json(new { Url = "Cascading error!" });
            }
        }

        [HttpPost]
        public void SetTempData()
        {
            TempData["js"] = "";
        }
    }
}