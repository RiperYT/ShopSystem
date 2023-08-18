using Dapper;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zlagoda_Net4._7._2.Admin;
using Zlagoda_Net4._7._2.Common;
using Zlagoda_Net4._7._2.Data;

namespace Zlagoda_Net4._7._2.Repositories
{
    internal class AdminRepository
    {
        public readonly NpgsqlConnection connection;
        public AdminRepository()
        {
            var connectionString = StaticInfo.PostgreConncection;
            connection = new NpgsqlConnection(connectionString);
        }

        public bool IsAdmin(string id, string password)
        {
            var sql = @"SELECT id_employee
                        FROM ""Employee""
                        WHERE id_employee = @user_id AND password = @password AND empl_role = 'admin'";

            return connection.Query<string>(sql, new { user_id = id, password = password }).ToList().Count > 0;
        }

        //1
        public void Add(Product product)
        {
            var str = $"INSERT INTO \"Product\" (category_number, product_name, characteristics) VALUES ({product.Category_Number}, '{product.Product_Name}', '{product.Characteristics}')";

            connection.Open();
            var command = new NpgsqlCommand(str, connection);
            command.ExecuteReader();
            connection.Close();
        }
        public void Add(ProductInShop product)
        {
            var s = "NULL";
            if (product.UPC_Prom != null)
                s = $"'{product.UPC_Prom}'";
            var str = $"INSERT INTO \"Store_Product\" (\"UPC\", \"UPC_prom\", id_product, selling_price, products_number, promotional_product) VALUES ('{product.UPC}', {s}, {product.Id}, {product.Product_Price}, {product.Count}, {product.IsPromotional})";

            connection.Open();
            var command = new NpgsqlCommand(str, connection);
            command.ExecuteReader();
            connection.Close();
        }
        public void Add(ClientFull client)
        {
            var str = $"INSERT INTO \"Customer_card\" (card_number, cust_surname, cust_name, cust_patronymic, phone_number, city, street, zip_code, percent) VALUES ('{client.Card_Number}', '{client.Surname}', '{client.Name}', ";

            if (client.Patronymic == null)
                str += "NULL, ";
            else
                str += $"'{client.Patronymic}', ";

            str += $"'{client.Phone_Number}', ";

            if (client.City == null)
                str += "NULL, ";
            else
                str += $"'{client.City}', ";

            if (client.Street == null)
                str += "NULL, ";
            else
                str += $"'{client.Street}', ";

            if (client.ZipCode == null)
                str += "NULL, ";
            else
                str += $"'{client.ZipCode}', ";

            str += $"{client.Percent})";

            connection.Open();
            var command = new NpgsqlCommand(str, connection);
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void Add(Category category)
        {
            var str = $"INSERT INTO \"Category\" (category_name) VALUES ('{category.name}');";

            connection.Open();
            var command = new NpgsqlCommand(str, connection);
            command.ExecuteReader();
            connection.Close();
        }
        public void Add(Employee employee, string password)
        {
            var str = $"INSERT INTO \"Employee\" (id_employee, empl_surname, empl_name, empl_patronymic, empl_role, salary, date_of_birth, date_of_start, phone_number, city, street, zip_code, password) VALUES (:id_employee, :empl_surname, :empl_name, :empl_patronymic, :empl_role, :salary, :date_of_birth, :date_of_start, :phone_number, :city, :street, :zip_code, :password)";

            connection.Open();
            var command = new NpgsqlCommand(str, connection);
            command.Parameters.AddWithValue(":id_employee", NpgsqlDbType.Varchar, employee.id);
            command.Parameters.AddWithValue(":empl_surname", NpgsqlDbType.Varchar, employee.surname);
            command.Parameters.AddWithValue(":empl_name", NpgsqlDbType.Varchar, employee.name);
            command.Parameters.AddWithValue(":empl_patronymic", NpgsqlDbType.Varchar, employee.patronymic);
            command.Parameters.AddWithValue(":empl_role", NpgsqlDbType.Varchar, employee.role);
            command.Parameters.AddWithValue(":salary", NpgsqlDbType.Numeric, employee.salary);
            command.Parameters.AddWithValue(":date_of_birth", NpgsqlDbType.Date, employee.birth);
            command.Parameters.AddWithValue(":date_of_start", NpgsqlDbType.Date, employee.start);
            command.Parameters.AddWithValue(":phone_number", NpgsqlDbType.Varchar, employee.phone);
            command.Parameters.AddWithValue(":city", NpgsqlDbType.Varchar, employee.city);
            command.Parameters.AddWithValue(":street", NpgsqlDbType.Varchar, employee.street);
            command.Parameters.AddWithValue(":zip_code", NpgsqlDbType.Varchar, employee.zip);
            command.Parameters.AddWithValue(":password", NpgsqlDbType.Varchar, password);
            command.ExecuteNonQuery();
            connection.Close();
        }

        //2
        public void Update(Product product)
        {
            var str = $"UPDATE \"Product\" SET category_number = {product.Category_Number}, product_name = '{product.Product_Name}', characteristics = '{product.Characteristics}' WHERE id_product = {product.Id};";

            connection.Open();
            var command = new NpgsqlCommand(str, connection);
            command.ExecuteReader();
            connection.Close();
        }
        public void UpdateInShop(ProductInShop product)
        {
            var s = "NULL";
            if (product.UPC_Prom != null)
                s = $"'{product.UPC_Prom}'";
            var str = $"UPDATE \"Store_Product\" SET \"UPC_prom\" = {s}, id_product = {product.Id}, selling_price = :selling_price, products_number = {product.Count}, promotional_product = {product.IsPromotional} WHERE \"UPC\" = '{product.UPC}'";

            connection.Open();
            var command = new NpgsqlCommand(str, connection);
            command.Parameters.AddWithValue(":selling_price", NpgsqlDbType.Numeric, product.Product_Price);
            command.ExecuteReader();
            connection.Close();
        }
        public void Update(ClientFull client)
        {
            var str = $"UPDATE \"Customer_card\" SET cust_surname = '{client.Surname}', cust_name = '{client.Name}', ";

            if (client.Patronymic == null)
                str += "cust_patronymic = NULL, ";
            else
                str += $"cust_patronymic = '{client.Patronymic}', ";

            str += $"phone_number = '{client.Phone_Number}', ";

            if (client.City == null)
                str += "city = NULL, ";
            else
                str += $"city = '{client.City}', ";

            if (client.Street == null)
                str += "street = NULL, ";
            else
                str += $"street = '{client.Street}', ";

            if (client.ZipCode == null)
                str += "zip_code = NULL, ";
            else
                str += $"zip_code = '{client.ZipCode}', ";

            str += $"percent = {client.Percent} WHERE card_number = '{client.Card_Number}'";

            connection.Open();
            var command = new NpgsqlCommand(str, connection);
            command.CommandType = CommandType.Text;
            command.ExecuteNonQuery();
            connection.Close();
        }
        public void Update(Category category)
        {
            var str = $"UPDATE \"Category\" SET category_name = '{category.name}' WHERE category_number = {category.id}";

            connection.Open();
            var command = new NpgsqlCommand(str, connection);
            command.ExecuteReader();
            connection.Close();
        }
        public void Update(Employee employee, string password)
        {
            var str = $"UPDATE \"Employee\" SET empl_surname = :empl_surname, empl_name = :empl_name, empl_patronymic = :empl_patronymic, phone_number = :phone_number, city = :city, street = :street, zip_code = :zip_code, empl_role = :empl_role, salary = :salary, date_of_birth = :date_of_birth, password = :password WHERE id_employee = :id_employee";

            connection.Open();
            var command = new NpgsqlCommand(str, connection);
            command.Parameters.AddWithValue(":id_employee", NpgsqlDbType.Varchar, employee.id);
            command.Parameters.AddWithValue(":empl_surname", NpgsqlDbType.Varchar, employee.surname);
            command.Parameters.AddWithValue(":empl_name", NpgsqlDbType.Varchar, employee.name);
            command.Parameters.AddWithValue(":empl_patronymic", NpgsqlDbType.Varchar, employee.patronymic);
            command.Parameters.AddWithValue(":empl_role", NpgsqlDbType.Varchar, employee.role);
            command.Parameters.AddWithValue(":salary", NpgsqlDbType.Numeric, employee.salary);
            command.Parameters.AddWithValue(":date_of_birth", NpgsqlDbType.Date, employee.birth);
            command.Parameters.AddWithValue(":date_of_start", NpgsqlDbType.Date, employee.start);
            command.Parameters.AddWithValue(":phone_number", NpgsqlDbType.Varchar, employee.phone);
            command.Parameters.AddWithValue(":city", NpgsqlDbType.Varchar, employee.city);
            command.Parameters.AddWithValue(":street", NpgsqlDbType.Varchar, employee.street);
            command.Parameters.AddWithValue(":zip_code", NpgsqlDbType.Varchar, employee.zip);
            command.Parameters.AddWithValue(":password", NpgsqlDbType.Varchar, password);
            command.ExecuteNonQuery();
            connection.Close();
        }

        //3
        public void DeleteProduct (int id)
        {
            var str = $"DELETE FROM \"Product\" WHERE id_product = {id}";

            connection.Open();
            var command = new NpgsqlCommand(str, connection);
            command.ExecuteReader();
            connection.Close();
        }
        public void DeleteInShop(string upc)
        {
            var str = $"DELETE FROM \"Store_Product\" WHERE \"UPC\" = '{upc}'";

            connection.Open();
            var command = new NpgsqlCommand(str, connection);
            command.ExecuteReader();
            connection.Close();
        }
        public void DeleteClient(string card_number)
        {
            var str = $"DELETE FROM \"Customer_card\" WHERE card_number = '{card_number}'";

            connection.Open();
            var command = new NpgsqlCommand(str, connection);
            command.ExecuteReader();
            connection.Close();
        }
        public void DeleteCheck(string check_number)
        {
            var str = $"DELETE FROM \"Check\" WHERE check_number = '{check_number}'";

            connection.Open();
            var command = new NpgsqlCommand(str, connection);
            command.ExecuteReader();
            connection.Close();
        }
        public void DeleteCategory(int id)
        {
            var str = $"DELETE FROM \"Category\" WHERE category_number = {id}";

            connection.Open();
            var command = new NpgsqlCommand(str, connection);
            command.ExecuteReader();
            connection.Close();
        }
        public void DeleteEmployee(string id)
        {
            var str = $"DELETE FROM \"Employee\" WHERE id_employee = '{id}'";

            connection.Open();
            var command = new NpgsqlCommand(str, connection);
            command.ExecuteReader();
            connection.Close();
        }

        //5
        public List<Employee> ListOfEmployees()
        {
            var employees = new List<Employee>();

            connection.Open();
            var command = new NpgsqlCommand("SELECT * FROM \"Employee\" ORDER BY empl_surname", connection);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var employee = new Employee();
                employee.id = reader.GetString(0);
                employee.surname = reader.GetString(1);
                employee.name = reader.GetString(2);

                if (!reader.IsDBNull(3))
                    employee.patronymic = reader.GetString(3);

                employee.salary = reader.GetDecimal(5);
                employee.birth = reader.GetDateTime(6);
                employee.start = reader.GetDateTime(7);
                employee.phone = reader.GetString(8);

                if (!reader.IsDBNull(9))
                    employee.city = reader.GetString(9);
                if (!reader.IsDBNull(10))
                    employee.street = reader.GetString(10);
                if (!reader.IsDBNull(11))
                    employee.zip = reader.GetString(11);
                employees.Add(employee);
            }
            connection.Close();

            return employees;
        }

