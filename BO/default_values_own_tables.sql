USE [lives]
GO
/****** Object:  Table [dbo].[EstadoVideo]    Script Date: 01/14/2012 19:25:34 ******/
SET IDENTITY_INSERT [dbo].[Estado] ON
INSERT [dbo].[Estado] ([id], [estado]) VALUES (1, N'Por Aprovar')
INSERT [dbo].[Estado] ([id], [estado]) VALUES (2, N'Aprovado')
INSERT [dbo].[Estado] ([id], [estado]) VALUES (3, N'Removido')
SET IDENTITY_INSERT [dbo].[Estado] OFF
/****** Object:  Table [dbo].[Categoria]    Script Date: 01/14/2012 19:25:34 ******/
SET IDENTITY_INSERT [dbo].[Categoria] ON
INSERT [dbo].[Categoria] ([id], [nome]) VALUES (1, N'Actividade')
INSERT [dbo].[Categoria] ([id], [nome]) VALUES (2, N'Local')
INSERT [dbo].[Categoria] ([id], [nome]) VALUES (3, N'Emoção')
INSERT [dbo].[Categoria] ([id], [nome]) VALUES (4, N'Tempo')
INSERT [dbo].[Categoria] ([id], [nome]) VALUES (5, N'Evento')
INSERT [dbo].[Categoria] ([id], [nome]) VALUES (6, N'Pessoas')
INSERT [dbo].[Categoria] ([id], [nome]) VALUES (7, N'Teste')
SET IDENTITY_INSERT [dbo].[Categoria] OFF
/****** Object:  Table [dbo].[Subcategoria]    Script Date: 01/14/2012 19:25:34 ******/
SET IDENTITY_INSERT [dbo].[Subcategoria] ON
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (1, N'Cantar', 1)
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (3, N'Andar', 1)
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (4, N'Estudar', 1)
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (5, N'Comer', 1)
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (6, N'Rir', 1)
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (7, N'Jogar', 1)
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (8, N'Aula', 2)
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (9, N'Ar Livre', 2)
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (10, N'Jardim', 2)
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (11, N'Cantina', 2)
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (12, N'Biblioteca', 2)
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (13, N'Ginásio', 2)
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (14, N'Feliz', 3)
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (15, N'Triste', 3)
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (16, N'Emocionado', 3)
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (17, N'Deprimido', 3)
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (18, N'Stressado', 3)
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (19, N'Frio', 4)
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (20, N'Chuva', 4)
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (21, N'Calor', 4)
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (22, N'Nublado', 4)
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (23, N'Celebração', 5)
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (24, N'Aula', 5)
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (25, N'Exposição', 5)
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (26, N'Conferência', 5)
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (27, N'Professores', 6)
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (28, N'Alunos', 6)
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (29, N'Pais', 6)
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (30, N'Professor', 6)
INSERT [dbo].[Subcategoria] ([id], [nome], [Categoria_id]) VALUES (31, N'Funcionário', 6)
SET IDENTITY_INSERT [dbo].[Subcategoria] OFF
/****** Object:  Table [dbo].[Video]    Script Date: 01/14/2012 19:25:34 ******/
SET IDENTITY_INSERT [dbo].[Video] ON
INSERT [dbo].[Video] ([id], [descricao], [url], [data], [titulo], [Estado_id], [id_user]) VALUES (3, N'Descrição 1', N'1.wmv', CAST(0x00009FCB00000000 AS DateTime), N'Video 1', 1, N'221eca07-a1bc-4f9b-8bfa-3d3f4963f1fb')
INSERT [dbo].[Video] ([id], [descricao], [url], [data], [titulo], [Estado_id], [id_user]) VALUES (4, N'Descrição 2', N'2.wmv', CAST(0x00009FCB00000000 AS DateTime), N'Video 2', 1, N'221eca07-a1bc-4f9b-8bfa-3d3f4963f1fb')
INSERT [dbo].[Video] ([id], [descricao], [url], [data], [titulo], [Estado_id], [id_user]) VALUES (5, N'Descrição 3', N'3.wmv', CAST(0x00009FCB00000000 AS DateTime), N'Video 3', 2, N'221eca07-a1bc-4f9b-8bfa-3d3f4963f1fb')
INSERT [dbo].[Video] ([id], [descricao], [url], [data], [titulo], [Estado_id], [id_user]) VALUES (6, N'Descrição 4', N'1.wmv', CAST(0x00009FCB00000000 AS DateTime), N'Video 4', 3, N'f1868e5d-f83c-434f-aaee-701d20d286bf')
INSERT [dbo].[Video] ([id], [descricao], [url], [data], [titulo], [Estado_id], [id_user]) VALUES (7, N'Descrição 5', N'2.wmv', CAST(0x00009FCB00000000 AS DateTime), N'Video 5', 1, N'f1868e5d-f83c-434f-aaee-701d20d286bf')
INSERT [dbo].[Video] ([id], [descricao], [url], [data], [titulo], [Estado_id], [id_user]) VALUES (8, N'Descrição 6', N'3.wmv', CAST(0x00009FCB00000000 AS DateTime), N'Video 6', 1, N'f1868e5d-f83c-434f-aaee-701d20d286bf')
INSERT [dbo].[Video] ([id], [descricao], [url], [data], [titulo], [Estado_id], [id_user]) VALUES (9, N'Descrição 7', N'1.wmv', CAST(0x00009FCB00000000 AS DateTime), N'Video 7', 2, N'f1868e5d-f83c-434f-aaee-701d20d286bf')
SET IDENTITY_INSERT [dbo].[Video] OFF
/****** Object:  Table [dbo].[SubcategoriaVideo]    Script Date: 01/14/2012 19:25:34 ******/
