create table "Customer_card"
(
    card_number     varchar(13) not null
        constraint customer_card_pk
            primary key,
    cust_surname    varchar(50) not null,
    cust_name       varchar(50) not null,
    cust_patronymic varchar(50),
    phone_number    varchar(13) not null,
    city            varchar(50),
    street          varchar(50),
    zip_code        varchar(9),
    percent         integer     not null
);

alter table "Customer_card"
    owner to zlagoda;

create unique index customer_card_card_number_uindex
    on "Customer_card" (card_number);

