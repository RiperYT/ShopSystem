create table "Check"
(
    check_number varchar(10)    not null
        constraint check_pk
            primary key,
    id_employee  varchar(10)    not null
        constraint check_employee_id_employee_fk
            references "Employee"
            on update cascade,
    card_number  varchar(13)
        constraint check_customer_card_card_number_fk
            references "Customer_card"
            on update cascade,
    print_date   timestamp      not null,
    sum_total    numeric(13, 4) not null,
    vat          numeric(13, 4) not null
);

alter table "Check"
    owner to zlagoda;

create unique index check_check_number_uindex
    on "Check" (check_number);

