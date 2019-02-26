using SQLite;
using System;
using System.IO;

namespace Hermes.Database
{
    public class DatabaseController : SQLiteConnection
    {
        private static readonly string sqliteFilename = "hermes.db3";

#if __IOS__
        // we need to put in /Library/ on iOS5.1 to meet Apple's iCloud terms
        // (they don't want non-user-generated data in Documents)
        private static readonly string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
        private static readonly string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder instead
#else
        // Android
        // Just use whatever directory SpecialFolder.Personal returns
        private static readonly string libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
#endif
        private static readonly string path = Path.Combine(libraryPath, sqliteFilename);

        public DatabaseController() : base(path)
        {
        }
    }
}
