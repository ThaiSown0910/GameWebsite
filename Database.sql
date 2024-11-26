--
-- PostgreSQL database dump
--

-- Dumped from database version 17.0
-- Dumped by pg_dump version 17.0

-- Started on 2024-11-26 09:37:22

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
-- TOC entry 4 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: pg_database_owner
--

CREATE SCHEMA public;


ALTER SCHEMA public OWNER TO pg_database_owner;

--
-- TOC entry 4873 (class 0 OID 0)
-- Dependencies: 4
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: pg_database_owner
--

COMMENT ON SCHEMA public IS 'standard public schema';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 226 (class 1259 OID 16719)
-- Name: AdminCreates; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AdminCreates" (
    adminid integer NOT NULL,
    username character varying(100) NOT NULL,
    password text NOT NULL,
    confirmpassword text NOT NULL,
    fullname text NOT NULL,
    mobilephone text NOT NULL
);


ALTER TABLE public."AdminCreates" OWNER TO postgres;

--
-- TOC entry 225 (class 1259 OID 16718)
-- Name: AdminCreates_adminid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."AdminCreates" ALTER COLUMN adminid ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."AdminCreates_adminid_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 227 (class 1259 OID 16726)
-- Name: AdminLogins; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."AdminLogins" (
    adminid integer NOT NULL,
    username character varying(100) NOT NULL,
    password text NOT NULL
);


ALTER TABLE public."AdminLogins" OWNER TO postgres;

--
-- TOC entry 229 (class 1259 OID 16747)
-- Name: Carts; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Carts" (
    cartid integer NOT NULL,
    gameid integer NOT NULL,
    customerid integer NOT NULL,
    quantity integer NOT NULL,
    addeddate timestamp with time zone NOT NULL
);


ALTER TABLE public."Carts" OWNER TO postgres;

--
-- TOC entry 228 (class 1259 OID 16746)
-- Name: Carts_cartid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Carts" ALTER COLUMN cartid ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Carts_cartid_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 231 (class 1259 OID 16826)
-- Name: Checkouts; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Checkouts" (
    saleid integer NOT NULL,
    customerid integer NOT NULL,
    saledate timestamp with time zone NOT NULL,
    totalinvoiceamount numeric NOT NULL,
    discount numeric NOT NULL,
    paymentnaration text NOT NULL,
    deliveryaddress1 text NOT NULL,
    deliveryaddress2 text NOT NULL,
    deliverycity text NOT NULL,
    deliverypincode text NOT NULL,
    deliverylandmark text NOT NULL
);


ALTER TABLE public."Checkouts" OWNER TO postgres;

--
-- TOC entry 230 (class 1259 OID 16825)
-- Name: Checkouts_saleid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Checkouts" ALTER COLUMN saleid ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Checkouts_saleid_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 219 (class 1259 OID 16623)
-- Name: GameCategories; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."GameCategories" (
    categoryid integer NOT NULL,
    categoryname text NOT NULL,
    description text NOT NULL
);


ALTER TABLE public."GameCategories" OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 16622)
-- Name: GameCategories_categoryid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."GameCategories" ALTER COLUMN categoryid ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."GameCategories_categoryid_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 221 (class 1259 OID 16631)
-- Name: Games; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Games" (
    gameid integer NOT NULL,
    title text NOT NULL,
    year integer NOT NULL,
    summary text NOT NULL,
    price double precision NOT NULL,
    categoryid integer NOT NULL,
    imageurl character varying(500) DEFAULT ''::character varying NOT NULL
);


ALTER TABLE public."Games" OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 16630)
-- Name: Games_gameid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Games" ALTER COLUMN gameid ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Games_gameid_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 224 (class 1259 OID 16673)
-- Name: Logins; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Logins" (
    customerid integer NOT NULL,
    username character varying(100) NOT NULL,
    password text NOT NULL
);


ALTER TABLE public."Logins" OWNER TO postgres;

