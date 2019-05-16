using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndaneApi.Models
{
    public class OtherProductLoad
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string Description { get; set; }
        public DateTime TimeStamp { get; set; }
        public string LoadNO { get; set; }
        public int ProductCount { get; set; }

        public double Expense { get; set; }



        //    public int OtherstockId { get; set; }//delete
        //    public OtherStock OtherStock { get; set; }//delete

        public OtherProductLoad()
        {
            TimeStamp = DateTime.Now;
        }
    }
}
