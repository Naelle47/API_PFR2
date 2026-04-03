--
-- PostgreSQL database dump
--

-- Dumped from database version 17.4 (Debian 17.4-1.pgdg120+2)
-- Dumped by pg_dump version 17.0

-- Started on 2026-04-03 12:00:16

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 2 (class 3079 OID 132002)
-- Name: postgres_fdw; Type: EXTENSION; Schema: -; Owner: -
--

CREATE EXTENSION IF NOT EXISTS postgres_fdw WITH SCHEMA public;


--
-- TOC entry 3426 (class 0 OID 0)
-- Dependencies: 2
-- Name: EXTENSION postgres_fdw; Type: COMMENT; Schema: -; Owner: 
--

COMMENT ON EXTENSION postgres_fdw IS 'foreign-data wrapper for remote PostgreSQL servers';


--
-- TOC entry 861 (class 1247 OID 132391)
-- Name: role_utilisateur; Type: TYPE; Schema: public; Owner: postgres
--

CREATE TYPE public.role_utilisateur AS ENUM (
    'Utilisateur',
    'Admin'
);


ALTER TYPE public.role_utilisateur OWNER TO postgres;

--
-- TOC entry 870 (class 1247 OID 132414)
-- Name: statut_inscription; Type: TYPE; Schema: public; Owner: postgres
--

CREATE TYPE public.statut_inscription AS ENUM (
    'EnAttente',
    'Validee',
    'Refusee'
);


ALTER TYPE public.statut_inscription OWNER TO postgres;

--
-- TOC entry 2077 (class 1417 OID 132180)
-- Name: ancienschema_server; Type: SERVER; Schema: -; Owner: postgres
--

CREATE SERVER ancienschema_server FOREIGN DATA WRAPPER postgres_fdw OPTIONS (
    dbname 'ancienschema',
    host 'localhost',
    port '5432'
);


ALTER SERVER ancienschema_server OWNER TO postgres;

--
-- TOC entry 3427 (class 0 OID 0)
-- Name: USER MAPPING postgres SERVER ancienschema_server; Type: USER MAPPING; Schema: -; Owner: postgres
--

CREATE USER MAPPING FOR postgres SERVER ancienschema_server OPTIONS (
    password 'password',
    "user" 'postgres'
);


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 221 (class 1259 OID 132405)
-- Name: api_games; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.api_games (
    id integer NOT NULL,
    nom character varying(255) NOT NULL,
    description text
);


ALTER TABLE public.api_games OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 132404)
-- Name: api_games_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.api_games_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.api_games_id_seq OWNER TO postgres;

--
-- TOC entry 3428 (class 0 OID 0)
-- Dependencies: 220
-- Name: api_games_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.api_games_id_seq OWNED BY public.api_games.id;


--
-- TOC entry 219 (class 1259 OID 132396)
-- Name: api_users; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.api_users (
    id integer NOT NULL,
    email character varying(255) NOT NULL,
    password character varying(255) NOT NULL,
    role public.role_utilisateur NOT NULL
);


ALTER TABLE public.api_users OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 132395)
-- Name: api_users_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.api_users_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.api_users_id_seq OWNER TO postgres;

--
-- TOC entry 3429 (class 0 OID 0)
-- Dependencies: 218
-- Name: api_users_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.api_users_id_seq OWNED BY public.api_users.id;


--
-- TOC entry 227 (class 1259 OID 132452)
-- Name: inscriptiontournoi; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.inscriptiontournoi (
    id integer NOT NULL,
    utilisateur_id integer NOT NULL,
    tournoi_id integer NOT NULL,
    statut public.statut_inscription DEFAULT 'EnAttente'::public.statut_inscription,
    date_inscription timestamp without time zone DEFAULT now()
);


ALTER TABLE public.inscriptiontournoi OWNER TO postgres;

--
-- TOC entry 226 (class 1259 OID 132451)
-- Name: inscriptiontournoi_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.inscriptiontournoi_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.inscriptiontournoi_id_seq OWNER TO postgres;

--
-- TOC entry 3430 (class 0 OID 0)
-- Dependencies: 226
-- Name: inscriptiontournoi_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.inscriptiontournoi_id_seq OWNED BY public.inscriptiontournoi.id;


--
-- TOC entry 225 (class 1259 OID 132434)
-- Name: reservation; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.reservation (
    id integer NOT NULL,
    utilisateur_id integer NOT NULL,
    jeu_id integer NOT NULL,
    date_debut timestamp without time zone NOT NULL,
    date_fin timestamp without time zone NOT NULL
);


ALTER TABLE public.reservation OWNER TO postgres;

--
-- TOC entry 224 (class 1259 OID 132433)
-- Name: reservation_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.reservation_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.reservation_id_seq OWNER TO postgres;

