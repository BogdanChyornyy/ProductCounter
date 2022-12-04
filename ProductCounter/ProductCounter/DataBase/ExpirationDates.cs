using MySqlConnector;
using ProductCounter.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ProductCounter.DataBase
{
    internal class ExpirationDates:DBOperator
    {
        private MySqlConnection _conn = DBUtils.GetDBConnection();
        private string[] _positionsArray = new string[2];
        private string _globalSqlQuery;
        public string[] ExpirationDatesUpdator(string barCode)
        {
            PositionExtractor(barCode);

            return _positionsArray;
        }
        public void PusherCall(string barcode, string[] valuesDB, string date)
        {
            DatePusher(barcode,valuesDB, date);
        }
        private void PositionExtractor(string barCode)
        {
            string[] arrayFilter = QuerySenderCall(barCode);
            _positionsArray[0] = arrayFilter[0];
            _positionsArray[1] = arrayFilter[1];
        }

        private void DatePusher(string barcode, string[] valuesDB, string date)
        {
            _conn.Open();
            _globalSqlQuery = $"INSERT INTO ExpirationDates (BarCode,Position,Count,DateOfExpiration) VALUES ({barcode},'{valuesDB[0]}',{valuesDB[1]},'{date}')";
            MySqlCommand command = new MySqlCommand(_globalSqlQuery, _conn);
            command.ExecuteScalar();
            _conn.Close();
        }
    }
}
