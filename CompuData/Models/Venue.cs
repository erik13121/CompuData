using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CompuData.Models
{
    public class Venue
    {
        public int VenueID { get; set; }

        [Required(ErrorMessage = "The Venue Name is required")]
        [RegularExpression("^[a-zA-Z0-9- ]*$", ErrorMessage = "No special characters are allowed (Except for -).")]
        [MaxLength(50, ErrorMessage = "Max of 50 characters allowed as the Venue Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "The Building ID is required")]
        public int BuildingID { get; set; }

        public string BuildingName { get; set; }

        public List<CodeFirst.Building> Buildings { get; set; }

        public string JavaScriptToRun { get; set; }

        public Venue() { }
        public Venue(int id, string name, int buildingID)
        {
            VenueID = id;
            Name = name;
            BuildingID = buildingID;
        }

        public static IEnumerable<CodeFirst.Venue> Data;
        public static IEnumerable<CodeFirst.Venue> GetData()
        {
            var db = new CodeFirst.CodeFirst();

            db.Configuration.LazyLoadingEnabled = false;

            var venues = db.Venues.ToList();

            Data = venues;
            return Data;
        }
    }
}