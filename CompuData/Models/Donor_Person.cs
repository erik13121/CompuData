using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Models
{
    public class Donor_Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int DonorPID { get; set; }

        [Required(ErrorMessage = "The First Name is required")]
        [RegularExpression("^[a-zA-Z- ]*$", ErrorMessage = "Only Text and spaces are allowed. No numbers or special characters are allowed.")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 100 characters as the First Name")]
        public string FirstName { get; set; }

        [RegularExpression("^[a-zA-Z- ]*$", ErrorMessage = "Only Text and spaces are allowed. No numbers or special characters allowed.")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Middle Name")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "The Last Name is required")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only Text and spaces are allowed. No numbers or special characters are allowed.")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Last Name")]
        public string SecondName { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only Text and spaces are allowed. No numbers or special characters are allowed.")]
        [MaxLength(10, ErrorMessage = "You are only allowed up to 50 characters as the Initials")]
        public string Initials { get; set; }

        [Phone(ErrorMessage = "Invalid Cell Number format")]
        public string CellNum { get; set; }

        [Required(ErrorMessage = "The Personal Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Email")]
        public string PersonalEmail { get; set; }

        [RegularExpression("^[a-zA-Z1-9 ]*$", ErrorMessage = "No special characters are allowed.")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Street Address")]
        public string StreetAddress { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only Text and spaces are allowed. No numbers or special characters are allowed.")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the City")]
        public string City { get; set; }

        [RegularExpression("\\d{4}", ErrorMessage = "The Area Code must consist of 4 numbers in the format (xxxx)")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Area Code")]
        public string AreaCode { get; set; }

        [Column(TypeName = "bit")]
        public bool Thanked { get; set; }

        public List<CodeFirst.Donor_Person> DonorPersons { get; set; }
        public string JavaScriptToRun { get; set; }
        public Donor_Person() { }
        public Donor_Person(int ID, string Fname, string Sname, string Init, string Mname,  string Num, string Email, bool ThankedStatus, string Street, string Cityname, string Area)
        {
            DonorPID = ID;
            FirstName = Fname;
            MiddleName = Mname;
            SecondName = Sname;
            Initials = Init;
            CellNum = Num;
            PersonalEmail = Email;
            Thanked = ThankedStatus;
            StreetAddress = Street;
            City = Cityname;
            AreaCode = Area;
        }
        
        public static IEnumerable<CodeFirst.Donor_Person> Data;
        public static IEnumerable<CodeFirst.Donor_Person> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var DonorPerson = db.Donor_Person.ToList();

            Data = DonorPerson;
            return Data;
        }
    }
}