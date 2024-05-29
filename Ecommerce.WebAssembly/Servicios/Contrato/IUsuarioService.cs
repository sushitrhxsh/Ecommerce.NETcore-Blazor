using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ecommerce.DTO;

namespace Ecommerce.WebAssembly.Servicios.Contrato
{
    public interface IUsuarioService
    {
        Task<ResponseDTO<List<UsuarioDTO>>> Lista(string rol, string buscar);
        Task<ResponseDTO<UsuarioDTO>> Obtener(int id);
        Task<ResponseDTO<SesionDTO>> Autorizacion(LoginDTO modelo);
        Task<ResponseDTO<UsuarioDTO>> Crear(UsuarioDTO modelo);
        Task<ResponseDTO<bool>> Editar(UsuarioDTO modelo);
        Task<ResponseDTO<bool>> Eliminar(int id);
    }
}