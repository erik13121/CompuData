using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class UserModifyController : Controller
    {
        // GET: UserModify
        public ActionResult Index(string userID)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (userID != null)
            {
                Models.User myModel = new Models.User();

                var intUserID = Int32.Parse(userID);
                var myUser = db.Users.Where(i => i.UserID == intUserID).FirstOrDefault();

                myModel.UserID = myUser.UserID;
                myModel.FirstName = myUser.FirstName;
                myModel.MiddleName = myUser.MiddleName;
                myModel.LastName = myUser.LastName;
                myModel.Initials = myUser.Initials;
                myModel.Password = myUser.Password;
                myModel.NationalID = myUser.NationalID;
                myModel.CellNum = myUser.CellNum;
                myModel.TelNum = myUser.TelNum;
                myModel.WorkNum = myUser.WorkNum;
                myModel.PersonalEmail = myUser.PersonalEmail;
                myModel.WorkEmail = myUser.WorkEmail;
                myModel.StreetAddress = myUser.StreetAddress;
                myModel.City = myUser.City;
                myModel.AreaCode = myUser.AreaCode;
                myModel.POAddress = myUser.POAddress;
                myModel.POCity = myUser.POCity;
                myModel.POAreaCode = myUser.POAreaCode;
                myModel.ValidLicense = myUser.ValidLicense;
                myModel.JobTitleID = myUser.JobTitleID;
                myModel.AccessLevelID = myUser.AccessLevelID;

                myModel.EmployeeTitles = db.Employee_Title.ToList();
                myModel.AccessLevels = db.Access_Level.ToList();
                return View(myModel);
            }

            Models.User model = new Models.User();
            model.EmployeeTitles = db.Employee_Title.ToList();
            model.AccessLevels = db.Access_Level.ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(Models.User model)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                Models.User myModel = new Models.User();
                var myUser = db.Users.Where(i => i.UserID == model.UserID).FirstOrDefault();

                myModel.UserID = myUser.UserID;
                myModel.FirstName = myUser.FirstName;
                myModel.MiddleName = myUser.MiddleName;
                myModel.LastName = myUser.LastName;
                myModel.Initials = myUser.Initials;
                myModel.Password = myUser.Password;
                myModel.NationalID = myUser.NationalID;
                myModel.CellNum = myUser.CellNum;
                myModel.TelNum = myUser.TelNum;
                myModel.WorkNum = myUser.WorkNum;
                myModel.PersonalEmail = myUser.PersonalEmail;
                myModel.WorkEmail = myUser.WorkEmail;
                myModel.StreetAddress = myUser.StreetAddress;
                myModel.City = myUser.City;
                myModel.AreaCode = myUser.AreaCode;
                myModel.POAddress = myUser.POAddress;
                myModel.POCity = myUser.POCity;
                myModel.POAreaCode = myUser.POAreaCode;
                myModel.ValidLicense = myUser.ValidLicense;
                myModel.JobTitleID = myUser.JobTitleID;
                myModel.AccessLevelID = myUser.AccessLevelID;

                myModel.EmployeeTitles = db.Employee_Title.ToList();
                myModel.AccessLevels = db.Access_Level.ToList();
                return View(myModel);
            }
            
            model.EmployeeTitles = db.Employee_Title.ToList();
            model.AccessLevels = db.Access_Level.ToList();
            return View(model);
        }

        [HttpPost]
        public ActionResult RedirectToModifyUserDetails(string userID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "UserModify", new { userID = userID });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult Modify([Bind(Prefix = "")]Models.User model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                var myUser = db.Users.Where(v => v.UserID == model.UserID).SingleOrDefault();

                if (myUser != null)
                {
                    myUser.UserID = model.UserID;
                    myUser.FirstName = model.FirstName;
                    myUser.MiddleName = model.MiddleName;
                    myUser.LastName = model.LastName;
                    myUser.Initials = model.Initials;
                    myUser.Password = model.Password;
                    myUser.NationalID = model.NationalID;
                    myUser.CellNum = model.CellNum;
                    myUser.TelNum = model.TelNum;
                    myUser.WorkNum = model.WorkNum;
                    myUser.PersonalEmail = model.PersonalEmail;
                    myUser.WorkEmail = model.WorkEmail;
                    myUser.StreetAddress = model.StreetAddress;
                    myUser.City = model.City;
                    myUser.AreaCode = model.AreaCode;
                    myUser.POAddress = model.POAddress;
                    myUser.POCity = model.POCity;
                    myUser.POAreaCode = model.POAreaCode;
                    myUser.ValidLicense = model.ValidLicense;
                    myUser.JobTitleID = model.JobTitleID;
                    myUser.AccessLevelID = model.AccessLevelID;
                    db.SaveChanges();
                }

                TempData["js"] = "myUpdateSuccess()";
                return RedirectToAction("Index", "User");
            }

            model.AccessLevels = db.Access_Level.ToList();
            model.EmployeeTitles = db.Employee_Title.ToList();
            return View("Index", model);
        }
    }
}