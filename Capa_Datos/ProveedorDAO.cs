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
    public class ProveedorDAO
    {
        public List<Proveedor> listaProveedor(string nombre)
        {
            var lista = new List<Proveedor>();
            DataTable dt = DBHelper.RetornaDataTable("SP_LISTA_PROVEEDORES", nombre);
            string cad_json = JsonConvert.SerializeObject(dt);
            lista = JsonConvert.DeserializeObject<List<Proveedor>>(cad_json);
            return lista;
        }
        public string GrabarProveedor(Proveedor obj)
        {
            try
            {
                object dato = DBHelper.RetornaScalar("SP_GRABAR_PROVEEDOR", obj.nom_prov, obj.pais_origen, obj.email, obj.telefono);
                string cod_prov = dato.ToString();
                return $"Proveedor {cod_prov} registrado correctamente";
            }
            catch (Exception e)
            {
                throw new Exception("Error" + e.Message);
            }
        }
        public string UpdateProveedor(Proveedor obj)
        {
            try
            {
                DBHelper.EjecutarSP("SP_UPDATE_PROVEEDOR", obj.cod_prov, obj.nom_prov, obj.pais_origen, obj.email, obj.telefono);
                return "Proveedor actualizado correctamente";
            }
            catch (Exception e)
            {
                throw new Exception("Error" + e.Message);
            }
        }
        public string DeleteProveedor(string id)
        {
            try
            {
                DBHelper.EjecutarSP("SP_DELETE_PROVEEDOR", id);
                return "Proveedor eliminado correctamente";
            }
            catch (Exception e)
            {
                throw new Exception("Error" + e.Message);
            }
        }
    }
}
