using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Capa_Datos;
using Capa_Entidad;

namespace Capa_Bussiness
{
    public class DistritoBussiness
    {
        DistritoDAO dao = new DistritoDAO();
        public List<Distrito> listaDistrito()
        {
            return dao.listaDistrito();
        }
    }
}
