using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndaneApi.Models
{
    public class OtherStock
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string Name { get; set; }
     

       
        public DateTime TimeStamp { get; set; }

      
        //public int TotalSale { get; set; }//delete
        //public int TotalProductNo { get; set; }//delete

        //public double Income { get; set; }//delete
        //public double Expense { get; set; }//delete
        //public double Profit { get; set; }//delete



       

    //    public OtherProductLoad OtherProductLoad { get; set; }//delete
    //    public OtherProductSale OtherProductSale { get; set; }//delete
    }
}
