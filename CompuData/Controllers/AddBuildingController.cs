using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddBuildingController : Controller
    {
        // GET: AddBuilding
        public ActionResult Index()
        {
            var db = new CodeFirst.CodeFirst();
            var building = new Models.Building();
            return View(building);
        }

        /*public ActionResult FromAddEquipmentScreen()
        {
            //equipmentModelToPassBack = equipmentModel;
            var db = new CodeFirst.CodeFirst();
            var type = new Models.EquipmentType();
            ViewBag.Referrer = "AddEquipment";
            return View("Index", type);
        }*/

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "")]Models.Building model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                if (db.Buildings.Count() > 0)
                {
                    var item = db.Buildings.OrderByDescending(a => a.BuildingID).FirstOrDefault();

                    db.Buildings.Add(new CodeFirst.Building
                    {
                        BuildingID = item.BuildingID + 1,
                        Name = model.Name,
                        StreetAddress = model.StreetAddress,
                        City = model.City,
                        AreaCode = model.AreaCode
                    });
                }
                else
                {
                    db.Buildings.Add(new CodeFirst.Building
                    {
                        BuildingID = 1,
                        Name = model.Name,
                        StreetAddress = model.StreetAddress,
                        City = model.City,
                        AreaCode = model.AreaCode
                    });
                }

                db.SaveChanges();

                /*if (Request.Form["Referrer"] == "AddEquipment")
                {
                    //TempData["EquipmentModel"] = equipmentModelToPassBack;
                    return RedirectToAction("Index", "AddEquipment");
                }
                else
                {
                    model.JavaScriptToRun = "mySuccess()";
                    TempData["model"] = model;
                    return RedirectToAction("Index", "EquipmentType");
                }*/

                model.JavaScriptToRun = "mySuccess()";
                TempData["model"] = model;
                return RedirectToAction("Index", "Buildings");
            }

            return View("Index", model);
        }
    }
}