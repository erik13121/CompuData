USE [master]
GO

DROP DATABASE CompuDataSQL
GO

/****** Object:  Database [CompuDataSQL]    Script Date: 2018/08/17 23:16:37 ******/
CREATE DATABASE [CompuDataSQL]
-- CONTAINMENT = NONE
-- ON  PRIMARY 
--( NAME = N'CompuDataSQL', FILENAME = N'F:\Program Files (x86)\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\CompuDataSQL.mdf' , SIZE = 4288KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
-- LOG ON 
--( NAME = N'CompuDataSQL_log', FILENAME = N'F:\Program Files (x86)\Microsoft SQL Server\MSSQL12.MSSQLSERVER\MSSQL\DATA\CompuDataSQL_log.ldf' , SIZE = 816KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [CompuDataSQL] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CompuDataSQL].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CompuDataSQL] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CompuDataSQL] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CompuDataSQL] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CompuDataSQL] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CompuDataSQL] SET ARITHABORT OFF 
GO
ALTER DATABASE [CompuDataSQL] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CompuDataSQL] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CompuDataSQL] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CompuDataSQL] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CompuDataSQL] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CompuDataSQL] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CompuDataSQL] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CompuDataSQL] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CompuDataSQL] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CompuDataSQL] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CompuDataSQL] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CompuDataSQL] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CompuDataSQL] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CompuDataSQL] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CompuDataSQL] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CompuDataSQL] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CompuDataSQL] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CompuDataSQL] SET RECOVERY FULL 
GO
ALTER DATABASE [CompuDataSQL] SET  MULTI_USER 
GO
ALTER DATABASE [CompuDataSQL] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CompuDataSQL] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CompuDataSQL] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CompuDataSQL] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [CompuDataSQL] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'CompuDataSQL', N'ON'
GO
USE [CompuDataSQL]
GO
/****** Object:  Table [dbo].[Access_Level]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Access_Level](
	[AccessLevelID] [int] NOT NULL,
	[LevelName] [varchar](50) NOT NULL,
	[LevelDescription] [varchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[AccessLevelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Active_Login]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Active_Login](
	[LoginID] [int] NOT NULL,
	[IPAddress] [nvarchar](20) NOT NULL,
	[LoginTimestamp] [datetime] NULL,
	[LatestActionTimestamp] [time](7) NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_Active_Login] PRIMARY KEY CLUSTERED 
(
	[LoginID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Audit_Log]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Audit_Log](
	[LogID] [int] NOT NULL,
	[TableEffected] [varchar](50) NOT NULL,
	[AttrivuteEffected] [varchar](50) NOT NULL,
	[Time] [time](7) NOT NULL,
	[Date] [date] NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[NewRecordValue] [varchar](50) NOT NULL,
	[UserID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Booking_Refreshment_Line]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Booking_Refreshment_Line](
	[Quantity] [int] NOT NULL,
	[UnitCost] [float] NOT NULL,
	[RefreshmentID] [int] NOT NULL,
	[RefreshBookingID] [int] NOT NULL,
	[BookingID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
	[VenueID] [int] NOT NULL,
	[BuildingID] [int] NOT NULL,
	[VenueBookingID] [int] NOT NULL,
 CONSTRAINT [PK_Booking_Refreshment_Line] PRIMARY KEY CLUSTERED 
(
	[RefreshmentID] ASC,
	[RefreshBookingID] ASC,
	[BookingID] ASC,
	[UserID] ASC,
	[VenueID] ASC,
	[BuildingID] ASC,
	[VenueBookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Building]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Building](
	[BuildingID] [int] NOT NULL,
	[Name] [varchar](30) NOT NULL,
	[PhysicalAddress] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[BuildingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Donation]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Donation](
	[DonationID] [int] NOT NULL,
	[QuantityAmount] [varchar](25) NOT NULL,
	[DateDate] [date] NOT NULL,
	[QuatityTypeID] [int] NOT NULL,
	[DonorPID] [int] NULL,
	[DonorOrgID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[DonationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Donation_Item]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Donation_Item](
	[DonationItemID] [int] NOT NULL,
	[Description] [varchar](150) NOT NULL,
	[TypeID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Donation_Line]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Donation_Line](
	[Description] [varchar](50) NOT NULL,
	[TypeID] [int] NOT NULL,
	[DonationID] [int] NOT NULL,
	[ProjectID] [int] NULL,
 CONSTRAINT [PK_Donation_Line] PRIMARY KEY CLUSTERED 
(
	[TypeID] ASC,
	[DonationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Donation_Type]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Donation_Type](
	[TypeID] [int] NOT NULL,
	[TypeName] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Donor_Org]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Donor_Org](
	[DonorOrgID] [int] NOT NULL,
	[OrgName] [varchar](50) NULL,
	[ContactNum] [varchar](50) NULL,
	[ContactEmail] [varchar](50) NULL,
	[Thanked] [bit] NULL,
	[StreetAddress] [varchar](50) null,
	[City] [varchar](50) null,
	[AreaCode] [varchar](50) null,
PRIMARY KEY CLUSTERED 
(
	[DonorOrgID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Donor_Person]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Donor_Person](
	[DonorPID] [int] NOT NULL,
	[FirstName] [varchar](50) NULL,
	[MiddleName] [varchar](50) NULL,
	[SecondName] [varchar](50) NULL,
	[Initials] [varchar](10) NULL,
	[CellNum] [varchar](50) NULL,
	[PersonalEmail] [varchar](50) NULL,
	[StreetAddress] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[AreaCode] [varchar](50) NULL,
	[Thanked] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[DonorPID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EFT_Requisition]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EFT_Requisition](
	[RequisitionID] [int] NOT NULL,
	[ApprovedProjectManger] [bit] NOT NULL,
	[ApprovedCEO] [bit] NOT NULL,
	[ReceiptFile] [varchar](50) NOT NULL,
	[VATInclusive] [bit] NOT NULL,
	[Date] [date] NOT NULL,
	[SupplierID] [int] NOT NULL,
	[ProjectID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_EFT_Requisition] PRIMARY KEY CLUSTERED 
(
	[RequisitionID] ASC,
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[EFT_Requisition_Line]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[EFT_Requisition_Line](
	[LineID] [int] NOT NULL,
	[Details] [varchar](50) NULL,
	[QuantityEFT] [int] NULL,
	[UnitPriceEFT] [float] NULL,
	[RequisitionID] [int] NOT NULL,
	[SupplierID] [int] NOT NULL,
 CONSTRAINT [PK_EFT_Requisition_Line] PRIMARY KEY CLUSTERED 
(
	[LineID] ASC,
	[RequisitionID] ASC,
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Employee_Title]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Employee_Title](
	[JobTitleID] [int] NOT NULL,
	[TitleName] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[JobTitleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Equipment]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Equipment](
	[EquipmentID] [int] NOT NULL,
	[ManufacturerName] [varchar](50) NOT NULL,
	[ModelNumber] [varchar](50) NOT NULL,
	[DatePurchased] [date] NOT NULL,
	[ServiceIntervalMonths] [int] NOT NULL,
	[Status] [varchar](50) NOT NULL,
	[UserID] [int] NULL,
	[TypeID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EquipmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Equipment_Booking]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equipment_Booking](
	[EquipmentBookingID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[EquipmentBookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Equipment_Booking_Line]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equipment_Booking_Line](
	[BookingID] [int] NOT NULL,
	[EquipmentID] [int] NOT NULL,
	[PagesPrinted] [int] NULL,
	[DateBooked] [date] NOT NULL,
	[TimeIn] [time](7) NOT NULL,
	[TimeOut] [time](7) NOT NULL,
	[EquipmentBookingID] [int] NOT NULL,
	[ProjectID] [int] NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_Equipment_Booking_Line] PRIMARY KEY CLUSTERED 
(
	[BookingID] ASC,
	[EquipmentID] ASC,
	[EquipmentBookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Equipment_Schedule_Line]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Equipment_Schedule_Line](
	[LineID] [int] NOT NULL,
	[EquipmentID] [int] NOT NULL,
	[Date] [date] NOT NULL,
	[TimeStart] [time](7) NOT NULL,
	[TimeEnd] [time](7) NOT NULL,
 CONSTRAINT [PK_Equipment_Schedule_Line] PRIMARY KEY CLUSTERED 
(
	[LineID] ASC,
	[EquipmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Equipment_Type]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Equipment_Type](
	[TypeID] [int] NOT NULL,
	[TypeName] [varchar](50) NOT NULL,
	[TypeDescription] [varchar](50) NOT NULL,
	[Removable] [bit] NOT NULL
PRIMARY KEY CLUSTERED 
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Funder_Org]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Funder_Org](
	[FunderOrgID] [int] NOT NULL,
	[OrgName] [varchar](50) NULL,
	[ContactNumber] [varchar](50) NULL,
	[EmailAddress] [varchar](50) NULL,
	[Bank] [varchar](50) NULL,
	[AccountNumber] [varchar](50) NULL,
	[BranchCode] [varchar](50) NULL,
	[StreetAddress] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[AreaCode] [varchar](50) NULL,
	[Thanked] [bit] NULL,
	[ProjectID] [int] NULL,
	[TypeID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FunderOrgID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Funder_Person]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Funder_Person](
	[FunderPersonID] [int] NOT NULL,
	[FirstName] [varchar](50) NULL,
	[MiddleName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[Initials] [varchar](10) NULL,
	[CellNum] [varchar](50) NULL,
	[PersonalEmail] [varchar](50) NULL,
	[Bank] [varchar](50) NULL,
	[AccountNumber] [varchar](50) NULL,
	[BranchCode] [varchar](50) NULL,
	[StreetAddress] [varchar](50) NULL,
	[City] [varchar](50) NULL,
	[AreaCode] [varchar](50) NULL,
	[Thanked] [bit] NULL,
	[ProjectID] [int] NULL,
	[TypeID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[FunderPersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Funder_Type]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Funder_Type](
	[TypeID] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Description] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Petty_Cash_Requisition]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Petty_Cash_Requisition](
	[RequisitionID] [int] NOT NULL,
	[ApprovalStatus] [varchar](50) NOT NULL,
	[VATInclusive] [bit] NULL,
	[ReqDate] [date] NOT NULL,
	[SupplierID] [int] NOT NULL,
	[ProjectID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_Petty_Cash_Requisition] PRIMARY KEY CLUSTERED 
(
	[RequisitionID] ASC,
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Petty_Cash_Requisition_Line]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Petty_Cash_Requisition_Line](
	[LineID] [int] NOT NULL,
	[Details] [varchar](50) NULL,
	[Quantity] [int] NOT NULL,
	[UnitPrice] [float] NULL,
	[RequisitionID] [int] NOT NULL,
	[SupplierID] [int] NULL,
 CONSTRAINT [PK_Petty_Cash_Requisition_Line] PRIMARY KEY CLUSTERED 
(
	[LineID] ASC,
	[RequisitionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Project]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Project](
	[ProjectID] [int] NOT NULL,
	[ProjectName] [varchar](50) NOT NULL,
	[StartDate] [date] NOT NULL,
	[ExpectedFinishDate] [date] NOT NULL,
	[Finished] [bit] NOT NULL,
	[ProjectDescription] [varchar](50) NOT NULL,
	[TypeID] [int] NOT NULL,
	[UserID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[ProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Project_Type]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Project_Type](
	[TypeID] [int] NOT NULL,
	[TypeName] [varchar](30) NOT NULL,
	[TypeDescription] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Quantity_Type]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Quantity_Type](
	[QuatityTypeID] [int] NOT NULL,
	[Description] [varchar](50) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[QuatityTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Refreshment]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Refreshment](
	[RefreshmentID] [int] NOT NULL,
	[Name] [varchar](30) NOT NULL,
	[Description] [varchar](50) NOT NULL,
	[UnitPrice] [money] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RefreshmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Refreshment_Booking]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Refreshment_Booking](
	[RefreshBookingID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RefreshBookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Repair_Request]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Repair_Request](
	[RequestID] [int] NOT NULL,
	[Approved] [bit] NOT NULL,
	[Repaired] [bit] NOT NULL,
	[Reason] [varchar](50) NOT NULL,
	[VehicleID] [int] NULL,
	[EquipmentID] [int] NULL,
	[RepPersonID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RequestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[RepairPerson]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[RepairPerson](
	[RepPersonID] [int] NOT NULL,
	[Name] [varchar](30) NOT NULL,
	[EmailAddress] [varchar](50) NOT NULL,
	[Bank] [varchar](30) NOT NULL,
	[AccountNumber] [varchar](16) NOT NULL,
	[BranchCode] [varchar](10) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[RepPersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Service]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[IntervalID] [int] NOT NULL,
	[ServiceDate] [date] NOT NULL,
	[Completed] [bit] NOT NULL,
	[VehicleID] [int] NOT NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[IntervalID] ASC,
	[VehicleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Software_Licenses]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Software_Licenses](
	[LicenceID] [int] NOT NULL,
	[SoftwareName] [varchar](50) NULL,
	[ActivationPeriodInMonths] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[LicenceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Software_Licenses_Line]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Software_Licenses_Line](
	[LastActivatedDate] [date] NULL,
	[Activated] [bit] NOT NULL,
	[EquipmentID] [int] NOT NULL,
	[LicenceID] [int] NOT NULL,
 CONSTRAINT [PK_Software_Licenses_Line] PRIMARY KEY CLUSTERED 
(
	[EquipmentID] ASC,
	[LicenceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Supplier](
	[SupplierID] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[VATNumber] [int] NOT NULL,
	[POAddress] [varchar](50) NULL,
	[POCity] [varchar](50) NULL,
	[POAreaCode] [varchar](50) NULL,
	[EmailAddress] [varchar](50) NULL,
	[ContactNumber] [varchar](50) NULL,
	[Bank] [varchar](50) NOT NULL,
	[AccountNumber] [int] NOT NULL,
	[BranchCode] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[User]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[UserID] [int] NOT NULL,
	[FirstName] [varchar](50) NOT NULL,
	[MiddleName] [varchar](50) NULL,
	[LastName] [varchar](50) NOT NULL,
	[Initials] [varchar](10) NULL,
	[Password] [varchar](50) NOT NULL,
	[NationalID] [varchar](13) NOT NULL,
	[CellNum] [varchar](50) NULL,
	[TelNum] [varchar](50) NOT NULL,
	[WorkNum] [varchar](50) NOT NULL,
	[PersonalEmail] [varchar](50) NOT NULL,
	[WorkEmail] [varchar](50) NOT NULL,
	[StreetAddress] [varchar](50) NOT NULL,
	[City] [varchar](50) NOT NULL,
	[AreaCode] [varchar](50) NOT NULL,
	[POAddress] [varchar](50) NULL,
	[POCity] [varchar](50) NULL,
	[POAreaCode] [varchar](50) NULL,
	[ValidLicense] [bit] NULL,
	[JobTitleID] [int] NOT NULL,
	[AccessLevelID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Vehicle_Booking]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Vehicle_Booking](
	[VehicleBookingID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[VehicleBookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Vehicle_Booking_Line]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Vehicle_Booking_Line](
	[VehicleID] [int] NOT NULL,
	[Reason] [varchar](40) NOT NULL,
	[ProjectID] [int] NOT NULL,
	[OdoEnd] [int] NULL,
	[DateBooked] [date] NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndTime] [time](7) NOT NULL,
	[VehicleBookingID] [int] NOT NULL,
	[IntervalID] [int] NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_Vehicle_Booking_Line] PRIMARY KEY CLUSTERED 
(
	[VehicleID] ASC,
	[ProjectID] ASC,
	[VehicleBookingID] ASC,
	[IntervalID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Vehicle_Schedule_Line]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Vehicle_Schedule_Line](
	[Veh_Schedule_ID] [int] NOT NULL,
	[Date] [varchar](25) NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndTime] [time](7) NOT NULL,
	[Status] [varchar](25) NOT NULL,
	[VehicleID] [int] NOT NULL,
 CONSTRAINT [PK_Vehicle_Schedule_Line] PRIMARY KEY CLUSTERED 
(
	[Veh_Schedule_ID] ASC,
	[VehicleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Vehicle_Type]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Vehicle_Type](
	[TypeID] [int] NOT NULL,
	[Name] [varchar](30) NOT NULL,
	[Description] [char](30) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Vehicles]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Vehicles](
	[VehicleID] [int] NOT NULL,
	[Brand] [varchar](50) NOT NULL,
	[Model] [varchar](50) NOT NULL,
	[NumberPlate] [varchar](50) NOT NULL,
	[DateOfPurchase] [date] NULL,
	[DateofLastRepair] [date] NULL,
	[DateofLicencePurchase] [date] NULL,
	[LicenseExpireDate] [date] NULL,
	[ServiceIntervalInMonths] [int] NULL,
	[ServiceIntervalInKMs] [int] NULL,
	[TypeID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[VehicleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Venue]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Venue](
	[VenueID] [int] NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[BuildingID] [int] NOT NULL,
 CONSTRAINT [PK_Venue] PRIMARY KEY CLUSTERED 
(
	[VenueID] ASC,
	[BuildingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Venue_Booking]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Venue_Booking](
	[VenueBookingID] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[VenueBookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Venue_Booking_Line]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Venue_Booking_Line](
	[BookingID] [int] NOT NULL,
	[NumberofPeople] [int] NULL,
	[DateBooked] [date] NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndTime] [time](7) NOT NULL,
	[UserID] [int] NOT NULL,
	[VenueID] [int] NOT NULL,
	[BuildingID] [int] NOT NULL,
	[VenueBookingID] [int] NOT NULL,
	[ProjectID] [int] NOT NULL,
 CONSTRAINT [PK_Venue_Booking_Line] PRIMARY KEY CLUSTERED 
(
	[BookingID] ASC,
	[UserID] ASC,
	[VenueID] ASC,
	[BuildingID] ASC,
	[VenueBookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Venue_Schedule_Line]    Script Date: 2018/08/17 23:16:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Venue_Schedule_Line](
	[ScheduleID] [int] NOT NULL,
	[BuildingID] [int] NOT NULL,
	[StartTime] [time](7) NOT NULL,
	[EndTime] [time](7) NOT NULL,
	[DateAvailable] [date] NOT NULL,
	[VenueID] [int] NOT NULL,
 CONSTRAINT [PK_Venue_Schedule_Line] PRIMARY KEY CLUSTERED 
(
	[ScheduleID] ASC,
	[BuildingID] ASC,
	[VenueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Index [Access_Level_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Access_Level_PK] ON [dbo].[Access_Level]
(
	[AccessLevelID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [Active_Login_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Active_Login_PK] ON [dbo].[Active_Login]
(
	[LoginID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Audit_Log_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Audit_Log_PK] ON [dbo].[Audit_Log]
(
	[LogID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [Booking_Refreshment_Line_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Booking_Refreshment_Line_PK] ON [dbo].[Booking_Refreshment_Line]
(
	[RefreshmentID] ASC,
	[RefreshBookingID] ASC,
	[BookingID] ASC,
	[UserID] ASC,
	[VenueID] ASC,
	[BuildingID] ASC,
	[VenueBookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Building_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Building_PK] ON [dbo].[Building]
(
	[BuildingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Donation_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Donation_PK] ON [dbo].[Donation]
(
	[DonationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Donation_Item_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Donation_Item_PK] ON [dbo].[Donation_Item]
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Donation_Line_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Donation_Line_PK] ON [dbo].[Donation_Line]
(
	[TypeID] ASC,
	[DonationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Donation_Type_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Donation_Type_PK] ON [dbo].[Donation_Type]
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Donor_Org_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Donor_Org_PK] ON [dbo].[Donor_Org]
(
	[DonorOrgID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Donor_Person_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Donor_Person_PK] ON [dbo].[Donor_Person]
(
	[DonorPID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Internet_Requisition_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Internet_Requisition_PK] ON [dbo].[EFT_Requisition]
(
	[RequisitionID] ASC,
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Internet_Requisition_Line_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Internet_Requisition_Line_PK] ON [dbo].[EFT_Requisition_Line]
(
	[LineID] ASC,
	[RequisitionID] ASC,
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [Job_Title_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Job_Title_PK] ON [dbo].[Employee_Title]
(
	[JobTitleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Equipment_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Equipment_PK] ON [dbo].[Equipment]
(
	[EquipmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Equipment_Booking_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Equipment_Booking_PK] ON [dbo].[Equipment_Booking]
(
	[EquipmentBookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Equipment_Booking_Line_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Equipment_Booking_Line_PK] ON [dbo].[Equipment_Booking_Line]
(
	[BookingID] ASC,
	[EquipmentID] ASC,
	[EquipmentBookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Equipment_Schedule_Line_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Equipment_Schedule_Line_PK] ON [dbo].[Equipment_Schedule_Line]
(
	[LineID] ASC,
	[EquipmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Asset_Type_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Asset_Type_PK] ON [dbo].[Equipment_Type]
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Funder_Org_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Funder_Org_PK] ON [dbo].[Funder_Org]
(
	[FunderOrgID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Funder_Person_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Funder_Person_PK] ON [dbo].[Funder_Person]
(
	[FunderPersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Funder_Type_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Funder_Type_PK] ON [dbo].[Funder_Type]
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Petty_Cash_Requisition_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Petty_Cash_Requisition_PK] ON [dbo].[Petty_Cash_Requisition]
(
	[RequisitionID] ASC,
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Petty_Cash_Requisition_Line_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Petty_Cash_Requisition_Line_PK] ON [dbo].[Petty_Cash_Requisition_Line]
(
	[LineID] ASC,
	[RequisitionID] ASC,
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Project_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Project_PK] ON [dbo].[Project]
(
	[ProjectID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Project_Type_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Project_Type_PK] ON [dbo].[Project_Type]
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Quantity_Type_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Quantity_Type_PK] ON [dbo].[Quantity_Type]
(
	[QuatityTypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Refreshment_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Refreshment_PK] ON [dbo].[Refreshment]
(
	[RefreshmentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [Refreshment_Booking_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Refreshment_Booking_PK] ON [dbo].[Refreshment_Booking]
(
	[RefreshBookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Repair_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Repair_PK] ON [dbo].[Repair_Request]
(
	[RequestID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [RepairPersom_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [RepairPersom_PK] ON [dbo].[RepairPerson]
(
	[RepPersonID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Service_Intervals_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Service_Intervals_PK] ON [dbo].[Service]
(
	[IntervalID] ASC,
	[VehicleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Software_Licenses_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Software_Licenses_PK] ON [dbo].[Software_Licenses]
(
	[LicenceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Software_Licenses_Line_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Software_Licenses_Line_PK] ON [dbo].[Software_Licenses_Line]
(
	[EquipmentID] ASC,
	[LicenceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Supplier_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Supplier_PK] ON [dbo].[Supplier]
(
	[SupplierID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [User_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [User_PK] ON [dbo].[User]
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Vehicle_Booking_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Vehicle_Booking_PK] ON [dbo].[Vehicle_Booking]
(
	[VehicleBookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Vehicle_Booking_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Vehicle_Booking_PK] ON [dbo].[Vehicle_Booking_Line]
(
	[VehicleID] ASC,
	[ProjectID] ASC,
	[VehicleBookingID] ASC,
	[IntervalID] ASC,
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Vehicle_Schedule_Line_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Vehicle_Schedule_Line_PK] ON [dbo].[Vehicle_Schedule_Line]
(
	[Veh_Schedule_ID] ASC,
	[VehicleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Vehicle_Type_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Vehicle_Type_PK] ON [dbo].[Vehicle_Type]
(
	[TypeID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Vehicles_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Vehicles_PK] ON [dbo].[Vehicles]
(
	[VehicleID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [Venue_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Venue_PK] ON [dbo].[Venue]
(
	[VenueID] ASC,
	[BuildingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
/****** Object:  Index [Venue_Booking_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Venue_Booking_PK] ON [dbo].[Venue_Booking]
(
	[VenueBookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [Booking_Line_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Booking_Line_PK] ON [dbo].[Venue_Booking_Line]
(
	[BookingID] ASC,
	[UserID] ASC,
	[VenueID] ASC,
	[BuildingID] ASC,
	[VenueBookingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
SET ANSI_PADDING ON

GO
/****** Object:  Index [Venue_Schedule_Line_PK]    Script Date: 2018/08/17 23:16:38 ******/
CREATE UNIQUE NONCLUSTERED INDEX [Venue_Schedule_Line_PK] ON [dbo].[Venue_Schedule_Line]
(
	[ScheduleID] ASC,
	[BuildingID] ASC,
	[VenueID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Active_Login]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO

ALTER TABLE [dbo].[Audit_Log]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO

ALTER TABLE [dbo].[Booking_Refreshment_Line]  WITH CHECK ADD FOREIGN KEY([BuildingID])
REFERENCES [dbo].[Building] ([BuildingID])
GO
ALTER TABLE [dbo].[Booking_Refreshment_Line]  WITH CHECK ADD FOREIGN KEY([RefreshmentID])
REFERENCES [dbo].[Refreshment] ([RefreshmentID])
GO
ALTER TABLE [dbo].[Booking_Refreshment_Line]  WITH CHECK ADD FOREIGN KEY([RefreshBookingID])
REFERENCES [dbo].[Refreshment_Booking] ([RefreshBookingID])
GO

ALTER TABLE [dbo].[Booking_Refreshment_Line]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO

ALTER TABLE [dbo].[Booking_Refreshment_Line]  WITH CHECK ADD FOREIGN KEY([VenueBookingID])
REFERENCES [dbo].[Venue_Booking] ([VenueBookingID])
GO

ALTER TABLE [dbo].[Booking_Refreshment_Line]  WITH CHECK ADD FOREIGN KEY([BookingID], [UserID], [VenueID], [BuildingID], [VenueBookingID])
REFERENCES [dbo].[Venue_Booking_Line] ([BookingID], [UserID], [VenueID], [BuildingID], [VenueBookingID])
GO
ALTER TABLE [dbo].[Booking_Refreshment_Line]  WITH CHECK ADD FOREIGN KEY([VenueID], [BuildingID])
REFERENCES [dbo].[Venue] ([VenueID], [BuildingID])
GO

ALTER TABLE [dbo].[Donation]  WITH CHECK ADD FOREIGN KEY([DonorOrgID])
REFERENCES [dbo].[Donor_Org] ([DonorOrgID])
GO

ALTER TABLE [dbo].[Donation]  WITH CHECK ADD FOREIGN KEY([DonorPID])
REFERENCES [dbo].[Donor_Person] ([DonorPID])
GO

ALTER TABLE [dbo].[Donation]  WITH CHECK ADD FOREIGN KEY([QuatityTypeID])
REFERENCES [dbo].[Quantity_Type] ([QuatityTypeID])
GO

ALTER TABLE [dbo].[Donation_Item]  WITH CHECK ADD FOREIGN KEY([TypeID])
REFERENCES [dbo].[Donation_Type] ([TypeID])
GO

ALTER TABLE [dbo].[Donation_Line]  WITH CHECK ADD FOREIGN KEY([DonationID])
REFERENCES [dbo].[Donation] ([DonationID])
GO

ALTER TABLE [dbo].[Donation_Line]  WITH CHECK ADD FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO

ALTER TABLE [dbo].[Donation_Line]  WITH CHECK ADD FOREIGN KEY([TypeID])
REFERENCES [dbo].[Donation_Type] ([TypeID])
GO

ALTER TABLE [dbo].[EFT_Requisition]  WITH CHECK ADD FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO

ALTER TABLE [dbo].[EFT_Requisition]  WITH CHECK ADD FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Supplier] ([SupplierID])
GO

