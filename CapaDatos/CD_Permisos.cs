﻿using CapaModelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class CD_Permisos
    {
        public static CD_Permisos _instancia = null;

        private CD_Permisos()
        {

        }

        public static CD_Permisos Instancia
        {
            get
            {
                if (_instancia == null)
                {
                    _instancia = new CD_Permisos();
                }
                return _instancia;
            }
        }
        public List<Permisos> ObtenerPermisos(int IdRol)
        {
            List<Permisos> rptListaPermisos = new List<Permisos>();
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                SqlCommand cmd = new SqlCommand("usp_ObtenerPermisos", oConexion);
                cmd.Parameters.AddWithValue("@IdRol", IdRol);
                cmd.CommandType = CommandType.StoredProcedure;

                try
                {
                    oConexion.Open();
                    SqlDataReader dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        rptListaPermisos.Add(new Permisos()
                        {
                            IdPermisos = Convert.ToInt32(dr["IdPermisos"].ToString()),
                            Menu = dr["Menu"].ToString(),
                            SubMenu = dr["SubMenu"].ToString(),
                            Activo = Convert.ToBoolean(dr["Activo"].ToString())
                        });
                    }
                    dr.Close();

                    return rptListaPermisos;

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    rptListaPermisos = null;
                    return rptListaPermisos;
                }
            }
        }

        public bool ActualizarPermisos(string Detalle)
        {
            bool respuesta = true;
            using (SqlConnection oConexion = new SqlConnection(Conexion.CN))
            {
                try
                {
                    SqlCommand cmd = new SqlCommand("usp_ActualizarPermisos", oConexion);
                    cmd.Parameters.Add("Detalle", SqlDbType.Xml).Value = Detalle;
                    cmd.Parameters.Add("Resultado", SqlDbType.Bit).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oConexion.Open();

                    cmd.ExecuteNonQuery();

                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    respuesta = false;
                }
            }
            return respuesta;
        }
    }
}
