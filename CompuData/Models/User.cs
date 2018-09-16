using System.Collections.Generic;
using CompuData.CodeFirst;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompuData.Models
{
    public class User
    {
        public int AccessLevelID { get; set; }
        [Required(ErrorMessage = "The Area Code is required")]
        [MaxLength(50)]
        [RegularExpression("\\d{4,6}", ErrorMessage = "The Area Code has to consist of between 4 and 6 numbers")]
        public string AreaCode { get; set; }
        [MaxLength(10, ErrorMessage = "The Cellphone Number must consist of 10 numbers in the format (xxxxxxxxxx)")]
        [RegularExpression("\\d{10}", ErrorMessage = "The Cellphone Number must consist of 10 numbers in the format (xxxxxxxxxx)")]
        public string CellNum { get; set; }
        [Required(ErrorMessage = "The City is required")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only Text and spaces are allowed. No numbers or special characters allowed.")]
        [MaxLength(50)]
        public string City { get; set; }
        [Required(ErrorMessage = "The First Name is required")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only Text and spaces are allowed. No numbers or special characters allowed.")]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string Initials { get; set; }
        public int JobTitleID { get; set; }
        [Required(ErrorMessage = "The Last Name is required")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only Text and spaces are allowed. No numbers or special characters allowed.")]
        [MaxLength(50)]
        public string LastName { get; set; }
        [MaxLength(50)]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only Text and spaces are allowed. No numbers or special characters allowed.")]
        public string MiddleName { get; set; }
        [Required(ErrorMessage = "The National ID is required")]
        [MaxLength(13)]
        [RegularExpression("\\d{13}", ErrorMessage = "The National ID must consist of 13 numbers")]
        public string NationalID { get; set; }
        [Required(ErrorMessage = "The Password is required")]
        [MaxLength(50)]
        public string Password { get; set; }
        [Required(ErrorMessage = "The Personal Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(50)]
        public string PersonalEmail { get; set; }
        [MaxLength(50)]
        public string POAddress { get; set; }
        public string POAreaCode { get; set; }
        public string POCity { get; set; }
        [Required(ErrorMessage = "The Street Address is required")]
        [MaxLength(50)]
        public string StreetAddress { get; set; }
        [Required(ErrorMessage = "The Telephone Number is required")]
        [MaxLength(10, ErrorMessage = "The Telephone Number must consist of 10 numbers in the format (xxxxxxxxxx)")]
        [RegularExpression("\\d{10}", ErrorMessage = "The Telephone Number must consist of 10 numbers in the format (xxxxxxxxxx)")]
        public string TelNum { get; set; }
        [Key]
        [Required(ErrorMessage = "The User ID is required")]
        public int UserID { get; set; }
        [Column(TypeName = "bit")]
        public bool? ValidLicense { get; set; }
        [Required(ErrorMessage = "The Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(50)]
        public string WorkEmail { get; set; }
        [Required(ErrorMessage = "The Work Number is required")]
        [MaxLength(10, ErrorMessage = "The Work Number must consist of 10 numbers in the format (xxxxxxxxxx)")]
        [RegularExpression("\\d{10}", ErrorMessage = "The Work Number must consist of 10 numbers in the format (xxxxxxxxxx)")]
        public string WorkNum { get; set; }
        public string JobTitleName { get; set; }
        public string AccessLevelName { get; set; }
        public List<Access_Level> AccessLevels { get; set; }
        public List<Employee_Title> EmployeeTitles { get; set; }
        public string JavaScriptToRun { get; set; }
        public User() { }
        public User(int id, string first, string middle, string last, string init, string pass, string nat, string cell, string tel, string workNum, string perEmail, string workEmail, string street, string city, string area, string poAddress, string poCity, string poArea, bool license, int titleID, int levelID)
        {
            UserID = id;
            FirstName = first;
            MiddleName = middle;
            LastName = last;
            Initials = init;
            Password = pass;
            NationalID = nat;
            CellNum = cell;
            TelNum = tel;
            WorkNum = workNum;
            PersonalEmail = perEmail;
            WorkEmail = workEmail;
            StreetAddress = street;
            City = city;
            AreaCode = area;
            POAddress = poAddress;
            POCity = poCity;
            POAreaCode = poArea;
            ValidLicense = license;
            JobTitleID = titleID;
            AccessLevelID = levelID;
        }

        public static IEnumerable<CodeFirst.User> Data;
        public static IEnumerable<CodeFirst.User> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var users = db.Users.ToList();

            Data = users;
            return Data;
        }
    }
}