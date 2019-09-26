using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace EmurbBUSControl.Models.DataModels
{
    public abstract class Database : IDisposable
    {
        protected readonly SqlConnection connection;
        protected readonly string connectionString = @"";

        protected Database()
        {
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public void Dispose()
        {
            connection.Close();
        }

    }
}
