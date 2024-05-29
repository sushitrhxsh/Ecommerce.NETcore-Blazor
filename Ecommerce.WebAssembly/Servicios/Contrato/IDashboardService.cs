using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTO;

namespace Ecommerce.WebAssembly.Servicios.Contrato
{
    public interface IDashboardService
    {
        Task<ResponseDTO<DashboardDTO>> Resumen();
    }
}