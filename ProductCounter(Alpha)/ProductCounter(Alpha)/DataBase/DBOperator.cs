using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using MySqlConnector;
using System.Net.NetworkInformation;
using ZXing.QrCode.Internal;
using ZXing;
using Xamarin.Essentials;

namespace ProductCounter_Alpha_
{
    class DBOperator
    {
        private MySqlConnection _conn = DBUtils.GetDBConnection();
        private string _sqlQuery;
        private string[] _extractedPosition = new string[2];
        public string message;

        public string[] QuerySenderCall(string barCode)
        {
            string[] example = QuerySender(barCode);
            return example;
        }

        public string DBConnectCall()
        {
            return DataBaseConnect();
        }


        private string[] QuerySender(string barCode)
        {
            _conn.Open();
            try
            {
                _sqlQuery = $"SELECT position FROM Positions WHERE barcode = {barCode + "    "}";
                MySqlCommand command = new MySqlCommand(_sqlQuery, _conn);
                _extractedPosition[0] = command.ExecuteScalar().ToString();
                
                _sqlQuery = $"SELECT count FROM Positions WHERE barcode = {barCode + "    "}";
                command = new MySqlCommand(_sqlQuery, _conn);
                _extractedPosition[1] = command.ExecuteScalar().ToString();
            }
            catch (NullReferenceException)
            {
                _extractedPosition[0] = "Штрих-код не существует";
            }
            finally
            {
                _conn.Close();
            }
            return _extractedPosition;
        }


        private string DataBaseConnect()
        {
            string excMess = "Соединение установлено!";
            try
            {
                _conn.Open();
                _conn.Close();
            }
            catch (Exception)
            {
                excMess = "Соединение не установлено!";
            }
            finally
            {
                message = excMess;
            }
            return message;
        }
    }
}
