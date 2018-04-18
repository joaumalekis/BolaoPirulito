using System;
using Acr.UserDialogs;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Firebase;
using Firebase.Auth;

namespace BolaoPirulito.Droid
{
    [Activity(Label = "BolaoPirulito", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);

            var options = new FirebaseOptions.Builder()
                .SetApplicationId("1:529070199774:android:89baf742f294fc49")
                .SetApiKey("AIzaSyD2jiFIazrni9vUYdTDs3yliR30rLZCpX4")
                .Build();

            UserDialogs.Init(this);
            FirebaseApp.InitializeApp(Application.Context, options);

            LoadApplication(new App());
        }
    }
}

