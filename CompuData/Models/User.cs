using System.Collections.Generic;
using CompuData.CodeFirst;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Security;

namespace CompuData.Models
{
    public class User
    {
        [Required(ErrorMessage = "Please Add an Access Level first")]
        public int AccessLevelID { get; set; }

        [Required(ErrorMessage = "The Area Code is required")]
        [RegularExpression("\\d{4}", ErrorMessage = "The Area Code must consist of 4 numbers in the format (xxxx)")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 4 numbers as the Area Code")]
        public string AreaCode { get; set; }

        [Phone(ErrorMessage = "Invalid Cell Number format")]
        public string CellNum { get; set; }

        [Required(ErrorMessage = "The City is required")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "No numbers or special characters are allowed.")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the City")]
        public string City { get; set; }

        [Required(ErrorMessage = "The First Name is required")]
        [RegularExpression("^[a-zA-Z- ]*$", ErrorMessage = "No numbers or special characters are allowed.")]
        [MaxLength(50, ErrorMessage = "Max allowed characters for First Name is 50")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The Initials is required")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "No numbers or special characters are allowed.")]
        [MaxLength(10, ErrorMessage = "Max allowed characters for Initials is 10")]
        public string Initials { get; set; }

        [Required(ErrorMessage = "Please Add a Job Title first")]
        public int JobTitleID { get; set; }

        [Required(ErrorMessage = "The Last Name is required")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "No numbers or special characters are allowed.")]
        [MaxLength(50, ErrorMessage = "Max allowed characters for Last Name is 50")]
        public string LastName { get; set; }

        [RegularExpression("^[a-zA-Z- ]*$", ErrorMessage = "No numbers or special characters are allowed.")]
        [MaxLength(50, ErrorMessage = "Max allowed characters for Middle Name(s) is 50")]
        public string MiddleName { get; set; }

        [Required(ErrorMessage = "The National ID is required")]
        [RegularExpression("\\d{13}", ErrorMessage = "The National ID must consist of 13 numbers in the format (xxxxxxxxxxxxx)")]
        public string NationalID { get; set; }

        [Required(ErrorMessage = "The Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "The Personal Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(50, ErrorMessage = "Max allowed characters for Personal Email is 50")]
        public string PersonalEmail { get; set; }

        [RegularExpression("^[a-zA-Z1-9 ]*$", ErrorMessage = "No special characters are allowed.")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the PO Address")]
        public string POAddress { get; set; }

        [RegularExpression("\\d{4}", ErrorMessage = "The Area Code must consist of 4 numbers in the format (xxxx)")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the PO Area Code")]
        public string POAreaCode { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "No numbers or special characters are allowed.")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the PO City")]
        public string POCity { get; set; }

        [Required(ErrorMessage = "The Street Address is required")]
        [RegularExpression("^[a-zA-Z1-9 ]*$", ErrorMessage = "No special characters are allowed.")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Address")]
        public string StreetAddress { get; set; }

        [Required(ErrorMessage = "The Telephone Number is required")]
        [Phone(ErrorMessage = "Invalid Telephone Number format")]
        public string TelNum { get; set; }

        [Required(ErrorMessage = "The User ID is required")]
        public int UserID { get; set; }

        [Column(TypeName = "bit")]
        public bool? ValidLicense { get; set; }

        [Required(ErrorMessage = "The Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Email Address")]
        public string WorkEmail { get; set; }

        [Required(ErrorMessage = "The Work Number is required")]
        [Phone(ErrorMessage = "Invalid Work Phone Number format")]
        public string WorkNum { get; set; }

        public string UserPicture { get; set; }

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