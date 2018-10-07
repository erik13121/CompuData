using CompuData.CodeFirst;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompuData.Models
{
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int VehicleID { get; set; }

        [Required(ErrorMessage = "The Brand is required")]
        [RegularExpression("^[a-zA-Z- ]*$", ErrorMessage = "No numbers or special characters are allowed")]
        [DataType(DataType.Text)]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Brand Name")]
        public string Brand { get; set; }

        [Required(ErrorMessage = "The Model is required")]
        [RegularExpression("^[a-zA-Z0-9- ]*$", ErrorMessage = "No special characters are allowed")]
        [DataType(DataType.Text)]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Model")]
        public string Model { get; set; }

        [Required(ErrorMessage = "The Number Plate is required")]
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "No spaces or special characters are allowed!")]
        [DataType(DataType.Text)]
        [MaxLength(8, ErrorMessage = "You are only allowed up to 8 characters on a License Plate")]
        public string NumberPlate { get; set; }

        [Required(ErrorMessage = "The Date of Purchase is required")]
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateofPurchase { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateofLastRepair { get; set; }

        [Required(ErrorMessage = "The Date of Licence Purchase is required")]
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? DateofLicencePurchase { get; set; }

        [Required(ErrorMessage = "The License Expire Date is required")]
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? LicenseExpireDate { get; set; }

        [RegularExpression("\\d{1,2}", ErrorMessage = "The Service Interval Months has to consist of between 1 and 2-digit numbers")]
        public int? ServiceIntervalInMonths { get; set; }

        [RegularExpression("\\d{4,6}", ErrorMessage = "The KMs Service Interval has to consist of between 4 and 6-digit numbers")]
        public int? ServiceIntervalInKMs { get; set; }

        [Required(ErrorMessage = "Please Add a Vehicle Type first")]
        public int TypeID { get; set; }

        public string TypeName { get; set; }

        public string JavaScriptToRun { get; set; }

        public List<CodeFirst.Vehicle_Type> VehicleTypes { get; set; }

        public Vehicle() { }
        public Vehicle(int id, string brand, string model, string numberPlate, DateTime? purchase, DateTime? repair, DateTime? licensePurchase, DateTime? expire, int? kmsInterval, int? monthInterval, int typeID)
        {
            VehicleID = id;
            Brand = brand;
            Model = model;
            NumberPlate = numberPlate;
            DateofPurchase = purchase;
            DateofLastRepair = repair;
            DateofLicencePurchase = licensePurchase;
            LicenseExpireDate = expire;
            ServiceIntervalInMonths = kmsInterval;
            ServiceIntervalInKMs = monthInterval;
            TypeID = typeID;
        }

        public static IEnumerable<CodeFirst.Vehicle> Data;
        public static IEnumerable<CodeFirst.Vehicle> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var vehicles = db.Vehicles.ToList();

            Data = vehicles;
            return Data;
        }
    }
}