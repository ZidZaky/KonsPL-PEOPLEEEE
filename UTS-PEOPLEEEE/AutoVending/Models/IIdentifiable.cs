using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoVending.Models
{
    public interface IIdentifiable
    {
        string Id { get; }
    }

    public class Item : IIdentifiable
    {
        public string Id { get; set; }
        public string Nama { get; set; }
        public decimal Harga { get; set; }
    }

}
