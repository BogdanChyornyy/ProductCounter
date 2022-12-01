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
        private string _globalSqlQuery;
        private string _difference;
        private static string _barCodeCurrent;
        private string[] _extractedPosition = new string[3];
        public int message;

        public string[] QuerySenderCall(string barCode)
        {
            _barCodeCurrent = barCode;

            string[] example = QuerySender(barCode);
            return example;
        }

        public int DBConnectCall()
        {
            return DataBaseConnect();
        }

        public string CounterCall(string quantityFact)
        {
            return Convert.ToString(Counter(quantityFact));
        }

        private string[] QuerySender(string barCode)
        {
            _conn.Open();
            try
            {
                _globalSqlQuery = $"SELECT position FROM Positions WHERE barcode = {barCode + "    "}";
                MySqlCommand command = new MySqlCommand(_globalSqlQuery, _conn);
                _extractedPosition[0] = command.ExecuteScalar().ToString();

                _globalSqlQuery = $"SELECT count FROM Positions WHERE barcode = {barCode + "    "}";
                command = new MySqlCommand(_globalSqlQuery, _conn);
                _extractedPosition[1] = command.ExecuteScalar().ToString();

                _globalSqlQuery = $"SELECT difference FROM Positions WHERE barcode = {barCode + "    "}";
                command = new MySqlCommand(_globalSqlQuery, _conn);
                _extractedPosition[2] = command.ExecuteScalar().ToString();
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

        private int DataBaseConnect()
        {
            message = 1;
            try
            {
                _conn.Open();
                _conn.Close();
            }
            catch (Exception)
            {
                message = 0;
            }
            
            return message;
        }

        private string Counter(string quantityFact)
        {
            try
            {
                _conn.Open();

                _globalSqlQuery = $"SELECT Fact FROM Positions WHERE barcode = {_barCodeCurrent + "    "}";
                MySqlCommand command = new MySqlCommand(_globalSqlQuery, _conn);
                int factValue = Convert.ToInt32(command.ExecuteScalar());


                int summ = Convert.ToInt32(quantityFact) + factValue;
                string convertedSumm = Convert.ToString(summ);

                _globalSqlQuery = $"UPDATE Positions SET Fact = {convertedSumm} WHERE BarCode = {_barCodeCurrent}";
                command = new MySqlCommand(_globalSqlQuery, _conn);
                command.ExecuteScalar();

                _globalSqlQuery = $"SELECT Difference FROM Positions WHERE barcode = {_barCodeCurrent + "    "}";
                command = new MySqlCommand(_globalSqlQuery, _conn);
                int difference = Convert.ToInt32(command.ExecuteScalar());

                if (difference == 0)
                {
                    _difference = "РАСХОЖДЕНИЙ НЕТ";
                }
                else
                {
                    _difference = Convert.ToString(difference);
                }
            }
            catch(Exception e)
            {
                _difference = Convert.ToString(e);
            }
            finally
            {
                _conn.Close();
            }

            return _difference;
        }
    }    
}
