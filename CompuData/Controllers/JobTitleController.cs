using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompuData.Models;

namespace CompuData.Controllers
{
    public class JobTitleController : Controller
    {
        // GET: JobTitle
        public ActionResult Index()
        {
            JobTitle myModel = new JobTitle();
            if (TempData["model"] != null)
            {
                myModel = (JobTitle)TempData["model"];
                TempData.Remove("model");
            }
            return View(myModel);
        }

        public ActionResult PageData(IDataTablesRequest request)
        {
            // Nothing important here. Just creates some mock data.
            var data = Models.JobTitle.GetData();

            // Global filtering.
            // Filter is being manually applied due to in-memmory (IEnumerable) data.
            // If you want something rather easier, check IEnumerableExtensions Sample.
            var filteredData = data.Where(_item =>
            _item.JobTitleID.ToString().Contains(request.Search.Value) ||
            _item.TitleName.ToUpper().Contains(request.Search.Value.ToUpper())
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
        public void SetTempData()
        {
            TempData["js"] = "";
        }

        [HttpPost]
        public ActionResult Delete(string titleID)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();
                var intTitleID = int.Parse(titleID);
                var title = db.Employee_Title.Where(t => t.JobTitleID == intTitleID).FirstOrDefault();
                db.Employee_Title.Remove(title);
                db.SaveChanges();

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "JobTitle");
                return Json(new { Url = redirectUrl });
            }
            catch
            {
                return Json(new { Url = "Cascading error!" });
            }
        }
    }
}