--
-- TOC entry 223 (class 1259 OID 16666)
-- Name: Registers; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Registers" (
    customerid integer NOT NULL,
    username character varying(100) NOT NULL,
    password text NOT NULL,
    confirmpassword text NOT NULL,
    email character varying(150) DEFAULT ''::character varying NOT NULL
);


ALTER TABLE public."Registers" OWNER TO postgres;

--
-- TOC entry 222 (class 1259 OID 16665)
-- Name: Registers_customerid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

ALTER TABLE public."Registers" ALTER COLUMN customerid ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public."Registers_customerid_seq"
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);


--
-- TOC entry 217 (class 1259 OID 16617)
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO postgres;

--
-- TOC entry 4862 (class 0 OID 16719)
-- Dependencies: 226
-- Data for Name: AdminCreates; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AdminCreates" (adminid, username, password, confirmpassword, fullname, mobilephone) FROM stdin;
1	thaison0910	admin	admin	Trịnh Thái Sơn	0912345678
3	thaison1234	123456	123456	Sơn nè	0123456789
2	admin	admin	admin	Admin 1	0123456789
4	thaison0910	123456	123456	Trịnh Thái Sơn	0123456789
\.


--
-- TOC entry 4863 (class 0 OID 16726)
-- Dependencies: 227
-- Data for Name: AdminLogins; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."AdminLogins" (adminid, username, password) FROM stdin;
1	thaison0910	admin
3	thaison1234	123456
2	admin	admin
4	thaison0910	123456
\.


--
-- TOC entry 4865 (class 0 OID 16747)
-- Dependencies: 229
-- Data for Name: Carts; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Carts" (cartid, gameid, customerid, quantity, addeddate) FROM stdin;
11	4	6	1	2024-11-18 14:21:14.739+07
12	14	6	1	2024-11-18 14:36:40.217+07
97	2	5	1	2024-11-21 14:38:40.927+07
98	1	5	1	2024-11-21 14:38:41.599+07
\.


--
-- TOC entry 4867 (class 0 OID 16826)
-- Dependencies: 231
-- Data for Name: Checkouts; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Checkouts" (saleid, customerid, saledate, totalinvoiceamount, discount, paymentnaration, deliveryaddress1, deliveryaddress2, deliverycity, deliverypincode, deliverylandmark) FROM stdin;
8	1	2024-11-20 15:37:24.535+07	0	0		jberjgnerj	greb vnegnfwe	New York 	12321321	ffewfwefw
9	1	2024-11-20 16:05:30.21+07	0	0						
10	5	2024-11-21 14:39:19.907+07	0	0		regerger	gregergege	Ho Chi Minh	21312312	gergerger
\.


--
-- TOC entry 4855 (class 0 OID 16623)
-- Dependencies: 219
-- Data for Name: GameCategories; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."GameCategories" (categoryid, categoryname, description) FROM stdin;
1	Game hành động (Action)	các game như bắn súng, chiến đấu, và hành động phiêu lưu.
5	Game thể thao (Sports)	Mô phỏng các môn thể thao như bóng đá, bóng rổ, đua xe.
4	Game chiến thuật (Strategy)	Tập trung vào việc lập kế hoạch và chiến lược để đánh bại đối thủ.\nBao gồm các game chiến thuật thời gian thực (RTS) và theo lượt (Turn-based).
3	Game mô phỏng (Simulation)	Mô phỏng các hoạt động trong đời thực hoặc điều khiển phương tiện.
2	Game nhập vai (RPG - Role-Playing Game)	Người chơi thường vào vai một nhân vật, phát triển kỹ năng và khám phá thế giới.
\.


