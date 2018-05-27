using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace SearchEngineCore.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        [HttpGet("[action]")]
        public IEnumerable<SearchResult> GetResults()
        {
            return new List<SearchResult>
            {
                new SearchResult
                {
                    Url = "hue.pl",
                    PageRank = 0.1f,
                    MatchedWords = new (string, int)[] { ("abc",2), ("bca", 3) }
                },
                new SearchResult
                {
                    Url = "hue.com",
                    PageRank = 0.5f,
                    MatchedWords = new (string, int)[] { ("cba",4), ("bac", 5) }
                }
            };
            //return Enumerable.Range(1, 5).Select(index => new SearchResult
            //{
            //    DateFormatted = DateTime.Now.AddDays(index).ToString("d"),
            //    TemperatureC = rng.Next(-20, 55),
            //});
        }

        public class SearchResult
        {
            public string Url { get; set; }
            public float PageRank { get; set; }
            public (string, int)[] MatchedWords { get; set; }
            public bool FoundMatchInUrl { get; set; }
        }
    }
}
