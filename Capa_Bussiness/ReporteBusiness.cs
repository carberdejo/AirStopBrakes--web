using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Bussiness
{
    public class ReporteBusiness 
    {
        ReporteDAO dao = new ReporteDAO();

        public List<RepVenta> ListUsuVent(int cant)
        {
            return dao.ListUsuVent(cant);
        }

        public List<RepProducto> ListProVent(decimal imp)
        {
            return dao.ListProVent(imp);
        }
    }
}