ALTER TABLE [dbo].[EFT_Requisition]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO

ALTER TABLE [dbo].[EFT_Requisition_Line]  WITH CHECK ADD FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Supplier] ([SupplierID])
GO

ALTER TABLE [dbo].[EFT_Requisition_Line]  WITH CHECK ADD FOREIGN KEY([RequisitionID], [SupplierID])
REFERENCES [dbo].[EFT_Requisition] ([RequisitionID], [SupplierID])
GO

ALTER TABLE [dbo].[Equipment]  WITH CHECK ADD FOREIGN KEY([TypeID])
REFERENCES [dbo].[Equipment_Type] ([TypeID])
GO

ALTER TABLE [dbo].[Equipment]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO

ALTER TABLE [dbo].[Equipment_Booking_Line]  WITH CHECK ADD FOREIGN KEY([EquipmentBookingID])
REFERENCES [dbo].[Equipment_Booking] ([EquipmentBookingID])
GO

ALTER TABLE [dbo].[Equipment_Booking_Line]  WITH CHECK ADD FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO

ALTER TABLE [dbo].[Equipment_Booking_Line]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO

ALTER TABLE [dbo].[Equipment_Schedule_Line]  WITH CHECK ADD FOREIGN KEY([EquipmentID])
REFERENCES [dbo].[Equipment] ([EquipmentID])
GO

