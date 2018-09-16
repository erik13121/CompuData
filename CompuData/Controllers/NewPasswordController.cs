using CompuData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class NewPasswordController : Controller
    {
        // GET: NewPassword
        public ActionResult Index(string rt, string email)
        {
            ResetPasswordModel model = new ResetPasswordModel();
            model.ReturnToken = rt;
            model.Email = email;
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                CodeFirst.CodeFirst db = new CodeFirst.CodeFirst();
                var user = db.Users.Where(u => u.WorkEmail == model.Email).FirstOrDefault();
                user.Password = Crypto.Hash(model.Password, "MD5");
                if (user.WorkEmail != null)
                {
                    ViewBag.Message = "Successfully Changed";
                    Session["currentLogin"] = model.Email;
                }
                else
                {
                    ViewBag.Message = "Something went horribly wrong!";
                }
            }
            return View("Index", model);
        }
    }
}