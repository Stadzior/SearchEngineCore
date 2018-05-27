namespace SearchEngineCore.Models
{
    public class SearchResult
    {
        public string Url { get; set; }
        public double PageRank { get; set; }
        public double Rating { get; set; }
        public string[] MatchedWords { get; set; }
        public bool FoundMatchInUrl { get; set; }
    }
}