ALTER TABLE [dbo].[Funder_Org]  WITH CHECK ADD FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO

ALTER TABLE [dbo].[Funder_Org]  WITH CHECK ADD FOREIGN KEY([TypeID])
REFERENCES [dbo].[Funder_Type] ([TypeID])
GO

ALTER TABLE [dbo].[Funder_Person]  WITH CHECK ADD FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO

ALTER TABLE [dbo].[Funder_Person]  WITH CHECK ADD FOREIGN KEY([TypeID])
REFERENCES [dbo].[Funder_Type] ([TypeID])
GO

ALTER TABLE [dbo].[Petty_Cash_Requisition]  WITH CHECK ADD FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO

ALTER TABLE [dbo].[Petty_Cash_Requisition]  WITH CHECK ADD FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Supplier] ([SupplierID])
GO

ALTER TABLE [dbo].[Petty_Cash_Requisition]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO

ALTER TABLE [dbo].[Petty_Cash_Requisition_Line]  WITH CHECK ADD FOREIGN KEY([SupplierID])
REFERENCES [dbo].[Supplier] ([SupplierID])
GO

ALTER TABLE [dbo].[Petty_Cash_Requisition_Line]  WITH CHECK ADD FOREIGN KEY([RequisitionID], [SupplierID])
REFERENCES [dbo].[Petty_Cash_Requisition] ([RequisitionID], [SupplierID])
GO

