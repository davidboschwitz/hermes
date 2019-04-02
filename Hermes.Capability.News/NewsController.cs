using Hermes.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Hermes.Capability.News
{
    public class NewsController : INewsController
    {
        private DatabaseController DatabaseController;

        public ObservableCollection<NewsItem> Feed { get; }

        public NewsController(DatabaseController databaseController)
        {
            DatabaseController = databaseController;

            Initialize();

            Feed = new ObservableCollection<NewsItem>();

            foreach(var newsItem in DatabaseController.Table<NewsItem>())
            {
                Feed.Add(newsItem);
            }
        }


        private void Initialize()
        {
            DatabaseController.CreateTable<NewsItem>();
            /*Dummy Data*/
            NewsItem a1 = new NewsItem(
                title: "Flooding Occuring in Ames",
                body: "Overnight rainfall of 3 to 5 inches across Story County flooded numerous roadways and triggered mudslides, with authorities responding to reports of people trapped in stranded vehicles and forecasters issuing an elevated flood warning for the region Wednesday morning.",
                timeStamp: DateTime.Now.AddMinutes(12.35)
            );
            NewsItem a2 = new NewsItem(
                 title: "Red Cross Giving Aid at Memorial Union",
                 body: "The Red Cross is providing shelter, food, health services and emotional support during this challenging situation to those affected, like Rakiea, Jenna and Ollie, whose stories you can read here.  The Red Cross is working around the clock with our partners to get help to where it’s most needed, and we’re reaching more neighborhoods each day.",
                 timeStamp: DateTime.Now
             );

            DatabaseController.Insert(a1);
            DatabaseController.Insert(a2);
        }
    }
}
