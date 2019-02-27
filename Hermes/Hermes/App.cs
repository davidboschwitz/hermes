using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Hermes
{
    public class App : Application // superclass new in 1.3
    {
        public static double ScreenWidth;
        public static double ScreenHeight;

        public App()
        {
            MainPage = new MapPage();
        }

        public void SetWidth(double w)
        {
            ScreenWidth = w;
        }

        public void SetHeight(double h)
        {
            ScreenHeight = h;
        }

        public double GetWidth()
        {
            return ScreenWidth;
        }

        public double GetHeight()
        {
            return ScreenHeight;
        }
    }
}