ALTER TABLE [dbo].[Project]  WITH CHECK ADD FOREIGN KEY([TypeID])
REFERENCES [dbo].[Project_Type] ([TypeID])
GO

ALTER TABLE [dbo].[Project]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO

ALTER TABLE [dbo].[Repair_Request]  WITH CHECK ADD FOREIGN KEY([EquipmentID])
REFERENCES [dbo].[Equipment] ([EquipmentID])
GO

ALTER TABLE [dbo].[Repair_Request]  WITH CHECK ADD FOREIGN KEY([RepPersonID])
REFERENCES [dbo].[RepairPerson] ([RepPersonID])
GO

ALTER TABLE [dbo].[Repair_Request]  WITH CHECK ADD FOREIGN KEY([VehicleID])
REFERENCES [dbo].[Vehicles] ([VehicleID])
GO

ALTER TABLE [dbo].[Service]  WITH CHECK ADD FOREIGN KEY([VehicleID])
REFERENCES [dbo].[Vehicles] ([VehicleID])
GO

ALTER TABLE [dbo].[Software_Licenses_Line]  WITH CHECK ADD FOREIGN KEY([EquipmentID])
REFERENCES [dbo].[Equipment] ([EquipmentID])
GO

ALTER TABLE [dbo].[Software_Licenses_Line]  WITH CHECK ADD FOREIGN KEY([LicenceID])
REFERENCES [dbo].[Software_Licenses] ([LicenceID])
GO

ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([AccessLevelID])
REFERENCES [dbo].[Access_Level] ([AccessLevelID])
GO

ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([JobTitleID])
REFERENCES [dbo].[Employee_Title] ([JobTitleID])
GO

