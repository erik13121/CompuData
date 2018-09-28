using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CompuData.Models
{
    public class Supplier
    {
        public int SupplierID { get; set; }

        [Required(ErrorMessage = "A Supplier Name is required")]
        [RegularExpression("^[a-zA-Z1-9- ]*$", ErrorMessage = "No special characters are allowed.")]
        [MaxLength(50, ErrorMessage = "Max allowed characters for Supplier Name is 50")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The VAT Number is required")]
        [RegularExpression("^[4][0-9]{9}$", ErrorMessage = "A South African VAT Number must consist of 10 numbers in the format of a 4 followed by 9 more digits (4xxxxxxxxx)")]
        public string VATNumber { get; set; }

        [RegularExpression("^[a-zA-Z1-9 ]*$", ErrorMessage = "No special characters are allowed.")]
        [MaxLength(50, ErrorMessage = "Max allowed characters for Address is 50")]
        public string POAddress { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "No numbers or special characters are allowed.")]
        [MaxLength(50, ErrorMessage = "Max allowed characters for City is 50")]
        public string POCity { get; set; }

        [RegularExpression("\\d{4}", ErrorMessage = "The Area Code must consist of 4 numbers in the format (xxxx)")]
        [MaxLength(4, ErrorMessage = "Max allowed characters for Area Code is 4")]
        public string POAreaCode { get; set; }

        [Required(ErrorMessage = "An Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(50, ErrorMessage = "Max allowed characters for Email address is 50")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "The Contact number is required")]
        [Phone(ErrorMessage = "Invalid Contact Number format")]
        public string ContactNumber { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "No numbers or special characters are allowed.")]
        [MaxLength(50, ErrorMessage = "Max allowed characters for Bank is 50")]
        public string Bank { get; set; }

        [RegularExpression("\\d{9,16}", ErrorMessage = "The Account Number must consist of between 9 and 16 numbers")]
        public string AccountNumber { get; set; }

        [RegularExpression("\\d{4,10}", ErrorMessage = "The Branch Code must consist of between 4 and 10 numbers")]
        public string BranchCode { get; set; }

        public string JavaScriptToRun { get; set; }
        public Supplier() { }
        public Supplier(int ID, string SName, string SVATNumber,string SEmailAddress, string SContactNumber,string SBank, string SAccountNumber, string SBranchCode, string SPOAddress,string SPOCity, string SPOAreaCode)
        {
            SupplierID = ID;
            Name = SName;
            VATNumber = SVATNumber;
            EmailAddress = SEmailAddress;
            ContactNumber = SContactNumber;
            Bank = SBank;
            AccountNumber = SAccountNumber;
            BranchCode = SBranchCode;
            POAddress = SPOAddress;
            POCity = SPOCity;
            POAreaCode = SPOAreaCode; 
        }

        public static IEnumerable<CodeFirst.Supplier> Data;
        public static IEnumerable<CodeFirst.Supplier> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var suppliers = db.Suppliers.ToList();

            Data = suppliers;
            return Data;
        }
    }
}