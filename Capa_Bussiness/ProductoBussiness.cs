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
    public class ProductoBussiness
    {
        ProductosDAO dao = new ProductosDAO();

        public List<Producto> listaProducto(string nombre)
        {
            return dao.listaProducto(nombre);
        }
        public List<Producto> listaProductoCate(string cod_cate, string nombre)
        {
            return dao.listaProductoCate(cod_cate, nombre);
        }

        public string GrabarProducto(Producto obj)
        {
            try
            {
                return dao.GrabarProducto(obj);
            }
            catch (Exception e)
            {
                throw new Exception("Error" + e.Message);
            }
        }

        public string UpdateProducto(Producto obj)
        {
            try
            {
                return dao.UpdateProducto(obj);
            }
            catch (Exception e)
            {
                throw new Exception("Error" + e.Message);
            }
        }

        public string DeleteProducto(string id)
        {
            try
            {
                return dao.DeleteProducto(id);
            }
            catch (Exception e)
            {
                throw new Exception("Error" + e.Message);
            }
        }


        public List<Categoria> listaCategoria()
        {
            return dao.listaCategoria();
        }

        public string UpdateStockProducto(string cod_produc, int cantidad)
        {
            try
            {
                return dao.UpdateStockProducto(cod_produc, cantidad);
            }
            catch (Exception e)
            {
                throw new Exception("Error" + e.Message);
            }
        }

    }
}
