using EnvyLoaderSystem.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EnvyLoaderSystem
{
    public partial class Form1 : Form
    {
        public static Form1 Instance;
        private TextBox[] textboxes;
        public DatabaseManager<Licencias> database;
        private FormAgregar formAgregar;
        private Licencias licencia;
        public Form1()
        {
            InitializeComponent();
            textboxes = new TextBox[] { textBox1, textBox2};
            database = new DatabaseManager<Licencias>($"{Environment.CurrentDirectory}/database.json");
            Instance = this;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = string.Empty;
            for (int x = 0; x < checkedListBox1.CheckedItems.Count; x++)
            {
                s += " " + checkedListBox1.CheckedItems[x].ToString();

            }

            if (this.licencia != null)
            {
                formAgregar = new FormAgregar(s, licencia.licencia, licencia.ip, licencia.puerto);
                formAgregar.Show();
            }
            else
            {
                formAgregar = new FormAgregar(s);
                formAgregar.Show();
            }

           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
                Plugins plugins = new Plugins();
                foreach (var item in plugins.PluginsList) checkedListBox1.Items.Add(item + "", false);
                foreach (TextBox textbox in textboxes) textbox.TextAlign = HorizontalAlignment.Center;
                database.Cargar();

            ThreadPool.QueueUserWorkItem(delegate (object i)
            {
                UpdateTable(database.Datos);
            });



        }

        public static void UpdateTable(List<Licencias> licencias)
        {
            Form1.Instance.dataGridView1.Rows.Clear();

            
           

            foreach (Licencias licencia in licencias)
            {
                string s = string.Empty;
                foreach (string item in licencia.plugins)
                {
                    s += item + "-";
                }
                DataGridViewRow fila = new DataGridViewRow();
                fila.Cells.Add(new DataGridViewTextBoxCell() { Value = licencia.licencia });
                fila.Cells.Add(new DataGridViewTextBoxCell() { Value = licencia.ip });
                fila.Cells.Add(new DataGridViewTextBoxCell() { Value = licencia.puerto });
                fila.Cells.Add(new DataGridViewTextBoxCell() { Value = s});
                Form1.Instance.dataGridView1.Rows.Add(fila);
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            ThreadPool.QueueUserWorkItem(delegate (object i) { var lista = database.Buscar(x => x.licencia.Contains(textBox3.Text)); UpdateTable(lista); });
                
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                Licencias licencia = database.Buscar(x => x.licencia == dataGridView1.CurrentRow.Cells[0].Value.ToString())[0];
                textBox1.Text = licencia.licencia;
                textBox2.Text = licencia.ip + ":" + licencia.puerto;

                this.licencia = licencia;
                for (int i = 0; i < checkedListBox1.Items.Count; i++)
                {
                        checkedListBox1.SetItemChecked(i, false);
                }

                foreach (string item in licencia.plugins)
                {
                    for (int i = 0; i < checkedListBox1.Items.Count; i++)
                    {
                        if (item.Equals(checkedListBox1.Items[i]))
                        {
                            checkedListBox1.SetItemChecked(i, true);
                        }
                    }
                }
                
            }
            catch {
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                database.Eliminar(x => x == this.licencia);
                UpdateTable(database.Datos);
            }
            catch 
            {
                MessageBox.Show("No Se Pudo Eliminar");
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            this.licencia = null;
        }
    }
}
