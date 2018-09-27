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

        [Required(ErrorMessage = "The Organization Name is required")]
        [MaxLength(100, ErrorMessage = "You are only allowed up to 100 characters as the Organization Name")]
        public string OrgName { get; set; }

        [Required(ErrorMessage = "The Contact Number is required")]
        [RegularExpression("\\d{10}", ErrorMessage = "The Cellphone Number must consist of 10 numbers in the format (xxxxxxxxxx)")]
        [MaxLength(10, ErrorMessage = "The Cellphone Number must consist of 10 numbers in the format (xxxxxxxxxx)")]
        public string ContactNum { get; set; }

        [Required(ErrorMessage = "The Contact Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(50)]
        public string ContactEmail { get; set; }

        [Column(TypeName = "bit")]
        public bool Thanked { get; set; }

        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Street Address")]
        public string StreetAddress { get; set; }

        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the City")]
        public string City { get; set; }

        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Area Code")]
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