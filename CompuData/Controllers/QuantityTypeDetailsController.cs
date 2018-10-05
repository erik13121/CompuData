using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class QuantityTypeDetailsController : Controller
    {
        // GET: QuantityTypeDetails
        public ActionResult Index(string quatityTypeID)
        {
            Models.QuantityType myModel = new Models.QuantityType();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (quatityTypeID != null)
            {
                var intTypeID = Int32.Parse(quatityTypeID);
                var myType = db.Quantity_Type.Where(i => i.QuatityTypeID == intTypeID).FirstOrDefault();

                myModel.QuatityTypeID = myType.QuatityTypeID;
                myModel.Description = myType.Description;
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToQuantityTypeDetails(string quatityTypeID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "QuantityTypeDetails", new { quatityTypeID = quatityTypeID });
            return Json(new { Url = redirectUrl });
        }
    }
}