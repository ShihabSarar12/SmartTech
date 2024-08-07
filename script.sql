USE [master]
GO
/****** Object:  Database [SmartTech]    Script Date: 01-Aug-24 6:22:28 PM ******/
CREATE DATABASE [SmartTech]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SmartTech', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\SmartTech.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SmartTech_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\SmartTech_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [SmartTech] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SmartTech].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SmartTech] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SmartTech] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SmartTech] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SmartTech] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SmartTech] SET ARITHABORT OFF 
GO
ALTER DATABASE [SmartTech] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [SmartTech] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SmartTech] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SmartTech] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SmartTech] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SmartTech] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SmartTech] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SmartTech] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SmartTech] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SmartTech] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SmartTech] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SmartTech] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SmartTech] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SmartTech] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SmartTech] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SmartTech] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SmartTech] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SmartTech] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SmartTech] SET  MULTI_USER 
GO
ALTER DATABASE [SmartTech] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SmartTech] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SmartTech] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SmartTech] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [SmartTech] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [SmartTech] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [SmartTech] SET QUERY_STORE = ON
GO
ALTER DATABASE [SmartTech] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [SmartTech]
GO
/****** Object:  Table [dbo].[admins]    Script Date: 01-Aug-24 6:22:28 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[admins](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [varchar](191) NOT NULL,
	[profile] [varchar](191) NULL,
	[email] [varchar](191) NOT NULL,
	[number] [varchar](191) NOT NULL,
	[password] [varchar](191) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[banners]    Script Date: 01-Aug-24 6:22:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[banners](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[banner_title] [varchar](191) NOT NULL,
	[banner_description] [varchar](191) NOT NULL,
	[banner_image] [varchar](191) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[cart]    Script Date: 01-Aug-24 6:22:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cart](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[product_id] [bigint] NOT NULL,
	[user_id] [bigint] NOT NULL,
	[qnt] [bigint] NOT NULL,
	[price] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[order_products]    Script Date: 01-Aug-24 6:22:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[order_products](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[product_id] [bigint] NOT NULL,
	[price] [decimal](10, 2) NOT NULL,
	[qnt] [bigint] NOT NULL,
	[order_id] [varchar](65) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[orders]    Script Date: 01-Aug-24 6:22:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[orders](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[address] [varchar](191) NOT NULL,
	[shipping_id] [bigint] NOT NULL,
	[price] [decimal](10, 2) NOT NULL,
	[status] [varchar](20) NOT NULL,
	[user_id] [bigint] NOT NULL,
	[order_id] [varchar](65) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product_categories]    Script Date: 01-Aug-24 6:22:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_categories](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[category_name] [varchar](191) NOT NULL,
	[category_image] [varchar](191) NULL,
	[category_icon] [varchar](191) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[product_photos]    Script Date: 01-Aug-24 6:22:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[product_photos](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[product_id] [bigint] NOT NULL,
	[image] [varchar](191) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[products]    Script Date: 01-Aug-24 6:22:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[products](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[category_id] [bigint] NOT NULL,
	[name] [varchar](191) NOT NULL,
	[description] [nvarchar](max) NOT NULL,
	[discount] [bigint] NOT NULL,
	[price] [decimal](10, 2) NOT NULL,
	[stock_status] [int] NOT NULL,
	[status] [int] NOT NULL,
 CONSTRAINT [PK_products] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[shippings]    Script Date: 01-Aug-24 6:22:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[shippings](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [varchar](191) NOT NULL,
	[price] [decimal](10, 2) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[users]    Script Date: 01-Aug-24 6:22:29 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[name] [varchar](191) NOT NULL,
	[email] [varchar](191) NOT NULL,
	[number] [varchar](191) NOT NULL,
	[password] [varchar](191) NOT NULL,
	[image_url] [nvarchar](max) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[order_products] ADD  DEFAULT ((0.00)) FOR [price]
GO
ALTER TABLE [dbo].[orders] ADD  DEFAULT ((0.00)) FOR [price]
GO
ALTER TABLE [dbo].[products] ADD  DEFAULT ((0)) FOR [discount]
GO
ALTER TABLE [dbo].[products] ADD  DEFAULT ((0.00)) FOR [price]
GO
ALTER TABLE [dbo].[products] ADD  DEFAULT ((1)) FOR [stock_status]
GO
ALTER TABLE [dbo].[products] ADD  DEFAULT ((1)) FOR [status]
GO
ALTER TABLE [dbo].[shippings] ADD  DEFAULT ((0.00)) FOR [price]
GO
ALTER TABLE [dbo].[users] ADD  CONSTRAINT [DF_Users_ImageUrl]  DEFAULT ('https://static.vecteezy.com/system/resources/thumbnails/009/292/244/small/default-avatar-icon-of-social-media-user-vector.jpg') FOR [image_url]
GO
ALTER TABLE [dbo].[cart]  WITH CHECK ADD FOREIGN KEY([product_id])
REFERENCES [dbo].[products] ([id])
GO
ALTER TABLE [dbo].[cart]  WITH CHECK ADD FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[order_products]  WITH CHECK ADD  CONSTRAINT [FK_product_id] FOREIGN KEY([product_id])
REFERENCES [dbo].[products] ([id])
GO
ALTER TABLE [dbo].[order_products] CHECK CONSTRAINT [FK_product_id]
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD  CONSTRAINT [FK_shipping_id] FOREIGN KEY([shipping_id])
REFERENCES [dbo].[shippings] ([id])
GO
ALTER TABLE [dbo].[orders] CHECK CONSTRAINT [FK_shipping_id]
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD  CONSTRAINT [FK_user_id] FOREIGN KEY([user_id])
REFERENCES [dbo].[users] ([id])
GO
ALTER TABLE [dbo].[orders] CHECK CONSTRAINT [FK_user_id]
GO
ALTER TABLE [dbo].[product_photos]  WITH CHECK ADD  CONSTRAINT [FK_ProductPhotos_Products] FOREIGN KEY([product_id])
REFERENCES [dbo].[products] ([id])
GO
ALTER TABLE [dbo].[product_photos] CHECK CONSTRAINT [FK_ProductPhotos_Products]
GO
ALTER TABLE [dbo].[products]  WITH CHECK ADD  CONSTRAINT [category_id] FOREIGN KEY([category_id])
REFERENCES [dbo].[product_categories] ([id])
GO
ALTER TABLE [dbo].[products] CHECK CONSTRAINT [category_id]
GO
ALTER TABLE [dbo].[orders]  WITH CHECK ADD  CONSTRAINT [CK__orders__status__5BE2A6F2] CHECK  (([status]='delieverd' OR [status]='damage' OR [status]='cancel' OR [status]='return' OR [status]='shipping' OR [status]='processing' OR [status]='pending' OR [status]='cart'))
GO
ALTER TABLE [dbo].[orders] CHECK CONSTRAINT [CK__orders__status__5BE2A6F2]
GO
USE [master]
GO
ALTER DATABASE [SmartTech] SET  READ_WRITE 
GO
