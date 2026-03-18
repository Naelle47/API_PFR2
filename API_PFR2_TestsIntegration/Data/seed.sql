-- Enum pour les rôles utilisateur
CREATE TYPE role_utilisateur AS ENUM ('Utilisateur','Admin');

-- Enum pour le statut des inscriptions aux tournois
CREATE TYPE statut_inscription AS ENUM ('EnAttente','Validee','Refusee');

-- Create tables
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
    jeu_id INT NOT NULL,
    FOREIGN KEY (jeu_id) REFERENCES api_games(id)
);

CREATE TABLE reservation (
    id SERIAL PRIMARY KEY,
    utilisateur_id INT NOT NULL,
    jeu_id INT NOT NULL,
    date_debut TIMESTAMP NOT NULL,
    date_fin TIMESTAMP NOT NULL,
    FOREIGN KEY (utilisateur_id) REFERENCES api_users(id),
    FOREIGN KEY (jeu_id) REFERENCES api_games(id)
);

CREATE TABLE inscriptiontournoi (
    id SERIAL PRIMARY KEY,
    utilisateur_id INT NOT NULL,
    tournoi_id INT NOT NULL,
    statut statut_inscription NOT NULL DEFAULT 'EnAttente',
    date_inscription TIMESTAMP NOT NULL DEFAULT NOW(),
    FOREIGN KEY (utilisateur_id) REFERENCES api_users(id),
    FOREIGN KEY (tournoi_id) REFERENCES tournoi(id)
);

-- Seed test data
INSERT INTO api_users (email, password, role)
VALUES ('user1@nivo.fr', '$2a$11$wTCJX4N2dX5gPFqaHzwzcu5TTn4QwzLbGiXKJTyGCwo6I7hGYpoXS', 'Admin');