using EmurbBUSControl.Models.BusinessRule;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmurbBUSControl.Models.DataModels
{
    public class BusDAO : Database, ICrudDAO<Bus>
    {

        public Bus Get(string identifier)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = connection;

            cmd.CommandText = @"SELECT * FROM FlowRecords 
                                WHERE LicensePlate = OR BusNumber = ";

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
                return new Bus()
                {

                };

            return null;
        }

        #region CRUD

        public bool Add(Bus model)
        {
            throw new NotImplementedException();
        }

        public bool Change(int id, Bus model)
        {
            throw new NotImplementedException();
        }

        public Bus Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<Bus> Load()
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
