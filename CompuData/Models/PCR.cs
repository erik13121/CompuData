using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Models
{
    public class PCR
    {
        public int RequisitionID { get; set; }

        [Required]
        [MaxLength(50)]
        public string ApprovalStatus { get; set; }

        [Column(TypeName = "bit")]
        public bool? VATInclusive { get; set; }

        [Column(TypeName = "date")]
        public DateTime ReqDate { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SupplierID { get; set; }

        public string Name { get; set; }

        public int ProjectID { get; set; }

        public string ProjectName { get; set; }

        public int UserID { get; set; }

        public string Initials { get; set; }

        public string LastName { get; set; }

        public List<Supplier> Suppliers { get; set; }

        public List<Project> Projects { get; set; }

        public IEnumerable<SelectListItem> Users { get; set; }
    
        public string JavaScriptToRun { get; set; }

        public PCR() { }

        public PCR(int ID, string ApprovalStat, bool? VAT, DateTime Date, int SupID, int ProID, int UsersID)
        {
            RequisitionID = ID;
            ApprovalStatus = ApprovalStat;
            VATInclusive = VAT;
            ReqDate = Date;
            SupplierID = SupID;
            ProID = ProjectID;
            UsersID = UserID;        
        }

        public static IEnumerable<CodeFirst.Petty_Cash_Requisition> Data;
        public static IEnumerable<CodeFirst.Petty_Cash_Requisition> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var PCR = db.Petty_Cash_Requisition.ToList();

            Data = PCR;
            return Data;
        }
    }
}