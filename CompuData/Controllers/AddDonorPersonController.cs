using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddDonorPersonController : Controller
    {
        // GET: AddDonorPerson
        public ActionResult Index()
        {
            var db = new CodeFirst.CodeFirst();
            var DonorPerson = new Models.Donor_Person();
            return View(DonorPerson);
        }

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "")]Models.Donor_Person model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                if (db.Donor_Person.Count() > 0)
                {
                    var item = db.Donor_Person.OrderByDescending(a => a.DonorPID).FirstOrDefault();

                    db.Donor_Person.Add(new CodeFirst.Donor_Person
                    {
                        DonorPID = item.DonorPID + 1,
                        FirstName = model.FirstName,
                        MiddleName = model.MiddleName,
                        SecondName = model.SecondName,
                        Initials = model.Initials,
                        CellNum = model.CellNum,
                        PersonalEmail = model.PersonalEmail,
                        Thanked = false,
                        StreetAddress = model.StreetAddress,
                        City = model.City,
                        AreaCode = model.AreaCode,
                    });
                }
                else
                {
                    db.Donor_Person.Add(new CodeFirst.Donor_Person
                    {
                        DonorPID = 1,
                        FirstName = model.FirstName,
                        MiddleName = model.MiddleName,
                        SecondName = model.SecondName,
                        Initials = model.Initials,
                        CellNum = model.CellNum,
                        PersonalEmail = model.PersonalEmail,
                        Thanked = false,
                        StreetAddress = model.StreetAddress,
                        City = model.City,
                        AreaCode = model.AreaCode,
                    });
                }

                db.SaveChanges();
                model.JavaScriptToRun = "mySuccess()";
                TempData["model"] = model;
                return RedirectToAction("Index", "DonorPerson");
            }

            return View("Index", model);
        }
    }
}