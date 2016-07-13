using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LinqToTwitter;

namespace TwitterApi
{
    public class DwxTwitterApi
    {
        public async Task<IEnumerable<ITweet>> GetDwxTweets(string searchTerm)
        {
            var auth = new ApplicationOnlyAuthorizer
            {
                CredentialStore = new InMemoryCredentialStore
                {
                    ConsumerKey = "LzyD9KdaBGaVOGv7rA6WlMqlC",
                    ConsumerSecret = "nzqWvutFZTaQAWBoMlmvmfWHzQxw7RqWuYtON9YwqBKrH78aPn"
                }
            };

            await auth.AuthorizeAsync();
            using (var ctx = new TwitterContext(auth))
            {
                var searchResults =
                    (from search in ctx.Search
                        where search.Type == SearchType.Search &&
                              search.Query == searchTerm
                        select search).SingleOrDefault();
                return searchResults.Statuses.Select(x => new Tweet
                {
                    Id = x.ID,
                    Text = x.Text,
                    ScreenName = x.ScreenName
                }).ToList();
            }
        }
    }
}