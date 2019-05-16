using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IndaneApi.Models
{
    public class Empty
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int DeliveryPersonId { get; set; }
        public DeliveryPersonDetail DeliveryPersonDetail { get; set; }


        public DateTime TimeStamp { get; set; }
        public int In_No { get; set; }
        public double EmptyNo { get; set; }
        public double NewConnection { get; set; }
        public double EmptyPending { get; set; }
        public double ReturnedFull { get; set; }
        public double CashToBeRecevied { get; set; }
        public double CashRecevied { get; set; }

        [Display(Name = "Balance")]
        public double ReceviedBalance { get; set; }

        public int FullId { get; set; }
        public Full Full { get; set; }

        public Empty()
        {
            TimeStamp = DateTime.Now;
        }


    }
}
