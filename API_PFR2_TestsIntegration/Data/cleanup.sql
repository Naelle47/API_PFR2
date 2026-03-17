-- Cleanup test data
DELETE FROM inscriptiontournoi;
DELETE FROM reservation;
DELETE FROM tournoi;
DELETE FROM api_games;
DELETE FROM api_users;

-- Drop tables
DROP TABLE IF EXISTS inscriptiontournoi;
DROP TABLE IF EXISTS reservation;
DROP TABLE IF EXISTS tournoi;
DROP TABLE IF EXISTS api_games;
DROP TABLE IF EXISTS api_users;

-- Drop ENUMs
DROP TYPE IF EXISTS statut_inscription;
DROP TYPE IF EXISTS role_utilisateur;