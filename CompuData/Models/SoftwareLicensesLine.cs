using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Models
{
    public class SoftwareLicensesLine
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LineID { get; set; }

        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? LastActivatedDate { get; set; }

        [Column(TypeName = "bit")]
        public bool Activated { get; set; }

        [Key]
        [Column(Order = 0)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int EquipmentID { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int LicenceID { get; set; }

        public string Manufacturer { get; set; }

        public string ModelNumber { get; set; }

        public string SoftwareName { get; set; }

        public IEnumerable<SelectListItem> Equipments { get; set; }

        public List<CodeFirst.Software_Licenses> Softwares { get; set; }

        public string JavaScriptToRun { get; set; }

        public SoftwareLicensesLine() { }
        public SoftwareLicensesLine(int ID, int equipID, DateTime lastActivatedDate, bool activated)
        {
            LicenceID = ID;
            EquipmentID = equipID;
            LastActivatedDate = lastActivatedDate;
            Activated = activated;
        }

        public static IEnumerable<CodeFirst.Software_Licenses_Line> Data;
        public static IEnumerable<CodeFirst.Software_Licenses_Line> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var lines = db.Software_Licenses_Line.ToList();

            Data = lines;
            return Data;
        }
    }
}