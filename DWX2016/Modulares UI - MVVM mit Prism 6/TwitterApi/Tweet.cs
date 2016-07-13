namespace TwitterApi
{
    internal class Tweet : ITweet
    {
        public ulong Id { get; set; }
        public string Text { get; set; }
        public string ScreenName { get; set; }
    }
}