using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddAccessLevelController : Controller
    {
        // GET: AddAccessLevel
        public ActionResult Index()
        {
            var level = new Models.AccessLevel();
            return View(level);
        }

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "")]Models.AccessLevel model)
        {
            if (ModelState.IsValid)
            {
                var db = new CodeFirst.CodeFirst();
                if (db.Access_Level.Count() > 0)
                {
                    var item = db.Access_Level.OrderByDescending(a => a.AccessLevelID).FirstOrDefault();

                    db.Access_Level.Add(new CodeFirst.Access_Level
                    {
                        AccessLevelID = item.AccessLevelID + 1,
                        LevelName = model.LevelName,
                        LevelDescription = model.LevelDescription
                    });
                }
                else
                {
                    db.Access_Level.Add(new CodeFirst.Access_Level
                    {
                        AccessLevelID = 1,
                        LevelName = model.LevelName,
                        LevelDescription = model.LevelDescription
                    });
                }
                
                db.SaveChanges();
                model.JavaScriptToRun = "mySuccess()";
                TempData["model"] = model;
                return RedirectToAction("Index", "AccessLevels");
            }

            return View("Index", model);
        }
    }
}