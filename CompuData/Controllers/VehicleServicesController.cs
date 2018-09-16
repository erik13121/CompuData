using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompuData.Models;
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;

namespace CompuData.Controllers
{
    public class VehicleServicesController : Controller
    {
        // GET: VehicleServices
        public ActionResult Index()
        {
            VehicleSchedule myModel = new VehicleSchedule();
            if (TempData["model"] != null)
            {
                myModel = (VehicleSchedule)TempData["model"];
                TempData.Remove("model");
            }
            return View(myModel);
        }

        public ActionResult PageData(IDataTablesRequest request)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            
            // Nothing important here. Just creates some mock data.
            var data = VehicleService.GetData();

            //This code is for joining if we want to show information of the Vehicle instead of the ID
            var vehicles = db.Vehicles.ToList();
            var newData = (from s in data
                           join v in vehicles on s.VehicleID equals v.VehicleID
                           select new
                           {
                               IntervalID = s.IntervalID,
                               ServiceDate = s.ServiceDate.ToString("dd-MM-yyyy"),
                               Completed = s.Completed,
                               Brand = v.Brand,
                               Model = v.Model,
                               NumberPlate = v.NumberPlate
                           }).ToList();

             // Global filtering.
             // Filter is being manually applied due to in-memmory (IEnumerable) data.
             // If you want something rather easier, check IEnumerableExtensions Sample.
             var filteredData = newData.Where(_item =>
            _item.IntervalID.ToString().Contains(request.Search.Value) ||
            _item.ServiceDate.ToString().Contains(request.Search.Value.ToUpper()) ||
            _item.Completed.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.Brand.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.Model.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.NumberPlate.ToUpper().Contains(request.Search.Value.ToUpper())
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
        public ActionResult Delete(string intervalID)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();
                var intIntervalID = int.Parse(intervalID);
                var service = db.Services.Where(s => s.IntervalID == intIntervalID).FirstOrDefault();
                db.Services.Remove(service);
                db.SaveChanges();

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "VehicleServices");
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