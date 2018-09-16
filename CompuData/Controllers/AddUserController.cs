using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class AddUserController : Controller
    {
        // GET: AddUser
        public ActionResult Index()
        {
            var db = new CodeFirst.CodeFirst();
            var users = new Models.User();
            users.AccessLevels = db.Access_Level.ToList();
            users.EmployeeTitles = db.Employee_Title.ToList();
            return View(users);
        }

        [HttpPost]
        public ActionResult Create([Bind(Prefix = "")]Models.User model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                if (db.Users.Count() > 0)
                {
                    var item = db.Users.OrderByDescending(a => a.UserID).FirstOrDefault();

                    db.Users.Add(new CodeFirst.User
                    {
                        UserID = model.UserID + 1,
                        FirstName = model.FirstName,
                        MiddleName = model.MiddleName,
                        LastName = model.LastName,
                        Initials = model.Initials,
                        Password = Crypto.Hash(model.Password, "MD5"),
                        NationalID = model.NationalID,
                        CellNum = model.CellNum,
                        TelNum = model.TelNum,
                        WorkNum = model.WorkNum,
                        PersonalEmail = model.PersonalEmail,
                        WorkEmail = model.WorkEmail,
                        StreetAddress = model.StreetAddress,
                        City = model.City,
                        AreaCode = model.AreaCode,
                        POAddress = model.POAddress,
                        POCity = model.POCity,
                        POAreaCode = model.POAreaCode,
                        ValidLicense = model.ValidLicense,
                        JobTitleID = model.JobTitleID,
                        AccessLevelID = model.AccessLevelID,
                });
                }
                else
                {
                    db.Users.Add(new CodeFirst.User
                    {
                        UserID = 1,
                        FirstName = model.FirstName,
                        MiddleName = model.MiddleName,
                        LastName = model.LastName,
                        Initials = model.Initials,
                        Password = Crypto.Hash(model.Password, "MD5"),
                        NationalID = model.NationalID,
                        CellNum = model.CellNum,
                        TelNum = model.TelNum,
                        WorkNum = model.WorkNum,
                        PersonalEmail = model.PersonalEmail,
                        WorkEmail = model.WorkEmail,
                        StreetAddress = model.StreetAddress,
                        City = model.City,
                        AreaCode = model.AreaCode,
                        POAddress = model.POAddress,
                        POCity = model.POCity,
                        POAreaCode = model.POAreaCode,
                        ValidLicense = model.ValidLicense,
                        JobTitleID = model.JobTitleID,
                        AccessLevelID = model.AccessLevelID,
                    });
                }

                db.SaveChanges();
                model.JavaScriptToRun = "mySuccess()";
                TempData["model"] = model;
                return RedirectToAction("Index", "User");
            }

            model.AccessLevels = db.Access_Level.ToList();
            model.EmployeeTitles = db.Employee_Title.ToList();
            return View("Index", model);
        }
    }
}