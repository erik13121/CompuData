using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddJobTitleController : Controller
    {
        // GET: AddJobTitle
        public ActionResult Index()
        {
            var title = new Models.JobTitle();
            return View(title);
        }

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "")]Models.JobTitle model)
        {
            if (ModelState.IsValid)
            {
                var db = new CodeFirst.CodeFirst();
                if (db.Employee_Title.Count() > 0)
                {
                    var item = db.Employee_Title.OrderByDescending(a => a.JobTitleID).FirstOrDefault();

                    db.Employee_Title.Add(new CodeFirst.Employee_Title
                    {
                        JobTitleID = item.JobTitleID + 1,
                        TitleName = model.TitleName
                    });
                }
                else
                {
                    db.Employee_Title.Add(new CodeFirst.Employee_Title
                    {
                        JobTitleID = 1,
                        TitleName = model.TitleName
                    });
                }
                
                db.SaveChanges();
                model.JavaScriptToRun = "mySuccess()";
                TempData["model"] = model;
                return RedirectToAction("Index", "JobTitle");
            }

            return View("Index", model);
        }
    }
}