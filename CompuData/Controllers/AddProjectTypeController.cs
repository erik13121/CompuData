using CompuData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddProjectTypeController : Controller
    {
        // GET: AddProjectType
        public ActionResult Index()
        {
            ProjectType types = new ProjectType();
            return View(types);
        }

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "")]Models.ProjectType model)
        {
            if (ModelState.IsValid)
            {
                var db = new CodeFirst.CodeFirst();
                if (db.Project_Type.Count() > 0)
                {
                    var item = db.Project_Type.OrderByDescending(a => a.TypeID).FirstOrDefault();

                    db.Project_Type.Add(new CodeFirst.Project_Type
                    {
                        TypeID = item.TypeID + 1,
                        TypeName = model.TypeName,
                        TypeDescription = model.TypeDescription
                    });
                }
                else
                {
                    db.Project_Type.Add(new CodeFirst.Project_Type
                    {
                        TypeID = 1,
                        TypeName = model.TypeName,
                        TypeDescription = model.TypeDescription
                    });
                }

                db.SaveChanges();
                model.JavaScriptToRun = "mySuccess()";
                TempData["model"] = model;
                return RedirectToAction("Index", "ProjectType");
            }

            return View("Index", model);
        }
    }
}