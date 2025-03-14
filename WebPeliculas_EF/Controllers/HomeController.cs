using Datos.Model;
using Negocio;
using System;
using System.Collections.Generic;
using System.IO;
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
            N_Genero n_Genero = new N_Genero();
            List<Peliculas> peliculas = new List<Peliculas>();
            List<GenerosPelicula> generos = new List<GenerosPelicula>();
            try
            {
                generos = n_Genero.ObtenerGeneros();
                ViewBag.CatalogoGeneros = generos;
                peliculas = negocio.ObtnerPeliculas();
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = $"Error:{ex.Message}";
            }
            return View("Principal", peliculas);
        }
        public ActionResult VistaAgregar()
        {
            N_Genero negocio = new N_Genero();
            try
            {
                ViewBag.CatalogoGeneros = negocio.ObtenerGeneros();
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = $"Error:{ex.Message}";
                return View("VistaAgregar");
            }
            return View("AgregarPelicula");
        }
        public ActionResult AgregarPelicula(Peliculas pelicula, HttpPostedFileBase archivoImagen)
        {
            try
            {
                string rutaArchivo = Path.Combine(Server.MapPath("~/Imagenes"), archivoImagen.FileName);
                archivoImagen.SaveAs(rutaArchivo);

                N_Pelicula negocio = new N_Pelicula();
                pelicula.nombreImagen = archivoImagen.FileName;

                negocio.AgregarPelicula(pelicula);
                TempData["mensaje"] = $"La pelicula: {pelicula.nombre} se agrego correctamente";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = $"{ex.Message}";
                N_Genero negocioGenero = new N_Genero();
                List<GenerosPelicula> generos = negocioGenero.ObtenerGeneros();
                ViewBag.CatalogoGeneros = generos;
                return View("AgregarPelicula");
            }
        }
        public ActionResult VistaEditar(int ID)
        {
            N_Pelicula negocio = new N_Pelicula();
            Peliculas pelicula = new Peliculas();
            N_Genero n_Genero = new N_Genero();

            try
            {
                ViewBag.CatalogoGeneros = n_Genero.ObtenerGeneros();
                pelicula = negocio.ObtnerPelicula(ID);
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = $"Error:{ex.Message}";
            }
            return View("EditarPelicula", pelicula);
        }
        public ActionResult EditarPelicula(Peliculas pelicula, HttpPostedFileBase archivoImagen)
        {
            N_Pelicula n_Pelicula = new N_Pelicula();

            try
            {
                if (archivoImagen != null)
                {
                    string rutaArchivo = Path.Combine(Server.MapPath("~/Imagenes"), archivoImagen.FileName);
                    archivoImagen.SaveAs(rutaArchivo);
                    pelicula.nombreImagen = archivoImagen.FileName;
                }
                else
                {
                    pelicula.nombreImagen = n_Pelicula.ObtnerPelicula(pelicula.idPelicula).nombreImagen;
                }

                n_Pelicula.ActualizarPelicula(pelicula);
                TempData["mensaje"] = $"La pelicula: {pelicula.nombre} se actualizo correctamente";
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = $"Error:{ex.Message}";
                return View("VistaEditar", pelicula);
            }
            return RedirectToAction("Index");
        }
        public ActionResult VistaEliminar(int ID)
        {
            Peliculas pelicula = new Peliculas();
            N_Pelicula negocio = new N_Pelicula();
            try
            {
                pelicula = negocio.ObtnerPelicula(ID);
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = $"{ex.Message}";
                return View("INDEX");
            }
            return View("EliminarPelicula", pelicula);
        }
        public ActionResult EliminarPelicula(Peliculas pelicula, string Eliminar)
        {
            N_Pelicula negocio = new N_Pelicula();
            try
            {
                if (Eliminar == "Confirmar")
                {
                    negocio.EliminarPelicula(pelicula);
                    TempData["mensaje"] = $"La pelicula: {pelicula.nombre} se Elimino correctamente";
                }
                else
                {
                    TempData["mensaje"] = $"Cancelada operacion de eliminacion de la pelicula: {pelicula.nombre}";
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = $"{ex.Message}";
                return View("EliminarPelicula", pelicula);
            }
        }
        public ActionResult VistaGeneros()
        {
            N_Genero negocio = new N_Genero();
            List<GenerosPelicula> generos = new List<GenerosPelicula>();
            try
            {
                generos = negocio.ObtenerGeneros();
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = $"Ocurrio un error: { ex.Message}";
            }
            return View("GenerosPrincipal", generos);
        }
        public ActionResult VistaAgregarGenero()
        {
            return View("AgregarGenero");
        }
        public ActionResult AgregarGenero(GenerosPelicula entidadgenero)
        {
            try
            {
                N_Genero negocio = new N_Genero();
                negocio.AgregarGenero(entidadgenero);
                TempData["mensaje"] = $"El genero {entidadgenero.genero} ha sido agregado";
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = $"Error: {ex.Message}";
                return View("AgregarGenero");
            }
            return RedirectToAction("VistaGeneros");
        }

        public ActionResult VistaEditarGenero(int ID)
        {
            N_Genero negocio = new N_Genero();
            GenerosPelicula genero = negocio.ObtnerGenero(ID);
            List<GenerosPelicula> generos = negocio.ObtenerGeneros();
            ViewBag.CatalogoGeneros = generos;
            return View("EditarGenero", genero);
        }
        public ActionResult EditarGenero(GenerosPelicula entidadgenero)
        {
            N_Genero negocio = new N_Genero();
            try
            {
                negocio.ActualizarGenero(entidadgenero);
                TempData["mensaje"] = $"El genero {entidadgenero.genero} se actualizo correctamente";
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = $"Error: {ex.Message}";
                return View("EditarGenero", entidadgenero);
            }
            return RedirectToAction("VistaGeneros");
        }
        public ActionResult VistaEliminarGenero(int ID)
        {
            N_Genero negocio = new N_Genero();
            GenerosPelicula genero = negocio.ObtnerGenero(ID);
            return View("EliminarGenero", genero);
        }
        public ActionResult EliminarGenero(GenerosPelicula entidadgenero)
        {
            try
            {
                N_Genero negocio = new N_Genero();
                negocio.EliminarGenero(entidadgenero);
                TempData["mensaje"] = $"El genero {entidadgenero.genero} se elimino correctamente";
            }
            catch (Exception ex)
            {
                TempData["ERROR"] = $"Error: No puede eliminarse, ese genero esta asociado a peliculas";
                return View("EliminarGenero", entidadgenero);
            }
            return RedirectToAction("VistaGeneros");
        }
        public ActionResult Buscar(string Buscador)
        {
            List<Peliculas> lista = new List<Peliculas>();
            N_Pelicula negocio = new N_Pelicula();
            if (Buscador == "")
            {
                lista = negocio.ObtnerPeliculas();
            }
            else
            {
                lista = negocio.BuscadorGenero(Convert.ToInt16(Buscador));
            }


            N_Genero negocioGenero = new N_Genero();
            List<GenerosPelicula> generos = negocioGenero.ObtenerGeneros();
            ViewBag.CatalogoGeneros = generos;
            return View("Principal", lista);
        }
    }
}