using Android;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Autofac;
using Plugin.CurrentActivity;

[assembly: UsesFeature("android.hardware.camera", Required = false)]
[assembly: UsesFeature("android.hardware.camera.autofocus", Required = false)]
namespace Hermes.Droid
{
    [Activity(Label = "Hermes", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.FormsMaps.Init(this, savedInstanceState);

            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            var width = Resources.DisplayMetrics.WidthPixels;
            var height = Resources.DisplayMetrics.HeightPixels;
            var density = Resources.DisplayMetrics.Density;

            App.ScreenWidth = (width - 0.5f) / density;
            App.ScreenHeight = (height - 0.5f) / density;

            var builder = new ContainerBuilder();
            builder.RegisterModule(new AndroidModule());

            LoadApplication(new App(builder));
            this.RequestPermissions(new[]
            {
                Manifest.Permission.AccessCoarseLocation,
                Manifest.Permission.AccessFineLocation,
                Manifest.Permission.Bluetooth,
                Manifest.Permission.Camera,
                Manifest.Permission.ReadExternalStorage,
                Manifest.Permission.WriteExternalStorage,
            }, 0);
        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
        {
            if (requestCode == 0)
            {
                bool flag = false;
                for (var i = 0; i < permissions.Length; i++)
                {
                    System.Diagnostics.Debug.WriteLine($"OnPermissionsResult({permissions[i]}, {grantResults[i]})");
                    if (grantResults[i] == Permission.Denied)
                    {
                        flag = true;
                    }
                }
                if (flag)
                {
                    System.Diagnostics.Debug.WriteLine("Missing permissions!");
                    new AlertDialog.Builder(this)
                        .SetTitle("Permission Error")
                        .SetMessage("All permissions are required for use.")
                        .SetCancelable(false)
                        .SetPositiveButton("ok", (c, ev) =>
                        {
                            Process.KillProcess(Process.MyPid());
                        })
                        .Show();
                }
            }
        }
    }
}

