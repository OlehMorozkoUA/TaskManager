using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskMeneger
{
    public class TaskItem
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string Title { get; set; }
        public string Task { get; set; }
        public int PersonId { get; set; }
    }

    public class Tasks
    {
        public List<TaskItem> tasks = new List<TaskItem>();
        SqlConnection connection;

        public Tasks()
        {
            var setting = new ConnectionStringSettings()
            {
                Name = "ConnectionStringForManagerDb",
                ConnectionString = @"Data Source=.\SQLEXPRESS;
                                     Initial Catalog=ManagerDB;
                                     Integrated Security=True;"
            };

            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringForManagerDb"].ConnectionString;
            connection = new SqlConnection(connectionString);

            tasks = GetAllTasks();
        }

        private SqlCommand QueryToSQL(SqlConnection connection, string query)
        {
            connection.Open();
            SqlCommand command = new SqlCommand(query, connection);
            return command;
        }

        private List<TaskItem> GetAllTasks()
        {
            SqlCommand command = QueryToSQL(connection, "SELECT * FROM Tasks WHERE 1=1");
            SqlDataReader data = command.ExecuteReader();

            if (data.HasRows)
            {
                while (data.Read())
                {
                    tasks.Add(new TaskItem() { Id = data.GetInt32(0), DateTime = data.GetDateTime(1), Title = data.GetString(2), Task = data.GetString(3), PersonId = data.GetInt32(4) });
                }

                connection.Close();
                return tasks;
            }
            else
            {
                connection.Close();
                return null;
            }
        }

        public List<TaskItem> GetTasksById(int PersonId)
        {
            List<TaskItem> tasks = new List<TaskItem>();

            foreach(var task in this.tasks)
            {
                if (task.PersonId == PersonId) tasks.Add(task);
            }

            return tasks;
        }

        public void AddTask(TaskItem task)
        {
            tasks.Add(task);

            SqlCommand command = QueryToSQL(connection, $"INSERT INTO Tasks (DateTime, Title, Task, PersonId) VALUES ('{task.DateTime}', '{task.Title}','{task.Task}','{task.PersonId}'");
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
