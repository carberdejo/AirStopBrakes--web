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
    public class DistritoDAO
    {
        public List<Distrito> listaDistrito()
        {
            var lista = new List<Distrito>();
            DataTable dt = DBHelper.RetornaDataTable("SP_LISTA_DISTRITOS");
            string cad_json = JsonConvert.SerializeObject(dt);
            lista = JsonConvert.DeserializeObject<List<Distrito>>(cad_json);
            return lista;
        }
    }
}
