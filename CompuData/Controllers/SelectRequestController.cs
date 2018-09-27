using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class SelectRequestController : Controller
    {
        // GET: SelectRequest
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