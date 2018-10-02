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

        [Required(ErrorMessage = "The Status is required")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Approval Status")]
        public string ApprovalStatus { get; set; }

        [Column(TypeName = "bit")]
        public bool VATInclusive { get; set; }

        [Required(ErrorMessage = "The Requisition Date is Required")]
        [Column(TypeName = "date")]
        public DateTime ReqDate { get; set; }

        [Required(ErrorMessage = "A Supplier is required")]
        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SupplierID { get; set; }

        public string Name { get; set; }

        [Required(ErrorMessage = "A Project is required")]
        public int ProjectID { get; set; }

        public string ProjectName { get; set; }

        [Required(ErrorMessage = "A User is required")]
        public int UserID { get; set; }

        public string Initials { get; set; }

        public string LastName { get; set; }

        [RegularExpression("^[1-9]\\d{1,17}(\\.\\d{2}$)", ErrorMessage = "Total Amount must have between 1 and 18 characters and 2 decimal points")]
        public decimal? TotalAmount { get; set; }

        public List<CodeFirst.Supplier> Suppliers { get; set; }

        public List<CodeFirst.Project> Projects { get; set; }

        public IEnumerable<SelectListItem> Users { get; set; }
    
        public string JavaScriptToRun { get; set; }

        public PCR() { }

        public PCR(int ID, string ApprovalStat, bool VAT, DateTime Date, int SupID, int ProID, int UsersID)
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