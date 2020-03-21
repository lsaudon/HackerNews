using System;
using System.Collections.Generic;

namespace HackerNews.MobileApp.Models
{
    public class Story : IItem
    {
        public string By { get; set; }
        public long Descendants { get; set; }
        public long Id { get; set; }
        public List<long> Kids { get; set; }
        public long Score { get; set; }
        public string Text { get; set; }
        public long Time { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public Uri Url { get; set; }
    }
}