--
-- TOC entry 3431 (class 0 OID 0)
-- Dependencies: 224
-- Name: reservation_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.reservation_id_seq OWNED BY public.reservation.id;


--
-- TOC entry 223 (class 1259 OID 132422)
-- Name: tournoi; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.tournoi (
    id integer NOT NULL,
    nom character varying(255),
    date_debut timestamp without time zone NOT NULL,
    date_fin timestamp without time zone,
    capacite integer,
    jeu_id integer NOT NULL
);


ALTER TABLE public.tournoi OWNER TO postgres;

--
-- TOC entry 222 (class 1259 OID 132421)
-- Name: tournoi_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.tournoi_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.tournoi_id_seq OWNER TO postgres;

--
-- TOC entry 3432 (class 0 OID 0)
-- Dependencies: 222
-- Name: tournoi_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.tournoi_id_seq OWNED BY public.tournoi.id;


--
-- TOC entry 3245 (class 2604 OID 132408)
-- Name: api_games id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.api_games ALTER COLUMN id SET DEFAULT nextval('public.api_games_id_seq'::regclass);


--
-- TOC entry 3244 (class 2604 OID 132399)
-- Name: api_users id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.api_users ALTER COLUMN id SET DEFAULT nextval('public.api_users_id_seq'::regclass);


--
-- TOC entry 3248 (class 2604 OID 132455)
-- Name: inscriptiontournoi id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.inscriptiontournoi ALTER COLUMN id SET DEFAULT nextval('public.inscriptiontournoi_id_seq'::regclass);


--
-- TOC entry 3247 (class 2604 OID 132437)
-- Name: reservation id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.reservation ALTER COLUMN id SET DEFAULT nextval('public.reservation_id_seq'::regclass);


--
-- TOC entry 3246 (class 2604 OID 132425)
-- Name: tournoi id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tournoi ALTER COLUMN id SET DEFAULT nextval('public.tournoi_id_seq'::regclass);


--
-- TOC entry 3414 (class 0 OID 132405)
-- Dependencies: 221
-- Data for Name: api_games; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.api_games (id, nom, description) FROM stdin;
1	Catan	Un jeu de stratégie et de commerce où les joueurs construisent des colonies et échangent des ressources.
2	Carcassonne	Un jeu de pose de tuiles où les joueurs construisent des villes, routes et abbayes.
3	7 Wonders	Un jeu de draft de cartes où les joueurs développent leur civilisation antique.
4	Pandemic	Un jeu coopératif où les joueurs luttent ensemble contre des maladies mondiales.
5	Ticket to Ride	Les joueurs collectent des cartes de trains pour relier des villes et marquer des points.
6	Azul	Un jeu de stratégie abstrait basé sur la pose de tuiles colorées.
7	Splendor	Les joueurs incarnent des marchands de la Renaissance qui collectent des pierres précieuses.
8	Dixit	Un jeu créatif où l’on fait deviner des cartes illustrées avec des indices.
9	Terraforming Mars	Un jeu de stratégie où les joueurs développent la planète Mars pour la rendre habitable.
10	Wingspan	Un jeu de collection et d’optimisation où les joueurs attirent des oiseaux dans leur réserve.
11	Codenames	Un jeu d’association d’idées en équipe pour retrouver des mots secrets.
12	Scythe	Un jeu de stratégie et d’exploration dans une Europe alternative des années 1920.
13	The Crew	Un jeu coopératif de plis où les joueurs accomplissent des missions dans l’espace.
14	Kingdomino	Un jeu familial où les joueurs construisent leur royaume avec des dominos de terrains.
15	Unlock!	Une série de jeux d’escape game avec cartes et application mobile.
16	Root	Un jeu asymétrique où chaque joueur contrôle une faction animalière en lutte pour la forêt.
17	Gloomhaven	Un jeu d’aventure coopératif de type dungeon crawler avec campagne évolutive.
18	Just One	Un jeu coopératif de mots où l’on doit faire deviner des mots avec des indices uniques.
19	Puerto Rico	Un classique de la gestion où les joueurs développent des plantations et commercent.
20	Agricola	Un jeu de gestion de ferme où les joueurs développent leurs champs, animaux et famille.
\.


--
-- TOC entry 3412 (class 0 OID 132396)
-- Dependencies: 219
-- Data for Name: api_users; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.api_users (id, email, password, role) FROM stdin;
12	user1@nivo.fr	$2a$11$wTCJX4N2dX5gPFqaHzwzcu5TTn4QwzLbGiXKJTyGCwo6I7hGYpoXS	Admin
1	user2@nivo.fr	$2y$11$avfYdzs9HjzmD6XjdJmIfOSWN01YBUcC6rTrnmIT6tWxpEE08hp9W	Utilisateur
\.


