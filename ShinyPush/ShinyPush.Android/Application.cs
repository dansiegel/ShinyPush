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

namespace ShinyPush.Droid
{
    [Application]
    public class YourApplication : Shiny.ShinyAndroidApplication<SampleStartup>
    {
        public YourApplication(IntPtr handle, JniHandleOwnership transfer)
            : base(handle, transfer)
        {
        }
    }
}