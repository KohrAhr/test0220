using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace OAuthTestForSSO
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Generate Authorize Access Token to authenticate REST Web API.  
            string result = GetAuthorizeToken(txtAddress.Text).Result;

            txtLog.Text += result;

            // Call REST Web API method with authorize access token.  
            string responseObj = GetInfo(txtAddress.Text, result).Result;

            txtLog.Text += responseObj;
        }

        public static async Task<string> GetAuthorizeToken(string address)
        {

            // Initialization.  
            string responseObj = string.Empty;

            // Posting.  
            using (HttpClient client = new HttpClient())
            {
                // Setting Base address.  
                client.BaseAddress = new Uri(address);

                // Setting content type.  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Initialization.  
                HttpResponseMessage response = new HttpResponseMessage();
                List<KeyValuePair<string, string>> allIputParams = new List<KeyValuePair<string, string>>();

                // Convert Request Params to Key Value Pair.  
                allIputParams.Add(new KeyValuePair<string, string>("Authorization", "Basic " + "User name:User Secret" + ""));
                allIputParams.Add(new KeyValuePair<string, string>("grant_type", "client_credentials"));

                // URL Request parameters.  
                HttpContent requestParams = new FormUrlEncodedContent(allIputParams);

                // HTTP POST  
                response = await client.PostAsync("api/Handshake", requestParams).ConfigureAwait(false);

                // Verification  
                if (response.IsSuccessStatusCode)
                {
                    // Reading Response.  
                   responseObj = response.ToString();
                }
            }

            return responseObj;
        }

        public static async Task<string> GetInfo(string address, string authorizeToken)
        {
            // Initialization.  
            string responseObj = string.Empty;

            // HTTP GET.  
            using (HttpClient client = new HttpClient())
            {
                // Initialization
                string authorization = authorizeToken;

                // Setting Authorization
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authorization);

                // Setting Base address
                client.BaseAddress = new Uri(address);

                // Setting content type
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // Initialization
                HttpResponseMessage response = new HttpResponseMessage();

                // HTTP GET  
                response = await client.GetAsync("api/SecureRequest").ConfigureAwait(false);

                // Verification  
                if (response.IsSuccessStatusCode)
                {
                    // Reading Response.  
                    responseObj = response.ToString();
                }
            }

            return responseObj;
        }
    }
}
