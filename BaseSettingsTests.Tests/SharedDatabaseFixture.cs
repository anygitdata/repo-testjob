using Microsoft.EntityFrameworkCore;
using System;
using System.Data.Common;
using Microsoft.Data.SqlClient;
using TestJob.Models;

namespace BaseSettingsTests.Tests
{
    public class SharedDatabaseFixture : IDisposable
    {
        private static readonly object _lock = new object();
        //private static bool _databaseInitialized;

        public SharedDatabaseFixture()
        {
            Connection = new SqlConnection(@"Server=(localdb)\MSSQLLocalDB;Database=TestJob;Trusted_Connection=True;");

            Seed();
            Connection.Open();
        }

        public DbConnection Connection { get; }


        public DataContext CreateContext(DbTransaction transaction = null)
        {
            var context = new DataContext(new DbContextOptionsBuilder<DataContext>().UseSqlServer(Connection).Options);

            if (transaction != null)
            {
                context.Database.UseTransaction(transaction);
            }

            return context;
        }

        private void Seed()
        {
            lock (_lock)
            {
                //if (!_databaseInitialized)
                //{
                //    using (var context = CreateContext())
                //    {
                //        context.Database.EnsureDeleted();
                //        context.Database.EnsureCreated();

                //        var one = new Item("ItemOne");
                //        one.AddTag("Tag11");
                //        one.AddTag("Tag12");
                //        one.AddTag("Tag13");

                //        var two = new Item("ItemTwo");

                //        var three = new Item("ItemThree");
                //        three.AddTag("Tag31");
                //        three.AddTag("Tag31");
                //        three.AddTag("Tag31");
                //        three.AddTag("Tag32");
                //        three.AddTag("Tag32");

                //        context.AddRange(one, two, three);

                //        context.SaveChanges();
                //    }

                //    _databaseInitialized = true;
                //}
            }
        }

        public void Dispose() => Connection.Dispose();
    }
}
