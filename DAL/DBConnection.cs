using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;

namespace Plytoteka.DAL
{
    class DBConnection
    {
        private MySqlConnectionStringBuilder stringBuilder = new MySqlConnectionStringBuilder();

        private static DBConnection instance = null;
        public static DBConnection Instance
        {
            get
            {
                if (instance == null) instance = new DBConnection();
                return instance;
            }
        }

        public MySqlConnection Connection => new MySqlConnection(stringBuilder.ToString());

        private DBConnection()
        {
            stringBuilder.Server = Properties.Settings.Default.server;
            stringBuilder.Database = Properties.Settings.Default.database;
            stringBuilder.UserID = Properties.Settings.Default.userID;
            stringBuilder.Password = Properties.Settings.Default.password;
            stringBuilder.Port = Properties.Settings.Default.port;
        }
    }
}
