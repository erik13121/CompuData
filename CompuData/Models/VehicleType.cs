using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CompuData.Models
{
    public class VehicleType
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TypeID { get; set; }

        [Required(ErrorMessage = "The Vehicle Type Name is required")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "No numbers or special characters are allowed.")]
        [DataType(DataType.Text)]
        [MaxLength(30, ErrorMessage = "Name must be shorter than 30 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Description is required")]
        [RegularExpression("^[a-zA-Z0-9- ]*$", ErrorMessage = "No special characters are allowed.")]
        [DataType(DataType.Text)]
        [MaxLength(30, ErrorMessage = "Description must be shorter than 30 characters")]
        public string Description { get; set; }

        public string JavaScriptToRun { get; set; }

        public VehicleType() { }
        public VehicleType(int id, string name, string description)
        {
            TypeID = id;
            Name = name;
            Description = description;
        }

        public static IEnumerable<CodeFirst.Vehicle_Type> Data;
        public static IEnumerable<CodeFirst.Vehicle_Type> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            var vehicleTypes = db.Vehicle_Type.ToList();
            
            Data = vehicleTypes;
            return Data;
        }
    }
}