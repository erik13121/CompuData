using CompuData.CodeFirst;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Routing;

namespace CompuData.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Models.Login model)
        {
            if (model != null)
            {
                return View(model);
            }
            Models.Login newLogin = new Models.Login();
            return View(newLogin);
        }

        [HttpPost]
        public ActionResult CheckLogin(Models.Login model)
        {
            if (ModelState.IsValid)
            {
                CodeFirst.CodeFirst cbe = new CodeFirst.CodeFirst();
                //var s = cbe.GetLoginInfo(model.Email, model.Password);
                var item = cbe.Users.FirstOrDefault(s => s.WorkEmail == model.Email);

                if (item != null)
                {
                    item.Password = item.Password.Replace("\r\n", string.Empty);
                    if (item.Password.ToUpper() == Crypto.Hash(model.Password, "MD5"))
                    {
                        string ipAddress;
                        // Gets Client IP Address
                        ipAddress = Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                        if (ipAddress == "" || ipAddress == null)
                            ipAddress = Request.ServerVariables["REMOTE_ADDR"];

                        var activeLogin = cbe.Active_Login.ToList();
                        if (activeLogin.Count != 0)
                        {
                            var lastLogin = activeLogin.OrderByDescending(a => a.LoginID).First();

                            var newLogin = new Active_Login
                            {
                                LoginID = lastLogin.LoginID + 1,
                                IPAddress = ipAddress,
                                LoginTimestamp = DateTime.Now,
                                LatestActionTimestamp = DateTime.Now.TimeOfDay,
                                UserID = item.UserID
                            };

                            cbe.Active_Login.Add(newLogin);

                            Session["CurrentLogin"] = newLogin;

                            cbe.SaveChanges();
                        }
                        else
                        {
                            var newLogin = new Active_Login
                            {
                                LoginID = 1,
                                IPAddress = ipAddress,
                                LoginTimestamp = DateTime.Now,
                                LatestActionTimestamp = DateTime.Now.TimeOfDay,
                                UserID = item.UserID
                            };

                            cbe.Active_Login.Add(newLogin);

                            Session["CurrentLogin"] = newLogin;

                            cbe.SaveChanges();
                        }

                        return RedirectToAction("Index", "MainMenu");
                    }
                    else
                    {
                        ViewBag.NotValidPassword = "Incorrect Password entered";
                    }
                }
                else
                {
                    ViewBag.NotValidUser = "User Does not Exist";
                }
            }

            return View("Index", model);
        }

        
        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();

            return RedirectToAction("Index", "Login");
            //return View("../Login/Index");
        }
    }
}