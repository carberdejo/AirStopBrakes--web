using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class Usuario
    {
        
        public string cod_usu { get; set; }
        
        //[StringLength(50, ErrorMessage = "El nombre no puede exceder los 50 caracteres.")]
        //[RegularExpression("[A-Za-z1-9ñÑáéíóú0 ]+", ErrorMessage = "Error solo se permite el ingreso de letras")]

        public string nom_usu { get; set; }
        //[RegularExpression("[A-Za-z1-9ñÑáéíóú0 ]+", ErrorMessage = "Error solo se permite el ingreso de letras")]

        public string ape_usu { get; set; }
        //[RegularExpression("^9[0-9]{8}$", ErrorMessage = "El celular debe tener 9 dígitos y comenzar con 9.")]

        public string tel_usu { get; set; }
        [DataType(DataType.EmailAddress)]
        public string cor_usu { get; set; }
        public string pass_usu { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime fec_nac { get; set; }
        public string cod_dist { get; set; }
        public string cod_rol { get; set; }

    }
}
