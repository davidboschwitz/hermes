﻿using System;
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
    class ErrorDialogFragment : DialogFragment
    {
        public ErrorDialogFragment(Dialog dialog)
        {
            Dialog = dialog;
        }

        public new Dialog Dialog { get; }

        public override Dialog OnCreateDialog(Bundle savedInstanceState)
        {
            return Dialog;
        }
    }
}