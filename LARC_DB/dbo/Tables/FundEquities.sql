CREATE TABLE [dbo].[FundEquities]
(
	[FundSymbol] NVARCHAR(50) NOT NULL,
	[EquitySymbol] NVARCHAR(50) NOT NULL,
	[Weighting] DECIMAL(10,3) NOT NULL,
	[Shares] INT NOT NULL, 
    CONSTRAINT [PK_FundEquities] PRIMARY KEY ([EquitySymbol], [FundSymbol]), 
    CONSTRAINT [FK_FundEquities_Funds] FOREIGN KEY (FundSymbol) REFERENCES Funds(Symbol), 
    CONSTRAINT [FK_FundEquities_Equities] FOREIGN KEY (EquitySymbol) REFERENCES Equities(Symbol),
)
