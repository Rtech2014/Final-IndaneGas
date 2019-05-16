using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndaneApi.Models
{
    public class OtherProductSale
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string Description { get; set; }
      
        public DateTime TimeStamp { get; set; }
        //public string DeliveryPersonName { get; set; }
        public int SaleCount { get; set; }

        public double Income { get; set; }

        public double Profit { get; set; }

        //public string TakeNo { get; set; }



        //public int OtherstockId { get; set; }//delete
        //public OtherStock OtherStock { get; set; }//delete

        public OtherProductSale()
        {
            TimeStamp = DateTime.Now;
        }
    }
}
