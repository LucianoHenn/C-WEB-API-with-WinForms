using BancoBackend.dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoBackend.datos
{
    interface IBancoDao
    {
        bool AltaCliente(Cliente Cliente);

        bool ModificarCliente(Cliente Cliente);
        bool EliminarCuenta(string cbu);
        bool ELiminarTipoCuenta(int id);
        bool EliminarCliente(int id);
        Cliente Login(int nro_cliente, string contrasenia);
        List<TipoCuenta> TraerTipoCuentas();
        bool Transferir(string cbu, double monto);
        List<string> TraerClientes();

        bool CrearTipoCuenta(string tipo);
        Cliente TraerUnCliente(int nro_cliente);
        bool EditarTipoCuenta(int id, string tipo);
        DataTable CrearReporte(string desde, string hasta);
    }
}
