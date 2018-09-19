using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ModifyEquipmentTypeController : Controller
    {
        // GET: ModifyEquipmentType
        public ActionResult Index(string TypeID)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            Models.EquipmentType myModel = new Models.EquipmentType();
            if (TypeID != null)
            {
                var intEquipID = Int32.Parse(TypeID);
                var myEquipType = db.Equipment_Type.Where(i => i.TypeID == intEquipID).FirstOrDefault();

                myModel.TypeID = myEquipType.TypeID;
                myModel.TypeName = myEquipType.TypeName;
                myModel.TypeDescription = myEquipType.TypeDescription;
                myModel.Removable = myEquipType.Removable;

                return View(myModel);
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult Index(Models.EquipmentType model)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();

            if (ModelState.IsValid)
            {
                Models.EquipmentType myModel = new Models.EquipmentType();

                var myEquipType = db.Equipment_Type.Where(i => i.TypeID == model.TypeID).FirstOrDefault();

                myModel.TypeID = myEquipType.TypeID;
                myModel.TypeName = myEquipType.TypeName;
                myModel.TypeDescription = myEquipType.TypeDescription;
                myModel.Removable = myEquipType.Removable;


                return View(myModel);
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult RedirectToModifyEquipTypeDetails(string typeID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ModifyEquipmentType", new { typeID = typeID });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult Modify([Bind(Prefix = "")]Models.EquipmentType model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                var EquipType = db.Equipment_Type.Where(v => v.TypeID == model.TypeID).SingleOrDefault();

                if (EquipType != null)
                {
                    EquipType.TypeID = model.TypeID;
                    EquipType.TypeName = model.TypeName;
                    EquipType.TypeDescription = model.TypeDescription;
                    EquipType.Removable = model.Removable;
                    db.SaveChanges();
                }

                TempData["js"] = "myUpdateSuccess()";
                return RedirectToAction("Index", "EquipmentType");
            }

            return View("Index", model);
        }
    }
}