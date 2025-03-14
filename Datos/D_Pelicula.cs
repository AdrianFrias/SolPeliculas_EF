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

            //Para treaer toda la informacion solo de la tabla Peliculas
            //T_Peliculas = db.Peliculas.ToList();

            //Llamar la tabla pero con el INNER JOIN de generos
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
            db.Peliculas.Add(pelicula);
            db.SaveChanges();
            db.Dispose();
        }
        public void UpdatePelicula(Peliculas pelicula)
        {
            db.Peliculas.AddOrUpdate(pelicula);
            db.SaveChanges();
            db.Dispose();
        }
        public void DeletePelicula(int id)
        {
            Peliculas pelicula = db.Peliculas.Where(x => x.idPelicula == id).FirstOrDefault();
            db.Peliculas.Remove(pelicula);
            db.SaveChanges();
            db.Dispose();
        }
    }
}
