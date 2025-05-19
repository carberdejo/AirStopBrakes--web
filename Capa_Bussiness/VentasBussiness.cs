using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Bussiness
{
    public class VentasBussiness
    {
        VentasDAO dao = new VentasDAO();
        public string GrabarVenta(string cod_usu, decimal total_vent)
        {
            try
            {
                return dao.GrabarVenta(cod_usu, total_vent);
            }
            catch (Exception e)
            {
                throw new Exception("Error" + e.Message);
            }
        }

        public void GrabarDatalleVenta(string num_vta, Carrito obj)
        {
            try
            {
                dao.GrabarDatalleVenta(num_vta, obj);
            }
            catch (Exception e)
            {
                throw new Exception("Error" + e.Message);
            }
        }
    }
}
