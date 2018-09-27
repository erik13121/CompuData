using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class RepairPersonsController : Controller
    {
        // GET: RepairPersons
        public ActionResult Index()
        {
            Models.RepairPerson myModel = new Models.RepairPerson();
            if (TempData["model"] != null)
            {
                myModel = (Models.RepairPerson)TempData["model"];
                TempData.Remove("model");
            }
            return View(myModel);
        }

        public ActionResult PageData(IDataTablesRequest request)
        {
            // Nothing important here. Just creates some mock data.
            var data = Models.RepairPerson.GetData();
            
            var db = new CodeFirst.CodeFirst();
            var newData = (from e in data
                           select new
                           {
                               RepPersonID = e.RepPersonID,
                               Name = e.Name,
                               EmailAddress = e.EmailAddress,
                               Bank = e.Bank,
                               AccountNumber = e.AccountNumber,
                               BranchCode = e.BranchCode
                           }).ToList();

            // Global filtering.
            // Filter is being manually applied due to in-memmory (IEnumerable) data.
            // If you want something rather easier, check IEnumerableExtensions Sample.
            var filteredData = newData.Where(_item =>
            _item.RepPersonID.ToString().Contains(request.Search.Value) ||
            _item.Name.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.EmailAddress.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.Bank.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.AccountNumber.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.BranchCode.ToUpper().Contains(request.Search.Value.ToUpper())
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
        public ActionResult Delete(string personID)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();
                var intPersonID = int.Parse(personID);
                var person = db.RepairPersons.Where(v => v.RepPersonID == intPersonID).FirstOrDefault();
                db.RepairPersons.Remove(person);
                db.SaveChanges();

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "RepairPersons");
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