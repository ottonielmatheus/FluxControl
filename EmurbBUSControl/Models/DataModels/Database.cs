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
        protected readonly string connectionString = @"Server=LAPTOP-1I08H1PB\SQLEXPRESS;Database=EmurbFluxControl;Integrated Security=SSPI;";

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