ALTER TABLE [dbo].[Vehicle_Booking_Line]  WITH CHECK ADD FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO

ALTER TABLE [dbo].[Vehicle_Booking_Line]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO

ALTER TABLE [dbo].[Vehicle_Booking_Line]  WITH CHECK ADD FOREIGN KEY([VehicleBookingID])
REFERENCES [dbo].[Vehicle_Booking] ([VehicleBookingID])
GO

ALTER TABLE [dbo].[Vehicle_Booking_Line]  WITH CHECK ADD FOREIGN KEY([IntervalID], [VehicleID])
REFERENCES [dbo].[Service] ([IntervalID], [VehicleID])
GO

ALTER TABLE [dbo].[Vehicle_Schedule_Line]  WITH CHECK ADD FOREIGN KEY([VehicleID])
REFERENCES [dbo].[Vehicles] ([VehicleID])
GO

ALTER TABLE [dbo].[Vehicles]  WITH CHECK ADD FOREIGN KEY([TypeID])
REFERENCES [dbo].[Vehicle_Type] ([TypeID])
GO

ALTER TABLE [dbo].[Venue]  WITH CHECK ADD FOREIGN KEY([BuildingID])
REFERENCES [dbo].[Building] ([BuildingID])
GO

ALTER TABLE [dbo].[Venue_Booking_Line]  WITH CHECK ADD FOREIGN KEY([BuildingID])
REFERENCES [dbo].[Building] ([BuildingID])
GO

ALTER TABLE [dbo].[Venue_Booking_Line]  WITH CHECK ADD FOREIGN KEY([ProjectID])
REFERENCES [dbo].[Project] ([ProjectID])
GO

ALTER TABLE [dbo].[Venue_Booking_Line]  WITH CHECK ADD FOREIGN KEY([UserID])
REFERENCES [dbo].[User] ([UserID])
GO

ALTER TABLE [dbo].[Venue_Booking_Line]  WITH CHECK ADD FOREIGN KEY([VenueBookingID])
REFERENCES [dbo].[Venue_Booking] ([VenueBookingID])
GO

ALTER TABLE [dbo].[Venue_Booking_Line]  WITH CHECK ADD FOREIGN KEY([VenueID], [BuildingID])
REFERENCES [dbo].[Venue] ([VenueID], [BuildingID])
GO

ALTER TABLE [dbo].[Venue_Schedule_Line]  WITH CHECK ADD FOREIGN KEY([VenueID], [BuildingID])
REFERENCES [dbo].[Venue] ([VenueID], [BuildingID])
GO
USE [master]
GO
ALTER DATABASE [CompuDataSQL] SET  READ_WRITE 
GO
