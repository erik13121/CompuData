﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddEquipmentTypeController : Controller
    {
        // GET: AddEquipmentType
        public ActionResult Index()
        {
            var db = new CodeFirst.CodeFirst();
            var EquipType = new Models.EquipmentType();
            return View(EquipType);
        }

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "")]Models.EquipmentType model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                if (db.Equipment_Type.Count() > 0)
                {
                    var item = db.Equipment_Type.OrderByDescending(a => a.TypeID).FirstOrDefault();

                    db.Equipment_Type.Add(new CodeFirst.Equipment_Type
                    {
                        TypeID = item.TypeID + 1,
                        TypeName = model.TypeName,
                        TypeDescription = model.TypeDescription,
                        Removable = model.Removable,
                    });
                }
                else
                {
                    db.Equipment_Type.Add(new CodeFirst.Equipment_Type
                    {
                        TypeID = 1,
                        TypeName = model.TypeName,
                        TypeDescription = model.TypeDescription,
                        Removable = model.Removable,
                    });
                }

                db.SaveChanges();
                model.JavaScriptToRun = "mySuccess()";
                TempData["model"] = model;
                return RedirectToAction("Index", "EquipmentType");
            }

            return View("Index", model);
        }
    }
}