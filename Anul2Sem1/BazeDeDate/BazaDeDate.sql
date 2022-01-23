CREATE DATABASE ConcursDeTriatlon;

CREATE TABLE SportiviLegitimati (
    Id INT NOT NULL,
    Nume VARCHAR(255) NOT NULL,
    Prenume VARCHAR(255) NOT NULL,
    AnNastere DATETIME NULL,
    Sex VARCHAR(10) NULL,
    DenumireClub VARCHAR(255) NULL,
    PRIMARY KEY (Id)
);

INSERT INTO SportiviLegitimati VALUES
    (1, 'Doe', 'Jane', '2002-06-20 00:00:00', 'feminin', 'Castigatorii'),
    (2, 'Doe', 'John', '2001-04-21 00:00:00', 'masculin', 'Cei Mai Buni'),
    (3, 'Pelle', 'Remus', '1999-03-15 00:00:00', 'masculin', 'Random Club Name');
    
CREATE TABLE Concursuri (
    Id_Concurs INT NOT NULL,
    Localitate VARCHAR(255) NULL,
    `Data` DATETIME NULL,
    Descriere VARCHAR(255) NULL,
    PRIMARY KEY (Id_Concurs)
);

INSERT INTO Concursuri VALUES
    (1, 'Cluj', '2021-05-10 00:00:00', 'etapa 1 de calificare'),
    (2, 'Constanta', '2021-06-02 00:00:00', 'etapa 2 de calificare'),
    (3, 'Bucuresti', '2021-06-27 00:00:00', 'Campionat national');

CREATE TABLE Discipline (
    Id_Disciplina INT NOT NULL,
    Descriere VARCHAR(255) NULL,
    PRIMARY KEY (Id_Disciplina)
);

INSERT INTO Discipline VALUES
    (1, 'ironman'),
    (2, 'halfironman'),
    (3, 'proba amatori');

CREATE TABLE ConcursuriDiscipline (
    Id INT NOT NULL,
    Id_Concurs INT NOT NULL,
    Id_Disciplina INT NOT NULL,
    PRIMARY KEY (Id),
    FOREIGN KEY (Id_Concurs) REFERENCES Concursuri(Id_Concurs),
    FOREIGN KEY (Id_Disciplina) REFERENCES Discipline(Id_Disciplina)
);

INSERT INTO ConcursuriDiscipline VALUES
    (1, 1, 3),
    (2, 1, 2),
    (3, 2, 3),
    (4, 2, 2),
    (5, 3, 2),
    (6, 3, 1);

CREATE TABLE SportiviInscrisiConcursuri (
    Id_Participare INT NOT NULL,
    Id_Sportiv INT NOT NULL,
    Id_Concurs INT NOT NULL,
    Id_Disciplina INT NOT NULL,
    PRIMARY KEY (Id_Participare),
    FOREIGN KEY (Id_Sportiv) REFERENCES SportiviLegitimati(Id),
    FOREIGN KEY (Id_Concurs) REFERENCES Concursuri(Id_Concurs),
    FOREIGN KEY (Id_Disciplina) REFERENCES Discipline(Id_Disciplina)
);

INSERT INTO SportiviInscrisiConcursuri VALUES
    (1, 1, 1, 1),
    (2, 1, 2, 1),
    (3, 1, 3, 1),
    (4, 2, 1, 3),
    (5, 2, 2, 3),
    (6, 2, 3, 3),
    (7, 3, 1, 2),
    (8, 3, 2, 2);

CREATE TABLE RezultateSportivi (
    Id INT NOT NULL,
    Id_Sportiv INT NOT NULL,
    Id_Participare INT NOT NULL,
    Inot TIME NULL,
    Bicicleta TIME NULL,
    Alergare TIME NULL,
    RezultatFinal TIME NULL,
    PRIMARY KEY (Id),
    FOREIGN KEY (Id_Sportiv) REFERENCES SportiviLegitimati(Id),
    FOREIGN KEY (Id_Participare) REFERENCES SportiviInscrisiConcursuri(Id_Participare)
);

INSERT INTO RezultateSportivi (Id, Id_Participare, Inot, Bicicleta, Alergare) VALUES
    (1, 1, '00:12:05', '00:15:21', '00:10:59'),
    (2, 2, '00:15:05', NULL, '00:12:59'),
    (3, 3, NULL, '00:17:21', '00:13:59'),
    (4, 4, '00:16:05', '00:13:21', NULL),
    (5, 5, NULL, '00:08:21', NULL),
    (6, 6, '00:13:05', NULL, NULL),
    (7, 7, '00:10:05', '00:09:21', '00:11:59'),
    (8, 8, NULL, NULL, '00:11:59');
