using Microsoft.AspNetCore.Mvc;
using PazYSalvoAPP.Business.Services;
using PazYSalvoAPP.Models;
using PazYSalvoAPP.WebApp.Models.ViewModels;

namespace PazYSalvoAPP.WebApp.Controllers.Facturas
{
    public class FacturaController : Controller
    {
        private readonly FacturaService _facturaService;
        public FacturaController(FacturaService facturaService)
        {
            _facturaService = facturaService;
        }
        public IActionResult Index()
        {
            // Clientes
            List<Cliente> clientes = _facturaService.ObtenerClientes();

            // Pasar los datos a la vista
            ViewBag.Clientes = clientes;

            // estados
            List<Estado> estados = _facturaService.ObtenerEstados();

            // Pasar los datos a la vista
            ViewBag.estados = estados;

            // servicios
            List<Servicio> servicios = _facturaService.ObtenerServicios();

            // Pasar los datos a la vista
            ViewBag.servicios = servicios;

            // medios
            List<MediosDePago> mediosDePago = _facturaService.ObtenerMediosDePago();

            // Pasar los datos a la vista
            ViewBag.mediosDePago = mediosDePago;

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> ListarFacturas()
        {
            IQueryable<Factura>? consultaDeFacturas = await _facturaService.LeerTodos();

            List<Factura> listadoDeFacturas = consultaDeFacturas.Select(f => new Factura
            {
                Id = f.Id,
                Saldo = f.Saldo,
                ClienteId = f.ClienteId,
                ServicioAdquiridoId = f.ServicioAdquiridoId,
                MedioDePagoId = f.MedioDePagoId,
                EstadoId = f.EstadoId,               

            }).ToList();

            return PartialView("_ListadoDeFacturas",
                              listadoDeFacturas);
        }
        [HttpPost]
        public async Task<IActionResult> AgregarFacturas([FromBody] FacturaViewModel model)
        {
            Factura factura = new Factura() 
            {
                Saldo = model.Saldo,
                ClienteId= model.ClienteId,
                ServicioAdquiridoId = model.ServicioAdquiridoId,
                MedioDePagoId = model.MedioDePagoId,
                EstadoId= model.EstadoId,
            };

            bool response = await _facturaService.Insertar(factura);

            if (response)
            {

                return Json(new { success = true, message = "Factura agregada con éxito" });
            }
            else
            {
                return Json(new { success = false, message = "Error al agregar la factura" });
            }
                       
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarFacturas([FromBody] FacturaViewModel model)
        {
            Factura factura = new Factura()
            {
                Id = model.Id,
                Saldo = model.Saldo,
                ClienteId = model.ClienteId,
                ServicioAdquiridoId = model.ServicioAdquiridoId,
                MedioDePagoId = model.MedioDePagoId,
                EstadoId = model.EstadoId,

            };

            bool response = await _facturaService.Actualizar(factura);

            return StatusCode(StatusCodes.Status200OK,
                              new { valor = response });

        }

        public async Task<IActionResult> EditarFacturas(int id)
        {
            var factura = await _facturaService.Leer(id);
            FacturaViewModel facturaAEditar = new FacturaViewModel() 
            { 
                Saldo = factura.Saldo,
                ClienteId= factura.ClienteId,
                ServicioAdquiridoId = factura.ServicioAdquiridoId,
                MedioDePagoId = factura.MedioDePagoId,
                EstadoId= factura.EstadoId,

            };

            // Clientes
            List<Cliente> clientes = _facturaService.ObtenerClientes();

            // Pasar los datos a la vista
            ViewBag.Clientes = clientes;

            // estados
            List<Estado> estados = _facturaService.ObtenerEstados();

            // Pasar los datos a la vista
            ViewBag.estados = estados;

            // servicios
            List<Servicio> servicios = _facturaService.ObtenerServicios();

            // Pasar los datos a la vista
            ViewBag.servicios = servicios;

            // medios
            List<MediosDePago> mediosDePago = _facturaService.ObtenerMediosDePago();

            // Pasar los datos a la vista
            ViewBag.mediosDePago = mediosDePago;

            return View("EditarFacturas", facturaAEditar);
        }
    }

}

