USE [master]
GO
/****** Object:  Database [ECommerce]    Script Date: 1/31/2024 11:00:03 AM ******/
CREATE DATABASE [ECommerce]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ECommerce', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ECommerce.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ECommerce_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\ECommerce_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ECommerce] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ECommerce].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ECommerce] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ECommerce] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ECommerce] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ECommerce] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ECommerce] SET ARITHABORT OFF 
GO
ALTER DATABASE [ECommerce] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ECommerce] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ECommerce] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ECommerce] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ECommerce] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ECommerce] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ECommerce] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ECommerce] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ECommerce] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ECommerce] SET  ENABLE_BROKER 
GO
ALTER DATABASE [ECommerce] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ECommerce] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ECommerce] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ECommerce] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ECommerce] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ECommerce] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ECommerce] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ECommerce] SET RECOVERY FULL 
GO
ALTER DATABASE [ECommerce] SET  MULTI_USER 
GO
ALTER DATABASE [ECommerce] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ECommerce] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ECommerce] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ECommerce] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ECommerce] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ECommerce] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ECommerce] SET QUERY_STORE = OFF
GO
USE [ECommerce]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 1/31/2024 11:00:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 1/31/2024 11:00:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[IsActive] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 1/31/2024 11:00:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](500) NOT NULL,
	[Email] [nvarchar](500) NOT NULL,
	[PhoneNumber1] [nvarchar](20) NOT NULL,
	[PhoneNumber2] [nvarchar](20) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](50) NOT NULL,
	[Deleted] [bit] NOT NULL,
	[ImagePath] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 1/31/2024 11:00:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](1000) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[StockQuantity] [int] NOT NULL,
	[CategoryId] [int] NOT NULL,
	[Deleted] [bit] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductVariant]    Script Date: 1/31/2024 11:00:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductVariant](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NOT NULL,
	[Key] [nvarchar](1000) NOT NULL,
	[Value] [nvarchar](1000) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([Id], [Name], [IsActive]) VALUES (2, N'Kitchen Item', 0)
INSERT [dbo].[Category] ([Id], [Name], [IsActive]) VALUES (3, N'Clothes', 1)
INSERT [dbo].[Category] ([Id], [Name], [IsActive]) VALUES (4, N'Shoes', 1)
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Customer] ON 

INSERT [dbo].[Customer] ([Id], [FullName], [Email], [PhoneNumber1], [PhoneNumber2], [Username], [Password], [Deleted], [ImagePath]) VALUES (1, N'Muhammad Arsalan', N'marcsiddiqui@gmail.com', N'33', N'33', N'ma', N'22', 0, N'Content\Customer\1_Muhammad Arsalan_images.png')
SET IDENTITY_INSERT [dbo].[Customer] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [StockQuantity], [CategoryId], [Deleted]) VALUES (1, N'Jeans', N'This is jeans', CAST(5000.00 AS Decimal(18, 2)), 500, 3, 0)
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [StockQuantity], [CategoryId], [Deleted]) VALUES (2, N'Brown Shoes', N'Shoes', CAST(6000.00 AS Decimal(18, 2)), 500, 4, 0)
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [StockQuantity], [CategoryId], [Deleted]) VALUES (3, N'shirt', N'shirt', CAST(500.00 AS Decimal(18, 2)), 222, 3, 0)
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [StockQuantity], [CategoryId], [Deleted]) VALUES (4, N'Coat', N'coat', CAST(6000.00 AS Decimal(18, 2)), 15, 3, 0)
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [StockQuantity], [CategoryId], [Deleted]) VALUES (5, N'Slippres', N'slippers', CAST(900.00 AS Decimal(18, 2)), 50, 4, 0)
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [StockQuantity], [CategoryId], [Deleted]) VALUES (6, N'Dress pent', N'pent is very expensive', CAST(1800.00 AS Decimal(18, 2)), 50, 3, 0)
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [StockQuantity], [CategoryId], [Deleted]) VALUES (1002, N'gHADA', N'RUN VERY FAST', CAST(12000.00 AS Decimal(18, 2)), 123, 3, 1)
INSERT [dbo].[Product] ([Id], [Name], [Description], [Price], [StockQuantity], [CategoryId], [Deleted]) VALUES (1003, N'isra', N'AA', CAST(0.00 AS Decimal(18, 2)), 0, 3, 1)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[ProductVariant] ON 

