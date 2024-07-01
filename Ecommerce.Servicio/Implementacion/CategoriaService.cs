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
    public class CategoriaService:ICategoriaService
    {

        private readonly IGenericoRepositorio<Categoria> _categoriaRepositorio;
        private readonly IMapper _mapper;
        public CategoriaService(IGenericoRepositorio<Categoria> categoriaRepositorio, IMapper mapper)
        {
            _categoriaRepositorio = categoriaRepositorio;
            _mapper = mapper;
        }

        public async Task<CategoriaDTO> Crear(CategoriaDTO modelo)
        {
            try
            {
                var dbModelo = _mapper.Map<Categoria>(modelo);
                var responseModelo = await _categoriaRepositorio.Crear(dbModelo);

                if (responseModelo.IdCategoria != 0) {
                    return _mapper.Map<CategoriaDTO>(responseModelo);
                } else {
                    throw new TaskCanceledException("No puede crear");
                }

            } catch(Exception ex) {
                throw ex;
            }
        }

        public async Task<bool> Editar(CategoriaDTO modelo)
        {
            try
            {
                var consulta = _categoriaRepositorio.Consultar(p => p.IdCategoria == modelo.IdCategoria);
                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                if (fromDbModelo != null) {
                    fromDbModelo.Nombre = modelo.Nombre;
                    
                    var respuesta = await _categoriaRepositorio.Editar(fromDbModelo);

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
                var consulta = _categoriaRepositorio.Consultar(p => p.IdCategoria == id);
                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                if (fromDbModelo != null) {
                    var respuesta = await _categoriaRepositorio.Eliminar(fromDbModelo);

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

        public async Task<List<CategoriaDTO>> Lista(string buscar)
        {
            try
            {
                var consulta = _categoriaRepositorio.Consultar(p => 
                    p.Nombre!.ToLower().Contains(buscar.ToLower())
                );
                List<CategoriaDTO> lista =_mapper.Map<List<CategoriaDTO>>(await consulta.ToListAsync());
                return lista;

            } catch(Exception ex) {
                throw ex;
            }
        }

        public async Task<CategoriaDTO> Obtener(int id)
        {
            try
            {
                var consulta = _categoriaRepositorio.Consultar(p => p.IdCategoria == id);
                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                if (fromDbModelo != null) {
                    return _mapper.Map<CategoriaDTO>(fromDbModelo);
                } else {
                    throw new TaskCanceledException("No se encontraron coincidencias");
                }
                
            } catch(Exception ex) {
                throw ex;
            }
        }

    }
}