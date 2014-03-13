using System.Data.SqlClient;
using System.Linq;
using Dapper;
using HangFire.Storage;

namespace HangFire.SqlServer
{
    internal class SqlStoredSets : IStoredSets
    {
        private readonly SqlConnection _connection;

        public SqlStoredSets(SqlConnection connection)
        {
            _connection = connection;
        }

        public string GetFirstByLowestScore(string key, long fromScore, long toScore)
        {
            return _connection.Query<string>(
                @"select top 1 Value from HangFire.[Set] where [Key] = @key and Score between @from and @to order by Score",
                new { key, from = fromScore, to = toScore })
                .SingleOrDefault();
        }
    }
}