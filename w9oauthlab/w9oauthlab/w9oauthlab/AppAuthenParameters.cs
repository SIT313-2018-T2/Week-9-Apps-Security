namespace W9OAuthLab
{
    public class AppAuthenParameters
    {
        //This AppName is uesd to store and retrive account infromation on the device
        public static string AppName = "W9AuthLab";

        // The Android Client ID from the Google Cloud Platform
        //Copy from your notepad which you have done on the lab task 1
        public static string AndroidClientId = "991601995491-lk0istorugma3ohah557k1hq44ucjvnm.apps.googleusercontent.com";


        // You can read "docs.microsoft.com/en-us/xamarin/xamarin-forms/data-cloud/authentication/oauth" for each parameters' meaning
        public static string Scope = "https://www.googleapis.com/auth/userinfo.email";
        public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth";
        public static string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
        public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";
        //The AndroidRedirectUrl is reversed with AndroidClientId
        //But why ?
        //The details is "Processing the Redirect URL" section on the link "docs.microsoft.com/en-us/xamarin/xamarin-forms/data-cloud/authentication/oauth"
        public static string AndroidRedirectUrl = "com.googleusercontent.apps.991601995491-lk0istorugma3ohah557k1hq44ucjvnm:/oauth2redirect";

    }
}

