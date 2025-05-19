using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Capa_Datos
{
    public class DBHelper
    {
        static string cad_cn =
            ConfigurationManager.ConnectionStrings["cn1"]
                                .ConnectionString;

        private static void LlenarParametros(SqlCommand cmd, 
                                         params object[] valores)
        {
            int indice = 0;
            // descubrir y crear los parámetros que se esté utilizando
            // en el SqlCommand
            SqlCommandBuilder.DeriveParameters(cmd);
            // recorrer los parámetros
            foreach (SqlParameter p in cmd.Parameters)
            {
                // si el nombre del parámetro es diferente a
                // @RETURN_VALUE
                if (p.ParameterName != "@RETURN_VALUE")
                {
                    // asignar el valor correspondiente
                    p.Value = valores[indice];
                    indice++;
                }
            }   
        }

        // método para ejecutar procedimientos almacenados
        // en donde se utilice el INSERT, UPDATE, "DELETE"
        public static void EjecutarSP(
            string nombreSP, params object[] valoresparametros) // ExecuteNonQuery
        {
            SqlConnection cnx = new SqlConnection(cad_cn);
            cnx.Open();

            SqlCommand cmd = new SqlCommand(nombreSP, cnx);
            cmd.CommandType = CommandType.StoredProcedure;

            if (valoresparametros.Length > 0)
                LlenarParametros(cmd, valoresparametros);

            cmd.ExecuteNonQuery();
            cnx.Close();
        }


        // método para ejecutar procedimientos almacenados
        // en donde se utilice el SELECT
        public static DataTable RetornaDataTable(
            string nombreSP, params object[] valoresparametros)
        { 
            DataTable tabla = new DataTable();
            //
            using (SqlDataAdapter adap = new SqlDataAdapter(
                                           nombreSP, cad_cn))
            {
                // abrir la conexión a la bd
                adap.SelectCommand.Connection.Open();
                adap.SelectCommand.CommandType = CommandType.StoredProcedure;
                // si hay valores para los parámetros
                if (valoresparametros.Length > 0)
                {
                    LlenarParametros(adap.SelectCommand,
                        valoresparametros); 
                }
                // llenar el DataTable
                adap.Fill(tabla);
            }
            //
            return tabla;
        }


        // método para ejecutar procedimientos almacenados
        // en donde se utilice el INSERT, UPDATE, "DELETE"
        // dentro de una transacción de visual (SqlTransaction)
        public static void EjecutarSP_TRX(
            string nombreSP, params object[] valoresparametros) // ExecuteNonQuery
        {
            SqlConnection cnx = new SqlConnection(cad_cn);
            cnx.Open(); // abriendo la conexión con la BD

            // iniciamos la transacción
            SqlTransaction trx = cnx.BeginTransaction();

            // Try-Catch
            try
            {
                SqlCommand cmd = new SqlCommand(nombreSP, cnx, trx);
                cmd.CommandType = CommandType.StoredProcedure;

                if (valoresparametros.Length > 0)
                    LlenarParametros(cmd, valoresparametros);

                cmd.ExecuteNonQuery();
                // si no hubo error, entonces confirmamos la transacción
                trx.Commit();
            }
            catch (Exception ex) 
            {
                // si hubo error, entonces cancelamos la transacción
                trx.Rollback();
                throw new Exception(ex.Message);
            }
            finally {
                // si se llegó la conexión con la BD, entonces
                // cerramos la conexión
                if (cnx.State == ConnectionState.Open)
                    cnx.Close();    
            }
        }


        public static object EjecutarSP_TRX_Object(
            string nombreSP, params object[] valoresparametros) 
        {
            SqlConnection cnx = new SqlConnection(cad_cn);
            cnx.Open(); // abriendo la conexión con la BD

            // iniciamos la transacción
            SqlTransaction trx = cnx.BeginTransaction();

            // Try-Catch
            try
            {
                SqlCommand cmd = new SqlCommand(nombreSP, cnx, trx);
                cmd.CommandType = CommandType.StoredProcedure;

                if (valoresparametros.Length > 0)
                    LlenarParametros(cmd, valoresparametros);

                object respuesta = cmd.ExecuteScalar();
                // si no hubo error, entonces confirmamos la transacción
                trx.Commit();

                return respuesta;
            }
            catch (Exception ex)
            {
                // si hubo error, entonces cancelamos la transacción
                trx.Rollback();
                throw new Exception(ex.Message);
            }
            finally
            {
                // si se llegó la conexión con la BD, entonces
                // cerramos la conexión
                if (cnx.State == ConnectionState.Open)
                    cnx.Close();
            }
        }

        public static object RetornaScalar(string NombreSP,
                               params object[] Parametros)
        {
            SqlConnection cnx = new SqlConnection(cad_cn);
            cnx.Open();
            //
            SqlCommand comando = new SqlCommand(NombreSP, cnx);
            comando.CommandType = CommandType.StoredProcedure;
            //
            if (Parametros.Length > 0)
            {
                LlenarParametros(comando, Parametros);
            }
            //
            object respuesta = comando.ExecuteScalar();
            //
            cnx.Close();
            //
            return respuesta;
        }
    }
}