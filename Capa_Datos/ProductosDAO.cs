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
    public class ProductosDAO
    {
        public List<Proveedor> listaProveedor()
        {
            var lista = new List<Proveedor>();
            DataTable dt = DBHelper.RetornaDataTable("SP_LISTA_PROVEEDOR");
            string cad_json = JsonConvert.SerializeObject(dt);
            lista = JsonConvert.DeserializeObject<List<Proveedor>>(cad_json);

            return lista;
        }

        public List<Categoria> listaCategoria()
        {
            var lista = new List<Categoria>();
            DataTable dt = DBHelper.RetornaDataTable("SP_LISTA_CATEGORIA");
            string cad_json = JsonConvert.SerializeObject(dt);
            lista = JsonConvert.DeserializeObject<List<Categoria>>(cad_json);

            return lista;
        }

        public List<Producto> listaProducto( string nombre )
        {
            var lista = new List<Producto>();
            DataTable dt = DBHelper.RetornaDataTable("SP_LISTA_PRODUCTOS",nombre);
            string cad_json= JsonConvert.SerializeObject(dt);
            lista = JsonConvert.DeserializeObject<List<Producto>>(cad_json);

            return lista;
        }

        public List<Producto> listaProductoCate(string cod_cate,string nombre)
        {
            var lista = new List<Producto>();
            DataTable dt = DBHelper.RetornaDataTable("SP_LISTA_PRODUCTOS_CATE", cod_cate, nombre);
            string cad_json = JsonConvert.SerializeObject(dt);
            lista = JsonConvert.DeserializeObject<List<Producto>>(cad_json);

            return lista;
        }

        public string GrabarProducto(Producto obj)
        {
            try
            {
                DBHelper.EjecutarSP("PA_GRABAR_PRODUCTO", obj.nom_pro, obj.uni_med, obj.pre_pro,obj.stk_pro ,obj.mat_pro, obj.cod_prov, obj.cod_cate);
                return "Producto registrado correctamente";
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
                DBHelper.EjecutarSP("SP_UPDATE_PRODUCTO",obj.cod_produc, obj.nom_pro, obj.uni_med, obj.pre_pro, obj.stk_pro, obj.mat_pro, obj.cod_prov, obj.cod_cate);
                return "Producto actualizado correctamente";
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
                DBHelper.EjecutarSP("SP_DELETE_PRODUCTO", id);

                return "Producto eliminado exitosamente";
            }
            catch (Exception e)
            {
                throw new Exception("Error" + e.Message);
            }
        }

    }
}
