-- phpMyAdmin SQL Dump
-- version 5.2.3
-- https://www.phpmyadmin.net/
--
-- Gép: mysql:3306
-- Létrehozás ideje: 2026. Nov 23. 10:23
-- Kiszolgáló verziója: 26.10.0
-- PHP verzió: 8.3.37

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Adatbázis: `animalshelter`
--
CREATE DATABASE IF NOT EXISTS `animalshelter` DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;
USE `animalshelter`;

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `animals`
--

CREATE TABLE `animals` (
  `Id` char(36) NOT NULL,
  `Name` varchar(40) NOT NULL,
  `Species` varchar(30) NOT NULL,
  `Age` int NOT NULL,
  `Weight` double NOT NULL,
  `RegTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `ShelterId` char(36) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- A tábla adatainak kiíratása `animals`
--

INSERT INTO `animals` (`Id`, `Name`, `Species`, `Age`, `Weight`, `RegTime`, `ShelterId`) VALUES
('012e14c6-a1da-4da3-b1e4-6b0d18a6efae', 'Jóska', 'Kutya', 3, 12, '2026-11-23 10:27:59', '8d32af8e-bca2-5907-997f-05ff3b325fe7'),
('f6d0a8b1-e715-11f1-be47-dacc0d7b2b67', 'Bodri', 'Kutya', 4, 18.5, '2026-11-23 08:14:54', '8d32af8e-bca2-5907-997f-05ff3b325fe7'),
('f6d0aee7-e715-11f1-be47-dacc0d7b2b67', 'Cirmos', 'Macska', 2, 4.1, '2026-11-23 08:14:54', '95f1187f-f2c2-54b7-afc1-3dba453580ab'),
('f6d0b024-e715-11f1-be47-dacc0d7b2b67', 'Rex', 'Kutya', 6, 31, '2026-11-23 08:14:54', 'a2691474-e139-594f-a782-443415ee7d41'),
('f6d0b07a-e715-11f1-be47-dacc0d7b2b67', 'Füles', 'Nyúl', 1, 1.9, '2026-11-23 08:14:54', 'c41845d3-62ac-5c31-9b16-80193b82aec0'),
('f6d0b0b6-e715-11f1-be47-dacc0d7b2b67', 'Kormi', 'Macska', 5, 5.2, '2026-11-23 08:14:54', 'dd87a9dd-de9b-55c7-8fa6-fdafdfcc4a64');

-- --------------------------------------------------------

--
-- Tábla szerkezet ehhez a táblához `shelters`
--

CREATE TABLE `shelters` (
  `Id` char(36) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `City` varchar(40) NOT NULL,
  `Capacity` int NOT NULL,
  `RegTime` datetime DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

--
-- A tábla adatainak kiíratása `shelters`
--

INSERT INTO `shelters` (`Id`, `Name`, `City`, `Capacity`, `RegTime`) VALUES
('8d32af8e-bca2-5907-997f-05ff3b325fe7', 'Gazdira Vár', 'Debrecen', 25, '2026-08-05 08:04:01'),
('95f1187f-f2c2-54b7-afc1-3dba453580ab', 'Cirmos Menedék', 'Eger', 15, '2026-07-18 11:51:48'),
('a2691474-e139-594f-a782-443415ee7d41', 'Boldog Mancs', 'Szeged', 40, '2026-01-08 15:34:23'),
('c41845d3-62ac-5c31-9b16-80193b82aec0', 'Kis Menedék', 'Salgótarján', 3, '2026-05-25 09:52:06'),
('dd87a9dd-de9b-55c7-8fa6-fdafdfcc4a64', 'Négylábú Otthon', 'Miskolc', 30, '2026-04-10 08:46:25');

--
-- Indexek a kiírt táblákhoz
--

--
-- A tábla indexei `animals`
--
ALTER TABLE `animals`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `fk_animals_shelter` (`ShelterId`);

--
-- A tábla indexei `shelters`
--
ALTER TABLE `shelters`
  ADD PRIMARY KEY (`Id`);

--
-- Megkötések a kiírt táblákhoz
--

--
-- Megkötések a táblához `animals`
--
ALTER TABLE `animals`
  ADD CONSTRAINT `fk_animals_shelter` FOREIGN KEY (`ShelterId`) REFERENCES `shelters` (`Id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
