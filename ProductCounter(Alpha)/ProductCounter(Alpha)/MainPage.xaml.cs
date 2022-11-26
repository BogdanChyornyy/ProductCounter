using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using ZXing;

namespace ProductCounter_Alpha_
{
    public partial class MainPage : ContentPage
    {
        private string _link;

        public MainPage()
        {
            InitializeComponent();
        }

        private void ZXingScannerViev_OnScanResult(ZXing.Result result)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                scanResultText.Text = Convert.ToString(result);
            });
        }
    }
}