--
-- TOC entry 4857 (class 0 OID 16631)
-- Dependencies: 221
-- Data for Name: Games; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Games" (gameid, title, year, summary, price, categoryid, imageurl) FROM stdin;
4	Age of Empires IV	2021	Age of Empires IV is a real-time strategy video game developed by Relic Entertainment in partnership with World's Edge and published by Xbox Game Studios. It is the fourth installment of the Age of Empires series, and the first installment not developed by Ensemble Studios.	59.99	4	https://dynamedion.com/wp-content/uploads/2021/10/AoE4_613_HERO-1-1-1.jpg
5	EA SPORTS FC™ 25	2024	EA Sports FC 25 is a football video game published by EA Sports. It is the second instalment in the EA Sports FC series and the 32nd overall instalment of EA Sports' football simulation games.	59.99	5	https://assets.goal.com/images/v3/blt178d34156522b281/GOAL%20-%20Blank%20WEB%20-%20Facebook%20-%202024-07-17T151013.046.jpg?auto=webp&format=pjpg&width=3840&quality=60
2	The Witcher 3: Wild Hunt	2015	The Witcher 3: Wild Hunt is a 2015 action role-playing game developed and published by the Polish studio CD Projekt. It is the sequel to the 2011 game The Witcher 2: Assassins of Kings and the third game in The Witcher video game series, played in an open world with a third-person perspective.	74.99	2	https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTYIzM-VJdiRfX4jtM1twR6hYQTqK9qQ5yXdQ&s
1	Call of Duty: Modern Warfare III	2023	Call of Duty: Modern Warfare III is a 2023 first-person shooter game developed by Sledgehammer Games and published by Activision.	59.99	1	https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSHTz65TwErdOMX_mMCQaqpIDnyiDG0jXX-Ow&s
3	Stardew Valley	2016	Stardew Valley is a 2016 farm life simulation role-playing video game developed by Eric "ConcernedApe" Barone. Players take the role of a character who inherits their deceased grandfather's dilapidated farm in a place known as Stardew Valley.	24.99	3	https://assets.nintendo.com/image/upload/ar_16:9,c_lpad,w_1240/b_white/f_auto/q_auto/ncom/software/switch/70010000001801/7aa9c6cf5e7d4cecf481f18b1d7a9d79e7aab85045b22203effb2dda409bc5b7
7	Ghost of Tsushima	2020	Set in feudal Japan, players control a samurai named Jin Sakai, mastering stealth and katana-based combat as he fights to protect his homeland from invading Mongol forces.	39.99	1	https://upload.wikimedia.org/wikipedia/en/b/b6/Ghost_of_Tsushima.jpg
6	Devil May Cry 5	2019	Play as one of three demon hunters with unique abilities, combining sword and gunplay in stylish, fast-paced combat to fight demonic hordes.	24.99	1	https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTDdxCvQO3xhI8C6oxfTwEXgEHeuIYfgLjlcw&s
8	Hades	2020	A rogue-like dungeon crawler where players control Zagreus, son of Hades, attempting to escape the Underworld with unique, customizable weapons and abilities. The game mixes action and story with intense, rewarding gameplay.	24.99	1	https://images.ctfassets.net/5owu3y35gz1g/221gU3J0H5urRoHsNbYDpf/910e01518b59323f8624c1a5a2001ff5/Hades_Rating_Cover__1_.png?w=225&q=100
9	Sekiro: Shadows Die Twice	2019	A single-player game focusing on precision combat, stealth, and exploration. Players become a shinobi on a journey of revenge, using skillful counterattacks to survive challenging battles.	59.99	1	https://upload.wikimedia.org/wikipedia/en/6/6e/Sekiro_art.jpg
10	Elden Ring\n	2022	A dark fantasy RPG from the creators of Dark Souls, Elden Ring offers an expansive open world with challenging combat, exploration, and rich lore. Players create a custom character and journey to repair the shattered Elden Ring.	59.99	2	https://upload.wikimedia.org/wikipedia/vi/thumb/b/b9/Elden_Ring_Box_art.jpg/220px-Elden_Ring_Box_art.jpg
11	Dragon Quest XI: Echoes of an Elusive Age	2018	A classic turn-based RPG with a vibrant, colorful world and a story-rich experience. Players follow a young hero as they uncover secrets of their origin and battle against an ancient evil threatening the world.	39.99	2	https://cdn.tgdd.vn/GameApp/4/263887/Screentshots/dragon-quest-xi-s-echoes-of-an-elusive-age-game-hanh-01-12-2021-0.png
12	Final Fantasy XV	2016	An open-world action RPG where players control Prince Noctis on a journey to reclaim his throne. The game combines exploration, real-time combat, and deep storytelling in a modern fantasy setting.	34.99	2	https://upload.wikimedia.org/wikipedia/en/5/5a/FF_XV_cover_art.jpg
13	Disco Elysium: The Final Cut	2021	A unique, story-driven RPG that focuses on dialogue and decision-making. Players control a detective with a broken mind as he investigates a murder in a decaying city, with choices deeply affecting outcomes.	39.99	2	https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTWuDtkF7i-Fm3YtKAHzjtm2OCqO0HTxTvv-w&s
14	The Sims 4	2014	A life simulation game where players create and control Sims, customizing their appearance, personalities, and homes. Players guide them through careers, relationships, and day-to-day activities.	39.99	3	https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTzOiasS_WD9SXypzZzrKbV3U2LXRnwyTR2Rw&s
15	Planet Zoo\n	2019	A zoo management simulation where players design and build detailed animal habitats, manage staff, and ensure the well-being of both animals and visitors. It offers realistic animal behavior and breeding.	44.99	3	https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcS6xP4K_u5Rm8xaRDdHwnDpgDsAD9li4kxP3A&s
16	Microsoft Flight Simulator\n	2020	A hyper-realistic flight simulation game where players can pilot various aircraft across a fully mapped, detailed representation of Earth. It includes real-time weather, detailed landscapes, and accurate flight physics.	59.99	3	https://cdn.simulationdaily.com/2024/09/2b8be177-microsoft-flight-simulator-2024-preview.jpg
17	Cities: Skylines	2015	A city-building simulation where players design and manage a bustling metropolis. Players must balance resources, manage traffic, and create a livable environment for citizens while dealing with city management challenges.	29.99	3	https://cdn2.fptshop.com.vn/unsafe/1920x0/filters:quality(100)/2024_2_20_638440266076354560_cities-skylines.jpg
18	StarCraft II	2010	A fast-paced real-time strategy game where players command one of three factions—Terran, Zerg, or Protoss—in intense sci-fi battles. The game features a campaign and a competitive multiplayer scene.	9.99	4	https://upload.wikimedia.org/wikipedia/en/2/20/StarCraft_II_-_Box_Art.jpg
19	Crusader Kings III	2020	A grand strategy game where players manage a medieval dynasty through generations. With a focus on intrigue, diplomacy, and warfare, players shape the fate of their family and kingdom across centuries.	49.99	4	https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcR6ZgTopU5i2FkbXzaBI0DQe8y554Gt2D6Fng&s
20	Company of Heroes 2	2013	A World War II real-time strategy game that emphasizes tactical combat and resource management. Players control squads, tanks, and artillery in realistic scenarios, with destructible environments and dynamic battlefields.	19.99	4	https://shared.cloudflare.steamstatic.com/store_item_assets/steam/apps/231430/capsule_616x353.jpg?t=1661158807
21	Anno 1800	2019	A city-building and economic strategy game set during the Industrial Revolution. Players establish trade routes, manage resources, and grow their city into a thriving metropolis, balancing production, workforce, and trade.	59.99	4	https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRlO2FnTxdMWa3w9xNahAa9WT_vH52gKp2fsg&s
22	MLB: The Show 24	2024	The latest installment in the MLB series offers enhanced graphics and new gameplay modes, including "Storylines," which feature baseball legends. Players can manage teams, dive into realistic matches, and enjoy updated team rosters and visuals.	59.99	5	https://upload.wikimedia.org/wikipedia/en/d/d8/MLB_The_Show_24_Cover.jpg
23	TopSpin 2K25	2024	A return of the TopSpin tennis franchise, this game allows players to compete in major tournaments like Wimbledon and the US Open. It includes a robust player creator and lets you play as real-world tennis stars or customize your own athlete.	59.99	5	https://cdn.dlcompare.com/game_tetiere/upload/gameimage/file/topspin-2k25-tetiere-file-3b28b2ee.jpg.webp
24	NBA 2K25	2024	The latest in the NBA 2K series, NBA 2K25 allows players to step into the shoes of NBA and WNBA stars. It includes the popular MyCareer and MyTeam modes, offering both single-player and online multiplayer options.\n	59.99	5	https://bizweb.dktcdn.net/100/202/418/products/nba-2k25-ps5-02.jpg?v=1722762635033
\.


