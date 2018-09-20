using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ModifyFunderTypeController : Controller
    {
        // GET: ModifyFunderType
        public ActionResult Index(string TypeID)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            Models.FunderType myModel = new Models.FunderType();
            if (TypeID != null)
            {
                var intEquipID = Int32.Parse(TypeID);
                var myFunderType = db.Funder_Type.Where(i => i.TypeID == intEquipID).FirstOrDefault();

                myModel.TypeID = myFunderType.TypeID;
                myModel.Name = myFunderType.Name;
                myModel.Description = myFunderType.Description;

                return View(myModel);
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult Index(Models.FunderType model)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();

            if (ModelState.IsValid)
            {
                Models.FunderType myModel = new Models.FunderType();

                var myFunderType = db.Funder_Type.Where(i => i.TypeID == model.TypeID).FirstOrDefault();

                myModel.TypeID = myFunderType.TypeID;
                myModel.Name = myFunderType.Name;
                myModel.Description = myFunderType.Description;

                return View(myModel);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult RedirectToModifyFunderTypeDetails(string typeID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ModifyFunderType", new { typeID = typeID });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult Modify([Bind(Prefix = "")]Models.FunderType model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                var FunderType = db.Funder_Type.Where(v => v.TypeID == model.TypeID).SingleOrDefault();

                if (FunderType != null)
                {
                    FunderType.TypeID = model.TypeID;
                    FunderType.Name = model.Name;
                    FunderType.Description = model.Description;
                    db.SaveChanges();
                }

                TempData["js"] = "myUpdateSuccess()";
                return RedirectToAction("Index", "FunderType");
            }

            return View("Index", model);
        }
    }
}