using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
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

            connectionMessage.Text = dBOperator.DBConnectCall();
            conMessFrame.BackgroundColor = Color.ForestGreen;
        }

        private void ZXingScannerViev_OnScanResult(ZXing.Result result)
        {
            Flashlight.TurnOnAsync();
            DBOperator dBOperator = new DBOperator();
            Device.BeginInvokeOnMainThread(() =>
            {
                string[] QuaryOperResult = dBOperator.QuerySenderCall(Convert.ToString(result));
                quaryResult.Text = Convert.ToString(QuaryOperResult[0]);
                remind.Text = Convert.ToString(QuaryOperResult[1]);
            });
        }

        public void Message(string position)
        {
            Alerts(position);
        }


        public async void Alerts(string message)
        {
            await DisplayAlert("Topic", message, "OK");
        }

        //private async void Button_Clicked(object sender, EventArgs e)
        //{
        //    await Flashlight.TurnOnAsync();
        //}
    }
}

