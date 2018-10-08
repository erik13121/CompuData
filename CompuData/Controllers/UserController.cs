using CompuData.Models;
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            User myModel = new User();
            if (TempData["model"] != null)
            {
                myModel = (User)TempData["model"];
                TempData.Remove("model");
            }
            return View(myModel);
        }

        public ActionResult PageData(IDataTablesRequest request)
        {
            // Nothing important here. Just creates some mock data.
            var data = Models.User.GetData();

            var db = new CodeFirst.CodeFirst();
            var employeeTitles = db.Employee_Title.ToList();
            var accessLevels = db.Access_Level.ToList();
            var newData = (from d in data
                           join e in employeeTitles on d.JobTitleID equals e.JobTitleID
                           join a in accessLevels on d.AccessLevelID equals a.AccessLevelID
                           select new
                           {
                               UserID = d.UserID,
                               FirstName = d.FirstName,
                               MiddleName = d.MiddleName,
                               LastName = d.LastName,
                               Initials = d.Initials,
                               Password = d.Password,
                               NationalID = d.NationalID,
                               CellNum = d.CellNum,
                               TelNum = d.TelNum,
                               WorkNum = d.WorkNum,
                               PersonalEmail = d.PersonalEmail,
                               WorkEmail = d.WorkEmail,
                               StreetAddress = d.StreetAddress,
                               City = d.City,
                               AreaCode = d.AreaCode,
                               POAddress = d.POAddress,
                               POCity = d.POCity,
                               POAreaCode = d.POAreaCode,
                               ValidLicense = d.ValidLicense,
                               JobTitleName = e.TitleName,
                               AccessLevelName = a.LevelName,
                           }).ToList();

            // Global filtering.
            // Filter is being manually applied due to in-memmory (IEnumerable) data.
            // If you want something rather easier, check IEnumerableExtensions Sample.
            var filteredData = newData.Where(_item =>
            _item.UserID.ToString().Contains(request.Search.Value) ||
            _item.FirstName.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            (_item.MiddleName != null ? _item.MiddleName.ToUpper().Contains(request.Search.Value.ToUpper()) : false) ||
            _item.LastName.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            (_item.Initials != null ? _item.Initials.ToUpper().Contains(request.Search.Value.ToUpper()) : false) ||
            _item.Password.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.NationalID.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            (_item.CellNum != null ? _item.CellNum.ToUpper().Contains(request.Search.Value.ToUpper()) : false) ||
            _item.TelNum.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.WorkNum.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.PersonalEmail.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.WorkEmail.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.StreetAddress.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.City.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.AreaCode.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            (_item.POAddress != null ? _item.POAddress.ToUpper().Contains(request.Search.Value.ToUpper()) : false) ||
            (_item.POCity != null ? _item.POCity.ToUpper().Contains(request.Search.Value.ToUpper()) : false) ||
            (_item.POAreaCode != null ? _item.POAreaCode.ToUpper().Contains(request.Search.Value.ToUpper()) : false) ||
            _item.ValidLicense.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.JobTitleName.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.AccessLevelName.ToUpper().Contains(request.Search.Value.ToUpper())
            );

            // Paging filtered data.
            // Paging is rather manual due to in-memmory (IEnumerable) data.
            var dataPage = filteredData.Skip(request.Start).Take(request.Length);

            // Response creation. To create your response you need to reference your request, to avoid
            // request/response tampering and to ensure response will be correctly created.
            var response = DataTablesResponse.Create(request, data.Count(), filteredData.Count(), dataPage);

            // Easier way is to return a new 'DataTablesJsonResult', which will automatically convert your
            // response to a json-compatible content, so DataTables can read it when received.
            return new DataTablesJsonResult(response, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Delete(string userID)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();
                var intUserID = int.Parse(userID);
                var user = db.Users.Where(v => v.UserID == intUserID).FirstOrDefault();
                db.Users.Remove(user);
                db.SaveChanges();

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "User");
                return Json(new { Url = redirectUrl });
            }
            catch
            {
                return Json(new { Url = "Cascading error!" });
            }
        }

        [HttpPost]
        public void SetTempData()
        {
            TempData["js"] = "";
        }


    }
}