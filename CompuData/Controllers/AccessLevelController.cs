using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CompuData.Models;
using DataTables.AspNet.Core;
using DataTables.AspNet.Mvc5;

namespace CompuData.Controllers
{
    public class AccessLevelController : Controller
    {
        // GET: AccessLevel
        public ActionResult Index()
        {
            return View();
        }
    }
}