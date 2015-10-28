SET IDENTITY_INSERT [dbo].[Users] ON
INSERT INTO [dbo].[Users] ([UserID], [Name], [Password], [Enabled]) VALUES (1, N'admin', N'secret', 0)
SET IDENTITY_INSERT [dbo].[Users] OFF
