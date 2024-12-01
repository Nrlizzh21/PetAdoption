USE [master]
GO
/****** Object:  Database [PetAdoptionDB]    Script Date: 2/12/2024 5:59:30 AM ******/
CREATE DATABASE [PetAdoptionDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'PetAdoptionDB', FILENAME = N'D:\SQL Server 2019\MSSQL15.SQLEXPRESS\MSSQL\DATA\PetAdoptionDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'PetAdoptionDB_log', FILENAME = N'D:\SQL Server 2019\MSSQL15.SQLEXPRESS\MSSQL\DATA\PetAdoptionDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [PetAdoptionDB] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [PetAdoptionDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [PetAdoptionDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [PetAdoptionDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [PetAdoptionDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [PetAdoptionDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [PetAdoptionDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [PetAdoptionDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [PetAdoptionDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [PetAdoptionDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [PetAdoptionDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [PetAdoptionDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [PetAdoptionDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [PetAdoptionDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [PetAdoptionDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [PetAdoptionDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [PetAdoptionDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [PetAdoptionDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [PetAdoptionDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [PetAdoptionDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [PetAdoptionDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [PetAdoptionDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [PetAdoptionDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [PetAdoptionDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [PetAdoptionDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [PetAdoptionDB] SET  MULTI_USER 
GO
ALTER DATABASE [PetAdoptionDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [PetAdoptionDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [PetAdoptionDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [PetAdoptionDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [PetAdoptionDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [PetAdoptionDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [PetAdoptionDB] SET QUERY_STORE = OFF
GO
USE [PetAdoptionDB]
GO
/****** Object:  Table [dbo].[Adoptions]    Script Date: 2/12/2024 5:59:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Adoptions](
	[AdoptionID] [int] IDENTITY(1,1) NOT NULL,
	[UserID] [int] NOT NULL,
	[PetID] [int] NOT NULL,
	[Status] [nvarchar](50) NOT NULL,
	[RequestDate] [datetime2](7) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[PetName] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Adoptions] PRIMARY KEY CLUSTERED 
(
	[AdoptionID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pets]    Script Date: 2/12/2024 5:59:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pets](
	[PetID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Age] [nvarchar](50) NOT NULL,
	[Breed] [nvarchar](100) NOT NULL,
	[AdoptionStatus] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Pets] PRIMARY KEY CLUSTERED 
(
	[PetID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 2/12/2024 5:59:31 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[PhoneNumber] [nvarchar](50) NOT NULL,
	[Role] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Adoptions] ON 
GO
INSERT [dbo].[Adoptions] ([AdoptionID], [UserID], [PetID], [Status], [RequestDate], [UserName], [PetName]) VALUES (1, 1, 2, N'Pending', CAST(N'2024-11-22T14:59:12.4163916' AS DateTime2), N'Ahmad Firdaus', N'Bobby')
GO
INSERT [dbo].[Adoptions] ([AdoptionID], [UserID], [PetID], [Status], [RequestDate], [UserName], [PetName]) VALUES (4, 2, 1, N'Approved', CAST(N'2024-11-23T11:43:45.4749406' AS DateTime2), N'Rajesh Kumar', N'Comel')
GO
INSERT [dbo].[Adoptions] ([AdoptionID], [UserID], [PetID], [Status], [RequestDate], [UserName], [PetName]) VALUES (5, 3, 2, N'Approved', CAST(N'2024-11-29T07:28:09.2563157' AS DateTime2), N'Aiman', N'Bobby')
GO
INSERT [dbo].[Adoptions] ([AdoptionID], [UserID], [PetID], [Status], [RequestDate], [UserName], [PetName]) VALUES (6, 4, 7, N'Approved', CAST(N'2024-11-29T17:52:00.1899813' AS DateTime2), N'Hafiz Zaidi', N'Bella')
GO
INSERT [dbo].[Adoptions] ([AdoptionID], [UserID], [PetID], [Status], [RequestDate], [UserName], [PetName]) VALUES (7, 5, 6, N'Rejected', CAST(N'2024-11-29T17:52:16.9291001' AS DateTime2), N'Safarina', N'Putih')
GO
INSERT [dbo].[Adoptions] ([AdoptionID], [UserID], [PetID], [Status], [RequestDate], [UserName], [PetName]) VALUES (8, 5, 13, N'Approved', CAST(N'2024-11-30T01:04:57.8378190' AS DateTime2), N'Safarina', N'Oreo')
GO
INSERT [dbo].[Adoptions] ([AdoptionID], [UserID], [PetID], [Status], [RequestDate], [UserName], [PetName]) VALUES (9, 4, 11, N'Approved', CAST(N'2024-11-30T05:51:14.2915246' AS DateTime2), N'Hafiz Zaidi', N'Luna')
GO
INSERT [dbo].[Adoptions] ([AdoptionID], [UserID], [PetID], [Status], [RequestDate], [UserName], [PetName]) VALUES (10, 8, 8, N'Pending', CAST(N'2024-11-30T07:39:03.0949604' AS DateTime2), N'Ariff', N'Max')
GO
INSERT [dbo].[Adoptions] ([AdoptionID], [UserID], [PetID], [Status], [RequestDate], [UserName], [PetName]) VALUES (11, 7, 12, N'Approved', CAST(N'2024-11-30T12:58:40.1315714' AS DateTime2), N'Najwa', N'Milo')
GO
INSERT [dbo].[Adoptions] ([AdoptionID], [UserID], [PetID], [Status], [RequestDate], [UserName], [PetName]) VALUES (12, 6, 12, N'Rejected', CAST(N'2024-11-30T14:28:49.0244786' AS DateTime2), N'Nabila', N'Milo')
GO
INSERT [dbo].[Adoptions] ([AdoptionID], [UserID], [PetID], [Status], [RequestDate], [UserName], [PetName]) VALUES (14, 8, 10, N'Pending', CAST(N'2024-11-30T14:46:23.9744946' AS DateTime2), N'Ariff', N'Simba')
GO
INSERT [dbo].[Adoptions] ([AdoptionID], [UserID], [PetID], [Status], [RequestDate], [UserName], [PetName]) VALUES (15, 4, 6, N'Approved', CAST(N'2024-11-30T15:08:03.1028720' AS DateTime2), N'Hafiz Zaidi', N'Putih')
GO
INSERT [dbo].[Adoptions] ([AdoptionID], [UserID], [PetID], [Status], [RequestDate], [UserName], [PetName]) VALUES (16, 9, 17, N'Pending', CAST(N'2024-11-30T15:19:15.4250697' AS DateTime2), N'Karina', N'Belang')
GO
INSERT [dbo].[Adoptions] ([AdoptionID], [UserID], [PetID], [Status], [RequestDate], [UserName], [PetName]) VALUES (17, 7, 14, N'Pending', CAST(N'2024-11-30T15:41:03.0062521' AS DateTime2), N'Najwa', N'Coco')
GO
INSERT [dbo].[Adoptions] ([AdoptionID], [UserID], [PetID], [Status], [RequestDate], [UserName], [PetName]) VALUES (20, 2, 10, N'Pending', CAST(N'2024-12-01T12:27:39.1640251' AS DateTime2), N'Rajesh Kumar', N'Simba')
GO
INSERT [dbo].[Adoptions] ([AdoptionID], [UserID], [PetID], [Status], [RequestDate], [UserName], [PetName]) VALUES (26, 7, 15, N'Approved', CAST(N'2024-12-01T21:04:32.6904229' AS DateTime2), N'Najwa', N'Fluffy')
GO
SET IDENTITY_INSERT [dbo].[Adoptions] OFF
GO
SET IDENTITY_INSERT [dbo].[Pets] ON 
GO
INSERT [dbo].[Pets] ([PetID], [Name], [Age], [Breed], [AdoptionStatus]) VALUES (1, N'Comel', N'2 years', N'British Shorthair Cat ', N'Adopted')
GO
INSERT [dbo].[Pets] ([PetID], [Name], [Age], [Breed], [AdoptionStatus]) VALUES (6, N'Putih', N'2 years', N'Persian cat', N'Adopted')
GO
INSERT [dbo].[Pets] ([PetID], [Name], [Age], [Breed], [AdoptionStatus]) VALUES (7, N'Bella', N'3 years', N'Persian cat', N'Adopted')
GO
INSERT [dbo].[Pets] ([PetID], [Name], [Age], [Breed], [AdoptionStatus]) VALUES (8, N'Max', N'4 years', N'Labrador Retriever Dog', N'Available')
GO
INSERT [dbo].[Pets] ([PetID], [Name], [Age], [Breed], [AdoptionStatus]) VALUES (9, N'Daisy', N'2 years', N'Beagle Dog', N'Available')
GO
INSERT [dbo].[Pets] ([PetID], [Name], [Age], [Breed], [AdoptionStatus]) VALUES (10, N'Simba', N'5 years', N'British Shorthair Cat', N'Available')
GO
INSERT [dbo].[Pets] ([PetID], [Name], [Age], [Breed], [AdoptionStatus]) VALUES (12, N'Milo', N'3 years', N'Golden Retriever Dog', N'Adopted')
GO
INSERT [dbo].[Pets] ([PetID], [Name], [Age], [Breed], [AdoptionStatus]) VALUES (15, N'Fluffy', N'1 year	', N'Netherland Dwarf Rabbit', N'Adopted')
GO
INSERT [dbo].[Pets] ([PetID], [Name], [Age], [Breed], [AdoptionStatus]) VALUES (16, N'Snowball	', N'3 years', N'Flemish Giant Rabbit', N'Adopted')
GO
INSERT [dbo].[Pets] ([PetID], [Name], [Age], [Breed], [AdoptionStatus]) VALUES (19, N'BulusElla', N'4 years', N'Persian Cat', N'Adopted')
GO
INSERT [dbo].[Pets] ([PetID], [Name], [Age], [Breed], [AdoptionStatus]) VALUES (25, N'Mary', N'3 years', N'Persian cat', N'Available')
GO
INSERT [dbo].[Pets] ([PetID], [Name], [Age], [Breed], [AdoptionStatus]) VALUES (26, N'Marie', N'4 years', N'Persian Cat', N'Adopted')
GO
SET IDENTITY_INSERT [dbo].[Pets] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 
GO
INSERT [dbo].[Users] ([UserID], [Name], [Email], [PhoneNumber], [Role]) VALUES (1, N'Ahmad Firdaus', N'firdaus.ahmad@example.my', N'0123420789', N'Admin')
GO
INSERT [dbo].[Users] ([UserID], [Name], [Email], [PhoneNumber], [Role]) VALUES (2, N'Rajesh Kumar', N'rajesh.kumar@example.my', N'0102345329', N'Adopter')
GO
INSERT [dbo].[Users] ([UserID], [Name], [Email], [PhoneNumber], [Role]) VALUES (4, N'Hafiz Zaidi', N'hafiz2$.ahmad@example.my', N'0123457569', N'Adopter')
GO
INSERT [dbo].[Users] ([UserID], [Name], [Email], [PhoneNumber], [Role]) VALUES (5, N'Safarina', N'safAriiina@example.com', N'0145728202', N'Adopter')
GO
INSERT [dbo].[Users] ([UserID], [Name], [Email], [PhoneNumber], [Role]) VALUES (6, N'Nabila Alia', N'bella675@example.com', N'0165875789', N'Adopter')
GO
INSERT [dbo].[Users] ([UserID], [Name], [Email], [PhoneNumber], [Role]) VALUES (7, N'Najwa', N'wawa5&4@example.com', N'01167673488', N'Adopter')
GO
INSERT [dbo].[Users] ([UserID], [Name], [Email], [PhoneNumber], [Role]) VALUES (8, N'Ariff', N'ariff345@example.com', N'0136789044', N'Adopter')
GO
INSERT [dbo].[Users] ([UserID], [Name], [Email], [PhoneNumber], [Role]) VALUES (9, N'Karina', N'krin34@example.com', N'01123444332', N'Adopter')
GO
INSERT [dbo].[Users] ([UserID], [Name], [Email], [PhoneNumber], [Role]) VALUES (10, N'Aaron', N'aaron1&@example.com', N'01121679794', N'Adopter')
GO
INSERT [dbo].[Users] ([UserID], [Name], [Email], [PhoneNumber], [Role]) VALUES (12, N'Haris', N'haris45@example.com', N'0123445397', N'Adopter')
GO
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
USE [master]
GO
ALTER DATABASE [PetAdoptionDB] SET  READ_WRITE 
GO
