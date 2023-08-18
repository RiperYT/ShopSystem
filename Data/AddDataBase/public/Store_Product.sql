create table "Store_Product"
(
    "UPC"               varchar(12)    not null
        constraint store_product_pk
            primary key,
    "UPC_prom"          varchar(12)
        constraint store_product_store_product_upc_fk
            references "Store_Product"
            on update cascade on delete set null,
    id_product          integer        not null
        constraint store_product_product_id_product_fk
            references "Product"
            on update cascade,
    selling_price       numeric(13, 4) not null,
    products_number     integer        not null,
    promotional_product boolean        not null
);

alter table "Store_Product"
    owner to zlagoda;

create unique index store_product_upc_uindex
    on "Store_Product" ("UPC");

