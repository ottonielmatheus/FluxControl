using EmurbBUSControl.Models.BusinessRule;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmurbBUSControl.Models.DataModels
{
    public class FlowRecordDAO : Database
    {
        public bool Register(string licensePlate)
        {
            Bus busRegistered = null;

            using (BusDAO busDAO = new BusDAO())
                busRegistered = busDAO.Get(licensePlate);

            if (busRegistered != null)
                return this.Add
                (
                    new FlowRecord()
                    {
                        RegistryClerk = 
                        BusRegistered = busRegistered.Id,

                    }
                );
            

            return false;
        }

        public int Register(int busNumber)
        {
            Bus busRegistered = null;

            using (BusDAO busDAO = new BusDAO())
                busRegistered = busDAO.Get(busNumber);

            if (busRegistered != null)
                return this.Add
                (
                    new FlowRecord()
                    {

                    }
                );


            return false;
        }

        #region CRUD

        public int Add(FlowRecord model)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = @"INSERT INTO FlowRecords ()
                                VALUES ()";

            cmd.Parameters.AddWithValue("", model.RegistryClerk);
            cmd.Parameters.AddWithValue("", model.BusRegistered);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Change(int id, FlowRecord model)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = @"UPDATE FlowRecords 
                                SET 
                                WHERE ";

            cmd.Parameters.AddWithValue("", id);
            cmd.Parameters.AddWithValue("", model.RegistryClerk);
            cmd.Parameters.AddWithValue("", model.BusRegistered);

            return cmd.ExecuteNonQuery() > 0;
        }

        public FlowRecord Get(int id)
        {
            SqlCommand cmd = new SqlCommand();
            
            cmd.Connection = connection;
            cmd.CommandText = @"SELECT * FROM FlowRecords 
                                WHERE ";

            cmd.Parameters.AddWithValue("", id);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
                return new FlowRecord()
                {
                
                };

            return null;
        }

        public List<FlowRecord> Load()
        {
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = @"SELECT * FROM FlowRecords";

            List<FlowRecord> flowRecords = new List<FlowRecord>();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
                flowRecords.Add
                (   
                    new FlowRecord()
                    {

                    }
                );

            return flowRecords;
        }

        public bool Remove(int id)
        {
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = @"DELETE FROM FlowRecords 
                                WHERE ";

            cmd.Parameters.AddWithValue("", id);

            return cmd.ExecuteNonQuery() > 0;
        }

        #endregion
    }
}
