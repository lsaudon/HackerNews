namespace HackerNews.MobileApp.Models
{
    public class Job : IItem
    {
        public string By { get; set; }
        public long Id { get; set; }
        public long Score { get; set; }
        public string Text { get; set; }
        public long Time { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Url { get; set; }
    }
}
