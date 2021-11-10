using BancoBackend.acceso_a_datos;
using BancoBackend.datos;
using BancoBackend.dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoBackend.negocio
{
    public class Aplicacion : IAplicacion
    {
        private IBancoDao Dao;

        public Aplicacion()
        {
            Dao = new BancoDaoFactory().CrearDao();
        }

        public bool AltaCliente(Cliente Cliente)
        {
            return Dao.AltaCliente(Cliente);
        }

        public bool BajaCliente(int NumeroCliente)
        {
            throw new NotImplementedException();
        }

        public bool ELiminarCliente(int id)
        {
            return Dao.EliminarCliente(id);
        }

        public bool ELiminarCuenta(string cbu)
        {
            return Dao.EliminarCuenta(cbu);
        }

        public bool ELiminarTipoCuenta(int id)
        {
            return Dao.ELiminarTipoCuenta(id);
        }
        public Cliente Login(int nro_cliente, string contrasenia)
        {
            return Dao.Login(nro_cliente, contrasenia);
        }

        public DataTable CrearReporte(string desde, string hasta)
        {
             return Dao.CrearReporte(desde, hasta);
        }

        public bool Transferir(string cbu, double monto)
        {
            return Dao.Transferir(cbu, monto);
        }

        public bool ModificarCliente(Cliente Cliente)
        {
            return Dao.ModificarCliente(Cliente);
        }

        public List<TipoCuenta> TrearTipoCuentas()
        {
            return Dao.TraerTipoCuentas();
        }
        public bool CrearTipoCuenta(string tipo)
        {
            return Dao.CrearTipoCuenta(tipo);
        }

        public List<string> TraerClientes()
        {
            return Dao.TraerClientes();
        }

        public Cliente TraerUnCliente(int nro_cliente)
        {
            return Dao.TraerUnCliente(nro_cliente);
        }

        public bool EditarTipoCuenta(int id, string tipo)
        {
            return Dao.EditarTipoCuenta(id, tipo);
        }
    }
}
