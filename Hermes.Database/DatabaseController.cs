using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;

namespace Hermes.Database
{
    public class DatabaseController : SQLiteConnection
    {
        public static string DatabaseFilePath
        {
            get
            {
                var filename = "hermes.2.db3";
                string libraryPath;
                string platform = "none";
                try { platform = Device.RuntimePlatform; } catch (InvalidOperationException) { }
                switch (platform)
                {
                    case Device.Android:
                        libraryPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);//Xamarin.Forms.Application.Current.Properties["ExternalStorageDirectory"].ToString();
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
            CreateTable<HermesSavedProperty>();
            properties = new Dictionary<string, string>();
            foreach (var p in Table<HermesSavedProperty>())
            {
                properties.Add(p.Key, p.Value);
            }
        }

        private Dictionary<string, string> properties;

        public Guid PersonalID()
        {
            var property = GetProperty("PersonalID");
            if (property == null)
            {
                SetProperty("PersonalID", Guid.NewGuid().ToString());
                property = GetProperty("PersonalID");
            };
            return Guid.Parse(property);
        }

        public string GetProperty(string key)
        {
            properties.TryGetValue(key, out var property);
            return property;
        }

        public bool SetProperty(string key, string value)
        {
            properties[key] = value;
            var property = new HermesSavedProperty(key, value);
            var rowsAffected = Update(property);
            if (rowsAffected == 0)
            {
                // The item does not exists in the database so lets insert it
                rowsAffected = Insert(property);
            }
            return rowsAffected > 0;
        }

        class HermesSavedProperty
        {
            public HermesSavedProperty(string key, string value)
            {
                Key = key;
                Value = value;
            }

            public HermesSavedProperty() { }

            [PrimaryKey, Unique]
            public string Key { get; set; }
            public string Value { get; set; }
        }
    }
}
