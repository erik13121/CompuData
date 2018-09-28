using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CompuData.Models
{
    public class Building
    {
        public int BuildingID { get; set; }

        [Required(ErrorMessage = "The Building Name is required")]
        [RegularExpression("^[a-zA-Z0-9- ]*$", ErrorMessage = "No special characters are allowed.")]
        [MaxLength(30, ErrorMessage = "Max of 30 characters allowed as the Building Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Street Address is required")]
        [RegularExpression("^[a-zA-Z1-9 ]*$", ErrorMessage = "No special characters are allowed.")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Street Address")]
        public string StreetAddress { get; set; }

        [Required(ErrorMessage = "The City Name is required")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only Text and spaces are allowed. No numbers or special characters are allowed.")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the City")]
        public string City { get; set; }

        [Required(ErrorMessage = "The Area Code is required")]
        [RegularExpression("\\d{4}", ErrorMessage = "The Area Code must consist of 4 numbers in the format (xxxx)")]
        [Range(0, 9999)]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Area Code")]
        public string AreaCode { get; set; }

        public string JavaScriptToRun { get; set; }

        public Building() { }
        public Building(int id, string name, string address, string city, string area)
        {
            BuildingID = id;
            Name = name;
            StreetAddress = address;
            City = city;
            AreaCode = area;
        }

        public static IEnumerable<CodeFirst.Building> Data;
        public static IEnumerable<CodeFirst.Building> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var buildings = db.Buildings.ToList();

            Data = buildings;
            return Data;
        }
    }
}