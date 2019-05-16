using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IndaneApi.Models
{
    public class DeliveryPersonDetail
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MailId { get; set; }
        public string MobileNo { get; set; }
        public double Charges { get; set; }

        //public ICollection<Delivery> Deliveries { get; set; }
        public ICollection<Full> Fulls { get; set; }
        public virtual ICollection<Empty> Empties { get; set; }
    }
}
