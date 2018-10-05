using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class SelectProjectController : Controller
    {
        // GET: SelectProject
        public ActionResult Index(string button)
        {
            if (button == "Approve")
            {
                Session["btnClicked"] = "Approve";
            }
            else
            {
                Session["btnClicked"] = "Finalize";
            }
            return View();
        }
    }
}