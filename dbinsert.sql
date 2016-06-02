/*
Kunden Tabelle füllen
*/
/*Kunden in Kunden einfügen*/
insert into kunden (Nachname,Name,Geburtsdatum,EMail,PLZ,Ort,Strasse,Hausnummer,Passwort) values (
'Müller', 'Jens', '1980-10-06', 'jmueller@gmall.com', '07741', 'Jena', 'Lessingstraße', '8', 'Jens1980');
insert into kunden (Nachname,Name,Geburtsdatum,EMail,PLZ,Ort,Strasse,Hausnummer,Passwort) values (
'Meier', 'Heike', '1978-01-26', 'heimei@gxm.net', '01201', 'Dresden', 'Gartengasse', '13', 'MeinAmazonPasswort');
insert into kunden (Nachname,Name,Geburtsdatum,EMail,PLZ,Ort,Strasse,Hausnummer,Passwort) values (
'Schmidt', 'Katy', '1988-05-02', 'kaschmi@wep.ed', '10119', 'Berlin', 'van-der-Waals-Straße', '7', 'lang3sSich3resPWmitZ4hlen');
insert into kunden (Nachname,Name,Geburtsdatum,EMail,PLZ,Ort,Strasse,Hausnummer,Passwort) values (
'Schulz', 'Harry', '1963-12-20', 'harry@schulz.web', '50859', 'Köln', 'Stadtmauer', '44', 'schnulzenschulz');
insert into kunden (Nachname,Name,Geburtsdatum,EMail,PLZ,Ort,Strasse,Hausnummer,Passwort) values (
'Paul', 'Andrea', '1980-08-19', 'andi_paul@gmall.com', '10247', 'Berlin', 'Wiesenallee', '19', 'cvbgfhnjmklrftgz');
insert into kunden (Nachname,Name,Geburtsdatum,EMail,PLZ,Ort,Strasse,Hausnummer,Passwort) values (
'Muntz', 'Nelson', '1987-04-30', 'nm@yuhuu.com', '07743', 'Jena', 'Dornburger Straße', '1', '123456');
insert into kunden (Nachname,Name,Geburtsdatum,EMail,PLZ,Ort,Strasse,Hausnummer,Passwort) values (
'Zipfel', 'Karl', '1994-09-13', 'zippi@yodapho.ne', '99092', 'Erfurt', 'Ratsplatz', '12', 'zehnkleinejaegermeister');

/*Händler in Kunden einfügen*/
insert into kunden (Firma,PLZ,Ort,Strasse,Hausnummer,Haendler,Passwort) values (
'Radshop Plus', '01326', 'Dresden', 'Schillerstrasse', '20', 1, 'Radshop2010');
insert into kunden (Firma,PLZ,Ort,Strasse,Hausnummer,Haendler,Passwort) values (
'Bikefreunde', '07351', 'Lobenstein', 'Hauptstrasse', '33', 1, 'Abteilung_Einkauf');
insert into kunden (Nachname,Name,EMail,Firma,PLZ,Ort,Strasse,Hausnummer,Haendler,Passwort) values (
'Schnelle', 'Otto', 'info@ottos-fahrrad.de', 'Ottos Fahrradladen', '07743', 'Jena', 'Neugasse', '5', 1, 'flotterotto81');

select * from kunden;

/*
Produktkategorie Tabelle füllen
*/
insert into produktkategorie (Bezeichnung) values ('Rahmen');/*1*/
insert into produktkategorie (Bezeichnung) values ('Lenker');/*2*/
insert into produktkategorie (Bezeichnung) values ('Federung');/*3*/
insert into produktkategorie (Bezeichnung) values ('Sattel');/*4*/
insert into produktkategorie (Bezeichnung) values ('Schaltung');/*5*/
insert into produktkategorie (Bezeichnung) values ('Rad');/*6*/
insert into produktkategorie (Bezeichnung) values ('Reifen');/*7*/
insert into produktkategorie (Bezeichnung) values ('Klingel');/*8*/
insert into produktkategorie (Bezeichnung) values ('Licht');/*9*/
insert into produktkategorie (Bezeichnung) values ('Griff');/*10*/

select *  from produktkategorie;

/*
Produkt Tabelle füllen
*/
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'Diamant Standard Herren', 1, 180.75);
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'Diamant Standard Damen', 1, 180.75);
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'Schauff Tracking mit Gepäck', 1, 270.00);
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'Cube Downhill', 1, 419.90);
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'Conquest Ergonomic Desaster', 2, 120.00);
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'Handgelenkschmeichler', 2, 165.25);
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'Damenlenker Standard', 2, 100.00);
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'Doppelgabel', 3, 129.80);
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'Einarm rechts', 3, 170.50);
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'Einarm links', 3, 170.50);
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'Rennsattel hart', 4, 50.00);
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'TrackingTrainer Dura', 4, 80.00);
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'Stadttouring Echtleder', 4, 37.73);
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'Nabenschaltung 3 Gang', 5, 55.00);
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'Nabenschaltung 5 Gang', 5, 91.27);
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'Shimano KickIt 28k', 5, 320.50);
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'Zahnradschaltung Standard 21 Gang', 5, 75.75);
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'"Rolling Stone" 28" Downhillfelge', 6, 110.25);
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'Felge 24"', 6, 25.00);
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'Felge 26"', 6, 30.00);
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'Dunlop superslim Rennreifen', 7, 15.00);
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'Vulkanos Destroyer Grip 1000', 7, 45.16);
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'Gummireifen "walking home"', 7, 9.99);
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'Anschlagglocke', 8, 5.13);
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'Radialkraftglocke', 8, 8.95);
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'Dynamokomplettset Hallo Welt', 9, 49.90);
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'LED Setbeleuchtung inkl. Batterien', 9, 28.49);
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'Gummigriffe Blasenwunder', 10, 9.75);
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'Gummigriffe Chifluss', 10, 18.99);
insert into produkte (Bezeichnung, Produktkategorie, Preis) values (
'Korkgriffe aus nachhaltigem Anbau', 10, 12.30);

