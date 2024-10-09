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
    public class RecetasService
    {
        private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();


        public RecetasService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;

             _webHostEnvironment = webHostEnvironment;
             
        }

        public List<RecetasModel> GetRecetas()
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            RecetasModel almacen = new RecetasModel();

            List<RecetasModel> lista = new List<RecetasModel>();
            try
            {
                parametros = new ArrayList();
                DataSet ds = dac.Fill("sp_get_recetas", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {

                  lista = ds.Tables[0].AsEnumerable()
                    .Select(dataRow => new RecetasModel {
                        Id = int.Parse(dataRow["Id"].ToString()),
                        Nombre = dataRow["Nombre"].ToString(),
                        Estatus = int.Parse(dataRow["Estatus"].ToString()),
                        Fecha_crecion = dataRow["Total"].ToString(),
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

        public string InsertRecetas(RecetasModel entradas)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            string mensaje;

            parametros.Add(new SqlParameter { ParameterName = "@Nombre", SqlDbType = System.Data.SqlDbType.Int, Value = entradas.Nombre });
            parametros.Add(new SqlParameter { ParameterName = "@Usuario_registra", SqlDbType = System.Data.SqlDbType.Int, Value = entradas.Usuario_registra});
          

            try
            {
                DataSet ds = dac.Fill("InsertReceta", parametros);
                mensaje = ds.Tables[0].AsEnumerable().Select(dataRow => dataRow["mensaje"].ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return mensaje;
        }

        public string UpdateRecetas(RecetasModel entrada)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            string mensaje;

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = System.Data.SqlDbType.VarChar, Value = entrada.Id });
            parametros.Add(new SqlParameter { ParameterName = "@Nombre", SqlDbType = System.Data.SqlDbType.Int, Value = entrada.Nombre });
            parametros.Add(new SqlParameter { ParameterName = "@Estatus", SqlDbType = System.Data.SqlDbType.Int, Value = entrada.Estatus });
           

            try
            {
                DataSet ds = dac.Fill("UpdateRecetas", parametros);
                mensaje = ds.Tables[0].AsEnumerable().Select(dataRow => dataRow["mensaje"].ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensaje;
        }

      public void DeleteRecetas(int id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id });


            try
            {
                dac.ExecuteNonQuery("sp_delete_recetas", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}