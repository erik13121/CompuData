using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class RepairPersonDetailsController : Controller
    {
        // GET: RepairPersonDetails
        public ActionResult Index(string personID)
        {
            Models.RepairPerson myModel = new Models.RepairPerson();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
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
            }
            
            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToRepairPersonDetails(string personID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "RepairPersonDetails", new { personID = personID });
            return Json(new { Url = redirectUrl });
        }
    }
}