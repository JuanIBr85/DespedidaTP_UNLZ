using GestorEventos.Servicios.Entidades;
using GestorEventos.Servicios.Servicios;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace GestorEventos.WebAdmin.Controllers
{
	public class ServiciosController : Controller
	{

		// GET: ServiciosController
		public ActionResult Index()
		{
			ServicioDeServicios sServicios = new ServicioDeServicios(); 
//			servicioService.GetServicios();

			return View(sServicios.ObtenerServicios());
		}

		// GET: ServiciosController/Details/5
		public ActionResult Details(int id)
		{
            ServicioDeServicios sServicios = new ServicioDeServicios();

            return View(sServicios.ObtenerServicioId(id));
		}

		// GET: ServiciosController/Create
		public ActionResult Create()
		{
			return View();
		}

		// POST: ServiciosController/Create
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(IFormCollection collection)
		{
			try
			{
                ServicioDeServicios sServicios = new ServicioDeServicios();

                Servicio servicio = new Servicio();

				servicio.Descripcion = collection["Descripcion"].ToString();
				servicio.PrecioServicio = decimal.Parse(collection["PrecioServicio"].ToString());


				sServicios.AgregarServicio(servicio);


                return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: ServiciosController/Edit/5
		public ActionResult Edit(int id)
		{
            ServicioDeServicios sServicios = new ServicioDeServicios();


            return View(sServicios.ObtenerServicioId(id));
        }

		// POST: ServiciosController/Edit/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(int id, IFormCollection collection)
		{
			try
			{
                ServicioDeServicios sServicios = new ServicioDeServicios();

                Servicio servicio = new Servicio();

				servicio.IdServicio = int.Parse(collection["IdServicio"].ToString());
				servicio.Descripcion = collection["Descripcion"].ToString();
				servicio.PrecioServicio = decimal.Parse(collection["PrecioServicio"].ToString());

				sServicios.ModificarServicio(id, servicio);

				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}

		// GET: ServiciosController/Delete/5
		public ActionResult Delete(int id)
		{

            ServicioDeServicios sServicios = new ServicioDeServicios();

			Servicio servicio = sServicios.ObtenerServicioId(id); 

            return View(servicio);
        }

		// POST: ServiciosController/Delete/5
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection collection)
		{
			try
			{
                ServicioDeServicios sServicios = new ServicioDeServicios();

				sServicios.BorradoLogicoServicio(id);

				return RedirectToAction(nameof(Index));
			}
			catch
			{
				return View();
			}
		}
	}
}
