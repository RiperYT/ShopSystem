create table "Category"
(
    category_number serial
        constraint category_pk
            primary key,
    category_name   varchar(50) not null
);

alter table "Category"
    owner to zlagoda;

create unique index category_category_number_uindex
    on "Category" (category_number);

