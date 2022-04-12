using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Repository;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.EFHelpers
{
    //Super Thanks to Jon P. Smith for making this class
    internal class SqliteInMemory
    {
        private readonly List<string> _logs = new List<string>();

        public ImmutableList<string> Logs => _logs.ToImmutableList();

        public void ClearLogs() { _logs.Clear(); }

        public ApplicationContext GetContextWithSetup()
        {
            var context = new ApplicationContext(CreateOptions<ApplicationContext>());
            context.Database.EnsureCreated();


            return context;
        }

        public static DbContextOptions<T> CreateOptions<T>() where T : DbContext
        {
            //Thanks to https://www.scottbrady91.com/Entity-Framework/Entity-Framework-Core-In-Memory-Testing
            var connectionStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connectionString = connectionStringBuilder.ToString();
            var connection = new SqliteConnection(connectionString);
            connection.Open();           

            // create in-memory context
            var builder = new DbContextOptionsBuilder<T>();
            builder.UseSqlite(connection).EnableSensitiveDataLogging();

            return builder.Options;
        }
    }
}
