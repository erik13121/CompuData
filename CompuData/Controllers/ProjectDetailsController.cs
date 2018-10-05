using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ProjectDetailsController : Controller
    {
        // GET: ProjectDetails
        public ActionResult Index(string projectID)
        {
            Models.Project myModel = new Models.Project();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (projectID != null)
            {
                var intProID = Int32.Parse(projectID);
                var myProject = db.Projects.Where(i => i.ProjectID == intProID).FirstOrDefault();
                var mytypeID = db.Project_Type.Where(i => i.TypeID == myProject.TypeID).FirstOrDefault();
                var myUserID = db.Users.Where(i => i.UserID == myProject.UserID).FirstOrDefault();

                myModel.ProjectID = myProject.ProjectID;
                myModel.ProjectName = myProject.ProjectName;
                myModel.StartDate = myProject.StartDate;
                myModel.ExpectedFinishDate = myProject.ExpectedFinishDate;
                myModel.Finished = myProject.Finished;
                myModel.ProjectDescription = myProject.ProjectDescription;            
                myModel.TypeID = mytypeID.TypeID;
                myModel.UserID = myUserID.UserID;
                myModel.TypeName = db.Project_Type.Where(i => i.TypeID == mytypeID.TypeID).FirstOrDefault().TypeName;
                myModel.Initials = db.Users.Where(i => i.UserID == myUserID.UserID).FirstOrDefault().Initials;
                myModel.LastName = db.Users.Where(i => i.UserID == myUserID.UserID).FirstOrDefault().LastName;
            }
            
            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToProjectDetails(string projectID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ProjectDetails", new { projectID = projectID });
            return Json(new { Url = redirectUrl });
        }

    }
}