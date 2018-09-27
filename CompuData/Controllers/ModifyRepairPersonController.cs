using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ModifyRepairPersonController : Controller
    {
        // GET: ModifyRepairPerson
        public ActionResult Index(string personID)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            Models.RepairPerson myModel = new Models.RepairPerson();
            if (personID != null)
            {
                var intPersonID = Int32.Parse(personID);
                var myPerson = db.RepairPersons.Where(i => i.RepPersonID == intPersonID).FirstOrDefault();

                myModel.RepPersonID = myPerson.RepPersonID;
                myModel.Name = myPerson.Name;
                myModel.EmailAddress = myPerson.EmailAddress;
                myModel.Bank = myPerson.Bank;
                myModel.AccountNumber = myPerson.AccountNumber;
                myModel.BranchCode = myPerson.BranchCode;
                
                return View(myModel);
            }
            
            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToModifyRepairPersonDetails(string personID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ModifyRepairPerson", new { personID = personID });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult Modify([Bind(Prefix = "")]Models.RepairPerson model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                var person = db.RepairPersons.Where(v => v.RepPersonID == model.RepPersonID).SingleOrDefault();

                if (person != null)
                {
                    person.RepPersonID = model.RepPersonID;
                    person.Name = model.Name;
                    person.EmailAddress = model.EmailAddress;
                    person.Bank = model.Bank;
                    person.AccountNumber = model.AccountNumber;
                    person.BranchCode = model.BranchCode;
                    db.SaveChanges();
                }

                TempData["js"] = "myUpdateSuccess()";
                return RedirectToAction("Index", "RepairPersons");
            }

            return View("Index", model);
        }
    }
}