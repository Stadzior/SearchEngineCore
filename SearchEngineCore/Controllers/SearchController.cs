using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SearchEngineCore.Models;

namespace SearchEngineCore.Controllers
{
    [Route("api/[controller]")]
    public class SearchController : Controller
    {
        [HttpGet("[action]")]
        public IEnumerable<SearchResult> GetResults()
        {
            using (var context = new Context("MyIndexedWebDb"))
            {
                return new List<SearchResult>
                {
                    new SearchResult
                    {
                        Url = "http://hue.pl",
                        PageRank = 0.1f,
                        MatchedWords = new string[] { "abc", "bca" }
                    },
                    new SearchResult
                    {
                        Url = "http://hue.com",
                        PageRank = 0.5f,
                        MatchedWords = new string[] { "cba", "bac" }
                    }
                };
            };
        }
    }
}
