USE [TicketsMVC]
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'1', N'Admin')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'2', N'Cliente')
GO
INSERT [dbo].[AspNetUsers] ([Id], [EmpresaID], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName], [User_Id]) VALUES (N'75d87e82-aa8f-41ae-a29a-c02e09e3d19d', 1, N'soportetecnico@solucionesintegrales.com.ec', 0, N'AHp9z2XHIxSBO98zndCA2+eTfee0kENwnrbEATgNtqRMp/2BivbEoaAm6gZSQohNmg==', N'81b2ad6f-4d15-4981-acb4-9541528ef1e0', NULL, 0, 0, NULL, 1, 0, N'soportetecnico@solucionesintegrales.com.ec', NULL)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'75d87e82-aa8f-41ae-a29a-c02e09e3d19d', N'1')
GO
SET IDENTITY_INSERT [dbo].[Comboes] ON 

GO
INSERT [dbo].[Comboes] ([id], [Relacion], [Valor], [Descripcion]) VALUES (1, N'ticket_estado', 0, N'')
GO
INSERT [dbo].[Comboes] ([id], [Relacion], [Valor], [Descripcion]) VALUES (2, N'ticket_estado', 1, N'Ingresado')
GO
INSERT [dbo].[Comboes] ([id], [Relacion], [Valor], [Descripcion]) VALUES (3, N'ticket_estado', 2, N'Asignado')
GO
INSERT [dbo].[Comboes] ([id], [Relacion], [Valor], [Descripcion]) VALUES (4, N'ticket_estado', 3, N'Solucionado')
GO
INSERT [dbo].[Comboes] ([id], [Relacion], [Valor], [Descripcion]) VALUES (5, N'ticket_estado', 4, N'Rechazado')
GO
INSERT [dbo].[Comboes] ([id], [Relacion], [Valor], [Descripcion]) VALUES (6, N'ticket_estado', 5, N'Proformar')
GO
INSERT [dbo].[Comboes] ([id], [Relacion], [Valor], [Descripcion]) VALUES (8, N'ticket_prioridad', 0, N'Todos')
GO
INSERT [dbo].[Comboes] ([id], [Relacion], [Valor], [Descripcion]) VALUES (9, N'ticket_prioridad', 1, N'Normal')
GO
INSERT [dbo].[Comboes] ([id], [Relacion], [Valor], [Descripcion]) VALUES (10, N'ticket_prioridad', 2, N'Urgente')
GO
SET IDENTITY_INSERT [dbo].[Comboes] OFF
GO
SET IDENTITY_INSERT [dbo].[Plans] ON 

GO
INSERT [dbo].[Plans] ([id], [Descripcion], [Minutos], [EstReg]) VALUES (1, N'Facturación', 0, 1)
GO
INSERT [dbo].[Plans] ([id], [Descripcion], [Minutos], [EstReg]) VALUES (2, N'Basico', 600, 1)
GO
INSERT [dbo].[Plans] ([id], [Descripcion], [Minutos], [EstReg]) VALUES (3, N'Medio', 1500, 1)
GO
INSERT [dbo].[Plans] ([id], [Descripcion], [Minutos], [EstReg]) VALUES (5, N'Avanzado', 3000, 1)
GO
SET IDENTITY_INSERT [dbo].[Plans] OFF
GO
SET IDENTITY_INSERT [dbo].[TicketsCategorias] ON 

GO
INSERT [dbo].[TicketsCategorias] ([id], [Categoria], [isDescarga], [EstReg]) VALUES (1, N'Soporte', 1, 1)
GO
INSERT [dbo].[TicketsCategorias] ([id], [Categoria], [isDescarga], [EstReg]) VALUES (2, N'Garantía', 0, 1)
GO
INSERT [dbo].[TicketsCategorias] ([id], [Categoria], [isDescarga], [EstReg]) VALUES (3, N'Cortesía', 0, 1)
GO
INSERT [dbo].[TicketsCategorias] ([id], [Categoria], [isDescarga], [EstReg]) VALUES (4, N'Documentos Electrónicos', 0, 1)
GO
INSERT [dbo].[TicketsCategorias] ([id], [Categoria], [isDescarga], [EstReg]) VALUES (5, N'Capacitación', 1, 1)
GO
SET IDENTITY_INSERT [dbo].[TicketsCategorias] OFF
GO
