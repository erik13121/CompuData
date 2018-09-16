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
        [MaxLength(50, ErrorMessage = "You have entered too many characters!")]
        public string Name { get; set; }

        [RegularExpression("\\d{10}", ErrorMessage = "The VAT Number must consist of 10 numbers in the format (xxxxxxxxxx)")]
        public string VATNumber { get; set; }

        [MaxLength(50, ErrorMessage = "You have entered too many characters!")]
        public string POAddress { get; set; }

        [MaxLength(50, ErrorMessage = "You have entered too many characters!")]
        public string POCity { get; set; }

        [MaxLength(50, ErrorMessage = "You have entered too many characters!")]
        public string POAreaCode { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(50)]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "A Supplier Contact number is required")]
        [MaxLength(50)]
        [Phone(ErrorMessage = "Invalid Number format")]
        public string ContactNumber { get; set; }

        [MaxLength(50, ErrorMessage = "You have entered too many characters!")]
        public string Bank { get; set; }

        [RegularExpression("\\d{9,16}", ErrorMessage = "The Account Number must consist of between 9 and 16 numbers")]
        public int? AccountNumber { get; set; }

        [RegularExpression("\\d{4,10}", ErrorMessage = "The Branch Code must consist of between 4 and 10 numbers")]
        public int? BranchCode { get; set; }

        public string JavaScriptToRun { get; set; }
        public Supplier() { }
        public Supplier(int ID, string SName, string SVATNumber,string SEmailAddress, string SContactNumber,string SBank, int SAccountNumber, int SBranchCode, string SPOAddress,string SPOCity, string SPOAreaCode)
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