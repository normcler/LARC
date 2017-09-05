CREATE TABLE PortfolioHolding
(
	Symbol NVARCHAR(50) NOT NULL,
	NumberOfShares DECIMAL NOT NULL
        CONSTRAINT FK_PortfolioHolding PRIMARY KEY (Symbol)
)