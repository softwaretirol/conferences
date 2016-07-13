namespace TwitterApi
{
    public interface ITweet
    {
        ulong Id { get;  }
        string Text { get;  }
        string ScreenName { get;  }
    }
}