INSERT [dbo].[ProductVariant] ([Id], [ProductId], [Key], [Value]) VALUES (2, 6, N'Color', N'Black')
INSERT [dbo].[ProductVariant] ([Id], [ProductId], [Key], [Value]) VALUES (3, 6, N'Size', N'M')
INSERT [dbo].[ProductVariant] ([Id], [ProductId], [Key], [Value]) VALUES (4, 6, N'Size', N'L')
INSERT [dbo].[ProductVariant] ([Id], [ProductId], [Key], [Value]) VALUES (1002, 5, N'Size', N'S')
INSERT [dbo].[ProductVariant] ([Id], [ProductId], [Key], [Value]) VALUES (1003, 6, N'Size', N'S')
INSERT [dbo].[ProductVariant] ([Id], [ProductId], [Key], [Value]) VALUES (1004, 6, N'Color', N'Blue')
INSERT [dbo].[ProductVariant] ([Id], [ProductId], [Key], [Value]) VALUES (1005, 6, N'Color', N'Brown')
INSERT [dbo].[ProductVariant] ([Id], [ProductId], [Key], [Value]) VALUES (1006, 6, N'Color', N'pruple')
INSERT [dbo].[ProductVariant] ([Id], [ProductId], [Key], [Value]) VALUES (1007, 6, N'Color', N'Pink')
INSERT [dbo].[ProductVariant] ([Id], [ProductId], [Key], [Value]) VALUES (1008, 6, N'Size', N'XL')
INSERT [dbo].[ProductVariant] ([Id], [ProductId], [Key], [Value]) VALUES (1009, 6, N'Size', N'XXL')
INSERT [dbo].[ProductVariant] ([Id], [ProductId], [Key], [Value]) VALUES (1010, 6, N'Size', N'XXS')
INSERT [dbo].[ProductVariant] ([Id], [ProductId], [Key], [Value]) VALUES (1011, 6, N'Color', N'Yellow')
INSERT [dbo].[ProductVariant] ([Id], [ProductId], [Key], [Value]) VALUES (1012, 6, N'Color', N'Orange')
INSERT [dbo].[ProductVariant] ([Id], [ProductId], [Key], [Value]) VALUES (1013, 6, N'Color', N'Green')
INSERT [dbo].[ProductVariant] ([Id], [ProductId], [Key], [Value]) VALUES (1014, 6, N'Color', N'Maroon')
INSERT [dbo].[ProductVariant] ([Id], [ProductId], [Key], [Value]) VALUES (1015, 6, N'Color', N'White')
INSERT [dbo].[ProductVariant] ([Id], [ProductId], [Key], [Value]) VALUES (1016, 6, N'Color', N'Sea Green')
INSERT [dbo].[ProductVariant] ([Id], [ProductId], [Key], [Value]) VALUES (1018, 6, N'Size', N'xxll')
INSERT [dbo].[ProductVariant] ([Id], [ProductId], [Key], [Value]) VALUES (1019, 6, N'Size', N'xl')
INSERT [dbo].[ProductVariant] ([Id], [ProductId], [Key], [Value]) VALUES (1021, 1002, N'0', N'QWE')
INSERT [dbo].[ProductVariant] ([Id], [ProductId], [Key], [Value]) VALUES (1023, 1003, N'Color', N'adada')
INSERT [dbo].[ProductVariant] ([Id], [ProductId], [Key], [Value]) VALUES (1024, 1003, N'Color', N'adada')
INSERT [dbo].[ProductVariant] ([Id], [ProductId], [Key], [Value]) VALUES (1025, 1003, N'0', N'ghfgh')
INSERT [dbo].[ProductVariant] ([Id], [ProductId], [Key], [Value]) VALUES (1026, 1003, N'Color', N'fghfghfgh')
INSERT [dbo].[ProductVariant] ([Id], [ProductId], [Key], [Value]) VALUES (1027, 1003, N'Color', N'fghfghfgh')
INSERT [dbo].[ProductVariant] ([Id], [ProductId], [Key], [Value]) VALUES (1028, 1003, N'Color', N'fghfghfgh')
SET IDENTITY_INSERT [dbo].[ProductVariant] OFF
GO
ALTER TABLE [dbo].[Category] ADD  DEFAULT ((1)) FOR [IsActive]
GO
ALTER TABLE [dbo].[Customer] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ((0)) FOR [Price]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ((0)) FOR [StockQuantity]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ((0)) FOR [CategoryId]
GO
ALTER TABLE [dbo].[Product] ADD  DEFAULT ((0)) FOR [Deleted]
GO
ALTER TABLE [dbo].[ProductVariant] ADD  DEFAULT ((0)) FOR [ProductId]
GO
USE [master]
GO
ALTER DATABASE [ECommerce] SET  READ_WRITE 
GO
