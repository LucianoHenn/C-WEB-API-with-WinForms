using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BancoBackend.dominio
{
    public class Cliente
    {
        public int NroCliente { get; set; }
        public string Nombre { get; set; }

        public string Apellido { get; set; }

        public string DNI { get; set; }

        public List<Cuenta> Cuentas { get; set; }

        public bool EsAdmin { get; set; }

        public string Contrasenia { get; set; }

        public Cliente()
        {
            Cuentas = new List<Cuenta>();
        }

        public void AgregarCuenta(Cuenta c)
        {
            Cuentas.Add(c);
        }


        public void QuitarCuenta(int nro)
        {
            Cuentas.RemoveAt(nro);

        }

    }
}
