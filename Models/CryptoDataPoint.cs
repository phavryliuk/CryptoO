using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CryptoO.Models
{
    public class CryptoDataPoint
    {
        public DateTime Timestamp { get; set; }
        public double Price { get; set; }

        public CryptoDataPoint(DateTime timestamp, double price)
        {
            Timestamp = timestamp;
            Price = price;
        }
    }

}
