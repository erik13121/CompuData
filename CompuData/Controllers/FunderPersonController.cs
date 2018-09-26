using CompuData.CodeFirst;
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class FunderPersonController : Controller
    {
        // GET: FunderPerson
        public ActionResult Index()
        {

            Models.Funder_Person myModel = new Models.Funder_Person();
            if (TempData["model"] != null)
            {
                myModel = (Models.Funder_Person)TempData["model"];
                TempData.Remove("model");
            }
            return View(myModel);
        }

        public ActionResult PageData(IDataTablesRequest request)
        {
            // Nothing important here. Just creates some mock data.
            var data = Models.Funder_Person.GetData();

            var db = new CodeFirst.CodeFirst();
            var FunderType = db.Funder_Type.ToList();
            var Project = db.Projects.ToList();
            var newData = (from d in data
                           join p in Project on d.ProjectID equals p.ProjectID
                           into projects
                           from mP in projects.DefaultIfEmpty()
                           join f in FunderType on d.TypeID equals f.TypeID
                           select new
                           {
                               FunderPersonID = d.FunderPersonID,
                               FirstName = d.FirstName,
                               MiddleName = d.MiddleName,
                               LastName = d.LastName,
                               Initials = d.Initials,
                               CellNum = d.CellNum,
                               PersonalEmail = d.PersonalEmail,
                               Bank = d.Bank,
                               AccountNumber = d.AccountNumber,
                               BranchCode = d.BranchCode,
                               StreetAddress = d.StreetAddress,
                               City = d.City,
                               AreaCode = d.AreaCode,
                               Thanked = d.Thanked,
                               ProjectName = mP != null ? mP.ProjectName : "Not linked to Project",
                               TypeName = f.Name,
                           }).ToList();

            var filteredData = newData.Where(_item =>
            _item.FunderPersonID.ToString().Contains(request.Search.Value) ||
            (_item.FirstName != null ? _item.FirstName.ToUpper().Contains(request.Search.Value.ToUpper()) : false) ||
            (_item.MiddleName != null ? _item.MiddleName.ToUpper().Contains(request.Search.Value.ToUpper()) : false) ||
            (_item.LastName != null ? _item.LastName.ToUpper().Contains(request.Search.Value.ToUpper()) : false) ||
            (_item.Initials != null ? _item.Initials.ToUpper().Contains(request.Search.Value.ToUpper()) : false) ||
            (_item.CellNum != null ? _item.CellNum.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) : false) ||
            (_item.PersonalEmail != null ? _item.PersonalEmail.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) : false) ||
            (_item.Bank != null ? _item.Bank.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) : false) ||
            (_item.AccountNumber.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) ||
            (_item.BranchCode.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) ||
            (_item.StreetAddress.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            (_item.City != null ? _item.City.ToUpper().Contains(request.Search.Value.ToUpper()) : false) ||
            (_item.AreaCode != null ? _item.AreaCode.ToUpper().Contains(request.Search.Value.ToUpper()) : false) ||
            (_item.Thanked != null ? _item.Thanked.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) : false) ||
            (_item.ProjectName != null ? _item.ProjectName.ToUpper().Contains(request.Search.Value.ToUpper()) : false) ||
            _item.TypeName.ToUpper().Contains(request.Search.Value.ToUpper())
            ))));

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
        public ActionResult Delete(string FunderPersonID)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();
                var intFunderPersonID = int.Parse(FunderPersonID);
                var FunderPerson = db.Funder_Person.Where(v => v.FunderPersonID == intFunderPersonID).FirstOrDefault();
                db.Funder_Person.Remove(FunderPerson);
                db.SaveChanges();

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "FunderPerson");
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