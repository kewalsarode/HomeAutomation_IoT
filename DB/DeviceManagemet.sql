USE [master]
GO

/****** Object:  Database [DeviceManagement]    Script Date: 10/26/2018 17:42:37 ******/
IF  EXISTS (SELECT name FROM sys.databases WHERE name = N'DeviceManagement')
DROP DATABASE [DeviceManagement]
GO

USE [master]
GO

/****** Object:  Database [DeviceManagement]    Script Date: 10/26/2018 17:42:37 ******/
CREATE DATABASE [DeviceManagement] ON  PRIMARY 
( NAME = N'DeviceManagement', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\DeviceManagement.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'DeviceManagement_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\DeviceManagement_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO

ALTER DATABASE [DeviceManagement] SET COMPATIBILITY_LEVEL = 100
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DeviceManagement].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [DeviceManagement] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [DeviceManagement] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [DeviceManagement] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [DeviceManagement] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [DeviceManagement] SET ARITHABORT OFF 
GO

ALTER DATABASE [DeviceManagement] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [DeviceManagement] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [DeviceManagement] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [DeviceManagement] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [DeviceManagement] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [DeviceManagement] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [DeviceManagement] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [DeviceManagement] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [DeviceManagement] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [DeviceManagement] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [DeviceManagement] SET  DISABLE_BROKER 
GO

ALTER DATABASE [DeviceManagement] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [DeviceManagement] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [DeviceManagement] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [DeviceManagement] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [DeviceManagement] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [DeviceManagement] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [DeviceManagement] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [DeviceManagement] SET  READ_WRITE 
GO

ALTER DATABASE [DeviceManagement] SET RECOVERY FULL 
GO

ALTER DATABASE [DeviceManagement] SET  MULTI_USER 
GO

ALTER DATABASE [DeviceManagement] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [DeviceManagement] SET DB_CHAINING OFF 
GO



USE [DeviceManagement]
GO
/****** Object:  Table [dbo].[UsersAccount]    Script Date: 10/26/2018 17:44:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UsersAccount](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](max) NOT NULL,
	[passwordHash] [nvarchar](max) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[EmailId] [nvarchar](max) NOT NULL,
	[ContactNr] [nvarchar](12) NOT NULL,
	[Location] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_UserAccount] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[user]    Script Date: 10/26/2018 17:44:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[user](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](max) NOT NULL,
	[password] [varchar](max) NOT NULL,
 CONSTRAINT [PK_user] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Devices]    Script Date: 10/26/2018 17:44:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Devices](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserAccount_ID] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](50) NOT NULL,
	[DeviceSerialNr] [nvarchar](50) NOT NULL,
	[DeviceType] [nvarchar](50) NULL,
 CONSTRAINT [PK_Devices] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
