using Dapper;
using Npgsql;
using NpgsqlTypes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using Zlagoda_Net4._7._2.Cashier;
using Zlagoda_Net4._7._2.Common;
using Zlagoda_Net4._7._2.Data;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Zlagoda_Net4._7._2.Repositories
{
    internal class CashierRepository
    {
        private readonly NpgsqlConnection connection;
        public CashierRepository()
        {
            var connectionString = StaticInfo.PostgreConncection;
            connection = new NpgsqlConnection(connectionString);
        }

        public bool IsCashier(string id, string password)
        {
            var sql = @"SELECT id_employee
                        FROM ""Employee""
                        WHERE id_employee = @user_id AND password = @password AND empl_role = 'cashier'";

            return connection.Query<string>(sql, new { user_id = id, password = password }).ToList().Count > 0;
        }

        //1
        public List<Product> ListOfProducts()
        {
            var products = new List<Product>();

            connection.Open();
            var command = new NpgsqlCommand("SELECT id_product, product_name, ca.category_name, characteristics\r\nFROM \"Product\" pr\r\nJOIN \"Category\" AS ca ON ca.category_number = pr.category_number\r\nORDER BY product_name", connection);
            //command.Parameters.AddWithValue("user_id", user_id);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                products.Add(new Product(reader.GetInt32(0), reader.GetString(1), reader.GetString(2), reader.GetString(3)));
            }
            connection.Close();

            return products;
        }

        //2
        public List<ProductInShop> ListOfProductsInShop()
        {
            var products = new List<ProductInShop>();

            connection.Open();
            var command = new NpgsqlCommand("SELECT \"UPC\", sp.id_product, pr.product_name, selling_price, products_number, promotional_product, \"UPC_prom\"\r\nFROM \"Store_Product\" sp\r\nJOIN \"Product\" AS pr ON pr.id_product = sp.id_product\r\nORDER BY product_name;", connection);
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

        //3
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

        //4, 5
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

        //6
        public List<Client> ListOfClientsBySurname(string surname)
        {
            var clients = new List<Client>();

            connection.Open();
            var command = new NpgsqlCommand($"SELECT card_number, cust_surname, cust_name, phone_number, percent\r\nFROM \"Customer_card\"\r\nWHERE upper(cust_surname) LIKE '%{surname.ToUpper()}%'\r\nORDER BY cust_surname;", connection);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                clients.Add(new Client(reader.GetString(0), reader.GetString(1), reader.GetString(2), reader.GetString(3), reader.GetInt32(4)));
            }
            connection.Close();

            return clients;
        }
        
        //7
        public void AddCheck(Check check)
        {
            var str = $"INSERT INTO \"Check\"(check_number, id_employee, card_number, print_date, sum_total, vat) VALUES('{check.check_number}', '{StaticInfo.id}', ";

            if (check.card_number != null)
                str += $"'{check.card_number}', ";
            else
                str += "NULL, ";

            str += ":date, :sum, :vat)";
            connection.Open();
            var command = new NpgsqlCommand(str, connection);
            command.Parameters.AddWithValue("date", NpgsqlDbType.Timestamp, check.print_date);
            command.Parameters.AddWithValue("sum", NpgsqlDbType.Numeric, check.sum_total);
            command.Parameters.AddWithValue("vat", NpgsqlDbType.Numeric, check.vat);

            command.ExecuteReader();

            connection.Close();
        }

        //7
        public void AddSale(Sale sale)
        {
            var str = $"INSERT INTO \"Sale\" (\"UPC\", check_number, product_number, selling_price)  VALUES ('{sale.UPC}', '{sale.CheckNumber}', '{sale.Number}', :price)";

            connection.Open();
            var command = new NpgsqlCommand(str, connection);
            command.Parameters.AddWithValue("price", NpgsqlDbType.Numeric, sale.Price);
            command.ExecuteReader();

            connection.Close();
        }

        //7
        public void UpdateCountProduct(string upc, int minus)
        {
            var str = $"UPDATE \"Store_Product\" SET products_number = (products_number - {minus}) WHERE \"UPC\" = '{upc}'";

            connection.Open();
            var command = new NpgsqlCommand(str, connection);
            command.ExecuteReader();

            connection.Close();
        }

        //8
        public void AddClient(ClientFull client)
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

        //8
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
        public void UpdateClient(ClientFull client)
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

        //9
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

        public List<Check> ListOfChecks()
        {
            var list = new List<Check>();
            connection.Open();
            var command = new NpgsqlCommand($"SELECT check_number, card_number, print_date, sum_total, vat FROM \"Check\"", connection);

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

        //10
        public List<Check> GetChecksPeriod(string id_employee, DateTime from, DateTime to)
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

        //11
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

        //12
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

        //13
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

        public Employee AboutMe(string id)
        {
            var employee = new Employee();

            connection.Open();
            var command = new NpgsqlCommand($"SELECT * FROM \"Employee\" WHERE id_employee='{id}'", connection);

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
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
            }
            connection.Close();

            return employee;
        }
    }
}
