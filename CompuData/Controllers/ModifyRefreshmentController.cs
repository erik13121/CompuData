using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class ModifyRefreshmentController : Controller
    {
        // GET: ModifyRefreshment
        public ActionResult Index(string refreshmentID)
        {
            CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
            Models.Refreshment myModel = new Models.Refreshment();
            if (refreshmentID != null)
            {
                var intSupplierID = Int32.Parse(refreshmentID);
                var myRefreshment = db.Refreshments.Where(i => i.RefreshmentID == intSupplierID).FirstOrDefault();

                myModel.RefreshmentID = myRefreshment.RefreshmentID;
                myModel.Name = myRefreshment.Name;
                myModel.Description = myRefreshment.Description;
                myModel.UnitPrice = myRefreshment.UnitPrice;

                return View(myModel);
            }

            return View(myModel);
        }

        [HttpPost]
        public ActionResult RedirectToModifyRefreshmentDetails(string refreshmentID)
        {
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("Index", "ModifyRefreshment", new { refreshmentID = refreshmentID });
            return Json(new { Url = redirectUrl });
        }

        [HttpPost]
        public ActionResult Modify([Bind(Prefix = "")]Models.Refreshment model)
        {
            var db = new CodeFirst.CodeFirst();
            if (ModelState.IsValid)
            {
                var Refreshment = db.Refreshments.Where(v => v.RefreshmentID == model.RefreshmentID).SingleOrDefault();

                if (Refreshment != null)
                {
                    Refreshment.RefreshmentID = model.RefreshmentID;
                    Refreshment.Name = model.Name;
                    Refreshment.Description = model.Description;
                    Refreshment.UnitPrice = model.UnitPrice;
                    db.SaveChanges();
                }

                TempData["js"] = "myUpdateSuccess()";
                return RedirectToAction("Index", "Refreshment");
            }

            return View("Index", model);
        }
    }
}