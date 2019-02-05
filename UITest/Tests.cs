using System;
using System.Linq;
using NUnit.Framework;
using Xamarin.UITest;
using Xamarin.UITest.Queries;
using Xamarin.UITest.Android;
using Xamarin.UITest.iOS;

namespace UITest
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class Tests
    {
        IApp appI;
        IApp appA = ConfigureApp
                .Android
                .ApkFile("/path/to/android.apk")
                .StartApp();
        Platform platform;
        private String path = "com.xamarin.XamStore.apk";
        private String apiKey = "";

        public Tests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            appA = ConfigureApp.Android.ApkFile(path).StartApp();
            
            //appi = 
            //app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void WelcomeTextIsDisplayed()
        {
            //AppResult[] results = app.WaitForElement(c => c.Marked("Welcome to Xamarin.Forms!"));
            //app.Screenshot("Welcome screen.");

           // Assert.IsTrue(results.Any());
        }
    }
}
