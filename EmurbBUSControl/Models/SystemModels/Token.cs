﻿using EmurbBUSControl.Models.DataModels.BusinessRule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmurbBUSControl.Models.SystemModels
{
    public class Token
    {
        public int Id { get; set; }
        public string Hash { get; set; }
        public User User { get; set; }

        public Token(User user)
        {
            this.Hash = Guid.NewGuid().ToString();
            this.User = user;
        }
    }
}