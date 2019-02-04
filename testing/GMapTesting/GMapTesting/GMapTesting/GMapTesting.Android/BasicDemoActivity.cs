using Android.App;
using Android.OS;
using Android.Support.V7.App;

namespace GMapTesting.Droid
{
    class BasicDemoActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.BasicDemo);
        }
    }
}