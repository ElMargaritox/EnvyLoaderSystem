using EnvyLoaderSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnvyLoaderSystem
{
    public partial class FormAgregar : Form
    {
        private string[] plugins;
        private string ip;
        private string port;
        private string licence;
        public FormAgregar(string plugins)
        {
            InitializeComponent();
            licence = Licencias.getRandomLicence();
            textBox3.Text = licence;

            this.plugins = plugins.Split(' ');

            foreach (var item in this.plugins) comboBox1.Items.Add(item);
        }

        public FormAgregar(string plugins, string licence, string ip, string port)
        {
            InitializeComponent();
            this.plugins = plugins.Split(' ');
            foreach (var item in this.plugins) comboBox1.Items.Add(item);
            this.licence = licence;
            this.ip = ip;
            this.port = port;
            textBox3.Text = licence;
            textBox2.Text = port;
            textBox1.Text = ip;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = string.Empty;
            for (int x = 0; x < comboBox1.Items.Count; x++)
            {
                s += comboBox1.Items[x].ToString() + "-";

            }

            try
            {
                if (!textBox1.Text.Contains('.')) throw new Exception("La Ip Es Invalída.");
                if (textBox2.Text.Length > 5) throw new Exception("El Puerto Es Invalido.");

                try
                {
                    Licencias licencia2_data = Form1.Instance.database.Buscar(x => x.licencia == licence)[0];

                    var j = MessageBox.Show("Ya Exíste Una Licencia, Quieres Cambiar Los Datós?", ":P", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (j.ToString() == "Yes")
                    {
                        licencia2_data.ip = textBox1.Text;
                        licencia2_data.plugins = this.plugins;
                        licencia2_data.puerto = textBox2.Text;
                        Form1.Instance.database.Actualizar(x => x.licencia == licence, licencia2_data);

                        MessageBox.Show("Se Actualizaron Los Datos");
                    }
                }
                catch
                {
                    Licencias licencia = new Licencias
                    {
                        licencia = licence,
                        ip = textBox1.Text,
                        puerto = textBox2.Text,
                        plugins = this.plugins
                    };
                    Form1.Instance.database.Insertar(licencia);
                    MessageBox.Show("Se Agrego La Licencia");
                }

            }
            catch (Exception f)
            {

                MessageBox.Show(f.Message, f.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            Form1.UpdateTable(Form1.Instance.database.Datos);



        }

        private void FormAgregar_Load(object sender, EventArgs e)
        {
         
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void FormAgregar_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form1.UpdateTable(Form1.Instance.database.Datos);
            this.Dispose();
        }
    }
}
