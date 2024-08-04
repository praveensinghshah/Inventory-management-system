using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMS.Models
{
    public class ManagerDashboardViewModel
    {
        public List<InventoryTransaction> RecentTransactions { get; set; }
        public List<Order> SupplierOrders { get; set; }
    }
}