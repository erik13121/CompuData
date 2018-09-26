using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CompuData.Models
{
    public class SoftwareLicenses
    {
        public int LicenceID { get; set; }

        [Required(ErrorMessage = "The Name of the Software is required")]
        [MaxLength(50)]
        public string SoftwareName { get; set; }

        public int? ActivationPeriodInMonths { get; set; }

        public string JavaScriptToRun { get; set; }
        public SoftwareLicenses() { }
        public SoftwareLicenses(int ID, string SName, int SActivationPeriod)
        {
            LicenceID = ID;
            SoftwareName = SName;
            ActivationPeriodInMonths = SActivationPeriod;          
        }

        public static IEnumerable<CodeFirst.Software_Licenses> Data;
        public static IEnumerable<CodeFirst.Software_Licenses> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var SoftwareLicenses = db.Software_Licenses.ToList();

            Data = SoftwareLicenses;
            return Data;
        }
    }
}