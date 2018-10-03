using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class HelpPageController : Controller
    {
        // GET: HelpPage
        public ActionResult Index()
        {
            return View();
        }
    }
}