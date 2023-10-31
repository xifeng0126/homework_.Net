using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace homework_6
{
    public class DatabaseHelper
    {
        private static readonly string connectionString = "Data Source=Data/StudentManagement.db";

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }

        public static void CreateTables()
        {
            using (SQLiteConnection connection = GetConnection())
            {
                connection.Open();

                // 创建School表
                string createSchoolTable = "CREATE TABLE IF NOT EXISTS School (Id INTEGER PRIMARY KEY, Name TEXT);";
                SQLiteCommand createSchoolCommand = new SQLiteCommand(createSchoolTable, connection);
                createSchoolCommand.ExecuteNonQuery();

                // 创建Class表
                string createClassTable = "CREATE TABLE IF NOT EXISTS Class (Id INTEGER PRIMARY KEY, Name TEXT, SchoolId INTEGER);";
                SQLiteCommand createClassCommand = new SQLiteCommand(createClassTable, connection);
                createClassCommand.ExecuteNonQuery();

                // 创建Student表
                string createStudentTable = "CREATE TABLE IF NOT EXISTS Student (Id INTEGER PRIMARY KEY, Name TEXT, ClassId INTEGER);";
                SQLiteCommand createStudentCommand = new SQLiteCommand(createStudentTable, connection);
                createStudentCommand.ExecuteNonQuery();

                // 创建Log表
                string createLogTable = "CREATE TABLE IF NOT EXISTS Log (Id INTEGER PRIMARY KEY, Action TEXT, Timestamp DATETIME);";
                SQLiteCommand createLogCommand = new SQLiteCommand(createLogTable, connection);
                createLogCommand.ExecuteNonQuery();

                connection.Close();
            }
        }

    }

    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int SchoolId { get; set; }
    }

    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ClassId { get; set; }
    }

    public class Log
    {
        public int Id { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class DataManager
    {
        //学校的增删改查
        public static void AddSchool(School school)
        {
            using (SQLiteConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string insertQuery = "INSERT INTO School (Name) VALUES (@Name)";
                SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@Name", school.Name);
                insertCommand.ExecuteNonQuery();
            }
            LogAction($"Added school: {school.Name}");
        }

        public static void DeleteSchool(int id)
        {
            using (SQLiteConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string deleteQuery = "DELETE FROM School WHERE Id = @Id";
                SQLiteCommand deleteCommand = new SQLiteCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@Id", id);
                deleteCommand.ExecuteNonQuery();
            }
            LogAction($"Deleted school with id: {id}");
        }

        public static List<School> GetSchools()
        {
            List<School> schools = new List<School>();
            using (SQLiteConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM School";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        schools.Add(new School
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString()
                        });
                    }
                }
            }
            LogAction($"Seleted schools ");
            return schools;
        }


        public static void UpdateSchool(School updatedSchool)
        {
            using (SQLiteConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string updateQuery = "UPDATE School SET Name = @Name  WHERE Id = @Id";
                SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@Name", updatedSchool.Name);
                updateCommand.Parameters.AddWithValue("@Id", updatedSchool.Id);
                updateCommand.ExecuteNonQuery();
            }
            LogAction($"Updated school with id: {updatedSchool.Id}");
        }

        //日志
        public static void LogAction(string action)
        {
            using (SQLiteConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string insertQuery = "INSERT INTO Log (Action, Timestamp) VALUES (@Action, @Timestamp)";
                SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@Action", action);
                insertCommand.Parameters.AddWithValue("@Timestamp", DateTime.Now);
                insertCommand.ExecuteNonQuery();
            }
        }

        public static List<Log> GetLogs()
        {
            List<Log> logs = new List<Log>();
            using (SQLiteConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Log";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        logs.Add(new Log
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Action = reader["Action"].ToString(),
                            Timestamp = Convert.ToDateTime(reader["Timestamp"])
                        });
                    }
                }
            }
            return logs;
        }

        //班级的增删查改
        public static void AddClass(Class newClass)
        {
            using (SQLiteConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string insertQuery = "INSERT INTO Class (Name, SchoolId) VALUES (@Name, @SchoolId)";
                SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@Name", newClass.Name);
                insertCommand.Parameters.AddWithValue("@SchoolId", newClass.SchoolId);
                insertCommand.ExecuteNonQuery();
            }
            LogAction($"Added class: {newClass.Name}");
        }

        public static List<Class> GetClasses()
        {
            List<Class> classes = new List<Class>();
            using (SQLiteConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Class";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        classes.Add(new Class
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            SchoolId = Convert.ToInt32(reader["SchoolId"])
                        });
                    }
                }
            }
            LogAction($"Seleted class ");
            return classes;
        }

        public static void DeleteClass(int id)
        {
            using (SQLiteConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string deleteQuery = "DELETE FROM Class WHERE Id = @Id";
                SQLiteCommand deleteCommand = new SQLiteCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@Id", id);
                deleteCommand.ExecuteNonQuery();
            }
            LogAction($"Deleted class with id: {id}");
        }

        public static void UpdateClass(Class updatedClass)
        {
            using (SQLiteConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string updateQuery = "UPDATE Class SET Name = @Name, SchoolId = @SchoolId WHERE Id = @Id";
                SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@Name", updatedClass.Name);
                updateCommand.Parameters.AddWithValue("@SchoolId", updatedClass.SchoolId);
                updateCommand.Parameters.AddWithValue("@Id", updatedClass.Id);
                updateCommand.ExecuteNonQuery();
            }
            LogAction($"Updated class with id: {updatedClass.Id}");
        }
        //班级的增删查改

        public static void AddStudent(Student newStudent)
        {
            using (SQLiteConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string insertQuery = "INSERT INTO Student (Name, ClassId) VALUES (@Name, @ClassId)";
                SQLiteCommand insertCommand = new SQLiteCommand(insertQuery, connection);
                insertCommand.Parameters.AddWithValue("@Name", newStudent.Name);
                insertCommand.Parameters.AddWithValue("@ClassId", newStudent.ClassId);
                insertCommand.ExecuteNonQuery();
            }
            LogAction($"Added student: {newStudent.Name}");
        }

        public static List<Student> GetStudents()
        {
            List<Student> students = new List<Student>();
            using (SQLiteConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Student";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                using (SQLiteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        students.Add(new Student
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Name = reader["Name"].ToString(),
                            ClassId = Convert.ToInt32(reader["ClassId"])
                        });
                    }
                }
            }
            LogAction($"Seleted students ");
            return students;
        }

        public static void DeleteStudent(int id)
        {
            using (SQLiteConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string deleteQuery = "DELETE FROM Student WHERE Id = @Id";
                SQLiteCommand deleteCommand = new SQLiteCommand(deleteQuery, connection);
                deleteCommand.Parameters.AddWithValue("@Id", id);
                deleteCommand.ExecuteNonQuery();
            }
            LogAction($"Deleted student with id: {id}");
        }

        public static void UpdateStudent(Student updatedStudent)
        {
            using (SQLiteConnection connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string updateQuery = "UPDATE Student SET Name = @Name, ClassId = @ClassId WHERE Id = @Id";
                SQLiteCommand updateCommand = new SQLiteCommand(updateQuery, connection);
                updateCommand.Parameters.AddWithValue("@Name", updatedStudent.Name);
                updateCommand.Parameters.AddWithValue("@ClassId", updatedStudent.ClassId);
                updateCommand.Parameters.AddWithValue("@Id", updatedStudent.Id);
                updateCommand.ExecuteNonQuery();
            }
            LogAction($"Updated student with id: {updatedStudent.Id}");
        }
        //学生的增删查改
    }

}
