﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace BLL
{
    public class Facturas : ClaseMaestra
    {
        public int FacturaId { set; get; }
        public string Fecha { set; get; }
        public int EntradaId { set; get; }
        public int ClienteId { set; get; }
        public float CargoReparacion { set; get; }
        public float Total { set; get; }
        public float MontoAPagar { set; get; }
        public string DespachadoPor { set; get; }
        ArticulosVendidos ArticuloVendido = new ArticulosVendidos();
        public List<ArticulosVendidos> articulosVendidos { get; set; }
        ArticulosEntregados ArticuloEntregado = new ArticulosEntregados();
        public List<ArticulosEntregados> articulosEntregados { get; set; }
        ConexionDb conexion = new ConexionDb();

        public Facturas(int facturaId, string fecha, int entradaId, int clienteId, float cargoReparacion, float total, float montoAPagar, string despachadoPor)
        {
            this.FacturaId = facturaId;
            this.Fecha = fecha;
            this.EntradaId = entradaId;
            this.ClienteId = clienteId;
            this.CargoReparacion = cargoReparacion;
            this.Total = total;
            this.MontoAPagar = montoAPagar;
            this.DespachadoPor = despachadoPor;
        }

        public Facturas()
        {
            articulosVendidos = new List<ArticulosVendidos>();
            articulosEntregados = new List<ArticulosEntregados>();
        }

        public void InsertarArticuloVendido(string Pieza, string Marca, float Precio)
        {
            this.articulosVendidos.Add(new ArticulosVendidos(Pieza, Marca, Precio));
        }

        public void InsertarArticuloEntregado(string Articulo, string Problema, float Cargo)
        {
            this.articulosEntregados.Add(new ArticulosEntregados(Articulo, Problema, Cargo));
        }

        public override bool Insertar()
        {
            int retorno = 0;
            object identity;
            try
            {
                //obtengo el identity insertado en la tabla
                identity = conexion.ObtenerValor(String.Format("INSERT INTO Facturas (Fecha, EntradaId, ClienteId, CargoReparacion, Total, MontoAPagar, DespachadoPor) VALUES ('{0}'.{1},{2},{3},{4},{5},'{6}') Select @@Identity", this.Fecha, this.EntradaId, this.ClienteId, this.CargoReparacion, this.Total, this.MontoAPagar, this.DespachadoPor));

                //intento convertirlo a entero
                int.TryParse(identity.ToString(), out retorno);

                this.FacturaId = retorno;
                if (retorno > 0)
                {
                    conexion.Ejecutar(String.Format("UPDATE Entradas SET Entregado='True' WHERE EntradaId={0}", this.EntradaId));
                    foreach (ArticulosVendidos vendidos in this.articulosVendidos)
                    {
                        conexion.Ejecutar(String.Format("INSERT INTO ArticulosVendidos (FacturaId, Pieza, Marca, Precio) VALUES ({0},'{1}','{2}',{3})", retorno, vendidos.Pieza, vendidos.Marca, vendidos.Precio));
                    }
                    foreach (ArticulosEntregados entregados in this.articulosEntregados)
                    {
                        conexion.Ejecutar(String.Format("INSERT INTO ArticulosEntregados (FacturaId, Articulo, Problema, Cargo) VALUES ({0},'{1}','{2}',{3})", retorno, entregados.Articulo, entregados.Problema, entregados.Cargo));
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno > 0;
        }

        public override bool Editar()
        {
            bool retorno = false;
            try
            {
                retorno = conexion.Ejecutar(String.Format("UPDATE Facturas SET Fecha='{0}', EntradaId={1}, ClienteId={2}, CargoReparacion={3}, Total={4}, MontoAPagar={5}, DespachadoPor={6} WHERE FacturaId={7}", this.Fecha, this.EntradaId, this.ClienteId, this.CargoReparacion, this.Total, this.MontoAPagar, this.DespachadoPor, this.FacturaId));
                if (retorno)
                {
                    conexion.Ejecutar(String.Format("DELETE FROM ArticulosVendidos WHERE FacturaId= {0}", this.FacturaId));
                    conexion.Ejecutar(String.Format("DELETE FROM ArticulosEntregados WHERE FacturaId= {0}", this.FacturaId));
                    foreach (ArticulosVendidos vendidos in this.articulosVendidos)
                    {
                        conexion.Ejecutar(String.Format("INSERT INTO ArticulosVendidos (FacturaId, Pieza, Marca, Precio) VALUES ({0},'{1}','{2}',{3})", this.FacturaId, vendidos.Pieza, vendidos.Marca, vendidos.Precio));
                    }
                    foreach (ArticulosEntregados entregados in this.articulosEntregados)
                    {
                        conexion.Ejecutar(String.Format("INSERT INTO ArticulosEntregados (FacturaId, Articulo, Problema, Cargo) VALUES ({0},'{1}','{2}',{3})", this.FacturaId, entregados.Articulo, entregados.Problema, entregados.Cargo));
                    }
                }
            }
            catch (Exception ex) { throw ex; }
            return retorno;
        }

        public override bool Eliminar()
        {
            bool retorno = false;
            try
            {
                retorno = conexion.Ejecutar(String.Format("DELETE FROM Facturas WHERE FacturaId={0}", this.FacturaId));
                if (retorno)
                    conexion.Ejecutar(String.Format("DELETE FROM ArticulosVendidos WHERE FacturaId={0}", this.FacturaId));
                    conexion.Ejecutar(String.Format("DELETE FROM ArticulosEntregados WHERE FacturaId={0}", this.FacturaId));
            }
            catch (Exception ex) { throw ex; }
            return retorno;
        }

        public override bool Buscar(int IdBuscado)
        {
            DataTable dt = new DataTable();
            DataTable dtArticulosVendidos = new DataTable();
            DataTable dtArticulosEntregados = new DataTable();

            dt = conexion.ObtenerDatos("SELECT * FROM Facturas WHERE FacturaId=" + IdBuscado);
            if (dt.Rows.Count > 0)
            {
                this.FacturaId = (int)dt.Rows[0]["FacturaId"];
                this.Fecha = dt.Rows[0]["Fecha"].ToString();
                this.EntradaId = (int)dt.Rows[0]["EntradaId"];
                this.ClienteId = (int)dt.Rows[0]["ClienteId"];
                this.CargoReparacion = (float)Convert.ToDecimal(dt.Rows[0]["CargoReparacion"]);
                this.Total = (float)Convert.ToDecimal(dt.Rows[0]["Total"]);
                this.MontoAPagar = (float)Convert.ToDecimal(dt.Rows[0]["MontoAPagar"]);
                this.DespachadoPor = dt.Rows[0]["DespachadoPor"].ToString();

                dtArticulosVendidos = conexion.ObtenerDatos(String.Format("SELECT * FROM ArticulosVendidos WHERE FacturaId =" + IdBuscado));

                foreach (DataRow row in dtArticulosVendidos.Rows)
                {
                    InsertarArticuloVendido(row["Pieza"].ToString(), row["Marca"].ToString(), (float)Convert.ToDecimal(dt.Rows[0]["Precio"]));
                }

                dtArticulosEntregados = conexion.ObtenerDatos(String.Format("SELECT * FROM ArticulosEntregados WHERE FacturaId =" + IdBuscado));

                foreach (DataRow row in dtArticulosEntregados.Rows)
                {
                    InsertarArticuloEntregado(row["Articulo"].ToString(), row["Problema"].ToString(), (float)Convert.ToDecimal(dt.Rows[0]["Cargo"]));
                }
            }
            return dt.Rows.Count > 0;
        }

        public override DataTable Listado(string Campos, string Condicion, string Orden)
        {
            string ordenar = "";
            if (!Orden.Equals(""))
                ordenar = " orden by  " + Orden;
            return conexion.ObtenerDatos(("SELECT " + Campos + " FROM Facturas WHERE " + Condicion + ordenar));
        }
    }
}