        //6
        public List<Employee> ListOfEmployeesCashiers()
        {
            var employees = new List<Employee>();

            connection.Open();
            var command = new NpgsqlCommand("SELECT * FROM \"Employee\" WHERE empl_role = 'cashier' ORDER BY empl_surname", connection);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var employee = new Employee();
                employee.id = reader.GetString(0);
                employee.surname = reader.GetString(1);
                employee.name = reader.GetString(2);

                if (!reader.IsDBNull(3))
                    employee.patronymic = reader.GetString(3);

                employee.role = reader.GetString(4);
                employee.salary = reader.GetDecimal(5);
                employee.birth = reader.GetDateTime(6);
                employee.start = reader.GetDateTime(7);
                employee.phone = reader.GetString(8);

                if (!reader.IsDBNull(9))
                    employee.city = reader.GetString(9);
                if (!reader.IsDBNull(10))
                    employee.street = reader.GetString(10);
                if (!reader.IsDBNull(11))
                    employee.zip = reader.GetString(11);
                employees.Add(employee);
            }
            connection.Close();

            return employees;
        }

        //7
        public List<Client> ListOfClients()
        {
            var clients = new List<Client>();

            connection.Open();
            var command = new NpgsqlCommand("SELECT card_number, cust_surname, cust_name, phone_number, percent\r\nFROM \"Customer_card\"\r\nORDER BY cust_surname;", connection);
            //command.Parameters.AddWithValue("user_id", user_id);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                clients.Add(new Client(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4)));
            }
            connection.Close();

