using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Modelo;

namespace Ecommerce.Repositorio.Contrato
{
    public interface IVentaRepositorio: IGenericoRepositorio<Venta>
    {
        Task<Venta> Registar(Venta modelo);
    }
}