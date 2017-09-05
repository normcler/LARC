CREATE TABLE PortfolioFunds
(
	PortfolioID INT NOT NULL,
	[FundSymbol] NVARCHAR(50) NOT NULL,
	[NumberOfShares] DECIMAL(10, 3) NULL, 
    CONSTRAINT PK_PortfolioFunds 
		PRIMARY KEY (PortfolioID, [FundSymbol]),
	CONSTRAINT FK_PortfolioFunds_Portfolio
		FOREIGN KEY (PortfolioID) REFERENCES PortfolioDB(ID)
		ON DELETE CASCADE,
	CONSTRAINT FK_PortfolioFunds_Funds
		FOREIGN KEY ([FundSymbol]) REFERENCES Funds(Symbol)
		ON DELETE CASCADE
)