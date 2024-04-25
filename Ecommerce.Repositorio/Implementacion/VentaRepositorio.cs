using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.Modelo;
using Ecommerce.Repositorio.Contrato;
using Ecommerce.Repositorio.DBContext;

namespace Ecommerce.Repositorio.Implementacion
{
    public class VentaRepositorio:GenericoRepositorio<Venta>, IVentaRepositorio
    {

        private readonly DbecommerceContext _dbContext;
        public VentaRepositorio(DbecommerceContext dbContext):base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Venta> Registrar(Venta modelo)
        {
            Venta ventaGenerado = new Venta();
            
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    foreach(DetalleVenta dv in modelo.DetalleVenta)
                    {
                        Producto producto_encontrado = _dbContext.Productos.Where(p => p.IdProducto == dv.IdProducto).First();
                        
                        producto_encontrado.Cantidad = producto_encontrado.Cantidad - dv.Cantidad;
                        _dbContext.Productos.Update(producto_encontrado);
                    }
                    await _dbContext.SaveChangesAsync();

                    await _dbContext.Venta.AddAsync(modelo);
                    await _dbContext.SaveChangesAsync();
                    ventaGenerado = modelo;
                    transaction.Commit();

                } catch {
                    transaction.Rollback();
                    throw;
                }
            }
            
            return ventaGenerado;
        }
        
    }
}