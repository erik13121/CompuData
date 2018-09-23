using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ModifyProjectController : Controller
    {
        // GET: ModifyProject
        public ActionResult Index(string projectID)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (projectID != null)
            {
                Models.Project myModel = new Models.Project();


                var intProID = Int32.Parse(projectID);
                var myProject = db.Projects.Where(i => i.ProjectID == intProID).FirstOrDefault();

                myModel.ProjectID = myProject.ProjectID;
                myModel.ProjectName = myProject.ProjectName;
                myModel.StartDate = myProject.StartDate;
                myModel.Finished = myProject.Finished;
                myModel.ExpectedFinishDate = myProject.ExpectedFinishDate;
                myModel.ProjectDescription = myProject.ProjectDescription;               
                myModel.TypeID = myProject.TypeID;
                myModel.UserID = myProject.UserID;

                return View(myModel);
            }

            Models.Project model = new Models.Project();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(Models.Project model)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                Models.Project myModel = new Models.Project();
                var myProject = db.Projects.Where(i => i.ProjectID == model.ProjectID).FirstOrDefault();

                myModel.ProjectID = myProject.ProjectID;
                myModel.ProjectName = myProject.ProjectName;
                myModel.StartDate = myProject.StartDate;
                myModel.Finished = myProject.Finished;
                myModel.ExpectedFinishDate = myProject.ExpectedFinishDate;
                myModel.ProjectDescription = myProject.ProjectDescription;
                myModel.TypeID = myProject.TypeID;
                myModel.UserID = myProject.UserID;

                return View(myModel);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult RedirectToModifyProjectDetails(string projectID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ModifyProject", new { projectID = projectID });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult Modify([Bind(Prefix = "")]Models.Project model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                var myProject = db.Projects.Where(v => v.ProjectID == model.ProjectID).SingleOrDefault();

                if (myProject != null)
                {
                    myProject.ProjectID = model.ProjectID;
                    myProject.ProjectName = model.ProjectName;
                    myProject.StartDate = model.StartDate;
                    myProject.Finished = model.Finished;
                    myProject.ExpectedFinishDate = model.ExpectedFinishDate;
                    myProject.ProjectDescription = model.ProjectDescription;
                    myProject.TypeID = model.TypeID;
                    myProject.UserID = model.UserID;
                    db.SaveChanges();
                }

                TempData["js"] = "myUpdateSuccess()";
                return RedirectToAction("Index", "Project");
            }

            return View("Index", model);
        }
    }
}