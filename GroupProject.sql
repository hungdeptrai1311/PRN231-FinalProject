USE [master]
GO
/****** Object:  Database [GroupProject]    Script Date: 3/26/2024 3:24:08 AM ******/
CREATE DATABASE [GroupProject]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GroupProject', FILENAME = N'C:\SqlServer\MSSQL16.MSSQLSERVER\MSSQL\DATA\GroupProject.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'GroupProject_log', FILENAME = N'C:\SqlServer\MSSQL16.MSSQLSERVER\MSSQL\DATA\GroupProject_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [GroupProject] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GroupProject].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GroupProject] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GroupProject] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GroupProject] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GroupProject] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GroupProject] SET ARITHABORT OFF 
GO
ALTER DATABASE [GroupProject] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [GroupProject] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GroupProject] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GroupProject] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GroupProject] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GroupProject] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GroupProject] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GroupProject] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GroupProject] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GroupProject] SET  ENABLE_BROKER 
GO
ALTER DATABASE [GroupProject] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GroupProject] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GroupProject] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GroupProject] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GroupProject] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GroupProject] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GroupProject] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GroupProject] SET RECOVERY FULL 
GO
ALTER DATABASE [GroupProject] SET  MULTI_USER 
GO
ALTER DATABASE [GroupProject] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GroupProject] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GroupProject] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GroupProject] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [GroupProject] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [GroupProject] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'GroupProject', N'ON'
GO
ALTER DATABASE [GroupProject] SET QUERY_STORE = ON
GO
ALTER DATABASE [GroupProject] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [GroupProject]
GO
/****** Object:  Table [dbo].[Brand]    Script Date: 3/26/2024 3:24:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Brand](
	[BrandId] [int] IDENTITY(1,1) NOT NULL,
	[BrandName] [nvarchar](150) NULL,
	[BrandImage] [text] NULL,
	[Description] [ntext] NULL,
PRIMARY KEY CLUSTERED 
(
	[BrandId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cart]    Script Date: 3/26/2024 3:24:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cart](
	[CartId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[ProductId] [int] NULL,
	[Quantity] [int] NULL,
 CONSTRAINT [PK__Cart__51BCD7B73C719FD3] PRIMARY KEY CLUSTERED 
(
	[CartId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Category]    Script Date: 3/26/2024 3:24:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](50) NULL,
	[CategoryImage] [text] NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 3/26/2024 3:24:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[OrderId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[Date] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetail]    Script Date: 3/26/2024 3:24:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetail](
	[OrderDetailId] [int] IDENTITY(1,1) NOT NULL,
	[ProductId] [int] NULL,
	[OrderId] [int] NULL,
	[Quantity] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[OrderDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product]    Script Date: 3/26/2024 3:24:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](150) NULL,
	[Description] [ntext] NULL,
	[Price] [int] NULL,
	[Quantity] [int] NULL,
	[ProductImage] [text] NULL,
	[BrandId] [int] NULL,
	[CategoryId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Role]    Script Date: 3/26/2024 3:24:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [int] IDENTITY(1,1) NOT NULL,
	[RoleName] [nvarchar](20) NULL,
PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User]    Script Date: 3/26/2024 3:24:08 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [varchar](20) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Address] [nvarchar](max) NULL,
	[Phone] [nvarchar](10) NULL,
	[RoleId] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Brand] ON 

INSERT [dbo].[Brand] ([BrandId], [BrandName], [BrandImage], [Description]) VALUES (1, N'Mondelez Kinh Đô', N'https://i.ibb.co/QkjPcb2/kinhdo-logo.png', N'Nổi tiếng từ năm 1993, Mondelez Kinh Đô là đỉnh cao của sự hoàn hảo trong thế giới bánh kẹo Việt Nam. Với sự chuyển giao từ Kinh Đô, thương hiệu này đã khẳng định vị thế hàng đầu không chỉ trong nước mà còn trên 20 quốc gia trên thế giới. Bánh trung thu Mondelez Kinh Đô, bánh quy Cosy, bánh quy giòn AFC, bánh Oreo, bánh bông lan Solite... là những tên tuổi sáng giá, luôn đi đầu về chất lượng và phong cách. Dù có sự đổi chủ nhưng Mondelez Kinh Đô vẫn giữ vững niềm tin của người tiêu dùng bằng sự đẳng cấp và đổi mới không ngừng.')
INSERT [dbo].[Brand] ([BrandId], [BrandName], [BrandImage], [Description]) VALUES (2, N'Bibica', N'https://i.ibb.co/TgwSt4S/bibica-logo.png', N'Với hơn 20 năm phát triển, Bibica là đơn vị đầu tiên trong lĩnh vực bánh kẹo tại Việt Nam. Mạng lưới phân phối rộng khắp, Bibica không chỉ là thương hiệu quen thuộc với người Việt mà còn lan tỏa tầm nhìn quốc tế với mặt hàng bánh kẹo Việt có mặt ở 21 nước trên thế giới như Mỹ, Nhật, Singapore.

Bánh kẹo Bibica cam kết đem đến sản phẩm chất lượng, an toàn vệ sinh thực phẩm, với thiết kế bao bì đẹp mắt. Bánh trung thu, bánh Hura, Goody, kẹo cứng, kẹo mềm, kẹo bốn mùa, socola... đều nhận được sự ủng hộ từ người tiêu dùng.')
INSERT [dbo].[Brand] ([BrandId], [BrandName], [BrandImage], [Description]) VALUES (3, N'Hải Châu', N'https://i.ibb.co/KrNTfhx/haichau-logo.png', N'Công ty bánh kẹo Hải Châu trước đây là Nhà máy Hải Châu, thành lập từ năm 1965, là doanh nghiệp nhà nước thuộc Tổng Công ty mía đường. Nổi tiếng với chất lượng tốt, quy trình sản xuất đảm bảo vệ sinh an toàn thực phẩm, và đa dạng mặt hàng với giá cả hợp lý.

Đứng trong top đầu thương hiệu bánh kẹo tại Việt Nam, bánh kẹo Hải Châu không chỉ nhận được nhiều giải thưởng và huy chương quý giá mà còn mở rộng thị trường ra nước ngoài như Pháp, Đức, Nhật, Bỉ...

Sản phẩm đa dạng của bánh kẹo Hải Châu bao gồm bánh quy, bánh xốp, bánh trung thu, và kẹo đa dạng. Ngoài ra, các sản phẩm khác như lương khô, bột canh, hạt nêm cũng được người tiêu dùng đánh giá cao.')
INSERT [dbo].[Brand] ([BrandId], [BrandName], [BrandImage], [Description]) VALUES (4, N'Hải Hà', N'https://i.ibb.co/BcrPq0x/haiha-logo.png', N'Với hơn nửa thế kỷ hình thành và không ngừng phát triển, doanh nghiệp bánh kẹo Hải Hà (HAIHACO) nay là một trong những đơn vị sản xuất bánh kẹo hàng đầu tại Việt Nam. Hải Hà luôn đặt chất lượng sản phẩm lên hàng đầu để mang đến cho khách hàng những sản phẩm an toàn và chất lượng nhất.

Bánh kẹo Hải Hà đã được chứng nhận về vệ sinh thực phẩm, đoạt nhiều giải thưởng quốc tế và là đơn vị được đánh giá là “Hàng Việt Nam chất lượng cao” trong suốt 18 năm liên tiếp từ 1997-2015.

Bánh kẹo Hải Hà cam kết phục vụ khách hàng bình dân, đảm bảo chất lượng và giá cả hợp lý. Các sản phẩm chính của Hải Hà bao gồm kẹo chew, kẹo xốp, jelly, bánh xốp, bánh quy, bánh trung thu...')
INSERT [dbo].[Brand] ([BrandId], [BrandName], [BrandImage], [Description]) VALUES (5, N'Biscafun', N'https://i.ibb.co/k3DFHJQ/biscafun-logo.png', N'Bánh kẹo Biscafun chế biến tại nhà máy Biscafun, thuộc công ty cổ phần đường Quảng Ngãi. Mặc dù chiếm tỷ lệ nhỏ trong doanh nghiệp, nhưng với dây chuyền hiện đại, nguyên liệu chọn lọc, chất lượng với giá thành hợp lý và câu slogan ấn tượng “Vượt qua niềm vui”, Biscafun đã trở thành thương hiệu được lòng người tiêu dùng Việt Nam.

Sản phẩm của Biscafun liên tục đoạt giải “Hàng Việt Nam chất lượng cao”, và được vinh danh với nhiều giải thưởng cúp vàng. Bánh mềm phủ socola, bánh crack, kẹo các loại của Biscafun đặc biệt được ưa chuộng, đặc biệt là tại các vùng nông thôn.')
INSERT [dbo].[Brand] ([BrandId], [BrandName], [BrandImage], [Description]) VALUES (6, N'Hữu Nghị', N'https://i.ibb.co/X4qzDt0/huunghi-logo.png', N'Thương hiệu Hữu Nghị ra đời từ năm 1997, với hơn 20 năm cam kết sản xuất bánh kẹo, đã trở thành một phần không thể thiếu trong gia đình Việt. Với hơn 200.000 đại lý và hàng trăm nhà phân phối, Hữu Nghị gắn bó với mỗi gia đình.

Hữu Nghị không chỉ đảm bảo sản phẩm tuân thủ nghiêm ngặt tiêu chuẩn vệ sinh an toàn thực phẩm trên dây chuyền hiện đại mà còn đạt nhiều danh hiệu quan trọng như “Thương hiệu mạnh Việt Nam”, “Hàng Việt Nam chất lượng cao”, cúp vàng “Đóng góp cho sự phát triển cộng đồng”...')
SET IDENTITY_INSERT [dbo].[Brand] OFF
GO
SET IDENTITY_INSERT [dbo].[Cart] ON 

INSERT [dbo].[Cart] ([CartId], [UserId], [ProductId], [Quantity]) VALUES (5, 2, 1, 2)
SET IDENTITY_INSERT [dbo].[Cart] OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([CategoryId], [CategoryName], [CategoryImage]) VALUES (1, N'Bánh', N'https://i.ibb.co/BzrFZJP/banh-cate.png')
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [CategoryImage]) VALUES (2, N'Snack', N'https://i.ibb.co/8cVh7Jh/snack-cate.pn')
INSERT [dbo].[Category] ([CategoryId], [CategoryName], [CategoryImage]) VALUES (3, N'Kẹo', N'https://i.ibb.co/bFkPVNt/keo-cate.png')
SET IDENTITY_INSERT [dbo].[Category] OFF
GO
SET IDENTITY_INSERT [dbo].[Product] ON 

INSERT [dbo].[Product] ([ProductId], [ProductName], [Description], [Price], [Quantity], [ProductImage], [BrandId], [CategoryId]) VALUES (1, N'Bánh quy giòn AFC', N'Bánh quy giòn AFC không chỉ có hương vị thơm ngon mà còn phù hợp với chế độ ăn uống cân bằng và khỏe mạnh. Loại bánh này đảm bảo cung cấp năng lượng, protein, chất xơ, vitamin D, canxi,… giúp tiêu hóa tốt và hệ xương thêm chắc khỏe.', 25000, 100, N'https://batos.vn/images/products/2023/07/22/screenshot-1690009853-428.png', 1, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [Description], [Price], [Quantity], [ProductImage], [BrandId], [CategoryId]) VALUES (2, N'Bánh quy Cosy', N'Bánh Cosy là một trong những loại bánh quy nổi tiếng nhất tại Việt nam. Sản phẩm này được làm từ các thành phần như: bột mì, dầu cọ, muối, đường, canxi, natri, carbohydrate,… có tác dụng làm dịu cơn đói nhanh chóng và bổ sung nhiều chất dinh dưỡng cần thiết cho cơ thể.', 100000, 100, N'https://salt.tikicdn.com/ts/product/16/c8/95/27bd60381e3bdd6aa8b5fdc64c905cea.png', 1, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [Description], [Price], [Quantity], [ProductImage], [BrandId], [CategoryId]) VALUES (3, N'Bánh Solite', N'Bánh bông lan Solite được làm từ các thành phần tự nhiên như trứng gà, bột mì, đường, dầu nành, dầu cọ,… nên hương vị phù hợp với đa dạng khẩu vị của người dùng. Ngoài ra, loại bánh này còn được đóng hộp kỹ lưỡng – rất phù hợp để mua làm quà biếu tặng người thân và bạn bè trong những dịp lễ Tết.', 40000, 100, N'https://product.hstatic.net/200000203485/product/lan_288_dua_7195d0ddfea848bd90347eea09892863_grande.png', 1, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [Description], [Price], [Quantity], [ProductImage], [BrandId], [CategoryId]) VALUES (4, N'Snack khoai tây Slide', N'Snack khoai tây Slide được sản xuất theo quy trình khép kín, hiện đại để khoai tây vẫn giữ được những dưỡng chất như ban đầu. Sản phẩm này đảm bảo có đa dạng mùi vị cho khách hàng lựa chọn như: vị thịt nướng, vị phô mai béo ngậy,…', 50000, 100, N'https://cdn.tgdd.vn/Products/Images/3364/79562/bhx/-202303111047080332.jpg', 1, 2)
INSERT [dbo].[Product] ([ProductId], [ProductName], [Description], [Price], [Quantity], [ProductImage], [BrandId], [CategoryId]) VALUES (5, N'Bánh mì tươi', N'Bánh mì tươi Kinh Đô được làm từ các thành phần tự nhiên như: bột mì, đường xay, dầu cọ, dầu nành, men, sữa tươi, bơ dầu thay thế, đường fructose, shortening, muối,… nên đảm bảo bổ sung đầy đủ các dưỡng chất cần thiết cho cơ thể. Những chiếc bánh mì tươi Kinh Đô vừa thơm ngon vừa có đầy đủ 2 vị ngọt và mặn.', 4000, 100, N'https://product.hstatic.net/1000288770/product/banh_mi_tuoi_kinh_do_khong_nhan_goi_80_g_7dffb4c3bf58479a8ff9bfc94e846737.jpg', 1, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [Description], [Price], [Quantity], [ProductImage], [BrandId], [CategoryId]) VALUES (6, N'Bánh quy Oreo', N'Bánh Oreo có hương vị thơm ngon, giòn, béo và ngọt dịu với đa dạng nhân kem như: socola, dâu, vani,… Tuy nhiên, bánh hơi khô nên bạn có thể uống một ít nước trước khi ăn bánh hoặc chấm bánh với một ít sữa.

Bài viết trên đây của chúng tôi đã chia sẻ đến bạn đọc những thông tin hữu ích và thú vị về các loại bánh kẹo Kinh Đô nổi tiếng. Hy vọng sẽ giúp bạn chọn được loại bánh kẹo phù hợp nhất để mua làm quà tặng ý nghĩa cho người thân và bạn bè.', 25000, 100, N'https://www.thanhthuyfoods.vn/upload/sanpham/oreo-7418.png', 1, 1)
INSERT [dbo].[Product] ([ProductId], [ProductName], [Description], [Price], [Quantity], [ProductImage], [BrandId], [CategoryId]) VALUES (7, N'Bánh trung thu', N'Được thành lập từ năm 1993, thương hiệu bánh trung thu Kinh Đô đã trở thành một phần ký ức của rất nhiều người. Nói đến Kinh Đô, người ta sẽ nghĩ ngay đến những chiếc bánh nướng, bánh dẻo thơm phức, bóng bẩy đầy hấp dẫn. Ban đầu, đây chỉ là một cửa hàng bán bánh khá nhỏ nhưng theo thời gian, thương hiệu bánh này đã phát triển thành một thương hiệu vang danh khắp mọi nơi.', 100000, 2, N'https://cms.vietnamreport.net/source/5..png', 1, 1)
SET IDENTITY_INSERT [dbo].[Product] OFF
GO
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (1, N'admin')
INSERT [dbo].[Role] ([RoleId], [RoleName]) VALUES (2, N'customer')
SET IDENTITY_INSERT [dbo].[Role] OFF
GO
SET IDENTITY_INSERT [dbo].[User] ON 

INSERT [dbo].[User] ([UserId], [Email], [Password], [Name], [Address], [Phone], [RoleId]) VALUES (1, N'admin@gmail.com', N'1', N'ad', N'hn', N'012569856', 1)
INSERT [dbo].[User] ([UserId], [Email], [Password], [Name], [Address], [Phone], [RoleId]) VALUES (2, N'user@gmail.com', N'1', N'us', N'nh', N'0568569563', 2)
SET IDENTITY_INSERT [dbo].[User] OFF
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_Product]
GO
ALTER TABLE [dbo].[Cart]  WITH CHECK ADD  CONSTRAINT [FK_Cart_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Cart] CHECK CONSTRAINT [FK_Cart_User]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_User] FOREIGN KEY([UserId])
REFERENCES [dbo].[User] ([UserId])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_User]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Order] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([OrderId])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Order]
GO
ALTER TABLE [dbo].[OrderDetail]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_Product] FOREIGN KEY([ProductId])
REFERENCES [dbo].[Product] ([ProductId])
GO
ALTER TABLE [dbo].[OrderDetail] CHECK CONSTRAINT [FK_OrderDetail_Product]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Brand] FOREIGN KEY([BrandId])
REFERENCES [dbo].[Brand] ([BrandId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Brand]
GO
ALTER TABLE [dbo].[Product]  WITH CHECK ADD  CONSTRAINT [FK_Product_Category] FOREIGN KEY([CategoryId])
REFERENCES [dbo].[Category] ([CategoryId])
GO
ALTER TABLE [dbo].[Product] CHECK CONSTRAINT [FK_Product_Category]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleId])
REFERENCES [dbo].[Role] ([RoleId])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
USE [master]
GO
ALTER DATABASE [GroupProject] SET  READ_WRITE 
GO
