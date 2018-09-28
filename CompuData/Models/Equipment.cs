using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Models
{
    public class Equipment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EquipmentID { get; set; }

        [Required(ErrorMessage = "The Manufacturer Name is required")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Manufacturer Name")]
        public string ManufacturerName { get; set; }

        [Required(ErrorMessage = "The Model Number is required")]
        [RegularExpression("^[a-zA-Z1-9- ]*$", ErrorMessage = "No special characters are allowed.")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Model Number")]
        public string ModelNumber { get; set; }

        [Required(ErrorMessage = "The Date Purchased is Required")]
        [Column(TypeName = "date")]
        public DateTime DatePurchased { get; set; }

        [Required(ErrorMessage = "The Service Interval in Months is Required")]
        [RegularExpression("\\d{1,2}", ErrorMessage = "The Service Interval Months has to consist of between 1 and 2-digit numbers")]
        public int ServiceIntervalMonths { get; set; }

        [Required(ErrorMessage = "The Status is required")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Status")]
        public string Status { get; set; }

        public int? UserID { get; set; }

        public string UserName { get; set; }

        [Required(ErrorMessage = "Please Add an Equipment Type first")]
        public int TypeID { get; set; }

        public string TypeName { get; set; }

        public string JavaScriptToRun { get; set; }
        public List<SelectListItem> Users { get; set; }
        public List<CodeFirst.Equipment_Type> EquipmentTypes { get; set; }
        public Equipment() { }
        public Equipment(int id, string name, string model, DateTime purchase, int monthInterval, string status, int? userID, int typeID)
        {
            EquipmentID = id;
            ManufacturerName = name;
            ModelNumber = model;
            DatePurchased = purchase;
            ServiceIntervalMonths = monthInterval;
            Status = status;
            UserID = userID;
            TypeID = typeID;
        }

        public static IEnumerable<CodeFirst.Equipment> Data;
        public static IEnumerable<CodeFirst.Equipment> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var equipments = db.Equipments.ToList();

            Data = equipments;
            return Data;
        }
    }
}