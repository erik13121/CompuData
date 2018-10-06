using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class RefreshmentDetailsController : Controller
    {
        // GET: RefreshmentDetails
        public ActionResult Index(string refreshmentID)
        {
            Models.Refreshment myModel = new Models.Refreshment();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (refreshmentID != null)
            {
                var intSupplierID = Int32.Parse(refreshmentID);
                var myRefreshment = db.Refreshments.Where(i => i.RefreshmentID == intSupplierID).FirstOrDefault();

                myModel.RefreshmentID = myRefreshment.RefreshmentID;
                myModel.Name = myRefreshment.Name;
                myModel.Description = myRefreshment.Description;
                myModel.UnitPrice = myRefreshment.UnitPrice;
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToRefreshmentDetails(string refreshmentID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "RefreshmentDetails", new { refreshmentID = refreshmentID });
            return Json(new { Url = redirectUrl });
        }
    }
}