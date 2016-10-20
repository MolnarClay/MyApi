using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MyApp
{
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();
		}

        private async void BtnSubmit_OnClicked(object sender, EventArgs e)
        {
            pleaseWait.IsRunning = true;

            try
            {
                using (var client = new HttpClient())
                {
                    var url = "http://efa-net-oct2016-api.azurewebsites.net/token";
                    var un = WebUtility.UrlEncode(tbxUsername.Text.Trim());
                    var pw = WebUtility.UrlEncode(tbxPassword.Text.Trim());
                    var content = new StringContent($"grant_type=password&username={un}&password={pw}");

                    // Get the response.
                    var tokenDetails = await client.PostAsync(url, content);

                    var result = await tokenDetails.Content.ReadAsStringAsync();
                    await DisplayAlert("Result", result, "OK");
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.GetBaseException().Message);
            }
            finally
            {
                pleaseWait.IsRunning = false;
            }
        }
    }
}
