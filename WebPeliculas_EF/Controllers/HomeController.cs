using Datos.Model;
using Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebPeliculas_EF.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            N_Pelicula negocio = new N_Pelicula();
            List<Peliculas> peliculas = negocio.ObtnerPeliculas();
            return View("Principal",peliculas);
        }
    }
}