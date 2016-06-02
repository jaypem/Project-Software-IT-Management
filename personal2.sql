-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Erstellungszeit: 02. Jun 2016 um 19:15
-- Server-Version: 10.1.13-MariaDB
-- PHP-Version: 5.5.35

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Datenbank: `fahrrad`
--

-- --------------------------------------------------------

--
-- Tabellenstruktur für Tabelle `personal`
--

CREATE TABLE `personal` (
  `login` varchar(45) NOT NULL,
  `passwort` varchar(45) NOT NULL,
  `Nachname` varchar(45) NOT NULL,
  `Name` varchar(45) NOT NULL,
  `abteilung` varchar(45) NOT NULL,
  `admin` bit(1) NOT NULL DEFAULT b'0',
  `ansichtL` bit(1) NOT NULL DEFAULT b'0',
  `ansichtV` bit(1) NOT NULL DEFAULT b'0',
  `ansichtW` bit(1) NOT NULL DEFAULT b'0'
) ENGINE=InnoDB DEFAULT CHARSET=utf8;

--
-- Daten für Tabelle `personal`
--

INSERT INTO `personal` (`login`, `passwort`, `Nachname`, `Name`, `abteilung`, `admin`, `ansichtL`, `ansichtV`, `ansichtW`) VALUES
('admin', 'admin', 'Mustermann', 'Max', 'Geschäftsführer', b'1', b'1', b'1', b'1'),
('laden1', 'root', 'Mustermann', 'Erika', 'Laden', b'0', b'1', b'0', b'0'),
('praktikant1', 'root', 'Mustermann', 'Ulli', 'Praktikant allg', b'0', b'1', b'1', b'1'),
('verwaltung1', 'root', 'Mustermann', 'Anton', 'Verwaltung', b'0', b'0', b'1', b'0'),
('werk1', 'root', 'Mustermann', 'Martin', 'Werkstatt', b'0', b'0', b'0', b'1');

--
-- Indizes der exportierten Tabellen
--

--
-- Indizes für die Tabelle `personal`
--
ALTER TABLE `personal`
  ADD PRIMARY KEY (`login`);

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
