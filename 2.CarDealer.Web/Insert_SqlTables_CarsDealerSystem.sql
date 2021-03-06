USE [CarDealerDb]
GO
SET IDENTITY_INSERT [dbo].[Cars] ON
INSERT [dbo].[Cars] ([Id], [Make], [Model], [TravelledDistance]) VALUES (1, N'Opel',N'Omega', 2147483647)
INSERT [dbo].[Cars] ([Id], [Make], [Model], [TravelledDistance]) VALUES (2, N'BMW',N'1', 2147483647)
INSERT [dbo].[Cars] ([Id], [Make], [Model], [TravelledDistance]) VALUES (3, N'BMW',N'2', 2147483647)
INSERT [dbo].[Cars] ([Id], [Make], [Model], [TravelledDistance]) VALUES (4, N'BMW',N'3', 2147483647)
INSERT [dbo].[Cars] ([Id], [Make], [Model], [TravelledDistance]) VALUES (5, N'Audi',N'A6', 2147483647)
SET IDENTITY_INSERT [dbo].[Cars] OFF

SET IDENTITY_INSERT [dbo].[Customers] ON
INSERT [dbo].[Customers] ([Id], [Name], [BirthDate], [IsYoungDriver]) VALUES (1, N'Vladislav Penchev', CAST(N'1993-11-20T00:00:00.000' AS DateTime),1)
INSERT [dbo].[Customers] ([Id], [Name], [BirthDate], [IsYoungDriver]) VALUES (2, N'Pesho Benally', CAST(N'1996-11-20T00:00:00.000' AS DateTime),0)
INSERT [dbo].[Customers] ([Id], [Name], [BirthDate], [IsYoungDriver]) VALUES (3, N'Gosho Benally', CAST(N'1999-11-20T00:00:00.000' AS DateTime),0)
INSERT [dbo].[Customers] ([Id], [Name], [BirthDate], [IsYoungDriver]) VALUES (4, N'Ivan Benally', CAST(N'1911-11-20T00:00:00.000' AS DateTime),0)
INSERT [dbo].[Customers] ([Id], [Name], [BirthDate], [IsYoungDriver]) VALUES (5, N'Emmitt Benally', CAST(N'1993-11-20T00:00:00.000' AS DateTime),1)
SET IDENTITY_INSERT [dbo].[Customers] OFF

SET IDENTITY_INSERT [dbo].[Sales] ON
INSERT [dbo].[Sales] ([Id], [Discount], [CarId], [CustomerId]) VALUES (1,0.3,5,4)
INSERT [dbo].[Sales] ([Id], [Discount], [CarId], [CustomerId]) VALUES (2,0.2,4,3)
INSERT [dbo].[Sales] ([Id], [Discount], [CarId], [CustomerId]) VALUES (3,0.8,3,2)
INSERT [dbo].[Sales] ([Id], [Discount], [CarId], [CustomerId]) VALUES (4,0.5,2,5)
INSERT [dbo].[Sales] ([Id], [Discount], [CarId], [CustomerId]) VALUES (5,0.4,1,1)
SET IDENTITY_INSERT [dbo].[SAles] OFF

SET IDENTITY_INSERT [dbo].[Suppliers] ON
INSERT [dbo].[Suppliers] ([Id], [Name], [IsImporter]) VALUES (1,N'Zale',1)
INSERT [dbo].[Suppliers] ([Id], [Name], [IsImporter]) VALUES (2,N'pavhco',0)
INSERT [dbo].[Suppliers] ([Id], [Name], [IsImporter]) VALUES (3,N'Icaka',1)
INSERT [dbo].[Suppliers] ([Id], [Name], [IsImporter]) VALUES (4,N'Gancho',0)
INSERT [dbo].[Suppliers] ([Id], [Name], [IsImporter]) VALUES (5,N'Tasho',1)
SET IDENTITY_INSERT [dbo].[Suppliers] OFF

SET IDENTITY_INSERT [dbo].[Parts] ON
INSERT [dbo].[Parts] ([Id], [Name], [Price], [Quantity], [SupplierId]) VALUES (1,N'Bonnet/hood',1001.34,10,1)
INSERT [dbo].[Parts] ([Id], [Name], [Price], [Quantity], [SupplierId]) VALUES (2,N'dd',21.34,5,2)
INSERT [dbo].[Parts] ([Id], [Name], [Price], [Quantity], [SupplierId]) VALUES (3,N'hood',32.34,1,3)
INSERT [dbo].[Parts] ([Id], [Name], [Price], [Quantity], [SupplierId]) VALUES (4,N'Bonnet',42.34,232,4)
INSERT [dbo].[Parts] ([Id], [Name], [Price], [Quantity], [SupplierId]) VALUES (5,N'ttttttt',211.34,321,5)
SET IDENTITY_INSERT [dbo].[Parts] OFF

SET IDENTITY_INSERT [dbo].[PartCars] ON
INSERT [dbo].[PartCar] ([PartId], [CarId]) VALUES(1,5)
INSERT [dbo].[PartCar] ([PartId], [CarId]) VALUES(2,4)
INSERT [dbo].[PartCar] ([PartId], [CarId]) VALUES(3,3)
INSERT [dbo].[PartCar] ([PartId], [CarId]) VALUES(4,2)
INSERT [dbo].[PartCar] ([PartId], [CarId]) VALUES(5,1)
SET IDENTITY_INSERT [dbo].[PartCars] OFF


