CREATE TABLE [dbo].[Person]
(
	[Id] INT NOT NULL IDENTITY(1,1),
	[ParticipantId] INT NOT NULL,
	[Login] NVARCHAR(200) NOT NULL,
	[Password] NVARCHAR(MAX) NOT NULL,
	[Role] NVARCHAR(100) NOT NULL,

	CONSTRAINT pk_Person PRIMARY KEY ([Id]),
	CONSTRAINT un_Person UNIQUE ([Login]),

	CONSTRAINT fk_Person_Participant_Id FOREIGN KEY ([ParticipantId])
		REFERENCES [dbo].[Participant] ([Id])
		ON DELETE CASCADE
)
