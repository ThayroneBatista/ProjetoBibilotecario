using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Projeto_Bibliotecario //Thayrone Batista
{
    public partial class frmEstado : Form
    {
        string strcon = "Data Source=THAYRONE-PC;Initial Catalog=BDBiblio;Integrated Security=SSPI;";
        
        string acao = "";
        
        public frmEstado()
        {
            InitializeComponent();
        }

        private void selectall()
        {
            SqlConnection conn = new SqlConnection(strcon);
            conn.Open();
            string sql = "select * from TbEstado";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("CodEstado", "Codigo");
            dataGridView1.Columns.Add("NomeEstado", "Estado");
            dataGridView1.Columns.Add("UFEstado", "UF");

            while (dr.Read())
                dataGridView1.Rows.Add(dr["CodEstado"], dr["NomeEstado"], dr["UFEstado"]);
                conn.Close();

        }

        private void excluir()
        {
            int linha = dataGridView1.CurrentRow.Index;
            string cod = dataGridView1.Rows[linha].Cells[0].Value.ToString();
            string nome = dataGridView1.Rows[linha].Cells[1].Value.ToString();
            string uf = dataGridView1.Rows[linha].Cells[2].Value.ToString();

            DialogResult confirmar = MessageBox.Show("Deseja excluir " + nome + "(" + uf + ") ?", "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection(strcon);
                conn.Open();
                string sql = "delete from TbEstado where CodEstado = @cod";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@cod", cod));
                cmd.ExecuteNonQuery();

                selectall();
            }
        }

        private void alterar()
        {
            int linha = dataGridView1.CurrentRow.Index;
            string cod = dataGridView1.Rows[linha].Cells[0].Value.ToString();
            string nome = dataGridView1.Rows[linha].Cells[1].Value.ToString();
            string uf = dataGridView1.Rows[linha].Cells[2].Value.ToString();

            DialogResult confirmar = MessageBox.Show("Deseja alterar " + nome + "(" + uf + ") para " + txtNome.Text + "(" + txtUF.Text + ") ?", "Alterar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection(strcon);
                conn.Open();
                string sql = "update TbEstado set NomeEstado = @estado, UFEstado = @uf where CodEstado = @cod";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@estado", txtNome.Text));
                cmd.Parameters.Add(new SqlParameter("@uf", txtUF.Text));
                cmd.Parameters.Add(new SqlParameter("@cod", cod));
                cmd.ExecuteNonQuery();

                selectall();
            }
        }

        private void inserir()
        {
            DialogResult confirmar = MessageBox.Show("Deseja adicionar "+txtNome.Text+"("+txtUF.Text+")", "Adicionar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection(strcon);
                conn.Open();
                string sql = "insert into TbEstado values (@estado, @uf)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@estado", txtNome.Text));
                cmd.Parameters.Add(new SqlParameter("@uf", txtUF.Text));
                cmd.ExecuteNonQuery();

                selectall();
            }
         
        }

        private void comum()
        {
            dataGridView1.ClearSelection();
            txtNome.Enabled = false;
            txtUF.Enabled = false;
            txtNome.Text = null;
            txtUF.Text = null;
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnNovo.Enabled = true;
            dataGridView1.Enabled = true;

            if (dataGridView1.RowCount > 0)
            {
                int linha = dataGridView1.CurrentRow.Index;
                lblCodigo2.Text = dataGridView1.Rows[linha].Cells[0].Value.ToString();
                txtNome.Text = dataGridView1.Rows[linha].Cells[1].Value.ToString();
                txtUF.Text = dataGridView1.Rows[linha].Cells[2].Value.ToString();

            }
            else
            {
                lblCodigo2.Text = null;
                txtNome.Text = null;
                txtUF.Text = null;
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmEstado_Load(object sender, EventArgs e)
        {
            frmEstado.ActiveForm.AcceptButton = btnNovo;
            frmEstado.ActiveForm.CancelButton = btnFechar;
            
            dataGridView1.AllowDrop = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.BackgroundColor = System.Drawing.Color.White;
            
            btnSalvar.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnCancelar.Enabled = false;

            txtNome.MaxLength = 35;
            txtUF.MaxLength = 2;

            txtNome.Enabled = false;
            txtUF.Enabled = false;

            selectall();
        }

        private void btnNovo_Click_1(object sender, EventArgs e)
        {
            acao = "i";

            frmEstado.ActiveForm.AcceptButton = btnSalvar;
            frmEstado.ActiveForm.CancelButton = btnCancelar;

            dataGridView1.ClearSelection();
            dataGridView1.Enabled = false;

            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            btnExcluir.Enabled = false;
            btnAlterar.Enabled = false;

            txtNome.Enabled = true;
            txtUF.Enabled = true;

            lblCodigo2.Text = null;
            txtNome.Text = null;
            txtUF.Text = null;

            txtNome.Focus();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (acao == "a")
            {
                if (txtNome.Text == "" || txtUF.Text == "")
                {
                    MessageBox.Show("Preencha todos os campos", "Campo vazio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    
                    txtNome.Focus();
                }
                else
                {
                    SqlConnection conn = new SqlConnection(strcon);
                    conn.Open();
                    string sql = "select * from TbEstado where NomeEstado = @estado and UFEstado = @uf";
                    SqlCommand cmd = new SqlCommand(sql,conn);
                    cmd.Parameters.Add(new SqlParameter("@estado",txtNome.Text));
                    cmd.Parameters.Add(new SqlParameter("@uf", txtUF.Text));
                    SqlDataReader dr = cmd.ExecuteReader();
                  
                    if (dr.Read())
                    {
                        MessageBox.Show("Estado ou UF duplicados", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        alterar();
                        comum();
                        selectall();
                    }
                }
            }

            else if (acao == "i")
            {
                if (txtNome.Text == "" || txtUF.Text == "")
                {
                    MessageBox.Show("Preencha todos os campos", "Campo vazio", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtNome.Focus();
                }
                else
                {
                    SqlConnection conn = new SqlConnection(strcon);
                    conn.Open();
                    string sql = "select * from TbEstado where NomeEstado = @estado and UFEstado = @uf";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@estado", txtNome.Text));
                    cmd.Parameters.Add(new SqlParameter("@uf", txtUF.Text));
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        MessageBox.Show("Estado ou UF duplicados", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        inserir();
                        comum();
                        selectall();
                    }
                }
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            acao = "a";

            dataGridView1.Enabled = false;

            txtUF.Enabled = true;
            txtNome.Enabled = true;
            txtNome.Focus();
            txtNome.Text = null;
            txtUF.Text = null;

            btnSalvar.Enabled = true;
            btnExcluir.Enabled = false;
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            excluir();
            comum();
            selectall();
        }

        private void btnCancelar_Click_1(object sender, EventArgs e)
        {
            frmEstado.ActiveForm.AcceptButton = btnNovo;
            frmEstado.ActiveForm.CancelButton = btnFechar;

            comum();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int linha = dataGridView1.CurrentRow.Index;
            
            lblCodigo2.Text = dataGridView1.Rows[linha].Cells[0].Value.ToString();
            txtNome.Text = dataGridView1.Rows[linha].Cells[1].Value.ToString();
            txtUF.Text = dataGridView1.Rows[linha].Cells[2].Value.ToString();

            if (dataGridView1.SelectedRows.Count == 0)
            {
                btnAlterar.Enabled = true;
                btnExcluir.Enabled = true;
                btnCancelar.Enabled = true;
            }

            else
            {
                btnAlterar.Enabled = false;
                btnExcluir.Enabled = false;
                btnCancelar.Enabled = false;
            }
        }
    }
}