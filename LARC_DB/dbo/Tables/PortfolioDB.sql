﻿CREATE TABLE PortfolioDB
(
	ID INT IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(50) NOT NULL	
	CONSTRAINT PK_PortfolioDB PRIMARY KEY (ID),
)