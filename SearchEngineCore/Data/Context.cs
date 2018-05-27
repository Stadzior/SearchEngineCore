using Microsoft.Data.Sqlite;
using SearchEngineCore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace SearchEngineCore.Data
{
    /// <summary>
    /// Simple DAL (EF context like but much much simpler)
    /// </summary>
    public class Context : IDisposable
    {
        public string DbName { get; set; }
        public string DbFilePath => $"{Directory.GetCurrentDirectory()}\\{DbName}.db";
        public string ConnectionString => "Data Source=" + DbFilePath + ";";
        private readonly SqliteConnection _connection;

        public Context(string dbName)
        {
            DbName = dbName;
            _connection = new SqliteConnection(ConnectionString);
            _connection.Open();
        }

        public IEnumerable<RawSearchResult> GetResults(string[] searchTokens)
        {
            var query = "select s.Url," +
                " s.PageRank," +
                " w.Word," +
                " w.WordCount,";
            query = $"{query} (case when";
            foreach (var token in searchTokens)
                query = $"{query} s.Url like '%{token}%' or";
            query = query.Substring(0, query.Length - 3) + " then 1 else 0 end) as FoundMatchInUrl " +
            "from (select Word, WordCount, SiteId from words " +
            "where word in(";
            foreach (var token in searchTokens)
                query = $"{query}'{token}', ";
            query = query.Substring(0, query.Length - 2) + ")) w inner join sites s on s.Id = SiteId;";

            var command = new SqliteCommand(query, _connection);
            var reader = command.ExecuteReader();
            var rawOutput = new List<RawSearchResult>();
            while (reader.Read())
                rawOutput.Add(new RawSearchResult
                {
                    Url = (string)reader[0],
                    PageRank = (double)reader[1],
                    Word = (string)reader[2],
                    WordCount = (long)reader[3],
                    FoundMatchInUrl = ((long)reader[4]) > 0
                });
            return rawOutput;
        }

        public void Dispose()
        {
            _connection.Close();
            _connection.Dispose();
        }
    }
}
