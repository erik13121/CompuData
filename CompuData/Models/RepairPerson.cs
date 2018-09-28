using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompuData.Models
{
    public class RepairPerson
    {
        [Key]
        [Required(ErrorMessage = "The Repair Person ID is required")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int RepPersonID { get; set; }

        [Required(ErrorMessage = "The Person's Name is required")]
        [RegularExpression("^[a-zA-Z- ]*$", ErrorMessage = "No numbers or special characters are allowed.")]
        [MaxLength(30, ErrorMessage = "The Name(s) can only be up to 30 characters long")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Email Address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address Format")]
        [MaxLength(50, ErrorMessage = "The Email Address can only be up to 50 characters long")]
        public string EmailAddress { get; set; }

        [Required(ErrorMessage = "The Bank Name is required")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "No numbers or special characters allowed.")]
        [MaxLength(30, ErrorMessage = "The Bank Name can only be up to 30 characters long")]
        public string Bank { get; set; }

        [Required(ErrorMessage = "The Account Number is required")]
        [RegularExpression("\\d{9,16}", ErrorMessage = "The Account Number must consist of between 9 and 16 numbers")]
        [MaxLength(16, ErrorMessage = "The Account Number can only be up to 16 characters long")]
        public string AccountNumber { get; set; }

        [Required(ErrorMessage = "The Branch Code is required")]
        [RegularExpression("\\d{4,10}", ErrorMessage = "The Branch Code must consist of between 4 and 10 numbers")]
        [MaxLength(10, ErrorMessage = "The Branch Code can only be up to 10 characters long")]
        public string BranchCode { get; set; }

        public string JavaScriptToRun { get; set; }

        public RepairPerson() { }
        public RepairPerson(int id, string name, string email, string bank, string accountNumber, string branch)
        {
            RepPersonID = id;
            Name = name;
            EmailAddress = email;
            Bank = bank;
            AccountNumber = accountNumber;
            BranchCode = branch;
        }

        public static IEnumerable<CodeFirst.RepairPerson> Data;
        public static IEnumerable<CodeFirst.RepairPerson> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var persons = db.RepairPersons.ToList();

            Data = persons;
            return Data;
        }
    }
}