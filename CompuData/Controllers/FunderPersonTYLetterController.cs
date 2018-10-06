using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class FunderPersonTYLetterController : Controller
    {
        // GET: FunderPersonTYLetter
        public ActionResult Index()
        {
            var db = new CodeFirst.CodeFirst();
            var FunderPerson = new Models.Funder_Person();
            FunderPerson.FunderPersons = db.Funder_Person.ToList();
            return View(FunderPerson);
        }

        public ActionResult FunderPersonTYLetter(int FunderPersonID)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report/FunderPersonThankYouLetter.rpt")));
            rd.SetParameterValue("@PersonID", FunderPersonID);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/doc", "FunderThankYouLetterTemplate.doc");
            }
            catch
            {
                throw;
            }
        }
    }
}