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
            T_Peliculas = db.Peliculas.Include("GenerosPelicula").ToList();
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
            Peliculas pelicula = db.Peliculas.Where(x => x.idPelicula == id).FirstOrDefault();
            db.Peliculas.Remove(pelicula);
            db.SaveChanges();
            db.Dispose();
        }
        public List<Peliculas> ReadBuscador(int idgenero)
        {
            List<Peliculas> T_Peliculas = new List<Peliculas>();
            T_Peliculas = db.Peliculas.Include("GenerosPelicula").Where(x => x.idGeneroPelicula == idgenero).ToList();
            return T_Peliculas;
        }
        public bool ValidarPelicula(string nombrepel)
        {
            bool flag = db.Peliculas.Any(x => x.nombre == nombrepel);
            return flag;
        }
    }
}
