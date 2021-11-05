using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnvyLoaderSystem
{
    class Plugins
    {
        private string[] pluginsList;

        public Plugins()
        {
            loadPlugins();
        }

        public string[] PluginsList { get => pluginsList; }

        private void loadPlugins()
        {
            try
            {
                string data = File.ReadAllText($"{Environment.CurrentDirectory}/plugins.txt");
                if (data == string.Empty || !data.Contains(',')) throw new Exception("El Archivo 'plugins.txt' esta vacio o esta incorrecto");
                pluginsList = data.Split(',');
            }
            catch (FileNotFoundException e)
            {
                 var x = System.Windows.Forms.MessageBox.Show("No Se Pudo Encontrar El Archivo: " + e.FileName, e.Message, System.Windows.Forms.MessageBoxButtons.RetryCancel, System.Windows.Forms.MessageBoxIcon.Error);
                if(x.ToString() == "Retry") loadPlugins();
                Application.Exit();
            }
            catch(Exception e)
            {
                var x = System.Windows.Forms.MessageBox.Show(e.Message, e.Message, System.Windows.Forms.MessageBoxButtons.RetryCancel, System.Windows.Forms.MessageBoxIcon.Error);
                if (x.ToString() == "Retry") loadPlugins();
                Application.Exit();
            }
        }
    }

    
}
