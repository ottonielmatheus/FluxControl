using EmurbBUSControl.Models.DataModels.BusinessRule;
using EmurbBUSControl.Models.SystemModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace EmurbBUSControl.Models.DataModels
{
    public class TokenDAO : Database, ICrudDAO<Token>
    {
        public bool Add(Token model)
        {
            var cmd = new SqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = @"INSERT INTO Tokens (Hash, User_Id)
                                VALUES (@Hash, @UserId)";

            cmd.Parameters.AddWithValue("@Hash", model.Hash);
            cmd.Parameters.AddWithValue("@UserId", model.User.Id);

            return cmd.ExecuteNonQuery() > 0;
        }

        public bool Change(int id, Token model)
        {
            throw new NotImplementedException();
        }

        public Token Get(int id)
        {
            throw new NotImplementedException();
        }

        public Token GetByHash(string hash)
        {
            var cmd = new SqlCommand();
            Token model = null;

            cmd.Connection = connection;
            cmd.CommandText = @"SELECT t.*, u.*, u.id as id_User
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
                        Id = (int)reader["id_User"],
                        Name = (string)reader["Name"],
                        Registration = (int)reader["Registration"],
                        Email = (string)reader["Email"],
                        Type = (UserType)(short)reader["Type"]
                    };

                    model = new Token(user)
                    {
                        Id = (int)reader["Id"],
                        Hash = (string)reader["Hash"]
                    };
                }
                
            return model;
        }

        public List<Token> Load()
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            var cmd = new SqlCommand();

            cmd.Connection = connection;
            cmd.CommandText = @"DELETE Tokens
                                WHERE Id = @Id";

            cmd.Parameters.AddWithValue("@Id", id);

            return cmd.ExecuteNonQuery() > 0;
        }
    }
}
