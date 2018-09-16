using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class JobTitleDetailsController : Controller
    {
        // GET: VehicleDetails
        public ActionResult Index(string titleID)
        {
            Models.JobTitle myModel = new Models.JobTitle();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (titleID != null)
            {
                var intTitleID = Int32.Parse(titleID);
                var myType = db.Employee_Title.Where(i => i.JobTitleID == intTitleID).FirstOrDefault();

                myModel.JobTitleID = myType.JobTitleID;
                myModel.TitleName = myType.TitleName;
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToJobTitleDetails(string titleID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "JobTitleDetails", new { titleID = titleID });
            return Json(new { Url = redirectUrl });
        }
    }
}