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
        [RegularExpression("^[a-zA-Z1-9- ]*$", ErrorMessage = "No special characters are allowed.")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 100 characters as the Organization Name")]
        public string OrgName { get; set; }

        [Required(ErrorMessage = "The Contact Number is required")]
        [Phone(ErrorMessage = "Invalid Contact Number format")]
        public string ContactNum { get; set; }

        [Required(ErrorMessage = "The Contact Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(50, ErrorMessage = "Max of 50 characters allowed as the Contact Email")]
        public string ContactEmail { get; set; }

        [Column(TypeName = "bit")]
        public bool Thanked { get; set; }

        [RegularExpression("^[a-zA-Z1-9 ]*$", ErrorMessage = "No special characters are allowed.")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Street Address")]
        public string StreetAddress { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only Text and spaces are allowed. No numbers or special characters allowed.")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the City Name")]
        public string City { get; set; }

        [RegularExpression("\\d{4}", ErrorMessage = "The Area Code must consist of 4 numbers in the format (xxxx)")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Area Code")]
        public string AreaCode { get; set; }
        public string JavaScriptToRun { get; set; }

        public List<CodeFirst.Donor_Org> DonorOrgs { get; set; }
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