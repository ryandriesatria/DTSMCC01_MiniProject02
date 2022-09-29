using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiniProject02.ViewModels
{
    public class DetailsEdit
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public DateTime TransactionDate { get; set; }
        public int Quantity { get; set; }
        public int OldQuantity { get; set; }
        public int Total { get; set; }
        public string Kasir { get; set; }
    }
}
