﻿using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CompuData.Controllers
{
    public class SoftwareLicensesController : Controller
    {
        // GET: SoftwareLicenses
        public ActionResult Index()
        {
            Models.SoftwareLicenses myModel = new Models.SoftwareLicenses();
            if (TempData["model"] != null)
            {
                myModel = (Models.SoftwareLicenses)TempData["model"];
                TempData.Remove("model");
            }
            return View(myModel);
        }

        public ActionResult PageData(IDataTablesRequest request)
        {
            // Nothing important here. Just creates some mock data.
            var data = Models.SoftwareLicenses.GetData();

            // Global filtering.
            // Filter is being manually applied due to in-memmory (IEnumerable) data.
            // If you want something rather easier, check IEnumerableExtensions Sample.
            var filteredData = data.Where(_item =>
            _item.LicenceID.ToString().Contains(request.Search.Value) ||
            _item.SoftwareName.ToUpper().Contains(request.Search.Value.ToUpper()) ||
            _item.ActivationPeriodInMonths.ToString().ToUpper().Contains(request.Search.Value.ToUpper())
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
        public ActionResult Delete(string licenceID)
        {
            try
            {
                var db = new CodeFirst.CodeFirst();
                var intLicID = int.Parse(licenceID);
                var SoftLicence = db.Software_Licenses.Where(v => v.LicenceID == intLicID).FirstOrDefault();
                db.Software_Licenses.Remove(SoftLicence);
                db.SaveChanges();

                var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "SoftwareLicenses");
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