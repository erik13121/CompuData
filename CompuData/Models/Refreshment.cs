using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompuData.Models
{
    public class Refreshment
    {
        public int RefreshmentID { get; set; }

        [Required(ErrorMessage = "A Refreshment Name is required")]
        [RegularExpression("^[a-zA-Z1-9- ]*$", ErrorMessage = "No special characters are allowed.")]
        [MaxLength(30, ErrorMessage = "Max allowed characters for Refreshment Name is 30")]
        public string Name { get; set; }

        [Required(ErrorMessage = "A Refreshment Description is required")]
        [RegularExpression("^[a-zA-Z1-9- ]*$", ErrorMessage = "No special characters are allowed.")]
        [MaxLength(30, ErrorMessage = "Max allowed characters for Refreshment Description is 30")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Unit Price is required")]
        [Range(0.1, 99999)]
        //[RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid unit price. Please enter an amount followed by 2 decimal points with a .")]
        public decimal UnitPrice { get; set; }

        public string JavaScriptToRun { get; set; }
        public Refreshment() { }
        public Refreshment(int ID, string SName, string Descrip, decimal UnitP)
        {
            RefreshmentID = ID;
            Name = SName;
            Description = Descrip;
            UnitPrice = UnitP;
        }

        public static IEnumerable<CodeFirst.Refreshment> Data;
        public static IEnumerable<CodeFirst.Refreshment> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var Refreshments = db.Refreshments.ToList();

            Data = Refreshments;
            return Data;
        }
    }
}