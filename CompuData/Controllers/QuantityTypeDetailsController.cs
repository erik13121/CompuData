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
        public ActionResult Index(string quantityTypeID)
        {
            Models.QuantityType myModel = new Models.QuantityType();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (quantityTypeID != null)
            {
                var intTypeID = Int32.Parse(quantityTypeID);
                var myType = db.Quantity_Type.Where(i => i.QuantityTypeID == intTypeID).FirstOrDefault();

                myModel.QuantityTypeID = myType.QuantityTypeID;
                myModel.Description = myType.Description;
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToQuantityTypeDetails(string quantityTypeID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "QuantityTypeDetails", new { quantityTypeID = quantityTypeID });
            return Json(new { Url = redirectUrl });
        }
    }
}