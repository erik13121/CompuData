using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompuData.Models
{
    public class FunderType
    {
        public int TypeID { get; set; }

        [Required(ErrorMessage = "The Funder Type Name is required")]
        [RegularExpression("^[a-zA-Z- ]*$", ErrorMessage = "Only Text and spaces are allowed. No numbers or special characters allowed.")]
        [MaxLength(50, ErrorMessage = "The Funder Type Name can be a maximum of 50 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Funder Type Description is required")]
        [RegularExpression("^[a-zA-Z- ]*$", ErrorMessage = "No numbers or special characters are allowed.")]
        [MaxLength(50, ErrorMessage = "The Funder Type Description can be a maximum of 50 characters")]
        public string Description { get; set; }

        public string JavaScriptToRun { get; set; }

        public FunderType() { }
        public FunderType(int id, string name, string description)
        {
            TypeID = id;
            Name = name;
            Description = description;
        }

        public static IEnumerable<CodeFirst.Funder_Type> Data;
        public static IEnumerable<CodeFirst.Funder_Type> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var types = db.Funder_Type.ToList();

            Data = types;
            return Data;
        }
    }
}