using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class JobTitleModifyController : Controller
    {
        // GET: JobTitleModify
        public ActionResult Index(string titleID)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            Models.JobTitle myModel = new Models.JobTitle();
            if (titleID != null)
            {
                var intTitleID = Int32.Parse(titleID);
                var myType = db.Employee_Title.Where(i => i.JobTitleID == intTitleID).FirstOrDefault();

                myModel.JobTitleID = myType.JobTitleID;
                myModel.TitleName = myType.TitleName;

                db.SaveChanges();
            }
            
            return View(myModel);
        }

        [HttpPost]
        public ActionResult Index(Models.JobTitle model)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (model.JobTitleID != 0)
            {                
                var myType = db.Employee_Title.Where(i => i.JobTitleID == model.JobTitleID).FirstOrDefault();

                model.JobTitleID = myType.JobTitleID;
                model.TitleName = myType.TitleName;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Update([Bind(Prefix = "")]Models.JobTitle model)
        {
            if (ModelState.IsValid)
            {
                var db = new CodeFirst.CodeFirst();
                var title = db.Employee_Title.Where(v => v.JobTitleID == model.JobTitleID).SingleOrDefault();

                if (title != null)
                {
                    title.JobTitleID = model.JobTitleID;
                    title.TitleName = model.TitleName;
                    db.SaveChanges();
                }

                TempData["js"] = "myUpdateSuccess()";
                return RedirectToAction("Index", "JobTitle");
            }

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult RedirectToModifyJobTitleDetails(string titleID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "JobTitleModify", new { titleID = titleID });
            return Json(new { Url = redirectUrl });
        }
    }
}