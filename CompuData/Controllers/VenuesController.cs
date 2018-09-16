using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class VenuesController : Controller
    {
        // GET: Venues
        public ActionResult Index()
        {
            return View();
        }
    }
}