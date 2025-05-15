using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVending.Models
{
    public class Transaction
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public double TotalPrice { get; set; }
        public DateTime Timestamp { get; set; }
    }

}