select * from produkte;

/*
Regelwerk Tabelle füllen
*/
insert into regelwerk values (1,6);
insert into regelwerk values (2,6);
insert into regelwerk values (4,7);
insert into regelwerk values (3,9);
insert into regelwerk values (3,10);
insert into regelwerk values (4,14);
insert into regelwerk values (4,15);
insert into regelwerk values (1,18);
insert into regelwerk values (2,18);
insert into regelwerk values (4,26);
insert into regelwerk values (5,30);
insert into regelwerk values (16,19);
insert into regelwerk values (18,23);
insert into regelwerk values (18,21);
insert into regelwerk values (7,28);

select * from regelwerk;

/*
Konfigurationen Tabelle füllen
*/
insert into konfigurationen (Bezeichnung, KundenID) values (
'Zusammenstellung für Fam. Heinrich', 10);
insert into konfigurationen (Bezeichnung, KundenID) values (
'Streetracer', 8);
insert into konfigurationen (Bezeichnung, KundenID) values (
'Downhill Master', 8);
insert into konfigurationen (Bezeichnung, KundenID) values (
'Standardvorlage', 8);
insert into konfigurationen (Bezeichnung, KundenID) values (
'test', 10);

select * from konfigurationen;

/*
KonfigurationenPos Tabelle füllen
*/
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
1,1,2);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
1,2,7);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
1,3,8);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
1,4,13);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
1,5,15);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
1,6,20);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
1,7,23);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
1,8,25);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
1,9,26);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
1,10,30);

insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
2,1,3);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
2,2,5);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
2,3,8);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
2,4,12);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
2,5,17);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
2,6,20);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
2,7,21);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
2,8,24);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
2,9,26);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
2,10,29);

insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
3,1,4);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
3,2,5);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
3,3,9);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
3,4,11);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
3,5,16);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
3,6,18);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
3,7,22);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
3,8,24);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
3,9,27);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
3,10,29);

insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
4,1,1);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
4,2,5);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
4,3,8);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
4,4,13);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
4,5,17);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
4,6,19);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
4,7,23);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
4,8,24);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
4,9,26);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
4,10,28);

insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
5,1,3);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
5,2,6);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
5,3,8);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
5,4,12);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
5,5,16);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
5,6,20);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
5,7,22);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
5,8,24);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
5,9,26);
insert into konfigurationenpos (KonfigurationID, Position, ProduktID) values (
5,10,29);

select * from konfigurationenpos;

/*
Bestellung Tabelle füllen
*/

insert into bestellung (KundenID, Datum) values (
3,'2014-03-13');
insert into bestellung (KundenID, Datum) values (
10,'2014-03-24');
insert into bestellung (KundenID, Datum) values (
6,'2014-04-02');
insert into bestellung (KundenID, Datum) values (
8,'2014-04-29');
insert into bestellung (KundenID, Datum) values (
1,'2014-05-06');
insert into bestellung (KundenID, Datum) values (
8,'2014-06-19');

select * from bestellung;

/*
BestellungPos Tabelle füllen
*/
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
1,1,2,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
1,2,7,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
1,3,8,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
1,4,12,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
1,5,14,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
1,6,19,2);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
1,7,23,2);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
1,8,25,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
1,9,27,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
1,10,30,1);

insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
2,1,2,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
2,2,7,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
2,3,8,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
2,4,13,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
2,5,15,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
2,6,20,2);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
2,7,23,2);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
2,8,25,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
2,9,26,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
2,10,30,1);

insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
3,1,4,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
3,2,6,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
3,3,9,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
3,4,12,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
3,5,17,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
3,6,20,2);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
3,7,22,2);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
3,8,24,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
3,9,27,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
3,10,28,1);

insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
4,1,3,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
4,2,5,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
4,3,8,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
4,4,12,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
4,5,17,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
4,6,20,2);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
4,7,21,2);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
4,8,24,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
4,9,26,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
4,10,29,1);

insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
5,1,3,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
5,2,20,2);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
5,3,22,2);

insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
6,1,4,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
6,2,5,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
6,3,9,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
6,4,11,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
6,5,16,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
6,6,18,2);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
6,7,22,2);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
6,8,24,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
6,9,27,1);
insert into bestellungpos (BestellungID, Position, ProduktID, Menge) values (
6,10,29,1);

select * from bestellungpos;

/*Einfügen und Aktualisieren der Preise und Summen*/
update bestellungpos as bp
set bp.Preis = (select p.Preis from produkte as p where p.ProduktID = bp.ProduktID)
where bp.BestellungID > 0; /*benötigt für "safe Update"*/

update bestellungpos
set Summe = Menge * Preis
where BestellungID > 0;

update bestellung as b
set b.Bestellsumme = (select round(sum(bp.Summe),2) 
	from bestellungpos as bp where b.BestellungID = bp.BestellungID)
where b.BestellungID > 0;

select * from bestellungpos;
select * from bestellung;