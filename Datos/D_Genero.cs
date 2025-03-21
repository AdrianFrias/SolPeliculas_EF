using Datos.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos
{
    public class D_Genero
    {
        generacion31Entities db = new generacion31Entities();
        public List<GenerosPelicula> ReadGeneros()
        {
            List<GenerosPelicula> T_Generos = new List<GenerosPelicula>();
            T_Generos = db.GenerosPelicula.ToList();
            return T_Generos;
        }
        public GenerosPelicula ReadGenero(int ID)
        {
            GenerosPelicula T_Generos = db.GenerosPelicula.Where(x => x.idGeneroPelicula == ID).FirstOrDefault();
            return T_Generos;
        }
        public void CreateGenero(GenerosPelicula genero)
        {
            try
            {
                db.GenerosPelicula.Add(genero);
                db.SaveChanges();
                db.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateGenero(GenerosPelicula genero)
        {
            try
            {
                db.GenerosPelicula.AddOrUpdate(genero);
                db.SaveChanges();
                db.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteGenero(int id)
        {
            GenerosPelicula genero = db.GenerosPelicula.Where(x => x.idGeneroPelicula == id).FirstOrDefault();
            db.GenerosPelicula.Remove(genero);
            db.SaveChanges();
            db.Dispose();
        }
        public bool ValidarGenero(string nombregen)
        {
            bool flag = db.GenerosPelicula.Any(x => x.genero == nombregen);
            return flag;
        }
        
    }
}
