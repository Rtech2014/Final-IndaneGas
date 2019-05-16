using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndaneApi.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double CostPrice { get; set; }
        public double SellingPrice { get; set; }
        public double Weight { get; set; }
        public DateTime TimeStamp { get; set; }

      
        public ICollection< Stock> Stocks { get; set; }
        public ICollection<Loading> Loadings { get; set; }
        public ICollection<Full> Fulls{ get; set; }
        public ICollection<Empty> Empties { get; set; }
        public ICollection<OtherProductSale> OtherProductSales { get; set; }
        public ICollection<OtherProductLoad> OtherProductLoads { get; set; }
        public ICollection<OtherStock> OtherStocks { get; set; }


    }
}
