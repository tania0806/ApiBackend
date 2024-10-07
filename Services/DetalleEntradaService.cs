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
namespace reportesApi.Services
{
    public class DetalleEntradasService
    {
        private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();


        public DetalleEntradasService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;

             _webHostEnvironment = webHostEnvironment;
             
        }

        public List<DetalleEntradasModel> GetDetalleEntrada()
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            DetalleEntradasModel almacen = new DetalleEntradasModel();

            List<DetalleEntradasModel> lista = new List<DetalleEntradasModel>();
            try
            {
                parametros = new ArrayList();
                DataSet ds = dac.Fill("GetDetalleEntrada", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {

                  lista = ds.Tables[0].AsEnumerable()
                    .Select(dataRow => new DetalleEntradasModel {
                        Id = int.Parse(dataRow["Id"].ToString()),
                        IdEntrada = int.Parse(dataRow["IdEntrada"].ToString()),
                        Insumo = dataRow["Insumo"].ToString(),
                        Cantidad = dataRow["Cantidad"].ToString(),
                        Costo = decimal.Parse(dataRow["Costo"].ToString()),
                    
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public string InsertDetalleEntrada(DetalleEntradasModel entradas)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            string mensaje;

            parametros.Add(new SqlParameter { ParameterName = "@IdEntrada", SqlDbType = System.Data.SqlDbType.Int, Value = entradas.IdEntrada});
            parametros.Add(new SqlParameter { ParameterName = "@Insumo", SqlDbType = System.Data.SqlDbType.Int, Value = entradas.Insumo});
            parametros.Add(new SqlParameter { ParameterName = "@Cantidad", SqlDbType = System.Data.SqlDbType.Decimal, Value = entradas.Cantidad});
            parametros.Add(new SqlParameter { ParameterName = "@Costo", SqlDbType = System.Data.SqlDbType.Decimal, Value = entradas.Costo});
          
            try
            {
                DataSet ds = dac.Fill("InsertDetalleEntrada", parametros);
                mensaje = ds.Tables[0].AsEnumerable().Select(dataRow => dataRow["mensaje"].ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return mensaje;
        }

        public string UpdateDetalleEntrada(DetalleEntradasModel entrada)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            string mensaje;

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = System.Data.SqlDbType.VarChar, Value = entrada.Id });
            parametros.Add(new SqlParameter { ParameterName = "@IdEntrada", SqlDbType = System.Data.SqlDbType.Int, Value = entrada.IdEntrada});
            parametros.Add(new SqlParameter { ParameterName = "@Insumo", SqlDbType = System.Data.SqlDbType.VarChar, Value = entrada.Insumo});
            parametros.Add(new SqlParameter { ParameterName = "@Cantidad", SqlDbType = System.Data.SqlDbType.VarChar, Value = entrada.Cantidad});
          
            parametros.Add(new SqlParameter { ParameterName = "@Costo", SqlDbType = System.Data.SqlDbType.Decimal, Value = entrada.Costo});
           
            try
            {
                DataSet ds = dac.Fill("UpdateDetalleEntrada", parametros);
                mensaje = ds.Tables[0].AsEnumerable().Select(dataRow => dataRow["mensaje"].ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensaje;
        }

      public void DeleteDetalleEntrada(int id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id });


            try
            {
                dac.ExecuteNonQuery("DeleteDetalleEntrada", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}