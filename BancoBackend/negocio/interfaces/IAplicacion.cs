using BancoBackend.dominio;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoBackend.negocio
{
    public interface IAplicacion
    {
        bool AltaCliente(Cliente Cliente);

        bool BajaCliente(int NumeroCliente);

        bool ModificarCliente(Cliente Cliente);
        bool ELiminarCuenta(string cbu);

        bool ELiminarTipoCuenta(int id);
        bool ELiminarCliente(int id);
        Cliente Login(int nro_cliente, string contrasenia);
        List<TipoCuenta> TrearTipoCuentas();
        bool CrearTipoCuenta(string tipo);
        bool Transferir(String cbu, double monto);
        List<string> TraerClientes();
        Cliente TraerUnCliente(int nro_cliente);
        bool EditarTipoCuenta(int id, string tipo);

        DataTable CrearReporte(string desde, string hasta);
    }
}
