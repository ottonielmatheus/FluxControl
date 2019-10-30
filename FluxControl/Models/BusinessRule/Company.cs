﻿using EmurbBUSControl.Models.BusinessRule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmurbBUSControl.Models.DataModels.BusinessRule
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Thumbnail { get; set; }
        public short InvoiceInterval { get; set; }
        public List<Bus> Fleet { get; set; }
    }
}