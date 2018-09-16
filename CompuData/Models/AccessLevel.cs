using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompuData.Models
{
    public class AccessLevel
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int AccessLevelID { get; set; }

        [Required(ErrorMessage = "The Access Level Name is required")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only Text and spaces are allowed. No numbers or special characters allowed.")]
        [MaxLength(50)]
        public string LevelName { get; set; }

        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only Text and spaces are allowed. No numbers or special characters allowed.")]
        [MaxLength(50)]
        public string LevelDescription { get; set; }

        public string JavaScriptToRun { get; set; }

        public AccessLevel(){ }
        public AccessLevel(int id, string name, string description)
        {
            AccessLevelID = id;
            LevelName = name;
            LevelDescription = description;
        }

        public static IEnumerable<CodeFirst.Access_Level> Data;
        public static IEnumerable<CodeFirst.Access_Level> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var levels = db.Access_Level.ToList();

            Data = levels;
            return Data;
        }
    }
}