using CompuData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ProjectTypeModifyController : Controller
    {
        // GET: ProjectTypeModify
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
        public ActionResult Index(Models.ProjectType model)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                var myType = db.Project_Type.Where(i => i.TypeID == model.TypeID).FirstOrDefault();

                model.TypeID = myType.TypeID;
                model.TypeName = myType.TypeName;
                model.TypeDescription = myType.TypeDescription;
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Update([Bind(Prefix = "")]Models.ProjectType model)
        {
            if (ModelState.IsValid)
            {
                var db = new CodeFirst.CodeFirst();
                var type = db.Project_Type.Where(v => v.TypeID == model.TypeID).SingleOrDefault();

                if (type != null)
                {
                    type.TypeID = model.TypeID;
                    type.TypeName = model.TypeName;
                    type.TypeDescription = model.TypeDescription;
                    db.SaveChanges();
                }

                TempData["js"] = "myUpdateSuccess()";
                return RedirectToAction("Index", "ProjectType");
            }

            return View("Index", model);
        }

        [HttpPost]
        public ActionResult RedirectToModifyProjectTypeDetails(string typeID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ProjectTypeModify", new { typeID = typeID });
            return Json(new { Url = redirectUrl });
        }
    }
}