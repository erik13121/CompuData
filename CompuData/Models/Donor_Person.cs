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

        [MaxLength(50)]
        public string FirstName { get; set; }

        [MaxLength(50)]
        public string MiddleName { get; set; }

        [MaxLength(50)]
        public string SecondName { get; set; }

        [MaxLength(50)]
        public string Initials { get; set; }

        [MaxLength(10)]
        public string CellNum { get; set; }

        [MaxLength(50)]
        public string PersonalEmail { get; set; }

        [MaxLength(50)]
        public string StreetAddress { get; set; }

        [MaxLength(50)]
        public string City { get; set; }

        [MaxLength(50)]
        public string AreaCode { get; set; }

        [Column(TypeName = "bit")]
        public bool? Thanked { get; set; }
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