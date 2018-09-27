using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddRepairPersonController : Controller
    {
        // GET: AddRepairPerson
        public ActionResult Index()
        {
            var db = new CodeFirst.CodeFirst();
            var person = new Models.RepairPerson();
            return View(person);
        }

        public ActionResult FromAddRepairRequestScreen()
        {
            //equipmentModelToPassBack = equipmentModel;
            var db = new CodeFirst.CodeFirst();
            var person = new Models.RepairPerson();
            ViewBag.Referrer = "AddRepairRequest";
            return View("Index", person);
        }

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "")]Models.RepairPerson model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                if (db.RepairPersons.Count() > 0)
                {
                    var item = db.RepairPersons.OrderByDescending(a => a.RepPersonID).FirstOrDefault();

                    db.RepairPersons.Add(new CodeFirst.RepairPerson
                    {
                        RepPersonID = item.RepPersonID + 1,
                        Name = model.Name,
                        EmailAddress = model.EmailAddress,
                        Bank = model.Bank,
                        AccountNumber = model.AccountNumber,
                        BranchCode = model.BranchCode,
                    });
                }
                else
                {
                    db.RepairPersons.Add(new CodeFirst.RepairPerson
                    {
                        RepPersonID = 1,
                        Name = model.Name,
                        EmailAddress = model.EmailAddress,
                        Bank = model.Bank,
                        AccountNumber = model.AccountNumber,
                        BranchCode = model.BranchCode,
                    });
                }

                db.SaveChanges();

                if (Request.Form["Referrer"] == "AddRepairRequest")
                {
                    //TempData["EquipmentModel"] = equipmentModelToPassBack;
                    return RedirectToAction("Index", "AddRepairRequest", new { equipmentID = Session["equipmentID"].ToString()});
                }
                else
                {
                    model.JavaScriptToRun = "mySuccess()";
                    TempData["model"] = model;
                    return RedirectToAction("Index", "RepairPersons");
                }
            }

            return View("Index", model);
        }
    }
}