using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ModifyEquipmentDetailsController : Controller
    {
        // GET: ModifyEquipmentDetails
        public ActionResult Index(string equipmentID)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            Models.Equipment myModel = new Models.Equipment();
            if (equipmentID != null)
            {
                var intEquipmentID = Int32.Parse(equipmentID);
                var myEquipment = db.Equipments.Where(i => i.EquipmentID == intEquipmentID).FirstOrDefault();
                var myUser = db.Users.Where(u => u.UserID == myEquipment.UserID).FirstOrDefault();
                var myType = db.Equipment_Type.Where(t => t.TypeID == myEquipment.TypeID).FirstOrDefault();

                myModel.EquipmentID = myEquipment.EquipmentID;
                myModel.ManufacturerName = myEquipment.ManufacturerName;
                myModel.ModelNumber = myEquipment.ModelNumber;
                myModel.DatePurchased = myEquipment.DatePurchased;
                myModel.ServiceIntervalMonths = myEquipment.ServiceIntervalMonths;
                myModel.Status = myEquipment.Status;
                myModel.UserID = myEquipment.UserID;
                myModel.TypeID = myEquipment.TypeID;

                myModel.Users = db.Users
                .AsEnumerable()
                .Select(u => new SelectListItem
                {
                    Value = u.UserID.ToString(),
                    Text = u.FirstName + " " + u.LastName
                }).ToList();

                myModel.EquipmentTypes = db.Equipment_Type.ToList();
                return View(myModel);
            }

            myModel.Users = db.Users
                .AsEnumerable()
                .Select(u => new SelectListItem
                {
                    Value = u.UserID.ToString(),
                    Text = u.FirstName + " " + u.LastName
                }).ToList();

            myModel.EquipmentTypes = db.Equipment_Type.ToList();
            return View(myModel);
        }

        [HttpPost]
        public ActionResult Index(Models.Equipment model)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();

            if (ModelState.IsValid)
            {
                Models.Equipment myModel = new Models.Equipment();
                
                var myUser = db.Users.Where(u => u.UserID == model.UserID).FirstOrDefault();
                var myType = db.Equipment_Type.Where(t => t.TypeID == model.TypeID).FirstOrDefault();

                myModel.EquipmentID = model.EquipmentID;
                myModel.ManufacturerName = model.ManufacturerName;
                myModel.ModelNumber = model.ModelNumber;
                myModel.DatePurchased = model.DatePurchased;
                myModel.ServiceIntervalMonths = model.ServiceIntervalMonths;
                myModel.Status = model.Status;
                myModel.UserID = model.UserID;
                myModel.TypeID = model.TypeID;

                myModel.Users = db.Users
                .AsEnumerable()
                .Select(u => new SelectListItem
                {
                    Value = u.UserID.ToString(),
                    Text = u.FirstName + " " + u.LastName
                }).ToList();

                myModel.EquipmentTypes = db.Equipment_Type.ToList();
                return View(myModel);
            }

            model.Users = db.Users
                .AsEnumerable()
                .Select(u => new SelectListItem
                {
                    Value = u.UserID.ToString(),
                    Text = u.FirstName + " " + u.LastName
                }).ToList();

            model.EquipmentTypes = db.Equipment_Type.ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult RedirectToModifyEquipmentDetails(string equipmentID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ModifyEquipmentDetails", new { equipmentID = equipmentID });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult Modify([Bind(Prefix = "")]Models.Equipment model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                var equipment = db.Equipments.Where(v => v.EquipmentID == model.EquipmentID).SingleOrDefault();

                if (equipment != null)
                {
                    equipment.EquipmentID = model.EquipmentID;
                    equipment.ManufacturerName = model.ManufacturerName;
                    equipment.ModelNumber = model.ModelNumber;
                    equipment.DatePurchased = model.DatePurchased;
                    equipment.ServiceIntervalMonths = model.ServiceIntervalMonths;
                    equipment.Status = model.Status;
                    equipment.UserID = model.UserID;
                    equipment.TypeID = model.TypeID;
                    db.SaveChanges();
                }

                TempData["js"] = "myUpdateSuccess()";
                return RedirectToAction("Index", "Equipments");
            }

            model.Users = db.Users
                .AsEnumerable()
                .Select(u => new SelectListItem
                {
                    Value = u.UserID.ToString(),
                    Text = u.FirstName + " " + u.LastName
                }).ToList();

            model.EquipmentTypes = db.Equipment_Type.ToList();
            return View("Index", model);
        }
    }
}