            return clients;
        }
        public ClientFull GetClientFullInfo(string card_number)
        {
            connection.Open();
            var command = new NpgsqlCommand($"SELECT *\r\nFROM \"Customer_card\"\r\nWHERE card_number = '{card_number}'", connection);

            var reader = command.ExecuteReader();
            ClientFull client = new ClientFull();
            while (reader.Read())
            {
                client.Card_Number = reader.GetString(0);
                client.Surname = reader.GetString(1);
                client.Name = reader.GetString(2);
                if (reader.IsDBNull(3))
                    client.Patronymic = "";
                else
                    client.Patronymic = reader.GetString(3);
                client.Phone_Number = reader.GetString(4);
                if (reader.IsDBNull(5))
                    client.City = "";
                else
                    client.City = reader.GetString(5);
                if (reader.IsDBNull(6))
                    client.Street = "";
                else
                    client.Street = reader.GetString(6);
                if (reader.IsDBNull(7))
                    client.ZipCode = "";
                else
                    client.ZipCode = reader.GetString(7);
                client.Percent = reader.GetInt32(8);
            }
            connection.Close();

            return client;
        }

        //8
        public List<Category> ListOfCategories()
        {
            var categories = new List<Category>();

            connection.Open();
            var command = new NpgsqlCommand("SELECT * FROM \"Category\" ORDER BY category_name;", connection);
            //command.Parameters.AddWithValue("user_id", user_id);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                categories.Add(new Category(reader.GetInt32(0), reader.GetString(1)));
            }
            connection.Close();

            return categories;
        }

        //9
        public List<Product> ListOfProducts()
        {
            var products = new List<Product>();

            connection.Open();
            var command = new NpgsqlCommand("SELECT id_product, product_name, ca.category_name, characteristics, pr.category_number\r\nFROM \"Product\" pr\r\nJOIN \"Category\" AS ca ON ca.category_number = pr.category_number\r\nORDER BY product_name", connection);
            //command.Parameters.AddWithValue("user_id", user_id);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                products.Add(new Product(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4)));
            }
            connection.Close();

            return products;
        }

        //10
        public List<ProductInShop> ListOfProductsInShop()
        {
            var products = new List<ProductInShop>();

            connection.Open();
            var command = new NpgsqlCommand("SELECT \"UPC\", sp.id_product, pr.product_name, selling_price, products_number, promotional_product, \"UPC_prom\"\r\nFROM \"Store_Product\" sp\r\nJOIN \"Product\" AS pr ON pr.id_product = sp.id_product\r\nORDER BY products_number;", connection);
            //command.Parameters.AddWithValue("user_id", user_id);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader.IsDBNull(6))
                    products.Add(new ProductInShop(reader.GetString(0), reader.GetInt32(1), reader.GetString(2), reader.GetDecimal(3), reader.GetInt32(4), reader.GetBoolean(5), ""));
                else
                    products.Add(new ProductInShop(reader.GetString(0), reader.GetInt32(1), reader.GetString(2), reader.GetDecimal(3), reader.GetInt32(4), reader.GetBoolean(5), reader.GetString(6)));
            }
            connection.Close();

            return products;
        }

        //12
        public List<Client> ListOfClientsByPercent(int percent)
        {
            var clients = new List<Client>();

            connection.Open();
            var command = new NpgsqlCommand($"SELECT card_number, cust_surname, cust_name, phone_number, percent\r\nFROM \"Customer_card\"\r\nWHERE percent = {percent} \r\nORDER BY cust_surname;", connection);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                clients.Add(new Client(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4)));
            }
            connection.Close();

            return clients;
        }

        //13
        public List<Product> ListOfProductsByNameAndCategory(string name, string category)
        {
            var products = new List<Product>();

            connection.Open();
            var command = new NpgsqlCommand($"SELECT id_product, product_name, ca.category_name, characteristics\r\nFROM \"Product\" pr\r\nJOIN \"Category\" AS ca ON ca.category_number = pr.category_number\r\nWHERE upper(product_name) LIKE '%{name.ToUpper()}%' AND upper(ca.category_name) LIKE '%{category.ToUpper()}%'\r\nORDER BY product_name;", connection);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                products.Add(new Product(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
            }
            connection.Close();

            return products;
        }

        //14
        public List<ProductInShop> ListOfProductsInShopSearch(string upc)
        {
            var products = new List<ProductInShop>();

            connection.Open();
            var command = new NpgsqlCommand($"SELECT \"UPC\", sp.id_product, pr.product_name, selling_price, products_number, promotional_product, \"UPC_prom\"\r\nFROM \"Store_Product\" sp\r\nJOIN \"Product\" AS pr ON pr.id_product = sp.id_product\r\n WHERE upper(\"UPC\") LIKE '%{upc.ToUpper()}%' ORDER BY product_name;", connection);
            //command.Parameters.AddWithValue("user_id", user_id);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader.IsDBNull(6))
                    products.Add(new ProductInShop(reader.GetString(0), reader.GetInt32(1), reader.GetString(2), reader.GetDecimal(3), reader.GetInt32(4), reader.GetBoolean(5), ""));
                else
                    products.Add(new ProductInShop(reader.GetString(0), reader.GetInt32(1), reader.GetString(2), reader.GetDecimal(3), reader.GetInt32(4), reader.GetBoolean(5), reader.GetString(6)));
            }
            connection.Close();

            return products;
        }

        //15
        public List<ProductInShop> ListOfProductsInShopProm()
        {
            var products = new List<ProductInShop>();

            connection.Open();
            var command = new NpgsqlCommand("SELECT \"UPC\", sp.id_product, pr.product_name, selling_price, products_number, promotional_product, \"UPC_prom\"\r\nFROM \"Store_Product\" sp\r\nJOIN \"Product\" AS pr ON pr.id_product = sp.id_product\r\n WHERE promotional_product = true ORDER BY product_name;", connection);
            //command.Parameters.AddWithValue("user_id", user_id);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader.IsDBNull(6))
                    products.Add(new ProductInShop(reader.GetString(0), reader.GetInt32(1), reader.GetString(2), reader.GetDecimal(3), reader.GetInt32(4), reader.GetBoolean(5), ""));
                else
                    products.Add(new ProductInShop(reader.GetString(0), reader.GetInt32(1), reader.GetString(2), reader.GetDecimal(3), reader.GetInt32(4), reader.GetBoolean(5), reader.GetString(6)));
            }
            connection.Close();

            return products;
        }

        //16
        public List<ProductInShop> ListOfProductsInShopNotProm()
        {
            var products = new List<ProductInShop>();

            connection.Open();
            var command = new NpgsqlCommand("SELECT \"UPC\", sp.id_product, pr.product_name, selling_price, products_number, promotional_product, \"UPC_prom\"\r\nFROM \"Store_Product\" sp\r\nJOIN \"Product\" AS pr ON pr.id_product = sp.id_product\r\n WHERE promotional_product = false ORDER BY product_name;", connection);
            //command.Parameters.AddWithValue("user_id", user_id);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader.IsDBNull(6))
                    products.Add(new ProductInShop(reader.GetString(0), reader.GetInt32(1), reader.GetString(2), reader.GetDecimal(3), reader.GetInt32(4), reader.GetBoolean(5), ""));
                else
                    products.Add(new ProductInShop(reader.GetString(0), reader.GetInt32(1), reader.GetString(2), reader.GetDecimal(3), reader.GetInt32(4), reader.GetBoolean(5), reader.GetString(6)));
            }
            connection.Close();

            return products;
        }

        //17
        public List<Check> GetChecksPeriodIdEmployee(string id_employee, DateTime from, DateTime to)
        {
            var list = new List<Check>();
            connection.Open();
            var command = new NpgsqlCommand($"SELECT check_number, card_number, print_date, sum_total, vat FROM \"Check\" WHERE id_employee = '{id_employee}' AND date(print_date) >= :from AND date(print_date) <= :to", connection);
            command.Parameters.AddWithValue("from", NpgsqlDbType.Date, from);
            command.Parameters.AddWithValue("to", NpgsqlDbType.Date, to);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader.IsDBNull(1))
                    list.Add(new Check(reader.GetString(0), reader.GetDateTime(2), reader.GetDecimal(3), reader.GetDecimal(4)));
                else
                    list.Add(new Check(reader.GetString(0), reader.GetString(1), reader.GetDateTime(2), reader.GetDecimal(3), reader.GetDecimal(4)));
            }

            connection.Close();

            return list;
        }

        //18
        public List<Check> GetChecksPeriod(DateTime from, DateTime to)
        {
            var list = new List<Check>();
            connection.Open();
            var command = new NpgsqlCommand($"SELECT check_number, card_number, print_date, sum_total, vat FROM \"Check\" WHERE date(print_date) >= :from AND date(print_date) <= :to", connection);
            command.Parameters.AddWithValue("from", NpgsqlDbType.Date, from);
            command.Parameters.AddWithValue("to", NpgsqlDbType.Date, to);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader.IsDBNull(1))
                    list.Add(new Check(reader.GetString(0), reader.GetDateTime(2), reader.GetDecimal(3), reader.GetDecimal(4)));
                else
                    list.Add(new Check(reader.GetString(0), reader.GetString(1), reader.GetDateTime(2), reader.GetDecimal(3), reader.GetDecimal(4)));
            }

            connection.Close();

            return list;
        }

        //19
        public decimal SumEmployee(string id_employee, DateTime from, DateTime to)
        {
            decimal sum = 0;
            connection.Open();
            var command = new NpgsqlCommand($"SELECT sum(sum_total) FROM \"Check\" WHERE id_employee = '{id_employee}' AND date(print_date) >= :from AND date(print_date) <= :to", connection);
            command.Parameters.AddWithValue("from", NpgsqlDbType.Date, from);
            command.Parameters.AddWithValue("to", NpgsqlDbType.Date, to);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                sum = reader.GetDecimal(0);
            }

            connection.Close();

            return sum;
        }

        //20
        public decimal Sum( DateTime from, DateTime to)
        {
            decimal sum = 0;
            connection.Open();
            var command = new NpgsqlCommand($"SELECT sum(sum_total) FROM \"Check\" WHERE date(print_date) >= :from AND date(print_date) <= :to", connection);
            command.Parameters.AddWithValue("from", NpgsqlDbType.Date, from);
            command.Parameters.AddWithValue("to", NpgsqlDbType.Date, to);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                sum = reader.GetDecimal(0);
            }

            connection.Close();

            return sum;
        }

        //21
        public decimal SumNumber(string UPC, DateTime from, DateTime to)
        {
            decimal sum = 0;
            connection.Open();
            var command = new NpgsqlCommand($"SELECT sum(S.product_number) FROM \"Check\" JOIN \"Sale\" S on \"Check\".check_number = S.check_number WHERE S.\"UPC\" = '{UPC}'", connection);
            command.Parameters.AddWithValue("from", NpgsqlDbType.Date, from);
            command.Parameters.AddWithValue("to", NpgsqlDbType.Date, to);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                sum = reader.GetDecimal(0);
            }

            connection.Close();

            return sum;
        }

        //other
        public List<Check> GetChecksDay(string id_employee)
        {
            var list = new List<Check>();
            connection.Open();
            var command = new NpgsqlCommand($"SELECT check_number, card_number, print_date, sum_total, vat FROM \"Check\" WHERE id_employee = '{id_employee}' AND date(print_date) = current_date", connection);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                if (reader.IsDBNull(1))
                    list.Add(new Check(reader.GetString(0), reader.GetDateTime(2), reader.GetDecimal(3), reader.GetDecimal(4)));
                else
                    list.Add(new Check(reader.GetString(0), reader.GetString(1), reader.GetDateTime(2), reader.GetDecimal(3), reader.GetDecimal(4)));
            }

            connection.Close();

            return list;
        }

        public List<Sale> GetSales(string check_number)
        {
            var list = new List<Sale>();
            connection.Open();
            var command = new NpgsqlCommand($"SELECT * FROM \"Sale\" WHERE check_number = '{check_number}'", connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                list.Add(new Sale(reader.GetString(0), reader.GetString(1), reader.GetInt32(2), reader.GetDecimal(3)));
            }
            connection.Close();
            return list;
        }
    }
}
