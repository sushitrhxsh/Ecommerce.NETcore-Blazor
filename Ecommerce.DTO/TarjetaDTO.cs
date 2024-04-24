using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.DTO
{
    public class TarjetaDTO
    {
        [Required(ErrorMessage = "Ingrese Titular")]
        public string? Titular { get; set; }
        [Required(ErrorMessage = "Ingrese numero de tarjeta")]
        public string? Numero { get; set; }
        [Required(ErrorMessage = "Ingrese la vigencia")]
        public string? Vigencia { get; set; }
        [Required(ErrorMessage = "Ingrese el CVV")]
        public string? CVV { get; set; }
    }
}