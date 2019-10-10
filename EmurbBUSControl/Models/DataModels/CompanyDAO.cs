using EmurbBUSControl.Models.BusinessRule;
using EmurbBUSControl.Models.DataModels.BusinessRule;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmurbBUSControl.Models.DataModels
{
    public class CompanyDAO : Database //, ICrudDAO<Company>
    {
        public bool Add(Company model)
        {
            var cmd = new SqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = @"INSERT INTO Companies (Name, Thumbnail, InvoiceInterval) 
                                VALUES (@Name, @Thumbnail, @InvoiceInterval)";

            cmd.Parameters.AddWithValue("@Name", model.Name);
            cmd.Parameters.AddWithValue("@Thumbnail", model.Thumbnail);
            cmd.Parameters.AddWithValue("@InvoiceInterval", model.InvoiceInterval);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Change(int id, Company model)
        {
            var cmd = new SqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = @"UPDATE Companies
                                SET Name = @Name, Thumbnail = @Registration, Invoice_Interval = @InvoiceInterval
                                WHERE Id = @Id";

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Name", model.Name);
            cmd.Parameters.AddWithValue("@Thumbnail", model.Thumbnail);
            cmd.Parameters.AddWithValue("@InvoiceInterval", model.InvoiceInterval);

            return cmd.ExecuteNonQuery() > 0;
        }

        public Company Get(int id)
        {
            var cmd = new SqlCommand();
            Company model = null;

            cmd.Connection = connection;
            cmd.CommandText = @"SELECT company.*, bus.Id Bus_Id, bus.Number, bus.LicensePlate, bus.Company_Id
                                FROM Companies company
                                JOIN Buses bus ON bus.Company_Id = company.Id
                                WHERE company.Id = @Id";

            cmd.Parameters.AddWithValue("@Id", id);

            using (var reader = cmd.ExecuteReader())
                if (reader.Read())
                {
                    model = new Company()
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Thumbnail = (string)reader["Thumbnail"],
                        InvoiceInterval = (short)reader["Invoice_Interval"],
                        Fleet = new List<Bus>()
                    };

                    while (reader.Read())
                        model.Fleet.Add(
                        new Bus()
                        {
                            Id = (int)reader["Bus_Id"],
                            Number = (int)reader["Number"],
                            LicensePlate = (string)reader["LicensePlate"],
                            BusCompany = (int)reader["Company_Id"]

                        });
                }
                    

            return model;
        }

        public List<Company> Load()
        {
            var cmd = new SqlCommand();
            List<Company> models = new List<Company>();

            cmd.Connection = connection;
            cmd.CommandText = @"SELECT company.*, bus.Id Bus_Id, bus.Number, bus.LicensePlate, bus.Company_Id
                                FROM Companies company
                                JOIN Buses bus ON bus.Company_Id = company.Id
                                ORDER BY Company_Id";

            using (var reader = cmd.ExecuteReader())
            {
                Company model = null;

                while (reader.Read())
                {

                    if (model == null || model.Fleet[model.Fleet.Count - 1].BusCompany != (int)reader["Company_Id"])
                    {
                        if (model != null)
                            models.Add(model);

                        model = new Company()
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Thumbnail = (string)reader["Thumbnail"],
                            InvoiceInterval = (short)reader["Invoice_Interval"],
                            Fleet = new List<Bus>()
                        };
                    }

                    model.Fleet.Add(
                    new Bus()
                    {
                        Id = (int)reader["Bus_Id"],
                        Number = (int)reader["Number"],
                        LicensePlate = (string)reader["LicensePlate"],
                        BusCompany = (int)reader["Company_Id"]

                    });
                }

                models.Add(model);
            }

            return models;
        }

        public bool Remove(int id)
        {
            var cmd = new SqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = "DELETE FROM Companies WHERE Id = @Id";

            cmd.Parameters.AddWithValue("@Id", id);

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
