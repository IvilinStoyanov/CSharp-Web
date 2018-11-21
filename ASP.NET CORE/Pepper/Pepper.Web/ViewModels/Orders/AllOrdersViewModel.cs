using Pepper.Web.ViewModels.Orders;
using System.Collections.Generic;

namespace Chushka.Web.ViewModels.Orders
{
    public class AllOrdersViewModel
    {
        public AllOrdersViewModel()
        {
            this.Orders = new List<OrderDto>();
        }

        public ICollection<OrderDto> Orders { get; set; }
    }
}