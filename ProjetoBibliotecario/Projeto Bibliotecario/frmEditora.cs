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
    public partial class frmEditora : Form
    {
        string strcon = "Data Source=THAYRONE-PC;Initial Catalog=BDBiblio;Integrated Security=SSPI;";
        string acao = "";

        public frmEditora()
        {
            InitializeComponent();
        }

        private void selectall()
        {
            SqlConnection conn = new SqlConnection(strcon);
            conn.Open();
            string sql = "select * from TbEditora";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("CodEditora", "Codigo");
            dataGridView1.Columns.Add("NomeEditora", "Cidade");

            while (dr.Read())
                dataGridView1.Rows.Add(dr["CodEditora"], dr["NomeEditora"]);
            conn.Close();
        }

        private void inserir()
        {
            DialogResult confirmar = MessageBox.Show("Deseja adicionar " + txtEditora.Text + " ?", "Adcionar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection(strcon);
                conn.Open();
                string sql = "insert into TbEditora values (@editora)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@editora", txtEditora.Text));
                cmd.ExecuteNonQuery();

                selectall();
            }
        }

        private void alterar()
        {
            int linha = dataGridView1.CurrentRow.Index;
            string nome = dataGridView1.Rows[linha].Cells[1].Value.ToString();

            DialogResult confirmar = MessageBox.Show("Deseja alterar " + nome + " para " + txtEditora.Text + " ?", "Alterar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection(strcon);
                conn.Open();
                string sql = "update TbEditora set NomeEditora = @edi where NomeEditora = @editora";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@edi", txtEditora.Text));
                cmd.Parameters.Add(new SqlParameter("@editora", nome));
                cmd.ExecuteNonQuery();

                selectall();
            }
        }

        private void excluir()
        {
            int linha = dataGridView1.CurrentRow.Index;
            string cod = dataGridView1.Rows[linha].Cells[0].Value.ToString();
            string nome = dataGridView1.Rows[linha].Cells[1].Value.ToString();

            DialogResult confirmar = MessageBox.Show("Deseja excluir " + nome + " ?", "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmar == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection(strcon);
                conn.Open();
                string sql = "delete from TbEditora where CodEditora = @cod";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@cod", cod));
                cmd.ExecuteNonQuery();

                selectall();
            }

        }

        private void comum()
        {
            dataGridView1.ClearSelection();
            txtEditora.Enabled = false;
            txtEditora.Text = null;
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
                txtEditora.Text = dataGridView1.Rows[linha].Cells[1].Value.ToString();
            }
            else
            {
                lblCodigo2.Text = null;
                txtEditora.Text = null;
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmEditora_Load(object sender, EventArgs e)
        {
            frmEditora.ActiveForm.AcceptButton = btnNovo;
            frmEditora.ActiveForm.CancelButton = btnFechar;

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

            txtEditora.MaxLength = 35;

            txtEditora.Enabled = false;

            selectall();
        }

        private void btnNovo_Click(object sender, EventArgs e)
        {
            frmEditora.ActiveForm.AcceptButton = btnSalvar;
            frmEditora.ActiveForm.CancelButton = btnCancelar;

            acao = "i";

            dataGridView1.ClearSelection();
            dataGridView1.Enabled = false;

            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;

            txtEditora.Enabled = true;
            txtEditora.Text = null;
            lblCodigo2.Text = null;
            txtEditora.Focus();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            acao = "a";

            txtEditora.Enabled = true;
            txtEditora.Focus();
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
                if (txtEditora.Text != "")
                {
                    SqlConnection conn = new SqlConnection(strcon);
                    conn.Open();
                    string sql = "select * from TbEditora where NomeEditora = @edi";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@edi", txtEditora.Text));
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        MessageBox.Show("Editora já adicionada", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtEditora.Focus();
                    }
                    else
                    {
                        inserir();
                        comum();
                        selectall();
                    }
                }
                else
                {
                    MessageBox.Show("Digite uma editora", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtEditora.Focus();
                }
            }

            else if (acao == "a")
            {
                if (txtEditora.Text != "")
                {
                    SqlConnection conn = new SqlConnection(strcon);
                    conn.Open();
                    string sql = "select * from TbEditora where NomeEditora = @edi";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@edi", txtEditora.Text));
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        MessageBox.Show("Editora já adicionada", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtEditora.Focus();
                    }
                    else
                    {
                        alterar();
                        comum();
                        selectall();
                    }
                }
                else
                {
                    MessageBox.Show("Digite uma editora", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtEditora.Focus();
                }
            }
        }

        private void btmExcluir_Click(object sender, EventArgs e)
        {
            excluir();
            comum();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            frmEditora.ActiveForm.AcceptButton = btnNovo;
            frmEditora.ActiveForm.CancelButton = btnFechar;

            comum();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int linha = dataGridView1.CurrentRow.Index;
            lblCodigo2.Text = dataGridView1.Rows[linha].Cells[0].Value.ToString();
            txtEditora.Text = dataGridView1.Rows[linha].Cells[1].Value.ToString();

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