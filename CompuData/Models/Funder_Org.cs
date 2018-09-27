using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Models
{
    public class Funder_Org
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FunderOrgID { get; set; }

        [Required]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only Text and spaces are allowed. No numbers or special characters allowed.")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Organization Name")]
        public string OrgName { get; set; }

        [Required(ErrorMessage = "The Contact Number is required")]
        [MaxLength(10, ErrorMessage = "The Contact Number must consist of 10 numbers in the format (xxxxxxxxxx)")]
        [RegularExpression("\\d{10}", ErrorMessage = "The Contact Number must consist of 10 numbers in the format (xxxxxxxxxx)")]
        public string ContactNumber { get; set; }

        [Required(ErrorMessage = "The Personal Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Email Address")]
        public string EmailAddress { get; set; }

        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Bank Name")]
        public string Bank { get; set; }

        [RegularExpression("\\d{9,16}", ErrorMessage = "The Account Number must consist of between 9 and 16 numbers")]
        public string AccountNumber { get; set; }

        [RegularExpression("\\d{4,12}", ErrorMessage = "The Account Number must consist of between 4 and 12 numbers")]
        public string BranchCode { get; set; }

        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Street Address")]
        public string StreetAddress { get; set; }

        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the City")]
        public string City { get; set; }

        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Area Code")]
        public string AreaCode { get; set; }

        [Column(TypeName = "bit")]
        public bool? Thanked { get; set; }

        public int? ProjectID { get; set; }

        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Please Add a Funder Type first")]
        public int TypeID { get; set; }

        public string Name { get; set; }

        public string JavaScriptToRun { get; set; }
        public List<CodeFirst.Funder_Type> FunderTypes { get; set; }
        public List<CodeFirst.Project> Project { get; set; }
        public Funder_Org() { }
        public Funder_Org(int id, string name, string ContactNum, string Email, string Bankname, string AccNum, string Branch, string Streetnum, string cityName, string Area, bool ThankedStatus,int projectID, int typeID)
        {
            FunderOrgID = id;
            OrgName = name;
            ContactNumber = ContactNum;
            EmailAddress = Email;
            Bank = Bankname;
            AccountNumber = AccNum;
            BranchCode = Branch;
            StreetAddress = Streetnum;
            City = cityName;
            AreaCode = Area;
            Thanked = ThankedStatus;
            ProjectID = projectID;
            TypeID = typeID;
        }

        public static IEnumerable<CodeFirst.Funder_Org> Data;
        public static IEnumerable<CodeFirst.Funder_Org> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var Funder_Org = db.Funder_Org.ToList();

            Data = Funder_Org;
            return Data;
        }
    }
}