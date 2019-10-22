using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmurbBUSControl.Models.DataModels.BusinessRule
{
    public enum UserType
    {
        Transparency = 0,
        Operator = 1,
        Boss = 2,
        Manager = 3,
        Adminstrator = 4
    }

    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public UserType Type { get; set; }
        public string Email { get; set; }
        public int Registration { get; set; }
        public string Password { get; set; }
    }
}
