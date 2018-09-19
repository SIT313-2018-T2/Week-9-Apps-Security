using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.Linq;
using Xamarin.Auth;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace W9OAuthLab
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OAUthLoginPage : ContentPage
    {
        Account account;
        AccountStore store;

        public OAUthLoginPage()
        {
            InitializeComponent();
            store = AccountStore.Create();
            account = store.FindAccountsForService(AppAuthenParameters.AppName).FirstOrDefault();
        }
        void OnLoginClicked(object sender, EventArgs e)
        {
            string clientId = AppAuthenParameters.AndroidClientId;
            string redirectUri = AppAuthenParameters.AndroidRedirectUrl;


            var authenticator = new OAuth2Authenticator(
                clientId,
                null,
                AppAuthenParameters.Scope,
                new Uri(AppAuthenParameters.AuthorizeUrl),
                new Uri(redirectUri),
                new Uri(AppAuthenParameters.AccessTokenUrl),
                null,
                true);

            AuthenState.Authenticator = authenticator;
            authenticator.Completed += OnAuthCompleted;
            authenticator.Error += OnAuthError;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);
        }

        async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            UserInfo user = null;
            if (e.IsAuthenticated)
            {
                //if the authentication is successful.
                // Sending a request to identity  provider (Google) for retrieving the basic user data 
                // UserInfoUrl has been defined in the "AppAuthenParameters" class
                var request = new OAuth2Request("GET", new Uri(AppAuthenParameters.UserInfoUrl), null, e.Account);
                var response = await request.GetResponseAsync();
                if (response != null)
                {
                    // Deserialize the data and store it in the account store
                    string userJson = await response.GetResponseTextAsync();
                    user = JsonConvert.DeserializeObject<UserInfo>(userJson);
                }

                //Due to we use the FindAccountsForService class, we will delete the saved account
                if (account != null)
                {
                    store.Delete(account, AppAuthenParameters.AppName);
                }
                //Using Async method to save the new account
                await store.SaveAsync(account = e.Account, AppAuthenParameters.AppName);
                //Presents an alert dialog to the application user with a single cancel button
                await DisplayAlert("Email address", user.Email, "OK");
            }
        }

        void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            Debug.WriteLine("Authentication error: " + e.Message);
        }
    }
}