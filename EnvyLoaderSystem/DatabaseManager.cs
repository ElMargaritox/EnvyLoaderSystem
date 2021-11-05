using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvyLoaderSystem
{
    public class DatabaseManager<T>
    {
        private List<T> datos = new List<T>();

        private string ruta;

        public List<T> Datos { get => datos; }

        public DatabaseManager(string r)
        {
            this.ruta = r;
        }

        // Serializa El Json
        public void Guardar()
        {
            string texto = JsonConvert.SerializeObject(datos, Formatting.Indented);
            File.WriteAllText(ruta, texto);

        }

        // Deserializa el Json
        public void Cargar()
        {
            try
            {
                string archivo = File.ReadAllText(ruta);  datos = JsonConvert.DeserializeObject<List<T>>(archivo);
            }
            catch  { } // No Muestra Error Por Si El Archivo No Exíste.
        }

        public void Insertar(T Nuevo)
        {
            datos.Add(Nuevo);
            Guardar();
        }

        public List<T> Buscar(Func<T, bool> criterio)
        {
            return datos.Where(criterio).ToList();
        }

        public void Actualizar(Func<T, bool> criterio, T nuevo)
        {
            datos = datos.Select(x =>
            {
                if (criterio(x)) x = nuevo;
                return x;
            }).ToList();

            Guardar();
        }


        public void Eliminar(Func<T, bool> criterio)
        {
            datos = datos.Where(x => !criterio(x)).ToList();
            Guardar();
        }

    }
}
