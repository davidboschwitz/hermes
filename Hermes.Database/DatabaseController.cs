using SQLite;
using System;
using System.IO;
using Xamarin.Forms;

namespace Hermes.Database
{
    public class DatabaseController : SQLiteConnection
    {
        public static string DatabaseFilePath
        {
            get
            {
                var filename = "hermes.db3";
                string libraryPath;
                string platform = "none";
                try { platform = Device.RuntimePlatform; } catch (Exception) { }
                switch (platform)
                {
                    case Device.Android:
                        libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                        break;
                    case Device.iOS:
                        // we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
                        // (they don't want non-user-generated data in Documents)
                        var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
                        libraryPath = Path.Combine(documentsPath, "..", "Library");
                        break;
                    case Device.UWP:
                    default:
                        libraryPath = "";
                        break;
                }
                return Path.Combine(libraryPath, filename);
            }
        }

        public DatabaseController() : base(DatabaseFilePath)
        {
        }
    }
}
