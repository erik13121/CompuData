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
        [MaxLength(100, ErrorMessage = "You are only allowed up to 100 characters as the First Name")]
        public string FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Middle Name")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "The Last Name is required")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Last Name")]
        public string SecondName { get; set; }

        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Initials")]
        public string Initials { get; set; }

        [MaxLength(10, ErrorMessage = "The Cellphone Number must consist of 10 numbers in the format (xxxxxxxxxx)")]
        [RegularExpression("\\d{10}", ErrorMessage = "The Cellphone Number must consist of 10 numbers in the format (xxxxxxxxxx)")]
        public string CellNum { get; set; }

        [Required(ErrorMessage = "The Personal Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Email")]
        public string PersonalEmail { get; set; }

        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Street Address")]
        public string StreetAddress { get; set; }

        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the City")]
        public string City { get; set; }

        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Area Code")]
        public string AreaCode { get; set; }

        [Column(TypeName = "bit")]
        public bool Thanked { get; set; }
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