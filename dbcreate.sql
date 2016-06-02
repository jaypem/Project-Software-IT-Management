CREATE DATABASE `fahrrad` /*!40100 DEFAULT CHARACTER SET utf8 */;

use fahrrad;

CREATE TABLE `kunden` (
  `KundenID` int(11) NOT NULL AUTO_INCREMENT,
  `Nachname` varchar(45) DEFAULT NULL,
  `Name` varchar(45) DEFAULT NULL,
  `Geburtsdatum` date DEFAULT NULL,
  `EMail` varchar(45) NOT NULL,
  `PLZ` varchar(45) NOT NULL,
  `Ort` varchar(45) NOT NULL,
  `Strasse` varchar(45) NOT NULL,
  `Hausnummer` varchar(5) NOT NULL,
  `Haendler` bit(1) NOT NULL DEFAULT b'0',
  `Firma` varchar(45) DEFAULT NULL,
  `Passwort` varchar(45) NOT NULL,
  PRIMARY KEY (`KundenID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `produktkategorie` (
  `KategorieID` int(11) NOT NULL AUTO_INCREMENT,
  `Bezeichnung` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`KategorieID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `produkte` (
  `ProduktID` int(11) NOT NULL AUTO_INCREMENT,
  `Bezeichnung` varchar(45) NOT NULL,
  `Produktkategorie` int(11) NOT NULL,
  `Preis` double DEFAULT NULL,
  PRIMARY KEY (`ProduktID`),
  KEY `Produkt_Kategorie_idx` (`Produktkategorie`),
  CONSTRAINT `Produkt_Kategorie` FOREIGN KEY (`Produktkategorie`) REFERENCES `produktkategorie` (`KategorieID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `bestellung` (
  `BestellungID` int(11) NOT NULL AUTO_INCREMENT,
  `KundenID` int(11) NOT NULL,
  `Datum` date NOT NULL,
  `Bestellsumme` double DEFAULT NULL,
  PRIMARY KEY (`BestellungID`),
  KEY `HaendlerID_idx` (`KundenID`),
  CONSTRAINT `Best_Kund_Kund_Kund` FOREIGN KEY (`KundenID`) REFERENCES `kunden` (`KundenID`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `bestellungpos` (
  `BestellungID` int(11) NOT NULL,
  `Position` int(11) NOT NULL,
  `ProduktID` int(11) NOT NULL,
  `Menge` double NOT NULL,
  `Preis` double DEFAULT NULL,
  `Summe` double DEFAULT NULL,
  PRIMARY KEY (`BestellungID`,`Position`),
  KEY `H_B_ProduktID_idx` (`ProduktID`),
  CONSTRAINT `Bestpos_Best` FOREIGN KEY (`BestellungID`) REFERENCES `bestellung` (`BestellungID`) ON UPDATE CASCADE,
  CONSTRAINT `Bestpos_Prod` FOREIGN KEY (`ProduktID`) REFERENCES `produkte` (`ProduktID`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `konfigurationen` (
  `KonfigurationID` int(11) NOT NULL AUTO_INCREMENT,
  `Bezeichnung` varchar(45) DEFAULT NULL,
  `KundenID` int(11) NOT NULL,
  PRIMARY KEY (`KonfigurationID`),
  KEY `Konf_HaendlerID_idx` (`KundenID`),
  CONSTRAINT `Konf_KundenID` FOREIGN KEY (`KundenID`) REFERENCES `kunden` (`KundenID`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `konfigurationenpos` (
  `KonfigurationID` int(11) NOT NULL,
  `Position` int(11) NOT NULL,
  `ProduktID` int(11) NOT NULL,
  PRIMARY KEY (`KonfigurationID`,`Position`),
  KEY `konfposProd_prod_idx` (`ProduktID`),
  CONSTRAINT `konfposProd_prod` FOREIGN KEY (`ProduktID`) REFERENCES `produkte` (`ProduktID`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `konfPos_Konf` FOREIGN KEY (`KonfigurationID`) REFERENCES `konfigurationen` (`KonfigurationID`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

CREATE TABLE `regelwerk` (
  `TeilA` int(11) NOT NULL,
  `TeilB` int(11) NOT NULL,
  PRIMARY KEY (`TeilA`,`TeilB`),
  KEY `R_TeilB_Produkt_idx` (`TeilB`),
  CONSTRAINT `R_TeilA_Produkt` FOREIGN KEY (`TeilA`) REFERENCES `produkte` (`ProduktID`) ON UPDATE CASCADE,
  CONSTRAINT `R_TeilB_Produkt` FOREIGN KEY (`TeilB`) REFERENCES `produkte` (`ProduktID`) ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;