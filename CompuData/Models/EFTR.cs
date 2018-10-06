using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Models
{
    public class EFTR
    {
        public int RequisitionID { get; set; }

        [Column(TypeName = "bit")]
        public bool ApprovedProjectManger { get; set; }

        [Column(TypeName = "bit")]
        public bool ApprovedCEO { get; set; }

        [MaxLength(255)]
        public string ReceiptFile { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [RegularExpression("^[1-9]\\d{1,17}(\\.\\d{2}$)", ErrorMessage = "Total Amount must have between 1 and 18 characters and 2 decimal points")]
        public decimal? TotalAmount { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SupplierID { get; set; }

        [Required(ErrorMessage = "A Project is required")]
        public int ProjectID { get; set; }

        [Required(ErrorMessage = "A User is required")]
        public int UserID { get; set; }

        public string Name { get; set; }

        public string ProjectName { get; set; }

        public string Initials { get; set; }

        public string LastName { get; set; }

        public List<CodeFirst.Supplier> Suppliers { get; set; }

        public List<CodeFirst.Project> Projects { get; set; }

        public IEnumerable<SelectListItem> Users { get; set; }

        public List<CodeFirst.EFT_Requisition_Line> Lines { get; set; }

        public string JavaScriptToRun { get; set; }

        public EFTR() { }

        public EFTR(int ID, bool ApprovalCEO, bool ApprovedPM, DateTime reqDate, int SupID, int ProID, int UsersID)
        {
            RequisitionID = ID;
            ApprovedCEO = ApprovalCEO;
            ApprovedProjectManger = ApprovedPM;
            Date = reqDate;
            SupplierID = SupID;
            ProID = ProjectID;
            UsersID = UserID;
        }

        public static IEnumerable<CodeFirst.EFT_Requisition> Data;
        public static IEnumerable<CodeFirst.EFT_Requisition> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var EFTR = db.EFT_Requisition.ToList();

            Data = EFTR;
            return Data;
        }
    }
}