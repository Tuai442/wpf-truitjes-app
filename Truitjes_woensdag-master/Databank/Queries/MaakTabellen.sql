
CREATE TABLE Clubs (
    ClubId int IDENTITY,
    Competitie varchar(200) not null,
    PloegNaam varchar(200) not null,

    PRIMARY KEY (ClubId),
);


CREATE TABLE ClubSets (
    ClubSetId int IDENTITY,
    Uit bit not null,
    Versie varchar(200) not null,

    PRIMARY KEY (ClubSetId),
);

CREATE TABLE Truitjes (
    TruitjeId int IDENTITY,
    Prijs float not null,
    Seizoen varchar(200) not null,
    Maat varchar(100) not null,
    ClubId int not null,
    ClubSetId int not null,

    PRIMARY KEY (TruitjeId),
    FOREIGN KEY (ClubId) REFERENCES Clubs(ClubId),
    FOREIGN KEY (ClubSetId) REFERENCES ClubSets(ClubSetId),
);

CREATE TABLE Bestellingen (
    BestellingId int IDENTITY,
    Prijs float not null,
    Betaald bit not null,

    TruitjeId int not null,
    PRIMARY KEY (BestellingId),
    FOREIGN KEY (TruitjeId) REFERENCES Truitjes(TruitjeId)
);








