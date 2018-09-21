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
            Project.ProjectTypes = db.Project_Type.ToList();
            Project.Users = db.Users.AsEnumerable().Select(u => new SelectListItem{
                Value = u.UserID.ToString(),
                Text = u.Initials + " " + u.LastName
            }).ToList();
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
                        ProjectID = item.ProjectID + 1,
                        ProjectName = model.ProjectName,
                        StartDate = DateTime.Parse(model.StartDate.ToString("yyyy-MM-dd")),
                        ExpectedFinishDate = DateTime.Parse(model.ExpectedFinishDate.ToString("yyyy-MM-dd")),
                        Finished = false,                        
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
                        StartDate = DateTime.Parse(model.StartDate.ToString("yyyy-MM-dd")),
                        ExpectedFinishDate = DateTime.Parse(model.ExpectedFinishDate.ToString("yyyy-MM-dd")),
                        Finished = false,
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

            model.ProjectTypes = db.Project_Type.ToList();
            model.Users = db.Users.AsEnumerable().Select(u => new SelectListItem
            {
                Value = u.UserID.ToString(),
                Text = u.Initials + " " + u.LastName
            }).ToList();
            return View("Index", model);
        }
    }
}