--
-- TOC entry 4860 (class 0 OID 16673)
-- Dependencies: 224
-- Data for Name: Logins; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Logins" (customerid, username, password) FROM stdin;
1	thaison	123456
2	thaison0910	ngobi321
7	thaison02312312	ngobi321
5	thaisonne	123456
4	son1234	123456
3	thaison09101	123456
6	thaison7778	1234567
8	thaison123123	123456
\.


--
-- TOC entry 4859 (class 0 OID 16666)
-- Dependencies: 223
-- Data for Name: Registers; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."Registers" (customerid, username, password, confirmpassword, email) FROM stdin;
1	thaison	123456	123456	
2	thaison0910	ngobi321	ngobi321	
7	thaison02312312	ngobi321	ngobi321	
5	thaisonne	123456	123456	
4	son1234	123456	123456	123vvve@gmail.com
3	thaison09101	123456	123456	thaison0910@gmail.com
6	thaison7778	1234567	1234567	fewfwefwe@gmail.com
8	thaison123123	123456	123456	fwefefe@gmail.com
\.


--
-- TOC entry 4853 (class 0 OID 16617)
-- Dependencies: 217
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20241028083549_TblNewDB	8.0.8
20241029040335_AddTblLoginAndRegister	8.0.8
20241029042230_AddBangRegistervaLogin	8.0.8
20241106080033_themuploadanh	8.0.8
20241107023152_setnullforImageurl	8.0.8
20241112030143_AddAdminCreateAndLogin	8.0.8
20241118024743_AddImageURL	8.0.8
20241118041504_AddCart	8.0.8
20241120070143_AddCheckout	8.0.8
20241120071707_addCheckOut	8.0.8
20241120073544_AddCheckOut	8.0.8
20241120075339_AddCheckOut	8.0.8
20241125022836_addEmail	8.0.8
\.


