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
        [Required(ErrorMessage = "The Quanity Type ID is required")]
        public int QuantityTypeID { get; set; }

        [Required(ErrorMessage = "The Quantity Type Description is required")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Only Text and spaces are allowed. No numbers or special characters allowed.")]
        [MaxLength(250, ErrorMessage = "Max of 250 characters allowed as the Quantity Type Description")]
        public string Description { get; set; }

        public string JavaScriptToRun { get; set; }

        public QuantityType() { }
        public QuantityType(int quatityTypeID, string Descrip)
        {
            QuantityTypeID = quatityTypeID;
            Description = Descrip;
        }
        public static IEnumerable<CodeFirst.Quantity_Type> Data;
        public static IEnumerable<CodeFirst.Quantity_Type> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var quantityTypes = db.Quantity_Type.ToList();

            Data = quantityTypes;
            return Data;
        }
    }
}