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
    public class VehicleTypesController : Controller
    {
        // GET: VehicleTypes
        public ActionResult Index()
        {
            VehicleType myModel = new VehicleType();
            if (TempData["model"] != null)
            {
                myModel = (VehicleType)TempData["model"];
                TempData.Remove("model");
            }
            return View(myModel);
        }

        // GET: AddVehicle
        public ActionResult Add()
        {
            var types = new Models.VehicleType();
            return View(types);
        }

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "")]Models.VehicleType model)
        {
            if (ModelState.IsValid)
            {
                var db = new CodeFirst.CodeFirst();
                var item = db.Vehicle_Type.OrderByDescending(a => a.TypeID).FirstOrDefault();
                db.Vehicle_Type.Add(new CodeFirst.Vehicle_Type
                {
                    TypeID = item.TypeID + 1,
                    Name = model.Name,
                    Description = model.Description,
                });
                db.SaveChanges();
                model.JavaScriptToRun = "mySuccess()";
                TempData["model"] = model;
                return RedirectToAction("Index", "VehicleTypes");
            }

            return View("Add", model);
        }

        // GET: VehicleDetails
        public ActionResult Details(string typeID)
        {
            Models.VehicleType myModel = new Models.VehicleType();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (typeID != null)
            {
                var intTypeID = Int32.Parse(typeID);
                var myType = db.Vehicle_Type.Where(i => i.TypeID == intTypeID).FirstOrDefault();

                myModel.TypeID = myType.TypeID;
                myModel.Name = myType.Name;
                myModel.Description = myType.Description;
            }
            
            return View(myModel);
        }

        public ActionResult Modify(string typeID)
        {
            Models.VehicleType myModel = new Models.VehicleType();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (typeID != null)
            {
                var intTypeID = Int32.Parse(typeID);
                var myType = db.Vehicle_Type.Where(i => i.TypeID == intTypeID).FirstOrDefault();

                myModel.TypeID = myType.TypeID;
                myModel.Name = myType.Name;
                myModel.Description = myType.Description;
            }
            
            return View(myModel);
        }



        [HttpPost]
        public ActionResult Update([Bind(Prefix = "")]Models.VehicleType model)
        {
            if (ModelState.IsValid)
            {
                var db = new CodeFirst.CodeFirst();
                var vehicleType = db.Vehicle_Type.Where(v => v.TypeID == model.TypeID).SingleOrDefault();

                if (vehicleType != null)
                {
                    vehicleType.TypeID = model.TypeID;
                    vehicleType.Name = model.Name;
                    vehicleType.Description = model.Description;
                    db.SaveChanges();
                }

                TempData["js"] = "myUpdateSuccess()";
                return RedirectToAction("Index", "VehicleTypes");
            }

            return View("Modify", model);
        }

        [HttpPost]
        public ActionResult Delete(string typeID)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();
                var intTypeID = int.Parse(typeID);
                var type = db.Vehicle_Type.Where(t => t.TypeID == intTypeID).FirstOrDefault();
                db.Vehicle_Type.Remove(type);
                db.SaveChanges();

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "VehicleTypes");
                return Json(new { Url = redirectUrl });
            }
            catch
            {
                return Json(new { Url = "Cascading error!" });
            }
        }

        public ActionResult PageData(IDataTablesRequest request)
        {
            // Nothing important here. Just creates some mock data.
            var data = Models.VehicleType.GetData();

            // Global filtering.
            // Filter is being manually applied due to in-memmory (IEnumerable) data.
            // If you want something rather easier, check IEnumerableExtensions Sample.
            var filteredData = data.Where(_item =>
            _item.TypeID.ToString().Contains(request.Search.Value) ||
            _item.Name.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.Description.ToUpper().Contains(request.Search.Value.ToUpper())
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
        public void SetTempData()
        {
            TempData["js"] = "";
        }

        [HttpPost]
        public ActionResult RedirectToVehicleTypeDetails(string typeID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Details", "VehicleTypes", new { typeID = typeID });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult RedirectToModifyVehicleTypeDetails(string typeID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Modify", "VehicleTypes", new { typeID = typeID });
            return Json(new { Url = redirectUrl });
        }
    }
}