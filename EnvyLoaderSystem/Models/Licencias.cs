using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnvyLoaderSystem.Models
{
    public class Licencias
    {
        public string licencia;
        public string ip;
        public string puerto;
        public string[] plugins;
        public static string getRandomLicence()
        {
            Random rand = new Random();

            int i = 0;
            int numero;
            int numero2;
            char letra;
            char letra2;
            string key = string.Empty;

            while (i < 4)
            {
                numero = rand.Next(9);
                numero2 = rand.Next(10, 26);
                letra = (char)(((int)'A') + numero);
                letra2 = (char)(((int)'A') + numero2);

                if (i == 3) { key = key + letra.ToString().ToUpper() + numero2 + letra2.ToString().ToUpper(); break; }
                key = letra.ToString().ToUpper() + numero2 + letra2.ToString().ToUpper() + "-" + key;
                i++;

            }

            return key;
        }
    }

    
}
