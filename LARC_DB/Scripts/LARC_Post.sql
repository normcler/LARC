/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
INSERT INTO Address (Line1, Line2, PostalCode) 
	VALUES ('360 E. Randolph St. Apt. 408',
					'Chicago, IL', '60601')

INSERT INTO PortfolioDB (Name) VALUES ('test_portfolio')

INSERT INTO Funds (Symbol) VALUES 
('FPMAX'), 
('FSEVX'), 
('PRASX'), 
('PRHSX'),
('PRITX'), 
('PRNHX'), 
('VEMAX'), 
('VEUSX'), 
('VFSVX'), 
('VPADX'), 
('VTMSX'), 
('VTRIX')

DECLARE @portfolioId INT
SET @portfolioId = (SELECT ID FROM PortfolioDB WHERE Name = 'test_portfolio')

INSERT INTO PortfolioFunds (PortfolioID, FundSymbol, NumberOfShares) VALUES 
(@portfolioId, 'FPMAX', 3766.487),
(@portfolioId, 'FSEVX', 862.661),
(@portfolioId, 'PRASX', 5046.534),
(@portfolioId, 'PRHSX', 728.24),
(@portfolioId, 'PRITX', 3928.275),
(@portfolioId, 'PRNHX', 686.556),
(@portfolioId, 'VEMAX', 2515.0),
(@portfolioId, 'VEUSX', 237.304),
(@portfolioId, 'VFSVX', 830.336),
(@portfolioId, 'VPADX', 186.273),
(@portfolioId, 'VTMSX', 693.775),
(@portfolioId, 'VTRIX', 560.45)

INSERT INTO AccountDB (HomeAddressID, BillingAddressID, FirstName, LastName, Email)
	VALUES (1, 1, 'Norman', 'Clerman', 'norm.clerman@gmail.com')

INSERT INTO ClientDB (AccountID, PortfolioID) VALUES (1, 1)