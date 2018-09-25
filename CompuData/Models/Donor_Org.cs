using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Models
{
    public class Donor_Org
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DonorOrgID { get; set; }

        [MaxLength(50)]
        public string OrgName { get; set; }

        [MaxLength(10)]
        public string ContactNum { get; set; }

        [MaxLength(50)]
        public string ContactEmail { get; set; }

        [Column(TypeName = "bit")]
        public bool? Thanked { get; set; }

        [MaxLength(50)]
        public string StreetAddress { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(50)]
        public string AreaCode { get; set; }
        public string JavaScriptToRun { get; set; }
        public Donor_Org() { }
        public Donor_Org(int id, string name, string Num, string Email, bool ThankedStatus, string Street, string Cityname, string Area)
        {
            DonorOrgID = id;
            OrgName = name;
            ContactNum = Num;
            ContactEmail = Email;
            Thanked = ThankedStatus;
            StreetAddress = Street;
            City = Cityname;
            AreaCode = Area;
        }

        public static IEnumerable<CodeFirst.Donor_Org> Data;
        public static IEnumerable<CodeFirst.Donor_Org> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var DonorOrg = db.Donor_Org.ToList();

            Data = DonorOrg;
            return Data;
        }
    }
}