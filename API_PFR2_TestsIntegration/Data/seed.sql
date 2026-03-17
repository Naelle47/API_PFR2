-- Create ENUMs
CREATE TYPE role_utilisateur AS ENUM ('Utilisateur', 'Admin');
CREATE TYPE statut_inscription AS ENUM ('EnAttente', 'Validee', 'Refusee');

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
    statut statut_inscription DEFAULT 'EnAttente',
    date_inscription TIMESTAMP DEFAULT NOW()
);

-- Seed test data
INSERT INTO api_users (email, password, role)
VALUES ('user1@nivo.fr', '$2a$11$wTCJX4N2dX5gPFqaHzwzcu5TTn4QwzLbGiXKJTyGCwo6I7hGYpoXS', 'Admin'::role_utilisateur);