namespace CompuData.CodeFirst
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class CodeFirst : DbContext
    {
        public CodeFirst()
            : base("name=CodeFirst")
        {
            
        }

        public  DbSet<Access_Level> Access_Level { get; set; }
        public  DbSet<Active_Login> Active_Login { get; set; }
        public  DbSet<Audit_Log> Audit_Log { get; set; }
        public  DbSet<Booking_Refreshment_Line> Booking_Refreshment_Line { get; set; }
        public  DbSet<Building> Buildings { get; set; }
        public  DbSet<Donation> Donations { get; set; }
        public  DbSet<Donation_Item> Donation_Item { get; set; }
        public  DbSet<Donation_Line> Donation_Line { get; set; }
        public  DbSet<Donation_Type> Donation_Type { get; set; }
        public  DbSet<Donor_Org> Donor_Org { get; set; }
        public  DbSet<Donor_Person> Donor_Person { get; set; }
        public  DbSet<EFT_Requisition> EFT_Requisition { get; set; }
        public  DbSet<EFT_Requisition_Line> EFT_Requisition_Line { get; set; }
        public  DbSet<Employee_Title> Employee_Title { get; set; }
        public  DbSet<Equipment> Equipments { get; set; }
        public  DbSet<Equipment_Booking> Equipment_Booking { get; set; }
        public  DbSet<Equipment_Booking_Line> Equipment_Booking_Line { get; set; }
        public  DbSet<Equipment_Schedule_Line> Equipment_Schedule_Line { get; set; }
        public  DbSet<Equipment_Type> Equipment_Type { get; set; }
        public  DbSet<Funder_Org> Funder_Org { get; set; }
        public  DbSet<Funder_Person> Funder_Person { get; set; }
        public  DbSet<Funder_Type> Funder_Type { get; set; }
        public  DbSet<Petty_Cash_Requisition> Petty_Cash_Requisition { get; set; }
        public  DbSet<Petty_Cash_Requisition_Line> Petty_Cash_Requisition_Line { get; set; }
        public  DbSet<Project> Projects { get; set; }
        public  DbSet<Project_Type> Project_Type { get; set; }
        public  DbSet<Quantity_Type> Quantity_Type { get; set; }
        public  DbSet<Refreshment> Refreshments { get; set; }
        public  DbSet<Refreshment_Booking> Refreshment_Booking { get; set; }
        public  DbSet<Repair_Request> Repair_Request { get; set; }
        public  DbSet<RepairPerson> RepairPersons { get; set; }
        public  DbSet<Service> Services { get; set; }
        public  DbSet<Software_Licenses> Software_Licenses { get; set; }
        public  DbSet<Software_Licenses_Line> Software_Licenses_Line { get; set; }
        public  DbSet<Supplier> Suppliers { get; set; }
        public  DbSet<User> Users { get; set; }
        public  DbSet<Vehicle_Booking> Vehicle_Booking { get; set; }
        public  DbSet<Vehicle_Booking_Line> Vehicle_Booking_Line { get; set; }
        public  DbSet<Vehicle_Schedule_Line> Vehicle_Schedule_Line { get; set; }
        public  DbSet<Vehicle_Type> Vehicle_Type { get; set; }
        public  DbSet<Vehicle> Vehicles { get; set; }
        public  DbSet<Venue> Venues { get; set; }
        public  DbSet<Venue_Booking> Venue_Booking { get; set; }
        public  DbSet<Venue_Booking_Line> Venue_Booking_Line { get; set; }
        public  DbSet<Venue_Schedule_Line> Venue_Schedule_Line { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Access_Level>()
                .Property(e => e.LevelName)
                .IsUnicode(false);

            modelBuilder.Entity<Access_Level>()
                .Property(e => e.LevelDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Access_Level>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Access_Level)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Audit_Log>()
                .Property(e => e.TableEffected)
                .IsUnicode(false);

            modelBuilder.Entity<Audit_Log>()
                .Property(e => e.AttrivuteEffected)
                .IsUnicode(false);

            modelBuilder.Entity<Audit_Log>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Audit_Log>()
                .Property(e => e.NewRecordValue)
                .IsUnicode(false);

            modelBuilder.Entity<Booking_Refreshment_Line>()
                .Property(e => e.RefreshBookingID)
                .IsUnicode(false);

            modelBuilder.Entity<Building>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Building>()
                .Property(e => e.PhysicalAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Building>()
                .HasMany(e => e.Booking_Refreshment_Line)
                .WithRequired(e => e.Building)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Building>()
                .HasMany(e => e.Venues)
                .WithRequired(e => e.Building)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Building>()
                .HasMany(e => e.Venue_Booking_Line)
                .WithRequired(e => e.Building)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Donation>()
                .Property(e => e.QuantityAmount)
                .IsUnicode(false);

            modelBuilder.Entity<Donation>()
                .HasMany(e => e.Donation_Line)
                .WithRequired(e => e.Donation)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Donation_Item>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Donation_Line>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Donation_Type>()
                .Property(e => e.TypeName)
                .IsUnicode(false);

            modelBuilder.Entity<Donation_Type>()
                .HasOptional(e => e.Donation_Item)
                .WithRequired(e => e.Donation_Type);

            modelBuilder.Entity<Donation_Type>()
                .HasMany(e => e.Donation_Line)
                .WithRequired(e => e.Donation_Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Donor_Org>()
                .Property(e => e.OrgName)
                .IsUnicode(false);

            modelBuilder.Entity<Donor_Org>()
                .Property(e => e.ContactNum)
                .IsUnicode(false);

            modelBuilder.Entity<Donor_Org>()
                .Property(e => e.ContactEmail)
                .IsUnicode(false);

            modelBuilder.Entity<Donor_Org>()
                .Property(e => e.Thanked);
                //.HasPrecision(1, 0);

            modelBuilder.Entity<Donor_Person>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Donor_Person>()
                .Property(e => e.MiddleName)
                .IsUnicode(false);

            modelBuilder.Entity<Donor_Person>()
                .Property(e => e.SecondName)
                .IsUnicode(false);

            modelBuilder.Entity<Donor_Person>()
                .Property(e => e.Initials)
                .IsUnicode(false);

            modelBuilder.Entity<Donor_Person>()
                .Property(e => e.CellNum)
                .IsUnicode(false);

            modelBuilder.Entity<Donor_Person>()
                .Property(e => e.PersonalEmail)
                .IsUnicode(false);

            modelBuilder.Entity<Donor_Person>()
                .Property(e => e.Thanked);
            //.HasPrecision(1, 0);

            modelBuilder.Entity<EFT_Requisition>()
                .Property(e => e.ApprovedProjectManger);
            //.HasPrecision(1, 0);

            modelBuilder.Entity<EFT_Requisition>()
                .Property(e => e.ApprovedCEO);
                //.HasPrecision(1, 0);

            modelBuilder.Entity<EFT_Requisition>()
                .Property(e => e.ReceiptFile)
                .IsUnicode(false);

            modelBuilder.Entity<EFT_Requisition>()
                .Property(e => e.VATInclusive);
                //.HasPrecision(1, 0);

            modelBuilder.Entity<EFT_Requisition>()
                .HasMany(e => e.EFT_Requisition_Line)
                .WithRequired(e => e.EFT_Requisition)
                .HasForeignKey(e => new { e.RequisitionID, e.SupplierID })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<EFT_Requisition_Line>()
                .Property(e => e.Details)
                .IsUnicode(false);

            modelBuilder.Entity<Employee_Title>()
                .Property(e => e.TitleName)
                .IsUnicode(false);

            modelBuilder.Entity<Employee_Title>()
                .HasMany(e => e.Users)
                .WithRequired(e => e.Employee_Title)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Equipment>()
                .Property(e => e.ManufacturerName)
                .IsUnicode(false);

            modelBuilder.Entity<Equipment>()
                .Property(e => e.ModelNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Equipment>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Equipment>()
                .HasMany(e => e.Equipment_Schedule_Line)
                .WithRequired(e => e.Equipment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Equipment>()
                .HasMany(e => e.Software_Licenses_Line)
                .WithRequired(e => e.Equipment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Equipment_Booking>()
                .HasMany(e => e.Equipment_Booking_Line)
                .WithRequired(e => e.Equipment_Booking)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Equipment_Type>()
                .Property(e => e.TypeName)
                .IsUnicode(false);

            modelBuilder.Entity<Equipment_Type>()
                .Property(e => e.TypeDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Equipment_Type>()
                .Property(e => e.Removable);
                //.HasPrecision(1, 0);

            modelBuilder.Entity<Equipment_Type>()
                .HasMany(e => e.Equipments)
                .WithRequired(e => e.Equipment_Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Funder_Org>()
                .Property(e => e.OrgName)
                .IsUnicode(false);

            modelBuilder.Entity<Funder_Org>()
                .Property(e => e.ContactNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Funder_Org>()
                .Property(e => e.EmailAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Funder_Org>()
                .Property(e => e.Thanked);
                //.HasPrecision(1, 0);

            modelBuilder.Entity<Funder_Person>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<Funder_Person>()
                .Property(e => e.MiddleName)
                .IsUnicode(false);

            modelBuilder.Entity<Funder_Person>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<Funder_Person>()
                .Property(e => e.Initials)
                .IsUnicode(false);

            modelBuilder.Entity<Funder_Person>()
                .Property(e => e.PersonalEmail)
                .IsUnicode(false);

            modelBuilder.Entity<Funder_Person>()
                .Property(e => e.Thanked);
                //.HasPrecision(1, 0);

            modelBuilder.Entity<Funder_Type>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Funder_Type>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Funder_Type>()
                .HasMany(e => e.Funder_Org)
                .WithRequired(e => e.Funder_Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Funder_Type>()
                .HasMany(e => e.Funder_Person)
                .WithRequired(e => e.Funder_Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Petty_Cash_Requisition>()
                .Property(e => e.ApprovalStatus)
                .IsUnicode(false);

            modelBuilder.Entity<Petty_Cash_Requisition>()
                .Property(e => e.VATInclusive);
                //.HasPrecision(1, 0);

            modelBuilder.Entity<Petty_Cash_Requisition>()
                .HasMany(e => e.Petty_Cash_Requisition_Line)
                .WithRequired(e => e.Petty_Cash_Requisition)
                .HasForeignKey(e => new { e.RequisitionID, e.SupplierID })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Petty_Cash_Requisition_Line>()
                .Property(e => e.Details)
                .IsUnicode(false);

            modelBuilder.Entity<Project>()
                .Property(e => e.ProjectName)
                .IsUnicode(false);

            modelBuilder.Entity<Project>()
                .Property(e => e.Finished);
                //.HasPrecision(1, 0);

            modelBuilder.Entity<Project>()
                .Property(e => e.ProjectDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.EFT_Requisition)
                .WithRequired(e => e.Project)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.Petty_Cash_Requisition)
                .WithRequired(e => e.Project)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.Vehicle_Booking_Line)
                .WithRequired(e => e.Project)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project>()
                .HasMany(e => e.Venue_Booking_Line)
                .WithRequired(e => e.Project)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Project_Type>()
                .Property(e => e.TypeName)
                .IsUnicode(false);

            modelBuilder.Entity<Project_Type>()
                .Property(e => e.TypeDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Project_Type>()
                .HasMany(e => e.Projects)
                .WithRequired(e => e.Project_Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Quantity_Type>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Quantity_Type>()
                .HasMany(e => e.Donations)
                .WithRequired(e => e.Quantity_Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Refreshment>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Refreshment>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Refreshment>()
                .Property(e => e.UnitPrice)
                .HasPrecision(19, 4);

            modelBuilder.Entity<Refreshment>()
                .HasMany(e => e.Booking_Refreshment_Line)
                .WithRequired(e => e.Refreshment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Refreshment_Booking>()
                .HasMany(e => e.Booking_Refreshment_Line)
                .WithRequired(e => e.Refreshment_Booking)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Repair_Request>()
                .Property(e => e.Approved);
                //.HasPrecision(1, 0);

            modelBuilder.Entity<Repair_Request>()
                .Property(e => e.Repaired);
                //.HasPrecision(1, 0);

            modelBuilder.Entity<Repair_Request>()
                .Property(e => e.Reason)
                .IsUnicode(false);

            modelBuilder.Entity<RepairPerson>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<RepairPerson>()
                .Property(e => e.EmailAddress)
                .IsUnicode(false);

            modelBuilder.Entity<RepairPerson>()
                .Property(e => e.Bank)
                .IsUnicode(false);

            modelBuilder.Entity<RepairPerson>()
                .Property(e => e.AccountNumber)
                .IsUnicode(false);

            modelBuilder.Entity<RepairPerson>()
                .Property(e => e.BranchCode)
                .IsUnicode(false);

            modelBuilder.Entity<RepairPerson>()
                .HasMany(e => e.Repair_Request)
                .WithRequired(e => e.RepairPerson)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Service>()
                .Property(e => e.Completed);
                //.HasPrecision(1, 0);

            modelBuilder.Entity<Service>()
                .HasMany(e => e.Vehicle_Booking_Line)
                .WithRequired(e => e.Service)
                .HasForeignKey(e => new { e.VehicleID, e.IntervalID })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Software_Licenses>()
                .Property(e => e.SoftwareName)
                .IsUnicode(false);

            modelBuilder.Entity<Software_Licenses>()
                .HasMany(e => e.Software_Licenses_Line)
                .WithRequired(e => e.Software_Licenses)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Software_Licenses_Line>()
                .Property(e => e.Activated);
                //.HasPrecision(1, 0);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.POAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.POCity)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.POAreaCode)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.EmailAddress)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.ContactNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Bank)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.EFT_Requisition)
                .WithRequired(e => e.Supplier)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.EFT_Requisition_Line)
                .WithRequired(e => e.Supplier)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.Petty_Cash_Requisition)
                .WithRequired(e => e.Supplier)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.Petty_Cash_Requisition_Line)
                .WithRequired(e => e.Supplier)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .Property(e => e.FirstName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.MiddleName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.LastName)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Initials)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.PersonalEmail)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.WorkEmail)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.StreetAddress)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.City)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.AreaCode)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.POAddress)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.POCity)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.POCity)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.ValidLicense);
                //.HasPrecision(1, 0);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Active_Login)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Audit_Log)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Booking_Refreshment_Line)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.EFT_Requisition)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Equipment_Booking_Line)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Petty_Cash_Requisition)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Projects)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Vehicle_Booking_Line)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Venue_Booking_Line)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehicle_Booking>()
                .HasMany(e => e.Vehicle_Booking_Line)
                .WithRequired(e => e.Vehicle_Booking)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehicle_Booking_Line>()
                .Property(e => e.Reason)
                .IsUnicode(false);

            modelBuilder.Entity<Vehicle_Schedule_Line>()
                .Property(e => e.Date)
                .IsUnicode(false);

            modelBuilder.Entity<Vehicle_Schedule_Line>()
                .Property(e => e.Status)
                .IsUnicode(false);

            modelBuilder.Entity<Vehicle_Type>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Vehicle_Type>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Vehicle_Type>()
                .HasMany(e => e.Vehicles)
                .WithRequired(e => e.Vehicle_Type)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehicle>()
                .Property(e => e.Brand)
                .IsUnicode(false);

            modelBuilder.Entity<Vehicle>()
                .Property(e => e.Model)
                .IsUnicode(false);

            modelBuilder.Entity<Vehicle>()
                .Property(e => e.NumberPlate)
                .IsUnicode(false);

            modelBuilder.Entity<Vehicle>()
                .HasMany(e => e.Services)
                .WithRequired(e => e.Vehicle)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Vehicle>()
                .HasMany(e => e.Vehicle_Schedule_Line)
                .WithRequired(e => e.Vehicle)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Venue>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Venue>()
                .HasMany(e => e.Booking_Refreshment_Line)
                .WithRequired(e => e.Venue)
                .HasForeignKey(e => new { e.VenueID, e.BuildingID })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Venue>()
                .HasMany(e => e.Venue_Booking_Line)
                .WithRequired(e => e.Venue)
                .HasForeignKey(e => new { e.VenueID, e.BuildingID })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Venue>()
                .HasMany(e => e.Venue_Schedule_Line)
                .WithRequired(e => e.Venue)
                .HasForeignKey(e => new { e.VenueID, e.BuildingID })
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Venue_Booking>()
                .HasMany(e => e.Booking_Refreshment_Line)
                .WithRequired(e => e.Venue_Booking)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Venue_Booking>()
                .HasMany(e => e.Venue_Booking_Line)
                .WithRequired(e => e.Venue_Booking)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Venue_Booking_Line>()
                .HasMany(e => e.Booking_Refreshment_Line)
                .WithRequired(e => e.Venue_Booking_Line)
                .HasForeignKey(e => new { e.BookingID, e.UserID, e.VenueID, e.BuildingID, e.VenueBookingID })
                .WillCascadeOnDelete(false);
        }
    }
}
