using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ProjectArchivedController : Controller
    {
        // GET: ProjectArchived
        public ActionResult Index()
        {

            Models.Project myModel = new Models.Project();
            if (TempData["model"] != null)
            {
                myModel = (Models.Project)TempData["model"];
                TempData.Remove("model");
            }
            return View(myModel);
        }

        public ActionResult PageData(IDataTablesRequest request)
        {
            // Nothing important here. Just creates some mock data.
            var data = Models.Project.GetData();

            var db = new CodeFirst.CodeFirst();
            var Users = db.Users.ToList();
            var ProjectType = db.Project_Type.ToList();
            var newData = (from d in data
                           join e in Users on d.UserID equals e.UserID
                           join a in ProjectType on d.TypeID equals a.TypeID
                           where d.Finished == true
                           select new
                           {
                               ProjectID = d.ProjectID,
                               ProjectName = d.ProjectName,
                               StartDate = d.StartDate.ToString("dd-MM-yyyy"),
                               ExpectedFinishDate = d.ExpectedFinishDate.ToString("dd-MM-yyyy"),
                               Finished = d.Finished,
                               ProjectDescription = d.ProjectDescription,
                               TypeName = a.TypeName,
                               UserInitials = e.Initials,
                               UserLastName = e.LastName,
                           }).ToList();

            // Global filtering.
            // Filter is being manually applied due to in-memmory (IEnumerable) data.
            // If you want something rather easier, check IEnumerableExtensions Sample.
            var filteredData = newData.Where(_item =>
            _item.ProjectID.ToString().Contains(request.Search.Value) ||
            _item.ProjectName.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            (_item.StartDate != null ? _item.StartDate.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) : false) ||
            (_item.ExpectedFinishDate != null ? _item.ExpectedFinishDate.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) : false) ||
            _item.Finished.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.ProjectDescription.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.TypeName.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.UserInitials.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.UserLastName.ToUpper().Contains(request.Search.Value.ToUpper())
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
        public ActionResult Delete(string ProjectID)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();
                var intProID = int.Parse(ProjectID);
                var Projects = db.Projects.Where(v => v.ProjectID == intProID).FirstOrDefault();
                db.Projects.Remove(Projects);
                db.SaveChanges();

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "Project");
                return Json(new { Url = redirectUrl });
            }
            catch
            {
                return Json(new { Url = "Cascading error!" });
            }
        }

        [HttpPost]
        public void SetTempData()
        {
            TempData["js"] = "";
        }
    }
}