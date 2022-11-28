using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MySqlConnector;
//using MySql.Data.MySqlClient;
using System.Net.NetworkInformation;
using ProductCounter_Alpha_.DataBase;
using ZXing.QrCode.Internal;
using ZXing;
using ProductCounter_Alpha_;

namespace testDBAddingToMobile
{
    class DBOperator
    {
        public string QuerySenderCall(string barCode)
        {
            string example = QuerySender(barCode);
            return example;
        }

        public void DBConnectCall()
        {
            DataBaseConnect();
        }


        private string QuerySender(string barCode)
        {
            string sql = $"SELECT position FROM Positions WHERE barcode = {barCode}";

            MySqlConnection conn = DBUtils.GetDBConnection();

            conn.Open();

            MySqlCommand command = new MySqlCommand(sql, conn);

            string name = command.ExecuteScalar().ToString(); //Executor

            conn.Close();

            return name;
        }


        private void DataBaseConnect()
        {
            MySqlConnection conn = DBUtils.GetDBConnection();
            MainPage mainPage = new MainPage();

            try
            {
                conn.Open();
                conn.Close();
            }
            catch (Exception e)
            {
                mainPage.Alerts(e.Message);
            }
        }
    }
}
