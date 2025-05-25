using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Capa_Entidad
{
    public class Producto
    {
        [Required]
        public string cod_produc { get; set; }
        [Required]
        [RegularExpression("[A-Za-z1-9ñÑáéíóú0 ]+", ErrorMessage = "Error solo se permite el ingreso de letras")]
        public string nom_pro { get; set; }
        public string uni_med { get; set; }
        [Required]
        [Range(0, 5000)]
        public decimal pre_pro { get; set; }
        [Required]
        [Range(0, 5000)]
        public int stk_pro { get; set; }
        public string mat_pro { get; set; }
        public string cod_prov { get; set; }
        public string nom_prov { get; set; }
        public string cod_cate { get; set; }
        public string nom_cate { get; set; }
        
    }
}
