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

        public List<Token> Load()
        {
            throw new NotImplementedException();
        }

        public bool Remove(int id)
        {
            throw new NotImplementedException();
        }
    }
}
