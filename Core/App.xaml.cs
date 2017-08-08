using System.Net.Http;
using Xamarin.Forms;

namespace App3
{
    public partial class App : Application
    {
        public static HttpClientHandler HttpClientHandler { get; set; } = new HttpClientHandler();

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
