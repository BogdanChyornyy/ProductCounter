using ProductCounter;
using ProductCounter.DataBase;
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
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Date : ContentPage
    {
        private string[] _quaryOperResult = new string[2];
        private string _barCodeScanResult;
        ExpirationDates expirationDates = new ExpirationDates();
        public Date()
        {
            InitializeComponent();
        }
        private void ZXingScannerViev_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(() =>
            {

                _barCodeScanResult = Convert.ToString(result);
                _quaryOperResult = expirationDates.ExpirationDatesUpdator(_barCodeScanResult);

                quaryResult.Text = Convert.ToString(_quaryOperResult[0]);
            });
        }

        

        private void acceptDateBtn_Clicked(object sender, EventArgs e)
        {
            string expDateConv = expDate.Date.ToString("yyyy-MM-dd");
            expirationDates.PusherCall(_barCodeScanResult, _quaryOperResult, expDateConv);
            DisplayAlert("Уведомление", $"Позиция зафиксирована: {_quaryOperResult[0]}\r\nCо сроком до: {expDateConv}", "OK");
        }
    }
}