using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrudWF
{
    public partial class FormCadastrar : Form
    {
        public FormCadastrar()
        {
            InitializeComponent();
        }

        private void FormCadastrar_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBoxNumero_TextChanged(object sender, EventArgs e)
        {
            if (Regex.IsMatch(textBoxNumero.Text, "[^0-9]"))
            {
                MessageBox.Show("Por favor informe apenas números.");
                textBoxNumero.Text = textBoxNumero.Text.Remove(textBoxNumero.Text.Length - 1);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(
                textBoxNome.Text == "" ||
                textBoxSobrenome.Text == "" ||
                textBoxCpf.Text == "" ||
                dateTimePickerDataNascimento.Text == "" ||
                textBoxNumero.Text == "" ||
                textBoxComplemento.Text == "" ||
                textBoxCidade.Text == "" ||
                comboBoxEstado.Text == ""
            )
            {
                MessageBox.Show("Algum Campo está em Branco, favor verificar");
                return;
            }

            SqlConnection conn = new SqlConnection(
                ConfigurationManager
                .ConnectionStrings["CrudWF.Properties.Settings.sajerman_crudConnectionString"]
                .ConnectionString
            );
            var usuarios = new List<Usuario>();
            DateTime dataNascimento = Convert.ToDateTime(dateTimePickerDataNascimento.Text);
            string query = "insert into usuarios values (" +
                $"'{textBoxNome.Text}', " +
                $"'{textBoxSobrenome.Text}', " +
                $"'{textBoxCpf.Text}', " +
                "'" + dataNascimento.ToString("yyyy-MM-dd mm:ss") + "', " + 
                $"'{textBoxCep.Text}', " +
                $"'{textBoxEndereco.Text}', " +
                $"{textBoxNumero.Text}, " +
                $"'{textBoxComplemento.Text}', " +
                $"'{textBoxCidade.Text}', " +
                $"'{comboBoxEstado.Text.Trim()}')";
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = query;
                    command.ExecuteNonQuery();

                }
            }
            catch (Exception ee)
            {
                var message = ee.Message;
                MessageBox.Show("Algo de errado ocorreu!");
            }
            finally
            {
                conn.Close();
            }
            MessageBox.Show("Usuário Cadastrado");
            this.Close();
        }
    }
}
