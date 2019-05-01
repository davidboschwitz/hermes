using Hermes.Database;
using System;

namespace Hermes.Capability.News
{
    public class NewsItem : DatabaseItem
    {
        public string Title { get; set; }
        public string Body { get; set; }

        public NewsItem()
        {
            //for sqlite generation
        }

        public NewsItem(string title, string body, DateTime timeStamp) : base(Guid.NewGuid(), Guid.NewGuid(), new Guid(), DateTime.Now, DateTime.Now, Capability.Namespace, Capability.MessageNames.NewsItem)
        {
            //Add SenderID to constructor to avoid duplicate items
            Title = title;
            Body = body;
            CreatedTimestamp = timeStamp;
        }

        public override string ToString()
        {
            return $"{base.ToString()} {CreatedTimestamp} {Title} {Body}";
        }

    }
}
