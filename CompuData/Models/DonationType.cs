using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompuData.Models
{
    public class DonationType
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeID { get; set; }

        [Required(ErrorMessage = "The Type Name is required")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only Text and spaces are allowed. No numbers or special characters allowed.")]
        [MaxLength(50, ErrorMessage = "Max of 50 characters allowed as the Donation Type Name")]
        public string TypeName { get; set; }

        public string JavaScriptToRun { get; set; }

        public DonationType() { }
        public DonationType(int id, string name)
        {
            TypeID = id;
            TypeName = name;
        }

        public static IEnumerable<CodeFirst.Donation_Type> Data;
        public static IEnumerable<CodeFirst.Donation_Type> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var types = db.Donation_Type.ToList();

            Data = types;
            return Data;
        }
    }
}