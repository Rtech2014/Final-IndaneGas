using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndaneApi.Models
{
    public class Stock
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public string Name { get; set; }
        //public int Full { get; set; }//delete

        //public int Empty { get; set; }//delete
        //public int NewConnection { get; set; }//delete

        public DateTime TimeStamp { get; set; }

        //public int TotalFull { get; set; }//delete
        //public int TotalEmpty { get; set; }//delete
        //public int TotalCylinder { get; set; }//delete

        //public double Income { get; set; }//delete
        //public double Expense { get; set; }//delete
        //public double Profit { get; set; }//delete



      

        //public Loading Loading { get; set; }//delete
        //public Delivery Delivery { get; set; }//delete


    }
}
