CREATE TABLE [dbo].[PirateShip]
(
    [PirateId] INT NOT NULL, 
    [ShipId] INT NOT NULL, 
    CONSTRAINT [PK_PirateShip] PRIMARY KEY ([ShipId], [PirateId]), 
    CONSTRAINT [FK_PirateShip_Pirate] FOREIGN KEY ([PirateId]) REFERENCES [Pirate]([Id]), 
    CONSTRAINT [FK_PirateShip_Ship] FOREIGN KEY ([ShipId]) REFERENCES [Ship]([Id])
)
