﻿using SistemaDeGestion.Modelos;
using System.Data.SqlClient;

namespace SistemaDeGestion.Repositorios
{
    public class ProductosVendidosRepositorio
    {
        private SqlConnection? conexion;
        private String cadenaconexion = "Server=sql.bsite.net\\MSSQL2016;" +
                "Database=santiarenaza_primer_db;" +
                "User Id=santiarenaza_primer_db;" +
                "Password=123456;";

        public ProductosVendidosRepositorio()
        {
            try
            {
                conexion = new SqlConnection(cadenaconexion);
            }
            catch
            {

            }
        }

        public List<ProductosVendidos> listarProductosVendidos()
        {
            List<ProductosVendidos> listaProductosVendidos = new List<ProductosVendidos>();
            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM ProductoVendido", conexion))
                {
                    conexion.Open();
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    ProductosVendidos productosVendidos = new ProductosVendidos();
                                    productosVendidos.Id = Convert.ToInt64(reader["Id"]);
                                    productosVendidos.Stock = int.Parse(reader["Stock"].ToString());
                                    productosVendidos.IdProducto = Convert.ToInt64(reader["IdProducto"]);
                                    productosVendidos.IdVenta = Convert.ToInt64(reader["IdVenta"]);
                                    listaProductosVendidos.Add(productosVendidos);
                                }
                            }

                        }
                    }
                    conexion.Close();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            return listaProductosVendidos;

        }




    }
}
