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
    public class EntradasService
    {
        private  string connection;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private ArrayList parametros = new ArrayList();


        public EntradasService(IMarcatelDatabaseSetting settings, IWebHostEnvironment webHostEnvironment)
        {
             connection = settings.ConnectionString;

             _webHostEnvironment = webHostEnvironment;
             
        }

        public List<EntradasModel> GetEntradas()
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            EntradasModel almacen = new EntradasModel();

            List<EntradasModel> lista = new List<EntradasModel>();
            try
            {
                parametros = new ArrayList();
                DataSet ds = dac.Fill("GetEntradas", parametros);
                if (ds.Tables[0].Rows.Count > 0)
                {

                  lista = ds.Tables[0].AsEnumerable()
                    .Select(dataRow => new EntradasModel {
                        Id = int.Parse(dataRow["Id"].ToString()),
                        IdProvedor = int.Parse(dataRow["IdProvedor"].ToString()),
                        IdSucursal = int.Parse(dataRow["IdSucursal"].ToString()),
                        Total = decimal.Parse(dataRow["Total"].ToString()),
                    
                    }).ToList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lista;
        }

        public string InsertEnt(EntradasModel entradas)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            string mensaje;

            parametros.Add(new SqlParameter { ParameterName = "@IdProvedor", SqlDbType = System.Data.SqlDbType.Int, Value = entradas.IdProvedor });
            parametros.Add(new SqlParameter { ParameterName = "@IdSucursal", SqlDbType = System.Data.SqlDbType.Int, Value = entradas.IdSucursal});
            parametros.Add(new SqlParameter { ParameterName = "@Total", SqlDbType = System.Data.SqlDbType.Decimal, Value = entradas.Total});

            try
            {
                DataSet ds = dac.Fill("InsertEnt", parametros);
                mensaje = ds.Tables[0].AsEnumerable().Select(dataRow => dataRow["mensaje"].ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return mensaje;
        }

        public string UpdateEntradas(EntradasModel entrada)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            string mensaje;

            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = System.Data.SqlDbType.VarChar, Value = entrada.Id });
            parametros.Add(new SqlParameter { ParameterName = "@IdProvedor", SqlDbType = System.Data.SqlDbType.Int, Value = entrada.IdProvedor });
            parametros.Add(new SqlParameter { ParameterName = "@IdSucursal", SqlDbType = System.Data.SqlDbType.Int, Value = entrada.IdSucursal });
            parametros.Add(new SqlParameter { ParameterName = "@Total", SqlDbType = System.Data.SqlDbType.Decimal, Value = entrada.Total });
           

            try
            {
                DataSet ds = dac.Fill("UpdateEntradas", parametros);
                mensaje = ds.Tables[0].AsEnumerable().Select(dataRow => dataRow["mensaje"].ToString()).ToList()[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mensaje;
        }

      public void DeleteEntradas(int id)
        {
            ConexionDataAccess dac = new ConexionDataAccess(connection);
            parametros = new ArrayList();
            parametros.Add(new SqlParameter { ParameterName = "@Id", SqlDbType = SqlDbType.Int, Value = id });


            try
            {
                dac.ExecuteNonQuery("DeleteEntradas", parametros);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}