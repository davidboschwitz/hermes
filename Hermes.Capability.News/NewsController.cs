﻿using Hermes.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Hermes.Capability.News
{
    public class NewsController : INewsController
    {
        private DatabaseController DatabaseController;

        public event Action<Type, DatabaseItem> SendMessage;

        private ObservableCollection<NewsItem> feed;
        public ObservableCollection<NewsItem> Feed { get { return feed; } set { SetProperty(ref feed, value); } }

        public NewsController(DatabaseController databaseController)
        {
            DatabaseController = databaseController;

            Initialize();

            Feed = new ObservableCollection<NewsItem>();

            foreach(var newsItem in DatabaseController.Table<NewsItem>())
            {
                Feed.Add(newsItem);
                Debug.WriteLine($"NewsController.Feed.Add({newsItem.Title},{newsItem.Body})");
            }
        }
        
        public void InsertReport(string title, string body) {
            NewsItem report = new NewsItem(title: title, body:body,timeStamp: DateTime.Now);
            if (DatabaseController.Insert(report) != 0)
            {
                Feed.Add(report);
            }

            Feed = new ObservableCollection<NewsItem>(Feed.OrderByDescending(o => o.UpdatedTimestamp));
        }


        private void Initialize()
        {
            DatabaseController.CreateTable<NewsItem>();
            /*Dummy Data*/
            /*NewsItem a1 = new NewsItem(
                title: "Flooding Occuring in Ames",
                body: "Overnight rainfall of 3 to 5 inches across Story County flooded numerous roadways and triggered mudslides, with authorities responding to reports of people trapped in stranded vehicles and forecasters issuing an elevated flood warning for the region Wednesday morning.",
                timeStamp: DateTime.Now.AddMinutes(12.35)
            );
            NewsItem a2 = new NewsItem(
                 title: "Red Cross Giving Aid at Memorial Union",
                 body: "The Red Cross is providing shelter, food, health services and emotional support during this challenging situation to those affected, like Rakiea, Jenna and Ollie, whose stories you can read here.  The Red Cross is working around the clock with our partners to get help to where it’s most needed, and we’re reaching more neighborhoods each day.",
                 timeStamp: DateTime.Now
             );
            //Check for existing items
            DatabaseController.Insert(a1);
            DatabaseController.Insert(a2);*/
        }

        public void OnNotification(string messageNamespace, string messageName, Guid messageID)
        {
            throw new NotImplementedException();
        }

        #region INotifyPropertyChanged
        protected bool SetProperty<T>(ref T backingStore, T value,
            [CallerMemberName]string propertyName = "",
            Action onChanged = null)
        {
            if (EqualityComparer<T>.Default.Equals(backingStore, value))
                return false;

            backingStore = value;
            onChanged?.Invoke();
            OnPropertyChanged(propertyName);
            return true;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
