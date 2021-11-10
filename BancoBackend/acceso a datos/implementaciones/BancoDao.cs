using BancoBackend.dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoBackend.datos
{
    class BancoDao : IBancoDao
    {
        public bool AltaCliente(Cliente Cliente)
        {
            return HelperDao.GetInstance().AltaCliente(Cliente);
        }
        public bool ELiminarTipoCuenta(int id)
        {
            return HelperDao.GetInstance().EliminarTipoCuenta(id);
        }
        public bool EliminarCliente(int id)
        {
            return HelperDao.GetInstance().EliminarCliente(id);
        }

        public bool EliminarCuenta(string cbu)
        {
            return HelperDao.GetInstance().EliminarCuenta(cbu);
        }

        public Cliente Login(int nro_cliente, string contrasenia)
        {
            return HelperDao.GetInstance().Login(nro_cliente, contrasenia);
        }

        public bool Transferir(string cbu, double monto)
        {
            return HelperDao.GetInstance().Transferir(cbu, monto);
        }

        public bool ModificarCliente(Cliente Cliente)
        {
            return HelperDao.GetInstance().ModificarCliente(Cliente);
        }

        public List<TipoCuenta> TraerTipoCuentas()
        {
            return HelperDao.GetInstance().TraerTipoCuentas();
        }

        public bool  CrearTipoCuenta(string tipo)
        {
            return HelperDao.GetInstance().CrearTipoCuentas(tipo);
        }

        public List<string> TraerClientes()
        {
            return HelperDao.GetInstance().TraerClientes();
        }

        public Cliente TraerUnCliente(int nro_cliente)
        {
            return HelperDao.GetInstance().TraerUnCliente(nro_cliente);
        }

        public bool EditarTipoCuenta(int id, string tipo)
        {
            return HelperDao.GetInstance().EditarTipoCuenta(id, tipo);
        }

        public DataTable CrearReporte(string desde, string hasta)
        {
             return HelperDao.GetInstance().CrearReporte(desde, hasta);
        }
    }
}
