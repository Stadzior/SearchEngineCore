using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SearchEngineCore.Models
{
    public class RawSearchResult
    {
        public string Url { get; set; }
        public double PageRank { get; set; }
        public string Word { get; set; }
        public long WordCount { get; set; }
        public bool FoundMatchInUrl { get; set; }
    }
}
