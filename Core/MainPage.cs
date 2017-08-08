using System;
using System.Net.Http;
using System.Threading;
using Core;
using Xamarin.Forms;

namespace App3
{
    public class MainPage : ContentPage
    {
        private IMyApi _api;
        private CancellationTokenSource _cts;

        public MainPage()
        {
            var startButton = new Button
            {
                Text = "Start",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            startButton.Clicked += ButtonStartClick;

            var cancelButton = new Button
            {
                Text = "Cancel",
                HorizontalOptions = LayoutOptions.Center,
                VerticalOptions = LayoutOptions.CenterAndExpand
            };

            cancelButton.Clicked += ButtonCancelClick;

            Content = new StackLayout
            {
                Orientation = StackOrientation.Vertical,
                Children =
                {
                    startButton,
                    cancelButton
                }
            };

            var url = "http://slowwly.robertomurray.co.uk/delay/10000/url/https://requestb.in/1k84ruo1";
            var client = new HttpClient(new DiagnosticsHandler(App.HttpClientHandler)) { BaseAddress = new Uri(url) };
            _api = Refit.RestService.For<IMyApi>(client);
        }

        public async void ButtonStartClick(object sender, EventArgs e)
        {
            try
            {
                _cts = new CancellationTokenSource();
                var user = await _api.GetUserAsync(1, "hello", "world", _cts.Token).ConfigureAwait(false);
            }
            catch(Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public void ButtonCancelClick(object sender, EventArgs e)
        {
            _cts.Cancel();
        }
    }
}