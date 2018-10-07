using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class UserDetailsController : Controller
    {
        // GET: UserDetails
        public ActionResult Index(string userID)
        {
            Models.User myModel = new Models.User();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (userID != null)
            {
                var intUserID = Int32.Parse(userID);
                var myUser = db.Users.Where(i => i.UserID == intUserID).FirstOrDefault();
                var myTitleID = db.Employee_Title.Where(i => i.JobTitleID == myUser.JobTitleID).FirstOrDefault();
                var myAccessLevelID = db.Access_Level.Where(i => i.AccessLevelID == myUser.AccessLevelID).FirstOrDefault();

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
                myModel.JobTitleName = db.Employee_Title.Where(i => i.JobTitleID == myTitleID.JobTitleID).FirstOrDefault().TitleName;
                myModel.AccessLevelName = db.Access_Level.Where(i => i.AccessLevelID == myAccessLevelID.AccessLevelID).FirstOrDefault().LevelName;
            }

            myModel.EmployeeTitles = db.Employee_Title.ToList();
            myModel.AccessLevels = db.Access_Level.ToList();
            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToUserDetails(string userID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "UserDetails", new { userID = userID });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult Account()
        {
            var myModel = new Models.User();
            var login = (CodeFirst.Active_Login)Session["CurrentLogin"];
            var db = new CodeFirst.CodeFirst();

            var myUser = db.Users.Find(login.UserID);

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
            myModel.JobTitleName = db.Employee_Title.Where(i => i.JobTitleID == myUser.JobTitleID).FirstOrDefault().TitleName;
            myModel.AccessLevelName = db.Access_Level.Where(i => i.AccessLevelID == myUser.AccessLevelID).FirstOrDefault().LevelName;

            myModel.EmployeeTitles = db.Employee_Title.ToList();
            myModel.AccessLevels = db.Access_Level.ToList();

            return View("Index", myModel);
        }
    }
}