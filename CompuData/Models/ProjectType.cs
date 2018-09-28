using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompuData.Models
{
    public class ProjectType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeID { get; set; }

        [Required(ErrorMessage = "The Project Type Name is required")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only Text and spaces are allowed. No numbers or special characters allowed.")]
        [MaxLength(30, ErrorMessage = "The Project Type Name can be a maximum of 30 characters")]
        public string TypeName { get; set; }

        [Required(ErrorMessage = "The Project Type Description is required")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only Text and spaces are allowed. No numbers or special characters allowed.")]
        [MaxLength(50, ErrorMessage = "The Project Type Description can be a maximum of 30 characters")]
        public string TypeDescription { get; set; }

        public string JavaScriptToRun { get; set; }

        public ProjectType() { }
        public ProjectType(int id, string name, string description)
        {
            TypeID = id;
            TypeName = name;
            TypeDescription = description;
        }

        public static IEnumerable<CodeFirst.Project_Type> Data;
        public static IEnumerable<CodeFirst.Project_Type> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var types = db.Project_Type.ToList();

            Data = types;
            return Data;
        }
    }
}