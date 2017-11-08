using Android.App;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Locations;
using Android.Util;
using Android.Content;

namespace appLocation
{
    [Activity(Label = "appLocation", MainLauncher = true)]
    //Implement ILocationListener interface to get location updates
    public class MainActivity : Activity, ILocationListener
    {
        LocationManager locMgr;
        string tag = "MainActivity";
        Button button;
        TextView latitude;
        TextView longitude;
        TextView provider;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main);
            button = FindViewById<Button>(Resource.Id.myButton);
            latitude = FindViewById<TextView>(Resource.Id.latitude);
            longitude = FindViewById<TextView>(Resource.Id.longitude);
            provider = FindViewById<TextView>(Resource.Id.provider);
        }

        protected override void OnStart()
        {
            base.OnStart();
        }

        protected override void OnResume()
        {
            base.OnResume();

            //initialize location manager
            locMgr = GetSystemService(Context.LocationService) as LocationManager;

            button.Click += delegate
            {
                button.Text = "Locatioin Service Running";

                //GPS provider
                string Provider = LocationManager.GpsProvider;
                if (locMgr.IsProviderEnabled(Provider))
                {
                    locMgr.RequestLocationUpdates(Provider,2000,1,this);
                }
                else
                {
                    Log.Info(tag, Provider + " is not available. Does the device have location services enabled?");
                }
            };
        }

        protected override void OnPause()
        {
            base.OnPause();

            //RemoveUpdates takes a pending intent - here, we pass the current Activity
            locMgr.RemoveUpdates(this);
        }

        protected override void OnStop()
        {
            base.OnStop();
        }

        public void OnLocationChanged(Location location)
        {
            latitude.Text = "Latitude: " + location.Latitude.ToString();
            longitude.Text = "Longitude: " + location.Longitude.ToString();
            provider.Text = "Provider: " + location.Provider.ToString();
        }

        public void OnProviderDisabled(string provider)
        {
            throw new System.NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new System.NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            throw new System.NotImplementedException();
        }
    }
}

