using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Projeto_Bibliotecario //Thayrone Batista
{
    public partial class frmAutor : Form
    {
        //string strcon = "Data Source=MCRF02009;Initial Catalog=BDBiblio;Integrated Security=SSPI;";
        string strcon = "Data Source=THAYRONE-PC;Initial Catalog=BDBiblio;Integrated Security=SSPI;";
        string acao = "";
        public frmAutor()
        {
            InitializeComponent();
        }

        private void SelectAll()
        {
            SqlConnection conn = new SqlConnection(strcon);
            conn.Open();
            string sql = "select * from TbAutor";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("CodAutor", "Codigo");
            dataGridView1.Columns.Add("NomeAutor", "Autor");

            while (dr.Read())
                dataGridView1.Rows.Add(dr["CodAutor"], dr["NomeAutor"]);
            conn.Close();
        }

        private void Atualizar()
        {
            int linha = dataGridView1.CurrentRow.Index;
            string nome = dataGridView1.Rows[linha].Cells[1].Value.ToString();
            DialogResult confirmar = MessageBox.Show("Deseja alterar " + nome + " para " + txtAutor.Text + " ? ",
                                                      "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection(strcon);
                conn.Open();
                string sql = "update TbAutor set NomeAutor = @autor where NomeAutor = @aut";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@autor", txtAutor.Text));
                cmd.Parameters.Add(new SqlParameter("@aut", dataGridView1.Rows[linha].Cells[1].Value.ToString()));
                cmd.ExecuteNonQuery();
                conn.Close();
                SelectAll();
            }
        }

        private void Inserir()
        {
            DialogResult confirmar = MessageBox.Show("Deseja adicionar " + txtAutor.Text + " ?", "Adicionar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection(strcon);
                conn.Open();
                string sql = "insert into TbAutor values (@autor)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@autor", txtAutor.Text));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Adicionado com sucesso", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAutor.Text = null;
                txtAutor.Focus();
                SelectAll();
            }

        }

        private void Delete()
        {
            int linha = dataGridView1.CurrentRow.Index;
            string nome = dataGridView1.Rows[linha].Cells[1].Value.ToString();
            DialogResult confirmar = MessageBox.Show("Deseja excluir " + nome + " ?", "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (confirmar == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection(strcon);
                conn.Open();
                string sql = "delete from TbAutor where NomeAutor = @autor";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@autor", nome));
                cmd.ExecuteNonQuery();
                MessageBox.Show("Removido com sucesso", "Excluido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAutor.Text = null;
                txtAutor.Focus();
                SelectAll();
            }
        }

        private void ChangeData()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToResizeColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.BackgroundColor = System.Drawing.Color.White;

        }

        private void Comum()
        {
            dataGridView1.ClearSelection();
            txtAutor.Enabled = false;
            txtAutor.Text = null;
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
                txtAutor.Text = dataGridView1.Rows[linha].Cells[1].Value.ToString();
            }
            else
            {
                lblCodigo2.Text = null;
                txtAutor.Text = null;
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnNovo_Click_1(object sender, EventArgs e)
        {
            acao = "i";
            dataGridView1.ClearSelection();
            lblCodigo2.Text = null;
            txtAutor.Enabled = true;
            txtAutor.Focus();
            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            dataGridView1.Enabled = false;
            frmAutor.ActiveForm.AcceptButton = btnSalvar;
            frmAutor.ActiveForm.CancelButton = btnCancelar;
            txtAutor.Text = null;
        }

        private void frmAutor_Load(object sender, EventArgs e)
        {
            txtAutor.MaxLength = 80;
            txtAutor.Focus();
            SelectAll();
            btnCancelar.Enabled = false;
            btnSalvar.Enabled = false;
            txtAutor.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            ChangeData();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (acao == "i")
            {

                SqlConnection conn = new SqlConnection(strcon);
                conn.Open();
                string sql = "select * from TbAutor where NomeAutor = @autor";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@autor", txtAutor.Text));
                SqlDataReader dr;
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    MessageBox.Show("Autor já existe", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAutor.Focus();
                }
                else
                {
                    if (txtAutor.Text != "")
                    {
                        btnAlterar.Enabled = true;
                        dataGridView1.Enabled = true;
                        Inserir();
                        Comum();

                    }
                    else
                    {
                        MessageBox.Show("Insira o nome do autor", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtAutor.Focus();
                    }
                }
            }
            else if (acao == "a")
            {
                SqlConnection conn = new SqlConnection(strcon);
                conn.Open();
                string sql = "select * from TbAutor where NomeAutor = @autor";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@autor", txtAutor.Text));
                SqlDataReader dr;
                dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    MessageBox.Show("Autor já existe", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtAutor.Focus();
                }
                else
                {
                    if (txtAutor.Text != "")
                    {
                        dataGridView1.Enabled = true;
                        Atualizar();
                        Comum();

                    }
                    else
                    {
                        MessageBox.Show("Insira o nome do autor", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        txtAutor.Focus();
                    }
                }
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            btnNovo.Enabled = true;
            dataGridView1.Enabled = true;
            Comum();
            frmAutor.ActiveForm.AcceptButton = btnNovo;
            frmAutor.ActiveForm.CancelButton = btnFechar;
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            acao = "a";
            btnExcluir.Enabled = false;
            btnNovo.Enabled = false;
            btnSalvar.Enabled = true;
            txtAutor.Enabled = true;
            txtAutor.Focus();
            dataGridView1.Enabled = false;
            btnAlterar.Enabled = false;
            frmAutor.ActiveForm.AcceptButton = btnSalvar;
            frmAutor.ActiveForm.CancelButton = btnCancelar;

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            Delete();
            Comum();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int linha = dataGridView1.CurrentRow.Index;
            lblCodigo2.Text = dataGridView1.Rows[linha].Cells[0].Value.ToString();
            txtAutor.Text = dataGridView1.Rows[linha].Cells[1].Value.ToString();

            int sel = Convert.ToInt16(dataGridView1.SelectedCells.Count.ToString());
            if (sel != 0)
            {
                btnAlterar.Enabled = true;
                btnExcluir.Enabled = true;
                txtAutor.Focus();
                btnCancelar.Enabled = true;
                btnSalvar.Enabled = false;
            }
            else
            {
                btnAlterar.Enabled = false;
                btnExcluir.Enabled = false;
            }

        }
    }
}