USE [BookShop]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 2018-03-25 22:26:26 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[Price] [decimal](18, 2) NOT NULL,
	[Category] [nvarchar](max) NOT NULL,
	[ImageData] [varbinary](max) NULL,
	[ImageMimeType] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Products] PRIMARY KEY CLUSTERED 
(
	[ProductID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[Products] ON 

GO
INSERT [dbo].[Products] ([ProductID], [Name], [Description], [Price], [Category], [ImageData], [ImageMimeType]) VALUES (1, N'Jacek Dukaj - Lód', N'Długo oczekiwana powieść najlepszego polskiego pisarza S-F...', CAST(45.00 AS Decimal(18, 2)), N'Fikcja', NULL, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [Name], [Description], [Price], [Category], [ImageData], [ImageMimeType]) VALUES (2, N'Antoni Kępiński - Rytm Życia', N'"Rytm życia" zawiera eseje z pogranicza filozofii i medycyny, rozważające mechanizm powstawania i rozwoju patologicznych form ludzkiego postępowania w różnych sytuacjach życiowych np. sprawowania władzy czy uwięzienia.', CAST(50.00 AS Decimal(18, 2)), N'Psychologia', NULL, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [Name], [Description], [Price], [Category], [ImageData], [ImageMimeType]) VALUES (3, N'Yalom Irvin D. - Kat miłości', N'Jeśli nie możesz znieść bólu, spróbuj nadać mu sens… Oto prawdziwe historie dziesięciu osób, które skorzystały z terapii, aby rozwiązać zwyczajne problemy.', CAST(29.50 AS Decimal(18, 2)), N'Psychologia', NULL, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [Name], [Description], [Price], [Category], [ImageData], [ImageMimeType]) VALUES (4, N'David Foster Wallece - Krótkie wywiady z paskudnymi ludźmi', N'Książka nominowana w Plebiscycie Książka Roku 2015 lubimyczytać.pl w kategorii Literatura piękna', CAST(34.95 AS Decimal(18, 2)), N'Literatura współczesna', NULL, NULL)
GO
INSERT [dbo].[Products] ([ProductID], [Name], [Description], [Price], [Category], [ImageData], [ImageMimeType]) VALUES (5, N'George R.R. Martin - Gra o Tron', N'W Zachodnich Krainach o ośmiu tysiącach lat zapisanej historii widmo wojen i katastrofy nieustannie wisi nad ludźmi.', CAST(79500.00 AS Decimal(18, 2)), N'Fikcja', NULL, NULL)
GO
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
