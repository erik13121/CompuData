using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddFunderPersonController : Controller
    {
        // GET: AddFunderPerson
        public ActionResult Index()
        {
            var db = new CodeFirst.CodeFirst();
            var FunderPerson = new Models.Funder_Person();
            FunderPerson.FunderTypes = db.Funder_Type.ToList();
            FunderPerson.Project = db.Projects.ToList();
            return View(FunderPerson);
        }

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "")]Models.Funder_Person model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                if (db.Funder_Person.Count() > 0)
                {
                    var item = db.Funder_Person.OrderByDescending(a => a.FunderPersonID).FirstOrDefault();

                    db.Funder_Person.Add(new CodeFirst.Funder_Person
                    {
                        FunderPersonID = item.FunderPersonID + 1,
                        FirstName = model.FirstName,
                        MiddleName = model.MiddleName,
                        LastName = model.LastName,
                        Initials = model.Initials,
                        CellNum = model.CellNum,
                        PersonalEmail = model.PersonalEmail,
                        Bank = model.Bank,
                        AccountNumber = model.AccountNumber,
                        BranchCode = model.BranchCode,
                        StreetAddress = model.StreetAddress,
                        City = model.City,
                        AreaCode = model.AreaCode,
                        Thanked = false,
                        ProjectID = model.ProjectID,
                        TypeID = model.TypeID,
                    });
                }
                else
                {
                    db.Funder_Person.Add(new CodeFirst.Funder_Person
                    {
                        FunderPersonID = 1,
                        FirstName = model.FirstName,
                        MiddleName = model.MiddleName,
                        LastName = model.LastName,
                        Initials = model.Initials,
                        CellNum = model.CellNum,
                        PersonalEmail = model.PersonalEmail,
                        Bank = model.Bank,
                        AccountNumber = model.AccountNumber,
                        BranchCode = model.BranchCode,
                        StreetAddress = model.StreetAddress,
                        City = model.City,
                        AreaCode = model.AreaCode,
                        Thanked = false,
                        ProjectID = model.ProjectID,
                        TypeID = model.TypeID,
                    });
                }

                db.SaveChanges();
                model.JavaScriptToRun = "mySuccess()";
                TempData["model"] = model;
                return RedirectToAction("Index", "FunderPerson");

            }

            model.FunderTypes = db.Funder_Type.ToList();
            model.Project = db.Projects.ToList();
            return View("Index", model);
        }
}