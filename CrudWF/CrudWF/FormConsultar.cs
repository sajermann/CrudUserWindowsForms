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
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CrudWF
{
    public partial class FormConsultar : Form
    {
        public FormConsultar()
        {
            InitializeComponent();
        }

        private void FormConsultar_Load(object sender, EventArgs e)
        {
            SqlConnection conn = new SqlConnection(
                ConfigurationManager
                .ConnectionStrings["CrudWF.Properties.Settings.sajerman_crudConnectionString"]
                .ConnectionString
            );
            var usuarios = new List<Usuario>();

            string query = "select * from Usuarios";
            
            try
            {
                conn.Open();
                using (var command = conn.CreateCommand())
                {
                    command.CommandText = query;
                    DbDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var usuario = new Usuario
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nome = reader["nome"].ToString().Trim(),
                                Sobrenome = reader["sobrenome"].ToString().Trim(),
                                Cpf = reader["cpf"].ToString().Trim(),
                                DataNascimento = Convert.ToDateTime(reader["dataNascimento"].ToString().Trim()),
                                Cep = reader["cpf"].ToString().Trim(),
                                Endereco = reader["endereco"].ToString().Trim(),
                                Numero = Convert.ToInt32(reader["numero"]),
                                Complemento = reader["complemento"].ToString().Trim(),
                                Cidade = reader["cidade"].ToString().Trim(),
                                Estado = reader["estado"].ToString().Trim()
                            };
                            usuarios.Add(usuario);
                        }
                    }
                    reader.Dispose();
                }
            }
            catch (Exception ee)
            {
                var message = ee.Message;
            }
            finally
            {
                conn.Close();
            }

            foreach(var item in usuarios)
            {
                var nomeCompleto = $"{item.Nome} {item.Sobrenome}";
                var cpf = $"{item.Cpf}";
                var dataNasc = $"{item.DataNascimento.ToString("dd/MM/yyyy")}";
                var enderecoCompleto = $"{item.Endereco},{item.Numero}, {item.Complemento} - {item.Cidade}/{item.Estado} - {item.Cep} ";
                dataGridView2.Rows.Add(new object[] { nomeCompleto, cpf, dataNasc, enderecoCompleto });
            }
            

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormConsultar_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Form1.button2.Text = "Listar";
        }
    }
}
