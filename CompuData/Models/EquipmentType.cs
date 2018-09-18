using System.Collections.Generic;
using CompuData.CodeFirst;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompuData.Models
{
    public class EquipmentType
    {
        public int TypeID { get; set; }

        [Required(ErrorMessage = "An Equipment Type Name is required")]
        [MaxLength(50, ErrorMessage = "You have entered too many characters!")]
        public string TypeName { get; set; }

        [Required(ErrorMessage = "An Equipment Type Description is required")]
        [MaxLength(50, ErrorMessage = "You have entered too many characters!")]
        public string TypeDescription { get; set; }

        [Column(TypeName = "bit")]
        public bool Removable { get; set; }

        public string JavaScriptToRun { get; set; }
        public EquipmentType() { }
        public EquipmentType(int ID, string STypeName, string STypeDescription, bool SRemovable)
        {
            TypeID = ID;
            TypeName = STypeName;
            TypeDescription = STypeDescription;
            Removable = SRemovable;
            
        }

        public static IEnumerable<CodeFirst.Equipment_Type> Data;
        public static IEnumerable<CodeFirst.Equipment_Type> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var EquipType = db.Equipment_Type.ToList();

            Data = EquipType;
            return Data;
        }
    }
}