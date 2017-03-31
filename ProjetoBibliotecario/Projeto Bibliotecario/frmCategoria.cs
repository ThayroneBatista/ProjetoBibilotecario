using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Projeto_Bibliotecario
{
    public partial class frmCategoria : Form
    {
        string strcon = "Data Source=THAYRONE-PC;Initial Catalog=BDBiblio;Integrated Security=SSPI;";
        string acao = "";

        public frmCategoria()
        {
            InitializeComponent();
        }

        private void selectall()
        {
            SqlConnection conn = new SqlConnection(strcon);
            conn.Open();
            string sql = "select * from TbCategoria";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("CodCategoria", "Codigo");
            dataGridView1.Columns.Add("NomeCategoria", "Nome");

            while (dr.Read())
                dataGridView1.Rows.Add(dr["CodCategoria"], dr["NomeCategoria"]);
            conn.Close();
        }

        private void inserir()
        {
            DialogResult confirmar = MessageBox.Show("Deseja adicionar "+txtCategoria.Text+" ?", "Adcionar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection(strcon);
                conn.Open();
                string sql = "insert into TbCategoria values (@categoria)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@categoria", txtCategoria.Text));
                cmd.ExecuteNonQuery();

                selectall();
            }
        }

        private void alterar()
        {
            int linha = dataGridView1.CurrentRow.Index;
            string nome = dataGridView1.Rows[linha].Cells[1].Value.ToString();

            DialogResult confirmar = MessageBox.Show("Deseja alterar "+nome+" para "+txtCategoria.Text+" ?", "Alterar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection(strcon);
                conn.Open();
                string sql = "update TbCategoria set NomeCategoria = @cat where NomeCategoria = @categoria";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@cat", txtCategoria.Text));
                cmd.Parameters.Add(new SqlParameter("@categoria", nome));
                cmd.ExecuteNonQuery();

                selectall();
            }
        }

        private void excluir()
        {
            int linha = dataGridView1.CurrentRow.Index;
            string cod = dataGridView1.Rows[linha].Cells[0].Value.ToString();
            string nome = dataGridView1.Rows[linha].Cells[1].Value.ToString();

            DialogResult confirmar = MessageBox.Show("Deseja excluir "+nome+" ?", "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmar == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection(strcon);
                conn.Open();
                string sql = "delete from TbCategoria where CodCategoria = @cod";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@cod", cod));
                cmd.ExecuteNonQuery();

                selectall();
            }
            
        }

        private void comum()
        {
            dataGridView1.ClearSelection();
            txtCategoria.Enabled = false;
            txtCategoria.Text = null;
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
                txtCategoria.Text = dataGridView1.Rows[linha].Cells[1].Value.ToString();
            }
            else
            {
                lblCodigo2.Text = null;
                txtCategoria.Text = null;
            }
        }
        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmCategoria_Load(object sender, EventArgs e)
        {
            frmCategoria.ActiveForm.AcceptButton = btnNovo;
            frmCategoria.ActiveForm.CancelButton = btnFechar;

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

            txtCategoria.MaxLength = 35;
            
            txtCategoria.Enabled = false;
            
            selectall();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            frmCategoria.ActiveForm.AcceptButton = btnSalvar;
            frmCategoria.ActiveForm.CancelButton = btnCancelar;

            acao = "i";

            dataGridView1.ClearSelection();
            dataGridView1.Enabled = false;

            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;

            txtCategoria.Enabled = true;
            txtCategoria.Text = null;
            lblCodigo2.Text = null;
            txtCategoria.Focus();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            acao = "a";

            txtCategoria.Enabled = true;
            txtCategoria.Focus();
            btnSalvar.Enabled = true;

            dataGridView1.Enabled = false;

            btnNovo.Enabled = false;
            btnExcluir.Enabled = false;
            btnAlterar.Enabled = false;
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (acao == "i")
            {
                if (txtCategoria.Text != "")
                {
                    SqlConnection conn = new SqlConnection(strcon);
                    conn.Open();
                    string sql = "select * from TbCategoria where NomeCategoria = @cat";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@cat",txtCategoria.Text));
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        MessageBox.Show("Categoria já adicionada", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCategoria.Focus();
                    }
                    else
                    {
                        inserir();
                        comum();
                    }
                }
                else
                {
                    MessageBox.Show("Digite uma categoria","Invalido",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
                    txtCategoria.Focus();
                }
            }

            else if (acao == "a")
            {
                if (txtCategoria.Text != "")
                {
                    SqlConnection conn = new SqlConnection(strcon);
                    conn.Open();
                    string sql = "select * from TbCategoria where NomeCategoria = @cat";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@cat", txtCategoria.Text));
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        MessageBox.Show("Categoria já adicionada", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCategoria.Focus();
                    }
                    else
                    {
                        alterar();
                        comum();
                    }
                }
                else
                {
                    MessageBox.Show("Digite uma categoria", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCategoria.Focus();
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            excluir();
            comum();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            frmCategoria.ActiveForm.AcceptButton = btnNovo;
            frmCategoria.ActiveForm.CancelButton = btnFechar;

            comum();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int linha = dataGridView1.CurrentRow.Index;
            lblCodigo2.Text = dataGridView1.Rows[linha].Cells[0].Value.ToString();
            txtCategoria.Text = dataGridView1.Rows[linha].Cells[1].Value.ToString();

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
            }
        }

    }
}