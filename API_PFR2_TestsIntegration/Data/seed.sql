-- Simulate enums using VARCHAR + CHECK
-- Roles: 'Utilisateur', 'Admin'
-- Statuts: 'EnAttente', 'Validee', 'Refusee'

-- Drop tables if they exist (order matters à cause des FK)
IF OBJECT_ID('inscriptiontournoi', 'U') IS NOT NULL DROP TABLE inscriptiontournoi;
IF OBJECT_ID('reservation', 'U') IS NOT NULL DROP TABLE reservation;
IF OBJECT_ID('tournoi', 'U') IS NOT NULL DROP TABLE tournoi;
IF OBJECT_ID('api_games', 'U') IS NOT NULL DROP TABLE api_games;
IF OBJECT_ID('api_users', 'U') IS NOT NULL DROP TABLE api_users;

-- Create tables
CREATE TABLE api_users (
    id INT IDENTITY(1,1) PRIMARY KEY,
    email VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL,
    role VARCHAR(50) NOT NULL CHECK (role IN ('Utilisateur', 'Admin'))
);

CREATE TABLE api_games (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nom VARCHAR(255) NOT NULL,
    description TEXT NULL
);

CREATE TABLE tournoi (
    id INT IDENTITY(1,1) PRIMARY KEY,
    nom VARCHAR(255) NULL,
    date_debut DATETIME2 NOT NULL,
    date_fin DATETIME2 NULL,
    capacite INT NULL,
    jeu_id INT NOT NULL FOREIGN KEY REFERENCES api_games(id)
);

CREATE TABLE reservation (
    id INT IDENTITY(1,1) PRIMARY KEY,
    utilisateur_id INT NOT NULL FOREIGN KEY REFERENCES api_users(id),
    jeu_id INT NOT NULL FOREIGN KEY REFERENCES api_games(id),
    date_debut DATETIME2 NOT NULL,
    date_fin DATETIME2 NOT NULL
);

CREATE TABLE inscriptiontournoi (
    id INT IDENTITY(1,1) PRIMARY KEY,
    utilisateur_id INT NOT NULL FOREIGN KEY REFERENCES api_users(id),
    tournoi_id INT NOT NULL FOREIGN KEY REFERENCES tournoi(id),
    statut VARCHAR(50) NOT NULL DEFAULT 'EnAttente' CHECK (statut IN ('EnAttente', 'Validee', 'Refusee')),
    date_inscription DATETIME2 NOT NULL DEFAULT GETDATE()
);

-- Seed test data
INSERT INTO api_users (email, password, role)
VALUES ('user1@nivo.fr', '$2a$11$wTCJX4N2dX5gPFqaHzwzcu5TTn4QwzLbGiXKJTyGCwo6I7hGYpoXS', 'Admin');