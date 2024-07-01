using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Modelo;
using Ecommerce.DTO;
using Ecommerce.Repositorio.Contrato;
using Ecommerce.Servicio.Contrato;
using AutoMapper;

namespace Ecommerce.Servicio.Implementacion
{
    public class ProductoService:IProductoService
    {

        private readonly IGenericoRepositorio<Producto> _productoRepositorio;
        private readonly IMapper _mapper;
        public ProductoService(IGenericoRepositorio<Producto> productoRepositorio, IMapper mapper)
        {
            _productoRepositorio = productoRepositorio;
            _mapper = mapper;
        }

        public async Task<List<ProductoDTO>> Catalogo(string categoria, string buscar)
        {
            try
            {
                var consulta = _productoRepositorio.Consultar(p => 
                    string.Concat(p.Nombre.ToLower()).Contains(buscar.ToLower()) && 
                    p.IdCategoriaNavigation.Nombre.ToLower().Contains(categoria.ToLower())
                );
                List<ProductoDTO> lista =_mapper.Map<List<ProductoDTO>>(await consulta.ToListAsync());
                return lista;

            } catch(Exception ex) {
                throw ex;
            }
        }

        public async Task<ProductoDTO> Crear(ProductoDTO modelo)
        {
            try
            {
                var dbModelo = _mapper.Map<Producto>(modelo);
                var responseModelo = await _productoRepositorio.Crear(dbModelo);

                if (responseModelo.IdProducto != 0) {
                    return _mapper.Map<ProductoDTO>(responseModelo);
                } else {
                    throw new TaskCanceledException("No puede crear");
                }

            } catch(Exception ex) {
                throw ex;
            }
        }

        public async Task<bool> Editar(ProductoDTO modelo)
        {
             try
            {
                var consulta = _productoRepositorio.Consultar(p => p.IdProducto == modelo.IdProducto);
                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                if (fromDbModelo != null) {
                    fromDbModelo.Nombre = modelo.Nombre;
                    fromDbModelo.Descripcion = modelo.Descripcion;
                    fromDbModelo.IdCategoria = modelo.IdCategoria;
                    fromDbModelo.Precio = modelo.Precio;
                    fromDbModelo.Cantidad = modelo.Cantidad;
                    //fromDbModelo.PrecioOferta = modelo.PrecioOferta;
                    fromDbModelo.Imagen = modelo.Imagen;
                    
                    var respuesta = await _productoRepositorio.Editar(fromDbModelo);

                    if (!respuesta) {
                        throw new TaskCanceledException("No se pudo editar");
                    }

                    return respuesta;

                } else {
                    throw new TaskCanceledException("No se encontraron resultados");
                }

            } catch(Exception ex) {
                throw ex;
            }
        }

        public async Task<bool> Eliminar(int id)
        {
            try
            {
                var consulta = _productoRepositorio.Consultar(p => p.IdProducto == id);
                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                if (fromDbModelo != null) {
                    var respuesta = await _productoRepositorio.Eliminar(fromDbModelo);

                    if (!respuesta) {
                        throw new TaskCanceledException("No se pudo eliminar");
                    }

                    return respuesta;

                } else {
                    throw new TaskCanceledException("No se encontraron resultados");
                }

            } catch(Exception ex) {
                throw ex;
            }
        }

        public async Task<List<ProductoDTO>> Lista(string buscar)
        {
            try
            {
                var consulta = _productoRepositorio.Consultar(p =>
                    string.Concat(p.Nombre.ToLower()).Contains(buscar.ToLower())
                );
                consulta = consulta.Include(c => c.IdCategoriaNavigation);

                List<ProductoDTO> lista =_mapper.Map<List<ProductoDTO>>(await consulta.ToListAsync());
                
                return lista;

            } catch(Exception ex) {
                throw ex;
            }
        }

        public async Task<ProductoDTO> Obtener(int id)
        {
            try
            {
                var consulta = _productoRepositorio.Consultar(p => p.IdProducto == id);
                 consulta = consulta.Include(c => c.IdCategoriaNavigation);
                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                if (fromDbModelo != null) {
                    return _mapper.Map<ProductoDTO>(fromDbModelo);
                } else {
                    throw new TaskCanceledException("No se encontraron coincidencias");
                }
                
            } catch(Exception ex) {
                throw ex;
            }
        }

    }
}