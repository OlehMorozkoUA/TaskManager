using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace TaskMeneger
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Status { get; set; }
        public string Image { get; set; }

        public string Password { get; set; }

        public Person()
        {

        }
        public Person(int id,string name, string email, string phone, string status, string image, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            Status = status;
            Image = image;
            Password = password;
        }

        public override string ToString()
        {
            return $"Name = {Name}, Email = {Email}, Phone = {Phone}, Status = {Status}, Image = {Image}";
        }
    }

    public class ListPerson
    {
        List<Person> people = new List<Person>();
        SqlConnection connection;

        public ListPerson()
        {
            var setting = new ConnectionStringSettings()
            {
                Name = "ConnectionStringForManagerDb",
                ConnectionString = @"Data Source=.\SQLEXPRESS;
                                     Initial Catalog=ManagerDB;
                                     Integrated Security=True;"
            };

            Configuration config;
            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.ConnectionStrings.ConnectionStrings.Add(setting);

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringForManagerDb"].ConnectionString;
            connection = new SqlConnection(connectionString);

            config.Save();

            ConnectionStringsSection section = config.GetSection("connectionStrings") as ConnectionStringsSection;
            section.SectionInformation.ProtectSection("DataProtectionConfigurationProvider");
            config.Save();

            people = GetAllPeople();
        }

        private SqlCommand QueryToSQL(SqlConnection connection, string query)
        {
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            return command;
        }


        public List<Person> GetAllPeople()
        {
            SqlCommand command = QueryToSQL(connection, "SELECT * FROM Person WHERE 1=1");
            SqlDataReader data = command.ExecuteReader();

            if (data.HasRows)
            {
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        people.Add(new Person(data.GetInt32(0), data.GetString(1), data.GetString(2), data.GetString(3), data.GetString(4), data.GetString(5), data.GetString(6)));
                    }
                }

                connection.Close();
                return people;
            }
            else
            {
                connection.Close();
                return null;
            }
        }

        public void AddPerson(Person person)
        {
            people.Add(person);

            SqlCommand command = QueryToSQL(connection, $"INSERT INTO Person (Name, Email, Phone, Status, Image, Password) VALUES ('{person.Name}', '','','User','','{person.Password}')");
            command.ExecuteNonQuery();
            connection.Close();
        }

        public void UpdateById(int id, Person newPerson)
        {
            Person person = FindById(id);
            person = newPerson;

            SqlCommand command = QueryToSQL(connection, $"UPDATE Person SET [Name] = '{person.Name}', [Email] ='{person.Email}', [Phone] = '{person.Phone}' WHERE Id = {person.Id}");
            command.ExecuteNonQuery();
            connection.Close();
        }

        public Person FindById(int id)
        {
            for (int i = 0; i < people.Count; i++)
            {
                if (id == people[i].Id) return people[i];
            }
            return null;
        }

        public void AddImageForPerson(int id, string image)
        {
            FindById(id).Image = image;

            SqlCommand command = QueryToSQL(connection, $"UPDATE Person SET [Image] = '{image}' WHERE Id = {id}");
            command.ExecuteNonQuery();
            connection.Close();
        }

        public bool Login(string name, string password)
        {
            for (int i = 0; i < people.Count; i++)
            {
                if (name == people[i].Name && password == people[i].Password) return true;
            }
            return false;
        }

        public bool IsExist(string name)
        {
            for (int i = 0; i < people.Count; i++)
            {
                if (name == people[i].Name) return true;
            }
            return false;
        }

        public Person FindPerson(string name, string password)
        {
            for (int i = 0; i < people.Count; i++)
            {
                if (name == people[i].Name && password == people[i].Password) return people[i];
            }
            return null;
        }
    }
}
