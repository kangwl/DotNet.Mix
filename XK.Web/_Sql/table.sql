﻿CREATE TABLE [USER](
	ID INT IDENTITY PRIMARY KEY,
	UserID VARCHAR(20),
	[Password] VARCHAR(20),
	Name NVARCHAR(20),
	Sex INT,
	Birthday DATETIME,
	Email VARCHAR(60)
)