using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class EquipmentDetailsController : Controller
    {
        // GET: EquipmentDetails
        public ActionResult Index(string equipmentID)
        {
            Models.Equipment myModel = new Models.Equipment();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (equipmentID != null)
            {
                var intEquipmentID = Int32.Parse(equipmentID);
                var myEquipment = db.Equipments.Where(i => i.EquipmentID == intEquipmentID).FirstOrDefault();
                var myTypeID = db.Equipment_Type.Where(i => i.TypeID == myEquipment.TypeID).FirstOrDefault();

                myModel.EquipmentID = myEquipment.EquipmentID;
                myModel.ManufacturerName = myEquipment.ManufacturerName;
                myModel.ModelNumber = myEquipment.ModelNumber;
                myModel.DatePurchased = myEquipment.DatePurchased;
                myModel.ServiceIntervalMonths = myEquipment.ServiceIntervalMonths;
                myModel.Status = myEquipment.Status;
                myModel.UserID = myEquipment.UserID;
                myModel.TypeID = myEquipment.TypeID;
                myModel.TypeName = db.Equipment_Type.Where(i => i.TypeID == myEquipment.TypeID).FirstOrDefault().TypeName;
            }

            myModel.EquipmentTypes = db.Equipment_Type.ToList();
            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToEquipmentDetails(string equipmentID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "EquipmentDetails", new { equipmentID = equipmentID });
            return Json(new { Url = redirectUrl });
        }
    }
}