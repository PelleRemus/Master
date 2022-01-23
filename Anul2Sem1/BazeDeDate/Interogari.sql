# 1. Afisati sportivii legitimati la un anumit club
SELECT * FROM SportiviLegitimati
WHERE DenumireClub = 'Random Club Name';


# 2. Afisati lista concursurilor dintr-un anumit interval de timp
SELECT * FROM Concursuri
WHERE `Data` > '2021-06-01' AND `Data` < '2021-06-30';


# 3. Afisati lista disciplinelor disponibile la concursul de la Cluj
SELECT d.* FROM Discipline d
JOIN ConcursuriDiscipline cd
    ON cd.Id_Disciplina = d.Id_Disciplina
JOIN Concursuri c
    ON c.Id_Concurs = cd.Id_Concurs
WHERE c.Localitate = 'Cluj';


# 4. Afisati sportivii care au terminat toate probele la concursul de la cluj
SELECT sl.*, c.Localitate, rs.Inot, rs.Bicicleta, rs.Alergare FROM SportiviLegitimati sl
JOIN SportiviInscrisiConcursuri sic
    ON sl.Id = sic.Id_Sportiv
JOIN Concursuri c
    ON sic.Id_Concurs = c.Id_Concurs
JOIN RezultateSportivi rs
    ON sic.Id_Participare = rs.Id_Participare
WHERE c.Localitate = 'Cluj'
    AND rs.Inot IS NOT NULL AND rs.Bicicleta IS NOT NULL AND rs.Alergare IS NOT NULL;


#5. Afisati sportivii care nu au terminat concursul de la Cluj
SELECT sl.*, c.Localitate, rs.Inot, rs.Bicicleta, rs.Alergare FROM SportiviLegitimati sl
JOIN SportiviInscrisiConcursuri sic
    ON sl.Id = sic.Id_Sportiv
JOIN Concursuri c
    ON sic.Id_Concurs = c.Id_Concurs
JOIN RezultateSportivi rs
    ON sic.Id_Participare = rs.Id_Participare
WHERE c.Localitate = 'Cluj'
    AND (rs.Inot IS NULL OR rs.Bicicleta IS NULL OR rs.Alergare IS NULL);


#7. Creati o PS ce calculeaza rezultatele sortivilor dupa participarea la un concurs
SET SQL_SAFE_UPDATES = 0;
DROP PROCEDURE IF EXISTS CalculeazaRezultatFinal;
DELIMITER $
CREATE PROCEDURE CalculeazaRezultatFinal()
BEGIN
    UPDATE RezultateSportivi
    SET RezultatFinal = AddTime(AddTime(IfNull(Inot, '0'), IfNull(Bicicleta, '0')), IfNull(Alergare, '0'));
END $
CALL CalculeazaRezultatFinal();


#6. Afisati pentru un sportiv cel mai bun rezultat dintre toate rezultatele obtinute la concursurile la care a participat
SELECT sl.*, MIN(rs.RezultatFinal) `Cel Mai Bun Rezultat` FROM SportiviLegitimati sl
JOIN RezultateSportivi rs
    ON sl.Id = rs.Id_Sportiv
GROUP BY sl.Id;


#8. Creati o PS care afiseaza rezultatul unui sportiv la un concurs si pozitia ocupata
DROP PROCEDURE IF EXISTS GasestePozitiileSportivilor;
DELIMITER $
CREATE PROCEDURE GasestePozitiileSportivilor()
BEGIN
    SELECT sl.Id, sl.Nume, sl.Prenume, c.Localitate, rs.RezultatFinal,
        ROW_NUMBER() OVER(PARTITION BY sic.Id_Concurs ORDER BY rs.RezultatFinal) Pozitia
	FROM SportiviLegitimati sl
	JOIN SportiviInscrisiConcursuri sic
		ON sl.Id = sic.Id_Sportiv
	JOIN Concursuri c
		ON sic.Id_Concurs = c.Id_Concurs
	JOIN RezultateSportivi rs
		ON sic.Id_Participare = rs.Id_Participare;
END $
CALL GasestePozitiileSportivilor();


#9. Creati un view in care sa avem sportivii ordonati in ordine descrescatoare dupa rezultatul din etapa1
CREATE VIEW Etapa1Inot AS
SELECT sl.*, rs.Inot FROM SportiviLegitimati sl
JOIN RezultateSportivi rs
    ON sl.Id = rs.Id_Sportiv
GROUP BY sl.Id
ORDER BY rs.Inot DESC;
