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
/****** Object:  Table [dbo].[aspnet_WebEvent_Events]    Script Date: 01/14/2012 19:25:34 ******/
/****** Object:  Table [dbo].[aspnet_SchemaVersions]    Script Date: 01/14/2012 19:25:34 ******/
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'common', N'1', 1)
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'health monitoring', N'1', 1)
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'membership', N'1', 1)
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'personalization', N'1', 1)
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'profile', N'1', 1)
INSERT [dbo].[aspnet_SchemaVersions] ([Feature], [CompatibleSchemaVersion], [IsCurrentVersion]) VALUES (N'role manager', N'1', 1)
/****** Object:  Table [dbo].[aspnet_Membership]    Script Date: 01/14/2012 19:25:34 ******/
INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'a52f28e3-06e8-4e59-aac6-7b15f45c56e5', N'221eca07-a1bc-4f9b-8bfa-3d3f4963f1fb', N'bnalmV2hAYx7k3Yz2uHktLAn2z4=', 1, N'McYOyEP4IkI8Y1eAThzscA==', NULL, N'user@gmail.com', N'user@gmail.com', NULL, NULL, 1, 0, CAST(0x00009FD30162C6A8 AS DateTime), CAST(0x00009FD60164F922 AS DateTime), CAST(0x00009FD60164B2B5 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'a52f28e3-06e8-4e59-aac6-7b15f45c56e5', N'f1868e5d-f83c-434f-aaee-701d20d286bf', N'4cTIoxoe3zklspbkKo5SWazO7vU=', 1, N'4SYT/s84jU64RouHVX64dQ==', NULL, N'beatrizpousinha@gmail.com', N'beatrizpousinha@gmail.com', NULL, NULL, 1, 0, CAST(0x00009FD200F8D978 AS DateTime), CAST(0x00009FD8011D93EE AS DateTime), CAST(0x00009FD200F8D978 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
INSERT [dbo].[aspnet_Membership] ([ApplicationId], [UserId], [Password], [PasswordFormat], [PasswordSalt], [MobilePIN], [Email], [LoweredEmail], [PasswordQuestion], [PasswordAnswer], [IsApproved], [IsLockedOut], [CreateDate], [LastLoginDate], [LastPasswordChangedDate], [LastLockoutDate], [FailedPasswordAttemptCount], [FailedPasswordAttemptWindowStart], [FailedPasswordAnswerAttemptCount], [FailedPasswordAnswerAttemptWindowStart], [Comment]) VALUES (N'a52f28e3-06e8-4e59-aac6-7b15f45c56e5', N'd71d66b0-b150-4128-b74f-8a78aed03624', N'fV3eREHu8nxrkMWuC8VwndQiT5g=', 1, N'L4U6jTg5grobVjiuSkGFjg==', NULL, N'pppluis@gmail.com', N'pppluis@gmail.com', NULL, NULL, 1, 0, CAST(0x00009FD201709508 AS DateTime), CAST(0x00009FD700AB7F21 AS DateTime), CAST(0x00009FD201709508 AS DateTime), CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), 0, CAST(0xFFFF2FB300000000 AS DateTime), NULL)
/****** Object:  Table [dbo].[aspnet_Applications]    Script Date: 01/14/2012 19:25:34 ******/
INSERT [dbo].[aspnet_Applications] ([ApplicationName], [LoweredApplicationName], [ApplicationId], [Description]) VALUES (N'/', N'/', N'a52f28e3-06e8-4e59-aac6-7b15f45c56e5', NULL)
/****** Object:  Table [dbo].[aspnet_Profile]    Script Date: 01/14/2012 19:25:34 ******/
/****** Object:  Table [dbo].[aspnet_Paths]    Script Date: 01/14/2012 19:25:34 ******/
/****** Object:  Table [dbo].[aspnet_Users]    Script Date: 01/14/2012 19:25:34 ******/
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'a52f28e3-06e8-4e59-aac6-7b15f45c56e5', N'f1868e5d-f83c-434f-aaee-701d20d286bf', N'beatriz', N'beatriz', NULL, 0, CAST(0x00009FD80137F4A6 AS DateTime))
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'a52f28e3-06e8-4e59-aac6-7b15f45c56e5', N'207cffe6-d579-46fd-80e8-9e4c9d1172a6', N'ffa', N'ffa', NULL, 0, CAST(0x00009FD1014E5E70 AS DateTime))
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'a52f28e3-06e8-4e59-aac6-7b15f45c56e5', N'159aa77b-9e3d-49e0-8dce-5a650625a420', N'ola', N'ola', NULL, 0, CAST(0x00009FD10158BF14 AS DateTime))
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'a52f28e3-06e8-4e59-aac6-7b15f45c56e5', N'd71d66b0-b150-4128-b74f-8a78aed03624', N'paulo', N'paulo', NULL, 0, CAST(0x00009FD700ABAB69 AS DateTime))
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'a52f28e3-06e8-4e59-aac6-7b15f45c56e5', N'4de6b8a2-48ea-4d87-91e3-8a15cf96bb89', N'pauloluis', N'pauloluis', NULL, 0, CAST(0x00009FD1012AFFFA AS DateTime))
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'a52f28e3-06e8-4e59-aac6-7b15f45c56e5', N'5ab47680-b2e5-4490-ab87-b08114302113', N'teste', N'teste', NULL, 0, CAST(0x00009FD1014C4A68 AS DateTime))
INSERT [dbo].[aspnet_Users] ([ApplicationId], [UserId], [UserName], [LoweredUserName], [MobileAlias], [IsAnonymous], [LastActivityDate]) VALUES (N'a52f28e3-06e8-4e59-aac6-7b15f45c56e5', N'221eca07-a1bc-4f9b-8bfa-3d3f4963f1fb', N'user', N'user', NULL, 0, CAST(0x00009FD601650395 AS DateTime))
/****** Object:  Table [dbo].[aspnet_Roles]    Script Date: 01/14/2012 19:25:34 ******/
INSERT [dbo].[aspnet_Roles] ([ApplicationId], [RoleId], [RoleName], [LoweredRoleName], [Description]) VALUES (N'a52f28e3-06e8-4e59-aac6-7b15f45c56e5', N'ce999d04-6b0e-48a0-a86c-55df2c0e4aa8', N'admin', N'admin', NULL)
INSERT [dbo].[aspnet_Roles] ([ApplicationId], [RoleId], [RoleName], [LoweredRoleName], [Description]) VALUES (N'a52f28e3-06e8-4e59-aac6-7b15f45c56e5', N'3043da6a-e1c1-4862-9bf0-c8806c48179a', N'user', N'user', NULL)
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
/****** Object:  Table [dbo].[aspnet_UsersInRoles]    Script Date: 01/14/2012 19:25:34 ******/
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'4de6b8a2-48ea-4d87-91e3-8a15cf96bb89', N'ce999d04-6b0e-48a0-a86c-55df2c0e4aa8')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'd71d66b0-b150-4128-b74f-8a78aed03624', N'ce999d04-6b0e-48a0-a86c-55df2c0e4aa8')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'221eca07-a1bc-4f9b-8bfa-3d3f4963f1fb', N'3043da6a-e1c1-4862-9bf0-c8806c48179a')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'159aa77b-9e3d-49e0-8dce-5a650625a420', N'3043da6a-e1c1-4862-9bf0-c8806c48179a')
INSERT [dbo].[aspnet_UsersInRoles] ([UserId], [RoleId]) VALUES (N'f1868e5d-f83c-434f-aaee-701d20d286bf', N'3043da6a-e1c1-4862-9bf0-c8806c48179a')
/****** Object:  Table [dbo].[aspnet_PersonalizationPerUser]    Script Date: 01/14/2012 19:25:34 ******/
/****** Object:  Table [dbo].[aspnet_PersonalizationAllUsers]    Script Date: 01/14/2012 19:25:34 ******/
