using Datos.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class D_Pelicula
    {
        generacion31Entities db = new generacion31Entities();
        public List<Peliculas> ReadPeliculas()
        {
            List<Peliculas> T_Peliculas = new List<Peliculas>();
            List<spObtenerTodosPeliculas_Result> splista = new List<spObtenerTodosPeliculas_Result>();

            splista = db.spObtenerTodosPeliculas().ToList();
            foreach(spObtenerTodosPeliculas_Result obj in splista)
            {
                Peliculas p = new Peliculas();
                GenerosPelicula g = new GenerosPelicula();
                p.idPelicula = obj.idPelicula;
                p.nombre = obj.nombre;
                p.fechalanzamiento = obj.fechalanzamiento;
                p.idGeneroPelicula = obj.idGeneroPelicula;
                p.nombreImagen = obj.nombreImagen;
                p.idPelicula = obj.idPelicula;
                g.idGeneroPelicula = obj.idGeneroPelicula;
                g.genero = obj.genero;
                p.GenerosPelicula = g;
                T_Peliculas.Add(p);
            }

            //T_Peliculas = db.Peliculas.Include("GenerosPelicula").ToList();
            return T_Peliculas;
        }
        public Peliculas ReadPelicula(int ID)
        {
            Peliculas T_peliculas = db.Peliculas.Where(x => x.idPelicula == ID).FirstOrDefault();
            return T_peliculas;
        }
        public void CreatePelicula(Peliculas pelicula)
        {
            try
            {
                db.Peliculas.Add(pelicula);
                db.SaveChanges();
                db.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdatePelicula(Peliculas pelicula)
        {
            try
            {
                db.Peliculas.AddOrUpdate(pelicula);
                db.SaveChanges();
                db.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeletePelicula(int id)
        {

            //Peliculas pelicula = db.Peliculas.Where(x => x.idPelicula == id).FirstOrDefault();
            db.spEliminarPelicula(id);

            db.SaveChanges();
            db.Dispose();
            
        }
        public List<Peliculas> ReadBuscador(int idgenero)
        {
            List<Peliculas> T_Peliculas = new List<Peliculas>();
            T_Peliculas = db.Peliculas.Include("GenerosPelicula").Where(x => x.idGeneroPelicula == idgenero).ToList();
            return T_Peliculas;
        }
        public List<Peliculas> ReadBuscadorNombre(string buscar)
        {
            List<Peliculas> T_Peliculas = new List<Peliculas>();
            T_Peliculas = db.Peliculas.Include("GenerosPelicula").Where(x => x.nombre.Contains(buscar)||x.GenerosPelicula.genero.Contains(buscar)).ToList();
            return T_Peliculas;
        }
        public bool ValidarPelicula(string nombrepel)
        {
            bool flag = db.Peliculas.Any(x => x.nombre == nombrepel);
            return flag;
        }
        public Peliculas ValidarGeneroID(string nombregen, int ID)
        {
            return db.Peliculas.Where(x => x.nombre == nombregen && x.idGeneroPelicula == ID).FirstOrDefault();
        }
    }
}
