CREATE TABLE [dbo].[EventImage]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[EventId] INT NOT NULL,
	[Number] INT NOT NULL,
	[Data] VARBINARY(MAX) NOT NULL,

	CONSTRAINT pk_EventImage PRIMARY KEY ([Id]),
	CONSTRAINT un_EventImage UNIQUE ([EventId], [Number]),

	CONSTRAINT fk_EventImage_Event_Id FOREIGN KEY ([EventId])
		REFERENCES [dbo].[Event]([Id])
		ON DELETE CASCADE
)
