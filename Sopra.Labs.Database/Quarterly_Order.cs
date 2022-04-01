using System;
using System.Collections.Generic;

#nullable disable

namespace Sopra.Labs.Database
{
    public partial class Quarterly_Order
    {
        public string CustomerID { get; set; }
        public string CompanyName { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
