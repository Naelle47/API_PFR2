-- 1. Create ENUM types
CREATE TYPE role_utilisateur AS ENUM ('Utilisateur','Admin');
CREATE TYPE statut_inscription AS ENUM ('EnAttente','Validee','Refusee');

-- 2. Create tables
CREATE TABLE api_users (
    id SERIAL PRIMARY KEY,
    email VARCHAR(255) NOT NULL,
    password VARCHAR(255) NOT NULL,
    role role_utilisateur NOT NULL
);

CREATE TABLE api_games (
    id SERIAL PRIMARY KEY,
    nom VARCHAR(255) NOT NULL,
    description TEXT
);

CREATE TABLE tournoi (
    id SERIAL PRIMARY KEY,
    nom VARCHAR(255),
    date_debut TIMESTAMP NOT NULL,
    date_fin TIMESTAMP,
    capacite INT,
    jeu_id INT NOT NULL REFERENCES api_games(id)
);

CREATE TABLE reservation (
    id SERIAL PRIMARY KEY,
    utilisateur_id INT NOT NULL REFERENCES api_users(id),
    jeu_id INT NOT NULL REFERENCES api_games(id),
    date_debut TIMESTAMP NOT NULL,
    date_fin TIMESTAMP NOT NULL
);

CREATE TABLE inscriptiontournoi (
    id SERIAL PRIMARY KEY,
    utilisateur_id INT NOT NULL REFERENCES api_users(id),
    tournoi_id INT NOT NULL REFERENCES tournoi(id),
    statut statut_inscription NOT NULL DEFAULT 'EnAttente',
    date_inscription TIMESTAMP NOT NULL DEFAULT NOW()
);

-- 3. Seed fixed data

-- Users
INSERT INTO api_users (email, password, role) VALUES
('user1@nivo.fr', '$2a$11$wTCJX4N2dX5gPFqaHzwzcu5TTn4QwzLbGiXKJTyGCwo6I7hGYpoXS', 'Admin'),
('user2@nivo.fr', '$2a$11$somehashedpasswordexample', 'Utilisateur');

-- Games
INSERT INTO api_games (nom, description) VALUES
('Catan', 'Jeu de stratégie classique'),
('Carcassonne', 'Jeu de tuiles et stratégie');

-- Tournaments
INSERT INTO tournoi (nom, date_debut, date_fin, capacite, jeu_id) VALUES
('Tournoi Catan', '2026-04-10 10:00', '2026-04-10 12:00', 4, 1),
('Tournoi Carcassonne', '2026-04-11 14:00', '2026-04-11 16:00', 6, 2);

-- Reservations (fixed times)
INSERT INTO reservation (utilisateur_id, jeu_id, date_debut, date_fin) VALUES
(1, 1, '2026-04-01 10:00', '2026-04-01 12:00'),
(2, 2, '2026-04-01 14:00', '2026-04-01 16:00');

-- Tournament inscriptions
INSERT INTO inscriptiontournoi (utilisateur_id, tournoi_id, statut) VALUES
(1, 1, 'Validee'),
(2, 2, 'EnAttente');