--
-- TOC entry 4874 (class 0 OID 0)
-- Dependencies: 225
-- Name: AdminCreates_adminid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."AdminCreates_adminid_seq"', 3, true);


--
-- TOC entry 4875 (class 0 OID 0)
-- Dependencies: 228
-- Name: Carts_cartid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Carts_cartid_seq"', 106, true);


--
-- TOC entry 4876 (class 0 OID 0)
-- Dependencies: 230
-- Name: Checkouts_saleid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Checkouts_saleid_seq"', 10, true);


--
-- TOC entry 4877 (class 0 OID 0)
-- Dependencies: 218
-- Name: GameCategories_categoryid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."GameCategories_categoryid_seq"', 1, false);


--
-- TOC entry 4878 (class 0 OID 0)
-- Dependencies: 220
-- Name: Games_gameid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Games_gameid_seq"', 7, true);


--
-- TOC entry 4879 (class 0 OID 0)
-- Dependencies: 222
-- Name: Registers_customerid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public."Registers_customerid_seq"', 7, true);


--
-- TOC entry 4692 (class 2606 OID 16725)
-- Name: AdminCreates PK_AdminCreates; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AdminCreates"
    ADD CONSTRAINT "PK_AdminCreates" PRIMARY KEY (adminid);


--
-- TOC entry 4694 (class 2606 OID 16732)
-- Name: AdminLogins PK_AdminLogins; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AdminLogins"
    ADD CONSTRAINT "PK_AdminLogins" PRIMARY KEY (adminid);


--
-- TOC entry 4698 (class 2606 OID 16751)
-- Name: Carts PK_Carts; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Carts"
    ADD CONSTRAINT "PK_Carts" PRIMARY KEY (cartid);


