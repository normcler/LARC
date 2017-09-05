CREATE TABLE PortfolioPortfolioHolding
(
	PortfolioID INT NOT NULL,
	PortfolioHoldingSymbol NVARCHAR(50) NOT NULL,
	CONSTRAINT PK_PortfolioPortfolioHoling 
		PRIMARY KEY (PortfolioID, PortfolioHoldingSymbol),
	CONSTRAINT FK_PortfolioPortfolioHolding_Portfolio
		FOREIGN KEY (PortfolioID) REFERENCES PortfolioDB(ID)
		ON DELETE CASCADE,
	CONSTRAINT FK_PortfolioPortfolioHolding_PortfolioHolding
		FOREIGN KEY (PortfolioHoldingSymbol) REFERENCES PortfolioHolding(Symbol)
		ON DELETE CASCADE
)