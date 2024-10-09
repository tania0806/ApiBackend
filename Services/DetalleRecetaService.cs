using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using reportesApi.DataContext;
using reportesApi.Models;
using System.Collections.Generic;
using reportesApi.Models.Compras;
using OfficeOpenXml;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using ApiBackend.Models;
namespace reportesApi.Services
{
    public class DetalleRecetaService
    {
        private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();


        public DetalleRecetaService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;

             _webHostEnvironment = webHostEnvironment;
             
        }

        public List<DetalleRecetaModel> GetDetalleReceta()
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            DetalleRecetaModel almacen = new DetalleRecetaModel();

            List<DetalleRecetaModel> lista = new List<DetalleRecetaModel>();
            try
            {
                parametros = new ArrayList();
                DataSet ds = dac.Fill("GetDetalleReceta", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {

                  lista = ds.Tables[0].AsEnumerable()
                    .Select(dataRow => new DetalleRecetaModel {
                        Id = int.Parse(dataRow["Id"].ToString()),
                        IdReceta = int.Parse(dataRow["IdReceta"].ToString()),
                        Insumo = dataRow["Insumo"].ToString(),
                        Cantidad = decimal.Parse(dataRow["Cantidad"].ToString()),
                        Estatus = int.Parse(dataRow["Estatus"].ToString()),
                        Fecha_registro = dataRow["Fecha_registro"].ToString(),
                        Usuario_registra = int.Parse(dataRow["Usuario_registra"].ToString()),
                    
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public string InsertDetalleReceta(DetalleRecetaModel entradas)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            string mensaje;

            parametros.Add(new SqlParameter { ParameterName = "@IdReceta", SqlDbType = System.Data.SqlDbType.Int, Value = entradas.IdReceta});
            parametros.Add(new SqlParameter { ParameterName = "@Insumo", SqlDbType = System.Data.SqlDbType.VarChar, Value = entradas.Insumo});
            parametros.Add(new SqlParameter { ParameterName = "@Cantidad", SqlDbType = System.Data.SqlDbType.Decimal, Value = entradas.Cantidad });
            parametros.Add(new SqlParameter { ParameterName = "@Usuario_registra", SqlDbType = System.Data.SqlDbType.Int, Value = entradas.Usuario_registra});
          

            try
            {
                DataSet ds = dac.Fill("InsertDetalleReceta", parametros);
                mensaje = ds.Tables[0].AsEnumerable().Select(dataRow => dataRow["mensaje"].ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return mensaje;
        }

        public string UpdateDetalleReceta(DetalleRecetaModel entrada)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            string mensaje;

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = System.Data.SqlDbType.VarChar, Value = entrada.Id });
            parametros.Add(new SqlParameter { ParameterName = "@IdReceta", SqlDbType = System.Data.SqlDbType.Int, Value = entrada.IdReceta});
            parametros.Add(new SqlParameter { ParameterName = "@Insumo", SqlDbType = System.Data.SqlDbType.VarChar, Value = entrada.Insumo});
            parametros.Add(new SqlParameter { ParameterName = "@Cantidad", SqlDbType = System.Data.SqlDbType.Decimal, Value = entrada.Cantidad });
            parametros.Add(new SqlParameter { ParameterName = "@Usuario_registra", SqlDbType = System.Data.SqlDbType.Int, Value = entrada.Usuario_registra});
            parametros.Add(new SqlParameter { ParameterName = "@Estatus", SqlDbType = System.Data.SqlDbType.Int, Value = entrada.Estatus });
           

            try
            {
                DataSet ds = dac.Fill("UpdateDetalleReceta", parametros);
                mensaje = ds.Tables[0].AsEnumerable().Select(dataRow => dataRow["mensaje"].ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensaje;
        }

      public void DeleteDetalleReceta(int id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id });


            try
            {
                dac.ExecuteNonQuery("DeleteDetalleReceta", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}