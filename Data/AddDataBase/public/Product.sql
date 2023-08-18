create table "Product"
(
    id_product      serial
        constraint product_pk
            primary key,
    category_number integer      not null
        constraint product_category_category_number_fk
            references "Category"
            on update cascade,
    product_name    varchar(50)  not null,
    characteristics varchar(100) not null
);

alter table "Product"
    owner to zlagoda;

create unique index product_id_product_uindex
    on "Product" (id_product);

