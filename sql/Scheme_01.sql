CREATE TABLE TimeTrackerEntries (
	Id int not null IDENTITY(1,1),
	Description varchar(255) not null,
	StartTime datetime not null,
	EndTime datetime not null,
	PRIMARY KEY (Id)
)