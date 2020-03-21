namespace HackerNews.MobileApp.Models
{
    public class PollOpt : IItem
    {
        public string By { get; set; }
        public long Id { get; set; }
        public long Poll { get; set; }
        public long Score { get; set; }
        public string Text { get; set; }
        public long Time { get; set; }
        public string Type { get; set; }
    }
}
