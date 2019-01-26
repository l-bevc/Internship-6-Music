
BEGIN TRANSACTION
CREATE TABLE Musicians(
	MusicianId INT PRIMARY KEY IDENTITY,
	Name nvarchar(50) NOT NULL,
	Nationality nvarchar(50)
)

CREATE TABLE Albums(
	AlbumId INT PRIMARY KEY IDENTITY,
	Name nvarchar(50) NOT NULL,
	TimeOfPublish datetime2,
	MusicianId INT
)

CREATE TABLE Songs(
	SongId INT PRIMARY KEY IDENTITY,
	Name nvarchar(50) NOT NULL,
	Size time
)


COMMIT

BEGIN TRANSACTION

ALTER TABLE Albums
ADD FOREIGN KEY (MusicianId) REFERENCES Musicians(MusicianId)

CREATE TABLE AlbumSong(
	AlbumID INT NOT NULL FOREIGN KEY REFERENCES Albums(AlbumId),
	SongID INT NOT NULL FOREIGN KEY REFERENCES Songs(SongId)	
)

COMMIT

BEGIN TRANSACTION

INSERT INTO Musicians
(
    Name,
    Nationality
)
VALUES
(
    N'Prljavo Kazalište',
    N'Croatian'
),
(
    N'Red Hot Chili Peppers',
    N'American'
),
(
    N'Queen',
    N'English'
)


INSERT INTO Albums
(
    Name,
    TimeOfPublish,
    MusicianId
)
VALUES
(
    N'Crno bijeli svijet', -- Name - nvarchar
    '1980-06-22', -- TimeOfPublish - datetime2
    1 -- MusicianId - INT
),
(
    N'Heroj Ulice', -- Name - nvarchar
    '1980-01-01', -- TimeOfPublish - datetime2
    1 -- MusicianId - INT
),
(
    N'Californication', -- Name - nvarchar
    '1999-06-08', -- TimeOfPublish - datetime2
    2-- MusicianId - INT
),
(
    N'The Getaway', -- Name - nvarchar
    '2016-06-17', -- TimeOfPublish - datetime2
    2 -- MusicianId - INT
),
(
    N'A Night at the Opera', -- Name - nvarchar
    '1975-11-21', -- TimeOfPublish - datetime2
    3 -- MusicianId - INT
),
(
    N'A Day at the Races', -- Name - nvarchar
    '1976-12-10', -- TimeOfPublish - datetime2
    3 -- MusicianId - INT
)

