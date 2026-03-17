-- Drop tables in reverse order of creation to avoid FK issues
DROP TABLE IF EXISTS inscriptiontournoi;
DROP TABLE IF EXISTS reservation;
DROP TABLE IF EXISTS tournoi;
DROP TABLE IF EXISTS api_games;
DROP TABLE IF EXISTS api_users;

-- Drop ENUMs
DROP TYPE IF EXISTS statut_inscription;
DROP TYPE IF EXISTS role_utilisateur;