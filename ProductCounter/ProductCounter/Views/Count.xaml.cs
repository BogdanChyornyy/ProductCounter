using ProductCounter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZXing;
using ZXing.QrCode.Internal;

namespace ProductCounter.Views
{
    public partial class Count : ContentPage
    {
        private string[] _quaryOperResult = new string[3];
        private string _barCodeScanResult;
        public Count()
        {
            InitializeComponent();
        }
        //private void buttonTry_Clicked(object sender, EventArgs e)
        //{
        //    DBOperator dBOperator = new DBOperator();
        //    int tryToConnect = dBOperator.DBConnectCall();

        //    if (tryToConnect == 1)
        //    {
        //        conMessFrame.BackgroundColor = Color.DarkGreen;
        //    }
        //    else
        //    {
        //        conMessFrame.BackgroundColor = Color.DarkRed;
        //    };
        //}

        private void ZXingScannerViev_OnScanResult(ZXing.Result result)
        {
            DBOperator dBOperator = new DBOperator();
            Device.BeginInvokeOnMainThread(() =>
            {

                _barCodeScanResult = Convert.ToString(result);
                _quaryOperResult = dBOperator.QuerySenderCall(_barCodeScanResult);

                scannerFrame.BackgroundColor = Color.Green;

                quaryResult.Text = Convert.ToString(_quaryOperResult[0]);
                remind.Text = Convert.ToString(_quaryOperResult[1]);
                difference.Text = _quaryOperResult[2];
            });
        }

        private void countBtn_Clicked(object sender, EventArgs e)
        {
            if (quantityFact.Text != null)
            {
                DBOperator dBOperator = new DBOperator();
                Alerts(dBOperator.CounterCall(quantityFact.Text));
                difference.Text = dBOperator.QuerySenderCall(_barCodeScanResult)[2];
            }
            else
            {
                Alerts("Введите количество");
            }
        }

        private void exactlyBtn_Clicked(object sender, EventArgs e)
        {
            exactlyBtn.BackgroundColor = Color.DarkGreen;
            DBOperator dBOperator = new DBOperator();
            Alerts(dBOperator.CounterCall(remind.Text));
            difference.Text = dBOperator.QuerySenderCall(_barCodeScanResult)[2];

        }

        private async void Alerts(string allertMess)
        {
            await DisplayAlert("Предупреждение", "Значение добавлено: " + allertMess, "Принято");
        }
    }
}