INSERT INTO Songs
(
    Name,
    Size
)
VALUES
(
    N'Crno bijeli svijet', -- Name - nvarchar
    '00:03:11' -- Size - time
),
(
    N'Moderna djevojka', -- Name - nvarchar
    '00:03:54' -- Size - time
),
(
    N'Nove cipele', -- Name - nvarchar
    '00:03:29' -- Size - time
),
(
    N'Zagreb', -- Name - nvarchar
    '00:03:06' -- Size - time
),
(
    N'Mi plešemo', -- Name - nvarchar
    '00:04:31' -- Size - time
),
(
    N'Heroj ulice', -- Name - nvarchar
    '00:04:36' -- Size - time
),
(
    N'Amerika', -- Name - nvarchar
    '00:03:48' -- Size - time
),
(
    N'Djevojka snova', -- Name - nvarchar
    '00:03:10' -- Size - time
),
(
    N'Djevojke bi', -- Name - nvarchar
    '00:02:39' -- Size - time
),
(
    N'Lupam glavom u radio', -- Name - nvarchar
    '00:02:39' -- Size - time
),
(
    N'Around the World', -- Name - nvarchar
    '00:03:58' -- Size - time
),
(
    N'Scar Tissue', -- Name - nvarchar
    '00:03:37' -- Size - time
),
(
    N'Otherside', -- Name - nvarchar
    '00:04:15' -- Size - time
),
(
    N'Right on Time', -- Name - nvarchar
    '00:01:52' -- Size - time
),
(
    N'Road Trippin', -- Name - nvarchar
    '00:03:25' -- Size - time
),
(
    N'The Getaway', -- Name - nvarchar
    '00:04:10' -- Size - time
),
(
    N'Sick Love', -- Name - nvarchar
    '00:03:41' -- Size - time
),
(
    N'The Hunter', -- Name - nvarchar
    '00:04:00' -- Size - time
),
(
    N'We Turn Red', -- Name - nvarchar
    '00:03:20' -- Size - time
),
(
    N'Goodbye Angels', -- Name - nvarchar
    '00:04:29' -- Size - time
),
(
    N'Bohemian Rhapsody', -- Name - nvarchar
    '00:05:57' -- Size - time
),
(
    N'Love of My Life', -- Name - nvarchar
    '00:03:38' -- Size - time
),
(
    N'Good Company', -- Name - nvarchar
    '00:03:26' -- Size - time
),
(
    N'39', -- Name - nvarchar
    '00:03:30' -- Size - time
),
(
    N'Sweet Lady', -- Name - nvarchar
    '00:04:01' -- Size - time
),
(
    N'Somebody to Love', -- Name - nvarchar
    '00:04:56' -- Size - time
),
(
    N'You and I', -- Name - nvarchar
    '00:03:25' -- Size - time
),
(
    N'Long Away', -- Name - nvarchar
    '00:05:09' -- Size - time
),
(
    N'Tie Your Mother Down', -- Name - nvarchar
    '00:04:48' -- Size - time
),
(
    N'White Man', -- Name - nvarchar
    '00:04:59' -- Size - time
)


INSERT INTO AlbumSong
(
    AlbumID,
    SongID
)
VALUES
(
	2,
	1
),
(
    1, -- AlbumID - int
    1 -- SongID - int
),
(
    1, -- AlbumID - int
    2 -- SongID - int
),
(
    1, -- AlbumID - int
    3 -- SongID - int
),
(
    1, -- AlbumID - int
    4 -- SongID - int
),
(
    1, -- AlbumID - int
    5 -- SongID - int
),
(
    2, -- AlbumID - int
    6 -- SongID - int
),
(
    2, -- AlbumID - int
    7-- SongID - int
),
(
    2, -- AlbumID - int
    8 -- SongID - int
),
(
    2, -- AlbumID - int
    9 -- SongID - int
),
(
    2, -- AlbumID - int
    10 -- SongID - int
),
(
    3, -- AlbumID - int
    11 -- SongID - int
),
(
    3, -- AlbumID - int
    12 -- SongID - int
),
(
    3, -- AlbumID - int
    13 -- SongID - int
),
(
    3, -- AlbumID - int
    14 -- SongID - int
),
(
    3, -- AlbumID - int
    15 -- SongID - int
),
(
    4, -- AlbumID - int
    16 -- SongID - int
),
(
    4, -- AlbumID - int
    17 -- SongID - int
),
(
    4, -- AlbumID - int
    18 -- SongID - int
),
(
    4, -- AlbumID - int
    19 -- SongID - int
),
(
    4, -- AlbumID - int
    20 -- SongID - int
),
(
    5, -- AlbumID - int
    21 -- SongID - int
),
(
    5, -- AlbumID - int
    22 -- SongID - int
),
(
    5, -- AlbumID - int
    23 -- SongID - int
),
(
    5, -- AlbumID - int
    24 -- SongID - int
),
(
    5, -- AlbumID - int
    25 -- SongID - int
),
(
    6, -- AlbumID - int
    26 -- SongID - int
),
(
    6, -- AlbumID - int
    27 -- SongID - int
),
(
    6, -- AlbumID - int
    28 -- SongID - int
),
(
    6, -- AlbumID - int
    29 -- SongID - int
),
(
    6, -- AlbumID - int
    30 -- SongID - int
)


COMMIT

