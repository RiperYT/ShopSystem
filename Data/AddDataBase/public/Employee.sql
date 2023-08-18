create table "Employee"
(
    id_employee     varchar(10)    not null
        constraint employee_pk
            primary key,
    empl_surname    varchar(50)    not null,
    empl_name       varchar(50)    not null,
    empl_patronymic varchar(50)    not null,
    empl_role       varchar(10)    not null,
    salary          numeric(13, 4) not null,
    date_of_birth   date           not null,
    date_of_start   date           not null,
    phone_number    varchar(13)    not null,
    city            varchar(50)    not null,
    street          varchar(50)    not null,
    zip_code        varchar(9)     not null
);

alter table "Employee"
    owner to zlagoda;

