using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Entidad
{
    public class Carrito
    {
        public string cod_produc { get; set; }
        public string nom_pro { get; set; }
        public decimal precio { get; set; }
        public int cantidad { get; set; }
        public decimal importe
        {
            get
            {
                return precio * cantidad;
            }
        }        
    }
}
