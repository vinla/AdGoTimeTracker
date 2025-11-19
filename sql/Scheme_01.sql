CREATE TABLE TimeTrackerEntries (
	Id int not null IDENTITY(1,1),
	UserId varchar(255) not null,
	Description varchar(255) not null,
	StartTime datetime not null,
	EndTime datetime not null,
	PRIMARY KEY (Id)
)