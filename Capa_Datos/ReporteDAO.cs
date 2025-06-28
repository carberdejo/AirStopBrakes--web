using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Entidad;
using Newtonsoft.Json;

namespace Capa_Datos
{
    public class ReporteDAO
    {
        public List<RepVenta> ListUsuVent(int cant)
        {
            var lista = new List<RepVenta>();
            DataTable dt = DBHelper.RetornaDataTable("SP_REPORTE_VENTA_PRODUCTO",cant);
            string cad_json = JsonConvert.SerializeObject(dt);
            lista = JsonConvert.DeserializeObject<List<RepVenta>>(cad_json);
            return lista;
        }

        public List<RepProducto> ListProVent(decimal imp)
        {
            var lista = new List<RepProducto>();
            DataTable dt = DBHelper.RetornaDataTable("SP_REPORTE_PRODUCTO", imp);
            string cad_json = JsonConvert.SerializeObject(dt);
            lista = JsonConvert.DeserializeObject<List<RepProducto>>(cad_json);
            return lista;
        }
    }
}
