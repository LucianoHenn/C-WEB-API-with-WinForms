using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BancoBackend.dominio;
using BancoBackend.negocio;

namespace BancoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BancoController : ControllerBase
    {
        private IAplicacion app;

        private readonly ILogger<BancoController> _logger;

        public BancoController(ILogger<BancoController> logger)
        {
            _logger = logger;
            app = new Aplicacion();
        }

        [HttpPost("clientes")]
        public IActionResult Store(Cliente Cliente)
        {
            if (Cliente == null)
            {
                return BadRequest();
            }

            if (app.AltaCliente(Cliente))
                return Ok("Ok");
            else
                return Ok("No se pudo grabar el cliente");
        }

        [HttpPut("clientes")]
        public IActionResult Update(Cliente Cliente)
        {

            return Ok(app.ModificarCliente(Cliente));
        }

        //[HttpDelete("cuentas/{cbu}")]
        //public IActionResult DestroyCuenta(string cbu)
        //{

        //    return Ok(app.ELiminarCuenta(cbu));
        //}
        [HttpDelete("clientes/{id}")]
        public IActionResult Destroy(int id)
        {
            if (app.ELiminarCliente(id))
                return Ok("ok");
            return BadRequest("error");

        }

        [HttpPost("login")]
        public Cliente Login(int nro_cliente,string contrasenia)
        {
            return app.Login(nro_cliente, contrasenia);

        }

        [HttpGet("tipoCuenta")]
        public IActionResult TraerTipoCuentas()
        {
        
            return Ok(app.TrearTipoCuentas());
        }

        [HttpPost("tipoCuenta")]
        public IActionResult CrearTipoCuenta(string tipo)
        {

            return Ok(app.CrearTipoCuenta(tipo));
        }
        [HttpDelete("tipoCuenta/{id}")]
        public IActionResult DestroyTipoCuenta(int id)
        {
            if (app.ELiminarTipoCuenta(id))
                return Ok("ok");
            return BadRequest("error");

        }
        [HttpPut("tipoCuenta/{id}")]
        public IActionResult DestroyTipoCuenta(int id, string tipo)
        {
            if (app.EditarTipoCuenta(id,tipo))
                return Ok("ok");
            return BadRequest("error");

        }

        [HttpGet("clientes")]
        public IActionResult ShowClientes()
        {

            return Ok(app.TraerClientes());
        }

        [HttpGet("clientes/{nro_cliente}")]
        public IActionResult ShowOneClient(int nro_cliente)
        {

            return Ok(app.TraerUnCliente(nro_cliente));
        }

        [HttpPost("cuentas/{cbu}")]
        public IActionResult Transferir(string cbu, double monto)
        {

            return Ok(app.Transferir(cbu, monto));
        }
        //[HttpGet("cuentas/{id}")]
        //public IActionResult 

    }
}
