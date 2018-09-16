using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AccessLevelDetailsController : Controller
    {
        // GET: AccessLevelDetails
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
        public ActionResult RedirectToAccessLevelDetails(string levelID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "AccessLevelDetails", new { levelID = levelID });
            return Json(new { Url = redirectUrl });
        }
    }
}