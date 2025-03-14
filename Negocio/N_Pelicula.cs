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
    }
}
