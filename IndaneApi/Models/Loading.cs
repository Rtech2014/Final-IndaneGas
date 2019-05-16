using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndaneApi.Models
{
    public class Loading
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string LoadType { get; set; } //checkox with value '1Way' and '2Way'
        public DateTime TimeStamp { get; set; }
        public string LoadCount { get; set; }
        public int Full { get; set; }
        public int Empty { get; set; }//delete
        public double Expense { get; set; }


        //public int StockId { get; set; }//delete
        //public Stock Stock { get; set; }//delete

        public Loading()
        {
            TimeStamp = DateTime.Now;
        }

    }
}
