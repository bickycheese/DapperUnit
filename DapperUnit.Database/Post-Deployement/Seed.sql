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

insert into Country (Name) values ('Belgium')

insert into Pirate (Name, CountryId) values ('Stanny', 1)
insert into Pirate (Name, CountryId) values ('Jack', 1)

insert into Ship (Name, CountryId) values ('Marie Louise', 1)
insert into Ship (Name, CountryId) values ('Black Pearl', 1)

insert into PirateShip values (1, 1)
insert into PirateShip values (2, 1)
insert into PirateShip values (2, 2)
