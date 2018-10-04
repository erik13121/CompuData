using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ProjectCompleteController : Controller
    {
        // GET: ProjectComplete
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
                myModel.Finished = myProject.Finished;

                return View(myModel);
            }

            Models.Project model = new Models.Project();
            return View(model);
        }

        [HttpPost]
        public ActionResult RedirectToCompleteProjectDetails(string projectID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ProjectComplete", new { projectID = projectID });
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
                    myProject.Finished = model.Finished;
                    db.SaveChanges();
                }

                TempData["js"] = "myUpdateSuccess()";
                return RedirectToAction("Index", "Project");
            }

            return View("Index", model);
        }

    }
}