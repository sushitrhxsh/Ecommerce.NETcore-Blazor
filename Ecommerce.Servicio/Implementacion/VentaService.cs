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
    public class VentaService:IVentaService
    {
        
        private readonly IVentaRepositorio _ventaRepositorio;
        private readonly IMapper _mapper;
        public VentaService(IVentaRepositorio ventaRepositorio, IMapper mapper)
        {
            _ventaRepositorio = ventaRepositorio;
            _mapper = mapper;
        }

        public async Task<VentaDTO> Registrar(VentaDTO modelo)
        {
            try
            {
                var dbModelo = _mapper.Map<Venta>(modelo);
                var ventaGenerada = await _ventaRepositorio.Registrar(dbModelo);

                if (ventaGenerada.IdVenta == 0) {
                    throw new TaskCanceledException("No pudo registrar");
                } 

                return _mapper.Map<VentaDTO>(ventaGenerada);

            } catch(Exception ex) {
                throw ex;
            }
        }
    }
}