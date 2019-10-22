using EmurbBUSControl.Models.DataModels.BusinessRule;
using EmurbBUSControl.Models.SystemModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmurbBUSControl.Models.DataModels
{
    public class TokenDAO : Database
    {
        public int Add(Token model)
        {
            var cmd = new SqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = @"INSERT INTO Tokens (Hash, Expires, User_Id)
                                VALUES (@Hash, @Expires, @UserId)
                                SELECT CAST(@@IDENTITY AS INT)";

            cmd.Parameters.AddWithValue("@Hash", model.Hash);
            cmd.Parameters.AddWithValue("@Expires", model.Expires);
            cmd.Parameters.AddWithValue("@UserId", model.User.Id);

            SqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
                return reader.GetInt32(0);

            return 0;
        }

        public Token GetByHash(string hash)
        {
            var cmd = new SqlCommand();
            Token model = null;

            cmd.Connection = connection;
            cmd.CommandText = @"SELECT t.*, u.*
                                FROM Tokens t
                                JOIN Users u
                                ON t.User_Id = u.Id
                                WHERE Hash = @Hash";

            cmd.Parameters.AddWithValue("@Hash", hash);

            using (var reader = cmd.ExecuteReader())
                if (reader.Read())
                {
                    var user = new User()
                    {
                        Id = (int)reader["Id"],
                        Name = (string)reader["Name"],
                        Registration = (int)reader["Registration"],
                        Email = (string)reader["Email"],
                        Type = (UserType)(short)reader["Type"]
                    };

                    model = new Token(user)
                    {
                        Code = (int)reader["Code"],
                        Hash = (string)reader["Hash"],
                        Expires = (DateTime)reader["Expires"]
                    };
                }

            if (model != null && model.Expires < DateTime.Now)
            {
                this.Remove(model.Code);
                throw new Exception("Token expirado");
            }

            return model;
        }

        public bool Remove(int code)
        {
            var cmd = new SqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = @"DELETE Tokens
                                WHERE Code = @Code";

            cmd.Parameters.AddWithValue("@Code", code);

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
