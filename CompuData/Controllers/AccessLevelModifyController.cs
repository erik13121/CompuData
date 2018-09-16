using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AccessLevelModifyController : Controller
    {
        // GET: AccessLevelModify
        public ActionResult Index(string levelID)
        {
            Models.AccessLevel myModel = new Models.AccessLevel();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (levelID != null)
            {
                var intLevelID = Int32.Parse(levelID);
                var myType = db.Access_Level.Where(i => i.AccessLevelID == intLevelID).FirstOrDefault();

                myModel.AccessLevelID = myType.AccessLevelID;
                myModel.LevelName = myType.LevelName;
                myModel.LevelDescription = myType.LevelDescription;
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult Index(Models.AccessLevel model)
        {
            if (ModelState.IsValid)
            {
                CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
                
                var myType = db.Access_Level.Where(i => i.AccessLevelID == model.AccessLevelID).FirstOrDefault();

                model.AccessLevelID = myType.AccessLevelID;
                model.LevelName = myType.LevelName;
                model.LevelDescription = myType.LevelDescription;
            }
            
            return View(model);
        }

        [HttpPost]
        public ActionResult Update([Bind(Prefix = "")]Models.AccessLevel model)
        {
            if (ModelState.IsValid)
            {
                var db = new CodeFirst.CodeFirst();
                var level = db.Access_Level.Where(v => v.AccessLevelID == model.AccessLevelID).SingleOrDefault();

                if (level != null)
                {
                    level.AccessLevelID = model.AccessLevelID;
                    level.LevelName = model.LevelName;
                    level.LevelDescription = model.LevelDescription;
                    db.SaveChanges();
                }

                TempData["js"] = "myUpdateSuccess()";
                return RedirectToAction("Index", "AccessLevels");
            }

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult RedirectToModifyAccessLevelDetails(string levelID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "AccessLevelModify", new { levelID = levelID });
            return Json(new { Url = redirectUrl });
        }
    }
}