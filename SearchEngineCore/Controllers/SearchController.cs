using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SearchEngineCore.Data;
using SearchEngineCore.Models;

namespace SearchEngineCore.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        [Route("[action]")]
        public IEnumerable<SearchResult> GetResults(string searchInput)
        {
            if (searchInput == null)
                return new List<SearchResult>();

            var searchTokens = searchInput.Split(' ');
            using (var context = new Context("MyIndexedWebDb"))
            {
                var rawOutput = context.GetResults(searchTokens);
               
                var output = new List<SearchResult>();
                foreach (var url in rawOutput.Select(x => x.Url).Distinct())
                {
                    var firstUrlRawOutput = rawOutput.First(x => x.Url.Equals(url));
                    var urlPageRank = firstUrlRawOutput.PageRank;
                    var urlFoundMatchInUrl = firstUrlRawOutput.FoundMatchInUrl;
                    var urlWords = rawOutput.Where(x => x.Url.Equals(url)).Select(x => x.Word);
                    var urlRating = rawOutput.Sum(x => x.WordCount) * 0.33f + urlPageRank * 0.33f + (urlFoundMatchInUrl ? 0.33f : 0.0f); 
                    output.Add(new SearchResult
                    {
                        Url = url,
                        Rating = urlRating,
                        PageRank = urlPageRank,
                        FoundMatchInUrl = urlFoundMatchInUrl,
                        MatchedWords = urlWords.ToArray()
                    });
                }
                return output.OrderByDescending(x => x.Rating).ToArray();
            }
        }
    }
}
