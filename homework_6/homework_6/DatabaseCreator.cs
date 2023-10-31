using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace homework_6
{
    public class DatabaseCreator
    {
        public static void CreateDatabase()
        {
            string databaseFilePath = "Data/StudentManagement.db";

            if (!System.IO.File.Exists(databaseFilePath))
            {
                SQLiteConnection.CreateFile(databaseFilePath);
                DatabaseHelper.CreateTables();
            }
        }
    }
}
