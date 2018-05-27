namespace SearchEngineCore.Models
{
    public class SearchResult
    {
        public string Url { get; set; }
        public float PageRank { get; set; }
        public string[] MatchedWords { get; set; }
        public bool FoundMatchInUrl { get; set; }
    }
}
