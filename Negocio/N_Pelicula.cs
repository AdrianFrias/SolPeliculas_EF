using Datos;
using Datos.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class N_Pelicula
    {
        public List<Peliculas> ObtnerPeliculas()
        {
            D_Pelicula datos = new D_Pelicula();
            List<Peliculas> lista = datos.ReadPeliculas();
            return lista;
        }
        public Peliculas ObtnerPelicula(int ID)
        {
            D_Pelicula datos = new D_Pelicula();
            Peliculas pelicula = datos.ReadPelicula(ID);
            return pelicula;
        }
        public void AgregarPelicula(Peliculas pelicula)
        {
            D_Pelicula datos = new D_Pelicula();
            Peliculas obj = datos.ValidarGeneroID(pelicula.nombre, pelicula.idGeneroPelicula.Value);
            /*
            if (datos.ValidarPelicula(pelicula.nombre))
            {
                throw new Exception($"{pelicula.nombre} ya existe en la base de datos");
            }
            */
            if (obj != null)
            {
                throw new Exception($"{pelicula.nombre} con el genero {pelicula.idGeneroPelicula.Value} ya existe en la base de datos");
            }
            datos.CreatePelicula(pelicula);
        }
        public void ActualizarPelicula(Peliculas pelicula)
        {
            D_Pelicula datos = new D_Pelicula();
            datos.UpdatePelicula(pelicula);
        }
        public void EliminarPelicula(Peliculas pelicula)
        {
            D_Pelicula datos = new D_Pelicula();
            datos.DeletePelicula(pelicula.idPelicula);
        }
        public List<Peliculas> BuscadorGenero(int idgenero)
        {
            D_Pelicula datos = new D_Pelicula();
            List<Peliculas> coincidencias = new List<Peliculas>();
            coincidencias = datos.ReadBuscador(idgenero);
            return coincidencias;
        }
        public List<Peliculas> BuscadorNombre(string nombre)
        {
            D_Pelicula datos = new D_Pelicula();
            List<Peliculas> coincidencias = new List<Peliculas>();
            coincidencias = datos.ReadBuscadorNombre(nombre);
            return coincidencias;
        }
    }
}
