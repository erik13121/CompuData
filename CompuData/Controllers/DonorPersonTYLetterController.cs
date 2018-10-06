using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Controllers
{
    public class DonorPersonTYLetterController : Controller
    {
        // GET: DonorPersonTYLetter
        public ActionResult Index()
        {
            var db = new CodeFirst.CodeFirst();
            var DonorPerson = new Models.Donor_Person();
            DonorPerson.DonorPersons = db.Donor_Person.ToList();
            return View(DonorPerson);
        }

        public ActionResult DonorPersonTYLetter([Bind(Prefix = "")]Models.Donor_Person model)
        {
            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report/DonorPersonThankYouLetter.rpt")));
            rd.SetParameterValue("@PersonID", model.DonorPID);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/doc", "DonorThankYouLetterTemplate.doc");
            }
            catch
            {
                throw;
            }
        }
    }
}