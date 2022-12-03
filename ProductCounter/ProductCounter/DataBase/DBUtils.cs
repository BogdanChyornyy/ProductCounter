using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySqlConnector;

namespace ProductCounter
{
    class DBUtils
    {
        public static MySqlConnection GetDBConnection()
        {
            string host = "server235.hosting.reg.ru";
            int port = 3306;
            string database = "u1850467_productcounterdb";
            string username = "u1850467_counter";
            string password = "alfa&omega";

            return DBMySQLUtils.GetDBConnection(host, port, database, username, password);
        }

    }
}



