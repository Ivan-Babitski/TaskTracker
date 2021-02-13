using Microsoft.Data.SqlClient;
using System;
using System.Data;

namespace TaskTracker.IdentityServer.Data
{
    public class TaskContext : IDisposable
    {
        public SqlConnection connection{ get; set; }
        public SqlTransaction transaction{ get; set; }
        public TaskContext(string connectionString)
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
            transaction = connection.BeginTransaction();
        }

        public SqlCommand CreateCommand()
        {
            var command = connection.CreateCommand();
            command.Transaction = transaction;
            return command;
        }

        public void SaveChanges()
        {
            if (transaction == null)
            {
                throw new InvalidOperationException();
            }
            transaction.Commit();
            transaction = null;
        }

        public void StatusSupport()
        {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        public void Dispose()
        {
            if (transaction != null)
            {
                transaction.Rollback();
                transaction = null;
            }
            if (connection != null)
            {
                connection.Close();
                connection = null;
            }
        }
    }
}