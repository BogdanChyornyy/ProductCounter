using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using testDBAddingToMobile;
using Xamarin.Forms;
using ZXing;
using ZXing.QrCode.Internal;

namespace ProductCounter_Alpha_
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void buttonTry_Clicked(object sender, EventArgs e)
        {
            DBOperator dBOperator = new DBOperator();

            dBOperator.DBConnectCall();
            connectionMessage.Text = Convert.ToString("Connection successful!");
            conMessFrame.BackgroundColor = Color.ForestGreen;
        }

        private void ZXingScannerViev_OnScanResult(ZXing.Result result)
        {
            DBOperator dBOperator = new DBOperator();
            Device.BeginInvokeOnMainThread(() =>
            {
                string QuaryOperResult = dBOperator.QuerySenderCall(Convert.ToString(result));
                quaryResult.Text = Convert.ToString(QuaryOperResult);
            });
        }

        public void Message(string position)
        {
            quaryResult.Text = Convert.ToString(position); 
        }


        public async void Alerts(string message)
        {
            await DisplayAlert("Topic", message, "OK");
        }
    }
}

