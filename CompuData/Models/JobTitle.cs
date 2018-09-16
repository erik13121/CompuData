using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompuData.Models
{
    public class JobTitle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int JobTitleID { get; set; }

        [Required(ErrorMessage = "The Job Title Name is required")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only Text and spaces are allowed. No numbers or special characters allowed.")]
        [MaxLength(50)]
        public string TitleName { get; set; }

        public string JavaScriptToRun { get; set; }

        public JobTitle() { }
        public JobTitle(int id, string title)
        {
            JobTitleID = id;
            TitleName = title;
        }

        public static IEnumerable<CodeFirst.Employee_Title> Data;
        public static IEnumerable<CodeFirst.Employee_Title> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var employeeTitles = db.Employee_Title.ToList();

            Data = employeeTitles;
            return Data;
        }
    }
}