using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompuData.Models
{
    public class QuantityType
    {
        public int QuatityTypeID { get; set; }

        [Required(ErrorMessage = "The Quantity Type Description is required")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only Text and spaces are allowed. No numbers or special characters allowed.")]
        [MaxLength(250, ErrorMessage = "Max of 250 characters allowed as the Quantity Type Name")]
        public string Description { get; set; }

        public string JavaScriptToRun { get; set; }

        public QuantityType() { }
        public QuantityType(int id, string Descrip)
        {
            QuatityTypeID = id;
            Description = Description;
        }
        public static IEnumerable<CodeFirst.Quantity_Type> Data;
        public static IEnumerable<CodeFirst.Quantity_Type> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var Quantitytypes = db.Quantity_Type.ToList();

            Data = Quantitytypes;
            return Data;
        }
    }
}