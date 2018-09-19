using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;

namespace W9OAuthLab.Droid
{
    [Activity(Label = "CustomUrlSchemeInterceptorActivity", NoHistory = true, LaunchMode = LaunchMode.SingleTop)]
    [IntentFilter(
        new[] { Intent.ActionView },
        Categories = new[] { Intent.CategoryDefault, Intent.CategoryBrowsable },
        DataSchemes = new[] { "com.googleusercontent.apps.991601995491-lk0istorugma3ohah557k1hq44ucjvnm" },
        DataPath = "/oauth2redirect")]
    public class CustomUrlSchemeInterceptorActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Convert Android.Net.Url to Uri
            var uri = new Uri(Intent.Data.ToString());

            // Loading the  redirect Url page
            AuthenState.Authenticator.OnPageLoading(uri);

            Finish();
        }
    }
}
