using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ProjectTypeDetailsController : Controller
    {
        // GET: ProjectTypeDetails
        public ActionResult Index(string typeID)
        {
            Models.ProjectType myModel = new Models.ProjectType();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (typeID != null)
            {
                var intTypeID = Int32.Parse(typeID);
                var myType = db.Project_Type.Where(i => i.TypeID == intTypeID).FirstOrDefault();

                myModel.TypeID = myType.TypeID;
                myModel.TypeName = myType.TypeName;
                myModel.TypeDescription = myType.TypeDescription;
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToProjectTypeDetails(string typeID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ProjectTypeDetails", new { typeID = typeID });
            return Json(new { Url = redirectUrl });
        }
    }
}