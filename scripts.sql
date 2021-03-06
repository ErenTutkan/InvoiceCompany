USE [master]
GO
/****** Object:  Database [InvoiceWebsite]    Script Date: 1.09.2021 15:23:38 ******/
CREATE DATABASE [InvoiceWebsite]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'InvoiceWebsite', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\InvoiceWebsite.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'InvoiceWebsite_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\InvoiceWebsite_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [InvoiceWebsite] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [InvoiceWebsite].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [InvoiceWebsite] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [InvoiceWebsite] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [InvoiceWebsite] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [InvoiceWebsite] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [InvoiceWebsite] SET ARITHABORT OFF 
GO
ALTER DATABASE [InvoiceWebsite] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [InvoiceWebsite] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [InvoiceWebsite] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [InvoiceWebsite] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [InvoiceWebsite] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [InvoiceWebsite] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [InvoiceWebsite] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [InvoiceWebsite] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [InvoiceWebsite] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [InvoiceWebsite] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [InvoiceWebsite] SET  DISABLE_BROKER 
GO
ALTER DATABASE [InvoiceWebsite] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [InvoiceWebsite] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [InvoiceWebsite] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [InvoiceWebsite] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [InvoiceWebsite] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [InvoiceWebsite] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [InvoiceWebsite] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [InvoiceWebsite] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [InvoiceWebsite] SET  MULTI_USER 
GO
ALTER DATABASE [InvoiceWebsite] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [InvoiceWebsite] SET DB_CHAINING OFF 
GO
ALTER DATABASE [InvoiceWebsite] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [InvoiceWebsite] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [InvoiceWebsite]
GO
/****** Object:  Table [dbo].[Casing]    Script Date: 1.09.2021 15:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Casing](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [date] NULL,
	[cash] [float] NULL,
 CONSTRAINT [PK_Casing] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CasingTransaction]    Script Date: 1.09.2021 15:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CasingTransaction](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[Name] [nvarchar](50) NULL,
	[TransactionType] [int] NULL,
	[Description] [text] NULL,
	[Create_At] [int] NULL,
	[Paid] [float] NULL,
 CONSTRAINT [PK_CasingTransaction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Companies]    Script Date: 1.09.2021 15:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Companies](
	[ID] [int] IDENTITY(186,264) NOT NULL,
	[CompanyName] [nvarchar](50) NULL,
	[CompanyDescription] [nvarchar](300) NULL,
	[CompanyTelNo] [nvarchar](50) NULL,
	[CompanyEmail] [nvarchar](150) NULL,
	[CompanyAddress] [nvarchar](350) NULL,
	[CompanyType] [int] NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteDate] [datetime] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_Companies] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CompanyTransactions]    Script Date: 1.09.2021 15:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CompanyTransactions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[CompanyID] [int] NULL,
	[Cash] [float] NULL,
 CONSTRAINT [PK_CompanyTransaction] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Invoices]    Script Date: 1.09.2021 15:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoices](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[CompanyID] [int] NULL,
	[ProductId] [int] NULL,
	[InvoiceType] [int] NULL,
	[Weight] [float] NULL,
	[UnitPrice] [float] NULL,
	[Amount] [float] NULL,
	[Description] [text] NULL,
	[Paid] [float] NULL,
	[Create_At] [int] NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteDate] [datetime] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[OfficialInvoices]    Script Date: 1.09.2021 15:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OfficialInvoices](
	[ID] [int] IDENTITY(375,246) NOT NULL,
	[CompanyID] [int] NULL,
	[Date] [datetime] NULL,
	[WaybillNo] [nvarchar](50) NULL,
	[InvoiceNo] [nvarchar](50) NULL,
	[Amount] [float] NULL,
	[Description] [nvarchar](500) NULL,
	[Paid] [float] NULL,
	[Create_At] [int] NULL,
	[entry_printout] [int] NULL,
	[CreateDate] [datetime] NULL,
	[UpdateDate] [datetime] NULL,
	[DeleteDate] [datetime] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_OfficialInvoice] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Products]    Script Date: 1.09.2021 15:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](50) NULL,
	[Stock] [float] NULL,
 CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Roles]    Script Date: 1.09.2021 15:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SystemUser]    Script Date: 1.09.2021 15:23:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SystemUser](
	[ID] [int] IDENTITY(169,375) NOT NULL,
	[Username] [nvarchar](50) NULL,
	[Password] [nvarchar](150) NULL,
	[RoleID] [int] NULL,
	[Create_Date] [datetime] NULL,
	[Update_Date] [datetime] NULL,
	[Delete_Date] [datetime] NULL,
	[Status] [int] NULL,
 CONSTRAINT [PK_SystemUser] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[CasingTransaction]  WITH CHECK ADD  CONSTRAINT [FK_CasingTransaction_CasingTransaction] FOREIGN KEY([Create_At])
REFERENCES [dbo].[SystemUser] ([ID])
GO
ALTER TABLE [dbo].[CasingTransaction] CHECK CONSTRAINT [FK_CasingTransaction_CasingTransaction]
GO
ALTER TABLE [dbo].[CompanyTransactions]  WITH CHECK ADD  CONSTRAINT [FK_CompanyTransactions_Companies] FOREIGN KEY([CompanyID])
REFERENCES [dbo].[Companies] ([ID])
GO
ALTER TABLE [dbo].[CompanyTransactions] CHECK CONSTRAINT [FK_CompanyTransactions_Companies]
GO
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [FK_Invoices_Companies] FOREIGN KEY([CompanyID])
REFERENCES [dbo].[Companies] ([ID])
GO
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [FK_Invoices_Companies]
GO
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [FK_Invoices_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Products] ([ID])
GO
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [FK_Invoices_Product]
GO
ALTER TABLE [dbo].[Invoices]  WITH CHECK ADD  CONSTRAINT [FK_Invoices_SystemUser] FOREIGN KEY([Create_At])
REFERENCES [dbo].[SystemUser] ([ID])
GO
ALTER TABLE [dbo].[Invoices] CHECK CONSTRAINT [FK_Invoices_SystemUser]
GO
ALTER TABLE [dbo].[OfficialInvoices]  WITH CHECK ADD  CONSTRAINT [FK_OfficialInvoice_Companies] FOREIGN KEY([CompanyID])
REFERENCES [dbo].[Companies] ([ID])
GO
ALTER TABLE [dbo].[OfficialInvoices] CHECK CONSTRAINT [FK_OfficialInvoice_Companies]
GO
ALTER TABLE [dbo].[OfficialInvoices]  WITH CHECK ADD  CONSTRAINT [FK_OfficialInvoice_SystemUser] FOREIGN KEY([Create_At])
REFERENCES [dbo].[SystemUser] ([ID])
GO
ALTER TABLE [dbo].[OfficialInvoices] CHECK CONSTRAINT [FK_OfficialInvoice_SystemUser]
GO
ALTER TABLE [dbo].[SystemUser]  WITH CHECK ADD  CONSTRAINT [FK_SystemUser_Roles] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Roles] ([ID])
GO
ALTER TABLE [dbo].[SystemUser] CHECK CONSTRAINT [FK_SystemUser_Roles]
GO
USE [master]
GO
ALTER DATABASE [InvoiceWebsite] SET  READ_WRITE 
GO
