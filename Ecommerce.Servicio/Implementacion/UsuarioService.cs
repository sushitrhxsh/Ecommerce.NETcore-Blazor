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
    public class UsuarioService:IUsuarioService
    {
        
        private readonly IGenericoRepositorio<Usuario> _usuarioRepositorio;
        private readonly IMapper _mapper;
        public UsuarioService(IGenericoRepositorio<Usuario> usuarioRepositorio, IMapper mapper)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _mapper = mapper;
        }

        public async Task<SesionDTO> Autorizacion(LoginDTO modelo)
        {
            try
            {
                var consulta = _usuarioRepositorio.Consultar(p => p.Correo == modelo.Correo && p.Clave == modelo.Clave );
                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                if (fromDbModelo != null) {
                    return _mapper.Map<SesionDTO>(fromDbModelo);
                } else {
                    throw new TaskCanceledException("No se encontraron coincidencias");
                }

            } catch(Exception ex) {
                throw ex;
            }
        }

        public async Task<UsuarioDTO> Crear(UsuarioDTO modelo)
        {
            try
            {
                var dbModelo = _mapper.Map<Usuario>(modelo);
                var responseModelo = await _usuarioRepositorio.Crear(dbModelo);

                if (responseModelo.IdUsuario != 0) {
                    return _mapper.Map<UsuarioDTO>(responseModelo);
                } else {
                    throw new TaskCanceledException("No se puede crear, intentar nuevamente.");
                }

            } catch(Exception ex) {
                throw ex;
            }
        }

        public async Task<bool> Editar(UsuarioDTO modelo)
        {
            try
            {
                var consulta = _usuarioRepositorio.Consultar(p => p.IdUsuario == modelo.IdUsuario);
                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                if (fromDbModelo != null) {
                    fromDbModelo.NombreCompleto = modelo.NombreCompleto;
                    fromDbModelo.Correo = modelo.Correo;
                    fromDbModelo.Clave = modelo.Clave;
                    
                    var respuesta = await _usuarioRepositorio.Editar(fromDbModelo);

                    if (!respuesta) {
                        throw new TaskCanceledException("No se pudo editar, intentar nuevamente");
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
                var consulta = _usuarioRepositorio.Consultar(p => p.IdUsuario == id);
                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                if (fromDbModelo != null) {
                    var respuesta = await _usuarioRepositorio.Eliminar(fromDbModelo);

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

        public async Task<List<UsuarioDTO>> Lista(string rol, string buscar)
        {
            try
            {
                var consulta = _usuarioRepositorio.Consultar(p => 
                    p.Rol == rol && 
                    string.Concat(p.NombreCompleto.ToLower(), p.Correo.ToLower()).Contains(buscar.ToLower())
                );
                List<UsuarioDTO> lista =_mapper.Map<List<UsuarioDTO>>(await consulta.ToListAsync());
                return lista;

            } catch(Exception ex) {
                throw ex;
            }
        }

        public async Task<UsuarioDTO> Obtener(int id)
        {
            try
            {
                var consulta = _usuarioRepositorio.Consultar(p => p.IdUsuario == id);
                var fromDbModelo = await consulta.FirstOrDefaultAsync();

                if (fromDbModelo != null) {
                    return _mapper.Map<UsuarioDTO>(fromDbModelo);
                } else {
                    throw new TaskCanceledException("No se encontraron coincidencias");
                }
                
            } catch(Exception ex) {
                throw ex;
            }
        }

    }
}