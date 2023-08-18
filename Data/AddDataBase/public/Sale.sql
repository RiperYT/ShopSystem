create table "Sale"
(
    "UPC"          varchar(12)    not null
        constraint sale_store_product_upc_fk
            references "Store_Product"
            on update cascade,
    check_number   varchar(10)    not null
        constraint sale_check_check_number_fk
            references "Check"
            on update cascade on delete cascade,
    product_number integer        not null,
    selling_price  numeric(13, 4) not null,
    constraint sale_pk
        primary key (check_number, "UPC")
);

alter table "Sale"
    owner to zlagoda;

create unique index sale_check_number_uindex
    on "Sale" (check_number);

create unique index sale_upc_uindex
    on "Sale" ("UPC");

