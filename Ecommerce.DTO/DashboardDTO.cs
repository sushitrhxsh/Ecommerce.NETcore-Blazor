using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class DashboardDTO
    {
        public string? TotalIngresos { get; set; }
        public int TotalVentas { get; set; }
        public int TotalCliente { get; set; }
        public int TotalProductos { get; set; }
    }
}