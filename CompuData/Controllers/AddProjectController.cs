using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddProjectController : Controller
    {
        // GET: AddProject
        public ActionResult Index()
        {
            var db = new CodeFirst.CodeFirst();
            var Project = new Models.Project();
            return View(Project);
        }

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "")]Models.Project model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                if (db.Projects.Count() > 0)
                {
                    var item = db.Projects.OrderByDescending(a => a.ProjectID).FirstOrDefault();

                    db.Projects.Add(new CodeFirst.Project
                    {
                        ProjectID = model.ProjectID + 1,
                        ProjectName = model.ProjectName,
                        StartDate = model.StartDate,
                        ExpectedFinishDate = model.ExpectedFinishDate,
                        Finished = model.Finished,                        
                        ProjectDescription = model.ProjectDescription,                       
                        TypeID = model.TypeID,
                        UserID = model.UserID,
                    });
                }
                else
                {
                    db.Projects.Add(new CodeFirst.Project
                    {
                        ProjectID = 1,
                        ProjectName = model.ProjectName,
                        StartDate = model.StartDate,
                        ExpectedFinishDate = model.ExpectedFinishDate,
                        Finished = model.Finished,
                        ProjectDescription = model.ProjectDescription,
                        TypeID = model.TypeID,
                        UserID = model.UserID,
                    });
                }

                db.SaveChanges();
                model.JavaScriptToRun = "mySuccess()";
                TempData["model"] = model;
                return RedirectToAction("Index", "Project");
            }

            return View("Index", model);
        }
    }
}