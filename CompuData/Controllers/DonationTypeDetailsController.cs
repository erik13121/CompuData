using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class DonationTypeDetailsController : Controller
    {
        // GET: DonationTypeDetails
        public ActionResult Index(string typeID)
        {
            Models.DonationType myModel = new Models.DonationType();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (typeID != null)
            {
                var intTypeID = Int32.Parse(typeID);
                var myType = db.Donation_Type.Where(i => i.TypeID == intTypeID).FirstOrDefault();

                myModel.TypeID = myType.TypeID;
                myModel.TypeName = myType.TypeName;
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToDonationTypeDetails(string typeID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "DonationTypeDetails", new { typeID = typeID });
            return Json(new { Url = redirectUrl });
        }
    }
}