--
-- TOC entry 3420 (class 0 OID 132452)
-- Dependencies: 227
-- Data for Name: inscriptiontournoi; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.inscriptiontournoi (id, utilisateur_id, tournoi_id, statut, date_inscription) FROM stdin;
\.


--
-- TOC entry 3418 (class 0 OID 132434)
-- Dependencies: 225
-- Data for Name: reservation; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.reservation (id, utilisateur_id, jeu_id, date_debut, date_fin) FROM stdin;
19	12	1	2026-04-15 10:00:00	2026-04-15 12:00:00
21	12	1	2026-04-01 11:32:58.016	2026-04-01 13:32:58.016
22	12	2	2026-03-31 14:04:04.905703	2026-03-31 16:04:04.905758
23	12	1	2026-04-07 08:23:38.337	2026-04-07 10:23:38.337
24	12	1	2026-04-08 13:52:14.205	2026-04-08 15:52:14.205
25	1	1	2026-04-02 15:01:45.771716	2026-04-02 17:01:45.77179
\.


--
-- TOC entry 3416 (class 0 OID 132422)
-- Dependencies: 223
-- Data for Name: tournoi; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.tournoi (id, nom, date_debut, date_fin, capacite, jeu_id) FROM stdin;
\.


--
-- TOC entry 3433 (class 0 OID 0)
-- Dependencies: 220
-- Name: api_games_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.api_games_id_seq', 1, false);


--
-- TOC entry 3434 (class 0 OID 0)
-- Dependencies: 218
-- Name: api_users_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.api_users_id_seq', 1, true);


--
-- TOC entry 3435 (class 0 OID 0)
-- Dependencies: 226
-- Name: inscriptiontournoi_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.inscriptiontournoi_id_seq', 1, false);


--
-- TOC entry 3436 (class 0 OID 0)
-- Dependencies: 224
-- Name: reservation_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.reservation_id_seq', 25, true);


--
-- TOC entry 3437 (class 0 OID 0)
-- Dependencies: 222
-- Name: tournoi_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.tournoi_id_seq', 1, false);


--
-- TOC entry 3254 (class 2606 OID 132412)
-- Name: api_games api_games_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.api_games
    ADD CONSTRAINT api_games_pkey PRIMARY KEY (id);


--
-- TOC entry 3252 (class 2606 OID 132403)
-- Name: api_users api_users_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.api_users
    ADD CONSTRAINT api_users_pkey PRIMARY KEY (id);


--
-- TOC entry 3260 (class 2606 OID 132459)
-- Name: inscriptiontournoi inscriptiontournoi_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.inscriptiontournoi
    ADD CONSTRAINT inscriptiontournoi_pkey PRIMARY KEY (id);


--
-- TOC entry 3258 (class 2606 OID 132439)
-- Name: reservation reservation_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.reservation
    ADD CONSTRAINT reservation_pkey PRIMARY KEY (id);


--
-- TOC entry 3256 (class 2606 OID 132427)
-- Name: tournoi tournoi_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tournoi
    ADD CONSTRAINT tournoi_pkey PRIMARY KEY (id);


--
-- TOC entry 3264 (class 2606 OID 132465)
-- Name: inscriptiontournoi inscriptiontournoi_tournoi_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.inscriptiontournoi
    ADD CONSTRAINT inscriptiontournoi_tournoi_id_fkey FOREIGN KEY (tournoi_id) REFERENCES public.tournoi(id);


--
-- TOC entry 3265 (class 2606 OID 132460)
-- Name: inscriptiontournoi inscriptiontournoi_utilisateur_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.inscriptiontournoi
    ADD CONSTRAINT inscriptiontournoi_utilisateur_id_fkey FOREIGN KEY (utilisateur_id) REFERENCES public.api_users(id);


--
-- TOC entry 3262 (class 2606 OID 132445)
-- Name: reservation reservation_jeu_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.reservation
    ADD CONSTRAINT reservation_jeu_id_fkey FOREIGN KEY (jeu_id) REFERENCES public.api_games(id);


--
-- TOC entry 3263 (class 2606 OID 132440)
-- Name: reservation reservation_utilisateur_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.reservation
    ADD CONSTRAINT reservation_utilisateur_id_fkey FOREIGN KEY (utilisateur_id) REFERENCES public.api_users(id);


--
-- TOC entry 3261 (class 2606 OID 132428)
-- Name: tournoi tournoi_jeu_id_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.tournoi
    ADD CONSTRAINT tournoi_jeu_id_fkey FOREIGN KEY (jeu_id) REFERENCES public.api_games(id);


-- Completed on 2026-04-03 12:00:16

--
-- PostgreSQL database dump complete
--

