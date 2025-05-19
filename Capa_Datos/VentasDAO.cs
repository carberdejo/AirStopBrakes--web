using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
namespace Capa_Datos
{
    public class VentasDAO
    {
        public string GrabarVenta(string cod_usu,decimal total_vent)
        {
            try
            {
                string codigo = Convert.ToString(DBHelper.EjecutarSP_TRX_Object("PA_GRABAR_VENTA_IGV", cod_usu, total_vent));
                return codigo;
            }
            catch(Exception e) {
                throw new Exception("Error" + e.Message);
            }
        }

        public void GrabarDatalleVenta(string num_vta,Carrito obj)
        {
            try
            {
                DBHelper.EjecutarSP_TRX("PA_GRABAR_VENTAS_DETALLE", num_vta, obj.cod_produc,obj.precio,obj.cantidad);
            }
            catch (Exception e)
            {
                throw new Exception("Error" + e.Message);
            }
        }
    }
}
