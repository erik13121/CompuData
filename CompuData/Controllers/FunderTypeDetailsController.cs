using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class FunderTypeDetailsController : Controller
    {
        // GET: FunderTypeDetails
        public ActionResult Index(string typeID)
        {
            Models.FunderType myModel = new Models.FunderType();
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            if (typeID != null)
            {
                var intTypeID = Int32.Parse(typeID);
                var myFunderType = db.Funder_Type.Where(i => i.TypeID == intTypeID).FirstOrDefault();

                myModel.TypeID = myFunderType.TypeID;
                myModel.Name = myFunderType.Name;
                myModel.Description = myFunderType.Description;
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToFunderTypeDetails(string typeID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "FunderTypeDetails", new { typeID = typeID });
            return Json(new { Url = redirectUrl });
        }
    }
}