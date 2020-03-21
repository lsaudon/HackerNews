using System.Collections.Generic;

namespace HackerNews.MobileApp.Models
{
    public class Comment : IItem
    {
        public string By { get; set; }
        public long Id { get; set; }
        public List<long> Kids { get; set; }
        public long Parent { get; set; }
        public string Text { get; set; }
        public long Time { get; set; }
        public string Type { get; set; }
    }
}