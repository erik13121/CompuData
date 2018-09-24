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
    public class FunderOrgController : Controller
    {
        // GET: FunderOrg
        public ActionResult Index()
        {

            Models.Funder_Org myModel = new Models.Funder_Org();
            if (TempData["model"] != null)
            {
                myModel = (Models.Funder_Org)TempData["model"];
                TempData.Remove("model");
            }
            return View(myModel);
        }

        public ActionResult PageData(IDataTablesRequest request)
        {
            // Nothing important here. Just creates some mock data.
            var data = Models.Funder_Org.GetData();

            var db = new CodeFirst.CodeFirst();
            var FunderType = db.Funder_Type.ToList();
            var Project = db.Projects.ToList();
            var newData = (from d in data
                           join f in FunderType on d.TypeID equals f.TypeID
                           join p in Project on d.ProjectID equals p.ProjectID
                           select new
                           {
                                FunderOrgID = d.FunderOrgID,
                                OrgName = d.OrgName,
                                ContactNumber = d.ContactNumber,
                                EmailAddress = d.EmailAddress,
                                Bank = d.Bank,
                                AccountNumber = d.AccountNumber,
                                BranchCode = d.BranchCode,
                                StreetAddress = d.StreetAddress,
                                City = d.City,
                                AreaCode = d.AreaCode,
                                Thanked = d.Thanked,
                                ProjectName = p.ProjectName,
                                TypeName = f.Name,
                        }).ToList();

            var filteredData = newData.Where(_item =>
            _item.FunderOrgID.ToString().Contains(request.Search.Value) ||
            _item.OrgName.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.ContactNumber.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.EmailAddress.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.Bank.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.AccountNumber.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.BranchCode.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.StreetAddress.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.City.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.AreaCode.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.Thanked.ToString().ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.ProjectName.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.TypeName.ToUpper().Contains(request.Search.Value.ToUpper())
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
        public ActionResult Delete(string FunderOrgID)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();
                var intFunderOrgID = int.Parse(FunderOrgID);
                var FunderOrg = db.Funder_Org.Where(v => v.FunderOrgID == intFunderOrgID).FirstOrDefault();
                db.Funder_Org.Remove(FunderOrg);
                db.SaveChanges();

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "FunderOrg");
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