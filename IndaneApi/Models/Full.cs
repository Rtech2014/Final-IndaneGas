using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IndaneApi.Models
{
    public class Full
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int DeliveryPersonId { get; set; }
        public DeliveryPersonDetail DeliveryPersonDetail { get; set; }

        public double FullCount { get; set; }
      
        public DateTime TimeStamp { get; set; }

        [Display(Name="No of Takes")]
        public int Out_No { get; set; }
        public double CashToBeRecevied { get; set; }

        public Empty Empty { get; set; }


        public Full()
        {
            TimeStamp = DateTime.Now;
        }

       
    }
}
