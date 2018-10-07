using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class SelectEquipmentController : Controller
    {
        // GET: SelectEquipmentForProof
        public ActionResult Index(string button)
        {
            if (button == "Assign")
            {
                Session["btnClicked"] = "Assign";
            }

            return View();
        }
    }
}