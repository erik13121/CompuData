using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class EquipmentTypeDetailsController : Controller
    {
        // GET: EquipmentTypeDetails
        public ActionResult Index(string typeID)
        {
            Models.EquipmentType myModel = new Models.EquipmentType();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (typeID != null)
            {
                var intTypeID = Int32.Parse(typeID);
                var myEquipType = db.Equipment_Type.Where(i => i.TypeID == intTypeID).FirstOrDefault();

                myModel.TypeID = myEquipType.TypeID;
                myModel.TypeName = myEquipType.TypeName;
                myModel.TypeDescription = myEquipType.TypeDescription;
                myModel.Removable = myEquipType.Removable;

            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToEquipTypeDetails(string typeID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "EquipmentTypeDetails", new { typeID = typeID });
            return Json(new { Url = redirectUrl });
        }
    }
}