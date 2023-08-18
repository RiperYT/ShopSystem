--
-- PostgreSQL database dump
--

-- Dumped from database version 12.9 (Debian 12.9-1.pgdg110+1)
-- Dumped by pg_dump version 12.8

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

DROP DATABASE IF EXISTS zlagoda;
--
-- Name: zlagoda; Type: DATABASE; Schema: -; Owner: postgres
--

CREATE DATABASE zlagoda WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'en_US.utf8' LC_CTYPE = 'en_US.utf8';


ALTER DATABASE zlagoda OWNER TO postgres;

\connect zlagoda

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: Category; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Category" (
    category_number integer NOT NULL,
    category_name character varying(50) NOT NULL
);


ALTER TABLE public."Category" OWNER TO postgres;

--
-- Name: Category_category_number_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Category_category_number_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Category_category_number_seq" OWNER TO postgres;

--
-- Name: Category_category_number_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Category_category_number_seq" OWNED BY public."Category".category_number;


--
-- Name: Check; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Check" (
    check_number character varying(10) NOT NULL,
    id_employee character varying(10) NOT NULL,
    card_number character varying(13),
    print_date timestamp without time zone NOT NULL,
    sum_total numeric(13,4) NOT NULL,
    vat numeric(13,4) NOT NULL
);


ALTER TABLE public."Check" OWNER TO postgres;

--
-- Name: Customer_card; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Customer_card" (
    card_number character varying(13) NOT NULL,
    cust_surname character varying(50) NOT NULL,
    cust_name character varying(50) NOT NULL,
    cust_patronymic character varying(50),
    phone_number character varying(13) NOT NULL,
    city character varying(50),
    street character varying(50),
    zip_code character varying(9),
    percent integer NOT NULL
);


ALTER TABLE public."Customer_card" OWNER TO postgres;

--
-- Name: Employee; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Employee" (
    id_employee character varying(10) NOT NULL,
    empl_surname character varying(50) NOT NULL,
    empl_name character varying(50) NOT NULL,
    empl_patronymic character varying(50) NOT NULL,
    empl_role character varying(10) NOT NULL,
    salary numeric(13,4) NOT NULL,
    date_of_birth date NOT NULL,
    date_of_start date NOT NULL,
    phone_number character varying(13) NOT NULL,
    city character varying(50) NOT NULL,
    street character varying(50) NOT NULL,
    zip_code character varying(9) NOT NULL
);


ALTER TABLE public."Employee" OWNER TO postgres;

--
-- Name: Product; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Product" (
    id_product integer NOT NULL,
    category_number integer NOT NULL,
    product_name character varying(50) NOT NULL,
    characteristics character varying(100) NOT NULL
);


ALTER TABLE public."Product" OWNER TO postgres;

--
-- Name: Product_id_product_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Product_id_product_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Product_id_product_seq" OWNER TO postgres;

--
-- Name: Product_id_product_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Product_id_product_seq" OWNED BY public."Product".id_product;


--
-- Name: Sale; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Sale" (
    "UPC" character varying(12) NOT NULL,
    check_number character varying(10) NOT NULL,
    product_number integer NOT NULL,
    selling_price numeric(13,4) NOT NULL
);


ALTER TABLE public."Sale" OWNER TO postgres;

--
-- Name: Store_Product; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Store_Product" (
    "UPC" character varying(12) NOT NULL,
    "UPC_prom" character varying(12),
    id_product integer NOT NULL,
    selling_price numeric(13,4) NOT NULL,
    products_number integer NOT NULL,
    promotional_product boolean NOT NULL
);


ALTER TABLE public."Store_Product" OWNER TO postgres;

--
-- Name: Category category_number; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Category" ALTER COLUMN category_number SET DEFAULT nextval('public."Category_category_number_seq"'::regclass);


--
-- Name: Product id_product; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Product" ALTER COLUMN id_product SET DEFAULT nextval('public."Product_id_product_seq"'::regclass);


--
-- Name: Category category_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Category"
    ADD CONSTRAINT category_pk PRIMARY KEY (category_number);


--
-- Name: Check check_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Check"
    ADD CONSTRAINT check_pk PRIMARY KEY (check_number);


--
-- Name: Customer_card customer_card_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Customer_card"
    ADD CONSTRAINT customer_card_pk PRIMARY KEY (card_number);


--
-- Name: Employee employee_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Employee"
    ADD CONSTRAINT employee_pk PRIMARY KEY (id_employee);


--
-- Name: Product product_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Product"
    ADD CONSTRAINT product_pk PRIMARY KEY (id_product);


--
-- Name: Sale sale_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Sale"
    ADD CONSTRAINT sale_pk PRIMARY KEY (check_number, "UPC");


--
-- Name: Store_Product store_product_pk; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Store_Product"
    ADD CONSTRAINT store_product_pk PRIMARY KEY ("UPC");


--
-- Name: category_category_number_uindex; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX category_category_number_uindex ON public."Category" USING btree (category_number);


--
-- Name: check_check_number_uindex; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX check_check_number_uindex ON public."Check" USING btree (check_number);


--
-- Name: customer_card_card_number_uindex; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX customer_card_card_number_uindex ON public."Customer_card" USING btree (card_number);


--
-- Name: product_id_product_uindex; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX product_id_product_uindex ON public."Product" USING btree (id_product);


--
-- Name: sale_check_number_uindex; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX sale_check_number_uindex ON public."Sale" USING btree (check_number);


--
-- Name: sale_upc_uindex; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX sale_upc_uindex ON public."Sale" USING btree ("UPC");


--
-- Name: store_product_upc_uindex; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX store_product_upc_uindex ON public."Store_Product" USING btree ("UPC");


--
-- Name: Check check_customer_card_card_number_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Check"
    ADD CONSTRAINT check_customer_card_card_number_fk FOREIGN KEY (card_number) REFERENCES public."Customer_card"(card_number) ON UPDATE CASCADE;


--
-- Name: Check check_employee_id_employee_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Check"
    ADD CONSTRAINT check_employee_id_employee_fk FOREIGN KEY (id_employee) REFERENCES public."Employee"(id_employee) ON UPDATE CASCADE;


--
-- Name: Product product_category_category_number_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Product"
    ADD CONSTRAINT product_category_category_number_fk FOREIGN KEY (category_number) REFERENCES public."Category"(category_number) ON UPDATE CASCADE;


--
-- Name: Sale sale_check_check_number_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Sale"
    ADD CONSTRAINT sale_check_check_number_fk FOREIGN KEY (check_number) REFERENCES public."Check"(check_number) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- Name: Sale sale_store_product_upc_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Sale"
    ADD CONSTRAINT sale_store_product_upc_fk FOREIGN KEY ("UPC") REFERENCES public."Store_Product"("UPC") ON UPDATE CASCADE;


--
-- Name: Store_Product store_product_product_id_product_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Store_Product"
    ADD CONSTRAINT store_product_product_id_product_fk FOREIGN KEY (id_product) REFERENCES public."Product"(id_product) ON UPDATE CASCADE;


--
-- Name: Store_Product store_product_store_product_upc_fk; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Store_Product"
    ADD CONSTRAINT store_product_store_product_upc_fk FOREIGN KEY ("UPC_prom") REFERENCES public."Store_Product"("UPC") ON UPDATE CASCADE ON DELETE SET NULL;


--
-- PostgreSQL database dump complete
--

