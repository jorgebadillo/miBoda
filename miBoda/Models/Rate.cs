using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace miBoda.Models
{
    using SQLite.Net.Attributes;

    public class Rate
    {
        [PrimaryKey]
        public int RateId { get; set; }

        public string Code { get; set; }

        public double TaxRate { get; set; }

        public string Name { get; set; }

        public override int GetHashCode()
        {
            return RateId;
        }
    }
}
