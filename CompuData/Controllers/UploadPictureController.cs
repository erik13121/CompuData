using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class UploadPictureController : Controller
    {
        // GET: UploadPicture
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
        public ActionResult RedirectToUserUploadFileDetails(string userID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "UploadPicture", new { userID = userID });
            return Json(new { Url = redirectUrl });
        }

        public void Capture(string userID)
        {
            if (userID != null)
            {
                Models.User myModel = new Models.User();
                CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
                var intUserID = Int32.Parse(userID);
                var myUser = db.Users.Where(i => i.UserID == intUserID).FirstOrDefault();

                var stream = Request.InputStream;
                string dump;

                using (var reader = new StreamReader(stream))
                    dump = reader.ReadToEnd();

                var path = Server.MapPath("~/Files/" + myUser.FirstName + myUser.UserID + ".jpg");
                System.IO.File.WriteAllBytes(path, String_To_Bytes2(dump));

                myUser.UserPicture = "~/Files/" + myUser.FirstName + myUser.UserID + ".jpg";

                db.SaveChanges();

            }
            else
            {
                
            }

        }

        private byte[] String_To_Bytes2(string strInput)
        {
            int numBytes = (strInput.Length) / 2;
            byte[] bytes = new byte[numBytes];

            for (int x = 0; x < numBytes; ++x)
            {
                bytes[x] = Convert.ToByte(strInput.Substring(x * 2, 2), 16);
            }

            return bytes;
        }
    }
}