--
-- TOC entry 4701 (class 2606 OID 16832)
-- Name: Checkouts PK_Checkouts; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Checkouts"
    ADD CONSTRAINT "PK_Checkouts" PRIMARY KEY (saleid);


--
-- TOC entry 4683 (class 2606 OID 16629)
-- Name: GameCategories PK_GameCategories; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."GameCategories"
    ADD CONSTRAINT "PK_GameCategories" PRIMARY KEY (categoryid);


--
-- TOC entry 4686 (class 2606 OID 16637)
-- Name: Games PK_Games; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Games"
    ADD CONSTRAINT "PK_Games" PRIMARY KEY (gameid);


--
-- TOC entry 4690 (class 2606 OID 16679)
-- Name: Logins PK_Logins; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Logins"
    ADD CONSTRAINT "PK_Logins" PRIMARY KEY (customerid);


--
-- TOC entry 4688 (class 2606 OID 16672)
-- Name: Registers PK_Registers; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Registers"
    ADD CONSTRAINT "PK_Registers" PRIMARY KEY (customerid);


--
-- TOC entry 4681 (class 2606 OID 16621)
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- TOC entry 4695 (class 1259 OID 16762)
-- Name: IX_Carts_customerid; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Carts_customerid" ON public."Carts" USING btree (customerid);


--
-- TOC entry 4696 (class 1259 OID 16763)
-- Name: IX_Carts_gameid; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Carts_gameid" ON public."Carts" USING btree (gameid);


--
-- TOC entry 4699 (class 1259 OID 16838)
-- Name: IX_Checkouts_customerid; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Checkouts_customerid" ON public."Checkouts" USING btree (customerid);


--
-- TOC entry 4684 (class 1259 OID 16643)
-- Name: IX_Games_categoryid; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Games_categoryid" ON public."Games" USING btree (categoryid);


--
-- TOC entry 4704 (class 2606 OID 16733)
-- Name: AdminLogins FK_AdminLogins_AdminCreates_adminid; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."AdminLogins"
    ADD CONSTRAINT "FK_AdminLogins_AdminCreates_adminid" FOREIGN KEY (adminid) REFERENCES public."AdminCreates"(adminid) ON DELETE CASCADE;


--
-- TOC entry 4705 (class 2606 OID 16752)
-- Name: Carts FK_Carts_Games_gameid; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Carts"
    ADD CONSTRAINT "FK_Carts_Games_gameid" FOREIGN KEY (gameid) REFERENCES public."Games"(gameid) ON DELETE CASCADE;


--
-- TOC entry 4706 (class 2606 OID 16757)
-- Name: Carts FK_Carts_Logins_customerid; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Carts"
    ADD CONSTRAINT "FK_Carts_Logins_customerid" FOREIGN KEY (customerid) REFERENCES public."Logins"(customerid) ON DELETE CASCADE;


--
-- TOC entry 4707 (class 2606 OID 16833)
-- Name: Checkouts FK_Checkouts_Logins_customerid; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Checkouts"
    ADD CONSTRAINT "FK_Checkouts_Logins_customerid" FOREIGN KEY (customerid) REFERENCES public."Logins"(customerid) ON DELETE CASCADE;


--
-- TOC entry 4702 (class 2606 OID 16638)
-- Name: Games FK_Games_GameCategories_categoryid; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Games"
    ADD CONSTRAINT "FK_Games_GameCategories_categoryid" FOREIGN KEY (categoryid) REFERENCES public."GameCategories"(categoryid) ON DELETE CASCADE;


--
-- TOC entry 4703 (class 2606 OID 16680)
-- Name: Logins FK_Logins_Registers_customerid; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Logins"
    ADD CONSTRAINT "FK_Logins_Registers_customerid" FOREIGN KEY (customerid) REFERENCES public."Registers"(customerid) ON DELETE CASCADE;


-- Completed on 2024-11-26 09:37:22

--
-- PostgreSQL database dump complete
--
