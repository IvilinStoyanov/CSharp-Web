using System;

namespace Pepper.Web.ViewModels.Orders
{
    public class OrderDto
    {
        public int OrderId { get; set; }

        public string CustomerName { get; set; }

        public string ProductName { get; set; }

        public DateTime OrderedOn { get; set; }
    }
}