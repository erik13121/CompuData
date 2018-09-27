﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Models
{
    public class Funder_Person
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int FunderPersonID { get; set; }

        [Required(ErrorMessage = "The First Name is required")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the First Name")]
        public string FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Middle Name")]
        public string MiddleName { get; set; }

        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Last Name")]
        public string LastName { get; set; }

        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Initials")]
        public string Initials { get; set; }

        public string CellNum { get; set; }

        [Required(ErrorMessage = "The Personal Email is required")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Email Address")]
        public string PersonalEmail { get; set; }

        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Name of Bank")]
        public string Bank { get; set; }

        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Account Number")]
        public string AccountNumber { get; set; }

        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Branch Code")]
        public string BranchCode { get; set; }

        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Street Address")]
        public string StreetAddress { get; set; }

        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the City")]
        public string City { get; set; }

        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Area Code")]
        public string AreaCode { get; set; }

        [Column(TypeName = "bit")]
        public bool Thanked { get; set; }

        public int? ProjectID { get; set; }

        public string ProjectName { get; set; }

        [Required(ErrorMessage = "Please Add a Funder Type first")]
        public int TypeID { get; set; }
        public string Name { get; set; }

        public string JavaScriptToRun { get; set; }
        public List<CodeFirst.Funder_Type> FunderTypes { get; set; }
        public List<CodeFirst.Project> Project { get; set; }
        public Funder_Person() { }
        public Funder_Person(int id, string Fname, string Mname, string Lname, string Initial, string CelltNum, string Email, string Bankname, string AccNum, string Branch, string Streetnum, string cityName, string Area, bool ThankedStatus, int projectID, int typeID)
        {
            FunderPersonID = id;
            FirstName = Fname;
            MiddleName = Mname;
            LastName = Lname;
            Initials = Initial;
            CellNum = CelltNum;
            PersonalEmail = Email;
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

        public static IEnumerable<CodeFirst.Funder_Person> Data;
        public static IEnumerable<CodeFirst.Funder_Person> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var Funder_Person = db.Funder_Person.ToList();

            Data = Funder_Person;
            return Data;
        }
    }
}