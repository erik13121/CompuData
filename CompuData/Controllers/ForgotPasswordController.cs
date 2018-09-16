using CompuData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using System.Web.Mvc;
using System.Security.Cryptography;
using System.Net;

namespace CompuData.Controllers
{
    public class ForgotPasswordController : Controller
    {
        // GET: ForgotPassword
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LostPassword(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var foundUser = "";
                using (var db = new CodeFirst.CodeFirst())
                {
                    foundUser = (from u in db.Users
                                         where u.WorkEmail == model.Email
                                         select u.WorkEmail).FirstOrDefault();
                }
                if (foundUser != null && foundUser != "")
                {
                    // Generae password token that will be used in the email link to authenticate user
                    string sendThisInEmalandStoreinDB;
                    using (var rng = RandomNumberGenerator.Create())
                    {
                        Byte[] bytes = new Byte[8];
                        rng.GetBytes(bytes);

                        sendThisInEmalandStoreinDB = Convert.ToBase64String(bytes);
                    }
                    // Generate the html link sent via email
                    string resetLink = Url.Action("Index", "NewPassword", new { rt = sendThisInEmalandStoreinDB, email = model.Email }, "http");

                    // Email stuff
                    string subject = "Reset your password for CompuData";
                    string body = "Your Reset Password link: " + resetLink;
                    string from = "donotreply@CompuData.com";

                    MailMessage message = new MailMessage(from, model.Email);
                    message.Subject = subject;
                    message.Body = body;

                    SmtpClient client = new SmtpClient();
                    client.EnableSsl = true;

                    // Attempt to send the email
                    try
                    {
                        client.Send(message);

                        TempData["js"] = "reset()";
                        return RedirectToAction("Index", "Login");
                    }
                    catch (Exception e)
                    {
                        ModelState.AddModelError("", "Issue sending email: " + e.Message);
                    }
                }
                else // Email not found
                {
                    /* Note: You may not want to provide the following information
                    since it gives an intruder information as to whether a
                    certain email address is registered with this website or not.
                    If you're really concerned about privacy, you may want to
                    forward to the same "Success" page regardless whether an
                    user was found or not. This is only for illustration purposes.*/

                    ViewBag.NotValidUser = "No user found by that email.";
                }
            }

            /* You may want to send the user to a "Success" page upon the successful
             sending of the reset email link. Right now, if we are 100% successful
             nothing happens on the page. :P
            */
            
            return View("Index", model);
        }
    }
}