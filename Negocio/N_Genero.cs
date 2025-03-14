using Datos.Model;
using Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class N_Genero
    {
        public List<GenerosPelicula> ObtenerGeneros()
        {
            D_Genero datos = new D_Genero();
            List<GenerosPelicula> generos = new List<GenerosPelicula>();
            generos = datos.ReadGeneros();
            return generos;
        }
        public GenerosPelicula ObtnerGenero(int ID)
        {
            D_Genero datos = new D_Genero();
            GenerosPelicula genero = datos.ReadGenero(ID);
            return genero;
        }
        public void AgregarGenero(GenerosPelicula genero)
        {
            D_Genero datos = new D_Genero();
            if (datos.ValidarGenero(genero.genero))
            {
                //Mandara esta excepcion al controlador
                throw new Exception($"{genero.genero} ya existe en la base de datos");
            }
            datos.CreateGenero(genero);
        }
        public void ActualizarGenero(GenerosPelicula genero)
        {
            D_Genero datos = new D_Genero();
            datos.UpdateGenero(genero);
        }
        public void EliminarGenero(GenerosPelicula genero)
        {
            D_Genero datos = new D_Genero();
            datos.DeleteGenero(genero.idGeneroPelicula);
        }

    }
}
