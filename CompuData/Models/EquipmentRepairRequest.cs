using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Models
{
    public class EquipmentRepairRequest
    {
        [Key]
        [Required(ErrorMessage = "The Request ID is required")]
        public int RequestID { get; set; }

        [Column(TypeName = "bit")]
        public bool Approved { get; set; }

        [Column(TypeName = "bit")]
        public bool Repaired { get; set; }

        [Required(ErrorMessage = "The Reason for the Repair is required")]
        [MaxLength(50, ErrorMessage = "The Reason cannot be longer than 50 characters")]
        public string Reason { get; set; }

        public int? VehicleID { get; set; }

        public int? EquipmentID { get; set; }

        [Required(ErrorMessage = "Please Add a Repair Person first")]
        public int RepPersonID { get; set; }

        public string EquipmentName { get; set; }

        public string RepairPersonName { get; set; }

        public List<CodeFirst.RepairPerson> repairPeople { get; set; }

        public string JavaScriptToRun { get; set; }
        public EquipmentRepairRequest() { }
        public EquipmentRepairRequest(int id, bool approved, bool repaired, string reason, int? vehID, int? equipID, int repairPersonID)
        {
            RequestID = id;
            Approved = approved;
            Repaired = repaired;
            Reason = reason;
            VehicleID = VehicleID;
            EquipmentID = equipID;
            RepPersonID = repairPersonID;
        }

        public static IEnumerable<CodeFirst.Repair_Request> Data;
        public static IEnumerable<CodeFirst.Repair_Request> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var requests = db.Repair_Request.ToList();

            Data = requests;
            return Data;
        }
    }
}