using System;

namespace Pepper.Models
{
    public class Order
    {
        public int Id { get; set; }

        public virtual Product Product { get; set; }
        public int ProductId { get; set; }

        public PepperUser Client { get; set; }

        public DateTime OrderedOn { get; set; }
    }
}
