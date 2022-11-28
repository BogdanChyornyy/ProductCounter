using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;


//MySql.Data.MySqlClient;

namespace ProductCounter_Alpha_.DataBase
{
    class DBMySQLUtils
    {

        public static MySqlConnection

            GetDBConnection(string host, int port, string database, string username, string password)
        {
            // Connection String.
            String connString = "Server=" + host + ";Database=" + database
                + ";port=" + port + ";User Id=" + username + ";password=" + password + ";charset = utf8";

            MySqlConnection conn = new MySqlConnection(connString);

            return conn;
        }
    }
}


