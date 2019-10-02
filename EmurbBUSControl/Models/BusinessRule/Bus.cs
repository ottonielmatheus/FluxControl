using EmurbBUSControl.Models.DataModels.BusinessRule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmurbBUSControl.Models.BusinessRule
{
    public class Bus
    {
        public int Number { get; set; }
        public Company BusCompany { get; set; }
        public string LicensePlate { get; set; }
    }
}
