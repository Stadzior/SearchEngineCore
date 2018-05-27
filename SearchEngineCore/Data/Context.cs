using SearchEngineCore.Models;
using System.IO;

namespace SearchEngineCore.Data
{
    /// <summary>
    /// Simple DAL (EF context like but much much simpler)
    /// </summary>
    public class Context
    {
        public string DbName { get; set; } =>;
        public string DbFilePath => $"{Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName}\\{DbName}.db";
        public string ConnectionString => "Data Source=" + DbFilePath + ";Version=3;foreign keys=true";

        public Context(string dbName)
        {
            DbName = dbName;
        }

        public SearchResult[] GetResults(string searchInput)
        {

            //let query = "SELECT Id, Url, PageRank FROM sites;"
            //let command = new SQLiteCommand(query, connection)
            //let reader = command.ExecuteReader();
            //let output = seq { while reader.Read() do yield(reader.["Id"], reader.["Url"], reader.["PageRank"]) }
            //output
        }
    }
}
