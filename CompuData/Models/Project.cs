using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CompuData.Models
{
    public class Project
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int ProjectID { get; set; }

        [Required(ErrorMessage = "The Project Name is required")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Project Name")]
        public string ProjectName { get; set; }

        [Required(ErrorMessage = "The Start Date is Required")]
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "The Expected Finish Date is Required")]
        [Column(TypeName = "date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ExpectedFinishDate { get; set; }

        [Column(TypeName = "bit")]
        public bool Finished { get; set; }

        [Required(ErrorMessage = "The Project Description is required")]
        [RegularExpression("^[a-zA-Z1-9- ]*$", ErrorMessage = "No special characters are allowed.")]
        [MaxLength(50, ErrorMessage = "You are only allowed up to 50 characters as the Project Description")]
        public string ProjectDescription { get; set; }

        [Required(ErrorMessage = "Please Add a Project Type first")]
        public int TypeID { get; set; }

        public string TypeName { get; set; }

        [Required(ErrorMessage = "Please Add a User first")]
        public int UserID { get; set; }

        public string Initials { get; set; }

        public string LastName { get; set; }

        public string JavaScriptToRun { get; set; }
        public List<CodeFirst.Project_Type> ProjectTypes { get; set; }

        public IEnumerable<SelectListItem> Users { get; set; }
        public Project() { }
        public Project(int id, string name, DateTime mStartDate, DateTime ExpFinishDate, bool mFinished, string ProDesc, int userID, int typeID)
        {
            ProjectID = id;
            ProjectName = name;
            StartDate = mStartDate;
            ExpectedFinishDate = ExpFinishDate;
            Finished = mFinished;
            ProjectDescription = ProDesc;
            TypeID = typeID;
            UserID = userID;
        }

        public static IEnumerable<CodeFirst.Project> Data;
        public static IEnumerable<CodeFirst.Project> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var projects = db.Projects.ToList();

            Data = projects;
            return Data;
        }
    }
}