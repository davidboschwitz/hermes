using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace GMapTesting.Droid
{
    class SampleActivityMetaData
    {
        public SampleActivityMetaData(int titleResourceId, int descriptionId, Type activityToLaunch)
        {
            ActivityToLaunch = activityToLaunch;
            TitleResource = titleResourceId;
            DescriptionResource = descriptionId;
        }

        public Type ActivityToLaunch { get; }
        public int DescriptionResource { get; }
        public int TitleResource { get; }

        public void Start(Activity context)
        {
            var i = new Intent(context, ActivityToLaunch);
            context.StartActivity(i);
        }
    }
}