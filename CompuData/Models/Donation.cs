using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Models
{
    public class Donation
    {
        [Required]
        public int DonationID { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateDate { get; set; }

        public int? DonorPID { get; set; }

        public int? DonorOrgID { get; set; }

        public IEnumerable<SelectListItem> DonorPeople { get; set; }

        public IEnumerable<SelectListItem> DonorOrgs { get; set; }

        public int ItemID { get; set; }

        public List<CodeFirst.Donation_Item> Items { get; set; }

        public int TypeID { get; set; }

        public List<CodeFirst.Donation_Type> DonationTypes { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string OrgName { get; set; }

        public List<CodeFirst.Donation_Line> Lines { get; set; }

        public string JavaScriptToRun { get; set; }
        public Donation() { }
        public Donation(int id, DateTime date, int donorPID, int donorOID, int quantityid)
        {
            DonationID = id;
            DateDate = date;
            DonorPID = donorPID;
            DonorOrgID = donorOID;
        }

        public static IEnumerable<CodeFirst.Donation> Data;
        public static IEnumerable<CodeFirst.Donation> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var Donations = db.Donations.ToList();

            Data = Donations;
            return Data;
        }
    }
}