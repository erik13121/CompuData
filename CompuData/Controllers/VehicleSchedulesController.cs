using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompuData.Models;
using DataTables.AspNet.Mvc5;
using DataTables.AspNet.Core;

namespace CompuData.Controllers
{
    public class VehicleSchedulesController : Controller
    {
        // GET: VehicleSchedules
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
            var data = VehicleSchedule.GetData();

            //This code is for joining if we want to show information of the Vehicle instead of the ID
            var vehicles = db.Vehicles.ToList();
            var newData = (from s in data
                           join v in vehicles on s.VehicleID equals v.VehicleID
                           select new
                           {
                               ScheduleID = s.Veh_Schedule_ID,
                               Date = s.Date,
                               StartTime = s.StartTime,
                               EndTime = s.EndTime,
                               Status = s.Status,
                               Brand = v.Brand,
                               Model = v.Model,
                               NumberPlate = v.NumberPlate
                           }).ToList();

            // Global filtering.
            // Filter is being manually applied due to in-memmory (IEnumerable) data.
            // If you want something rather easier, check IEnumerableExtensions Sample.
            var filteredData = newData.Where(_item =>
           _item.ScheduleID.ToString().Contains(request.Search.Value) ||
           _item.Date.ToUpper().Contains(request.Search.Value.ToUpper()) ||
           _item.StartTime.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) ||
           _item.EndTime.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) ||
           _item.Status.ToUpper().Contains(request.Search.Value.ToUpper()) ||
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
        public ActionResult Delete(string scheduleID)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();
                var intScheduleID = int.Parse(scheduleID);
                var schedule = db.Vehicle_Schedule_Line.Where(s => s.Veh_Schedule_ID == intScheduleID).FirstOrDefault();
                db.Vehicle_Schedule_Line.Remove(schedule);
                db.SaveChanges();

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "VehicleSchedules");
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