using CompuData.CodeFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddEquipmentController : Controller
    {
        // GET: AddEquipment
        public ActionResult Index()
        {
            var db = new CodeFirst.CodeFirst();
            var equipments = new Models.Equipment();
            equipments.EquipmentTypes = db.Equipment_Type.ToList();
            return View(equipments);
        }

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "")]Models.Equipment model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                if (db.Equipments.Count() > 0)
                {
                    var item = db.Equipments.OrderByDescending(a => a.EquipmentID).FirstOrDefault();

                    db.Equipments.Add(new CodeFirst.Equipment
                    {
                        EquipmentID = item.EquipmentID + 1,
                        ManufacturerName = model.ManufacturerName,
                        ModelNumber = model.ModelNumber,
                        DatePurchased = model.DatePurchased,
                        ServiceIntervalMonths = model.ServiceIntervalMonths,
                        Status = model.Status,
                        UserID = model.UserID,
                        TypeID = model.TypeID
                    });
                }
                else
                {
                    db.Equipments.Add(new CodeFirst.Equipment
                    {
                        EquipmentID = 1,
                        ManufacturerName = model.ManufacturerName,
                        ModelNumber = model.ModelNumber,
                        DatePurchased = model.DatePurchased,
                        ServiceIntervalMonths = model.ServiceIntervalMonths,
                        Status = model.Status,
                        UserID = model.UserID,
                        TypeID = model.TypeID
                    });
                }

                db.SaveChanges();
                model.JavaScriptToRun = "mySuccess()";
                TempData["model"] = model;
                return RedirectToAction("Index", "Equipments");
            }

            model.EquipmentTypes = db.Equipment_Type.ToList();
            return View("Index", model);
        }
    }
}