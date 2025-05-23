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
    public class ProveedorBussiness
    {
        ProveedorDAO dao = new ProveedorDAO();
        public List<Proveedor> listaProveedor()
        {
            return dao.listaProveedor();
        }
        public string GrabarProveedor(Proveedor obj)
        {
            try
            {
                return dao.GrabarProveedor(obj);
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
                return dao.UpdateProveedor(obj);
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
                return dao.DeleteProveedor(id);
            }
            catch (Exception e)
            {
                throw new Exception("Error" + e.Message);
            }
        }
    }
}
