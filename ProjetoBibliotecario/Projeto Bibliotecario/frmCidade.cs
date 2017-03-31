using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace Projeto_Bibliotecario
{
    public partial class frmCidade : Form
    {
        string strcon = "Data Source=THAYRONE-PC;Initial Catalog=BDBiblio;Integrated Security=SSPI;";
        string acao = "";

        public frmCidade()
        {
            InitializeComponent();
        }

        private void selectall()
        {
            SqlConnection conn = new SqlConnection(strcon);
            conn.Open();
            string sql = "select TbCidade.*, TbEstado.NomeEstado from "+
            "tbcidade inner join TbEstado on (TbCidade.CodEstado=TbEstado.CodEstado)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr =  cmd.ExecuteReader();

            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("CodCidade", "CodigoCidade");
            dataGridView1.Columns.Add("NomeCidade", "Cidade");
            dataGridView1.Columns.Add("CodEstado", "CodigoEstado");
            dataGridView1.Columns.Add("NomeEstado", "Estado");

            while (dr.Read())
                dataGridView1.Rows.Add(dr["CodCidade"], dr["NomeCidade"], dr["CodEstado"], dr["NomeEstado"]);
            conn.Close();
        }

        private void inserir()
        {
            DialogResult confirmar = MessageBox.Show("Deseja adicionar "+txtCidade.Text+" ?", "Adcionar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection(strcon);
                conn.Open();
                string sql = "insert into TbCidade values (@cidade,@estado)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@cidade", txtCidade.Text));
                cmd.Parameters.Add(new SqlParameter("@estado", cboEstado.SelectedValue));
                cmd.ExecuteNonQuery();

                selectall();
            }
        }

        private void alterar()
        {
            int linha = dataGridView1.CurrentRow.Index;
            string codcidade = dataGridView1.Rows[linha].Cells[0].Value.ToString();

            DialogResult confirmar = MessageBox.Show("Deseja salvar as alteraraçoes acima ?", "Alterar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection(strcon);
                conn.Open();
                string sql = "update TbCidade set NomeCidade = @cid, CodEstado = @cod where CodCidade = @codcidade";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@cid", txtCidade.Text));
                cmd.Parameters.Add(new SqlParameter("@cod", cboEstado.SelectedValue));
                cmd.Parameters.Add(new SqlParameter("@codcidade", codcidade));
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
                string sql = "delete from TbCidade where CodCidade = @cod";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@cod", cod));
                cmd.ExecuteNonQuery();

                selectall();
                carregar();
            }
        }

        protected void carregar()
        {
            SqlConnection conn = new SqlConnection(strcon);
            conn.Open();

            try
            {
                //string sql = "select distinct TbCidade.CodEstado, TbEstado.NomeEstado from " +
                //"tbcidade inner join TbEstado on (TbCidade.CodEstado=TbEstado.CodEstado)";
                string sql = "select CodEstado,NomeEstado from TbEstado";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(dr);

                this.cboEstado.DataSource = table;
                this.cboEstado.DisplayMember = "NomeEstado";
                this.cboEstado.ValueMember = "CodEstado";

                dr.Close();
                dr.Dispose();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
        }

        private void comum()
        {
            dataGridView1.ClearSelection();
            cboEstado.Enabled = false;
            txtCidade.Enabled = false;
            txtCidade.Text = null;
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
                txtCidade.Text = dataGridView1.Rows[linha].Cells[1].Value.ToString();
                cboEstado.SelectedValue = dataGridView1.Rows[linha].Cells[2].Value;
            }
            else
            {
                lblCodigo2.Text = null;
                txtCidade.Text = null;
                cboEstado.SelectedIndex = -1;
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmCidade_Load(object sender, EventArgs e)
        {
            frmCidade.ActiveForm.AcceptButton = btnNovo;
            frmCidade.ActiveForm.CancelButton = btnFechar;

            dataGridView1.AllowDrop = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.MultiSelect = false;
            dataGridView1.ReadOnly = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.BackgroundColor = System.Drawing.Color.White;

            btnSalvar.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnCancelar.Enabled = false;

            txtCidade.MaxLength = 35;

            txtCidade.Enabled = false;
            cboEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEstado.Enabled = false;

            selectall();
            carregar();

            if (dataGridView1.RowCount != 0)
            {
                cboEstado.SelectedValue = dataGridView1.Rows[0].Cells[2].Value;
            }
            else
            {
                cboEstado.SelectedIndex = -1;
            }
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

            cboEstado.Enabled = true;
            txtCidade.Enabled = true;
            cboEstado.Enabled = true;

            cboEstado.SelectedIndex = -1;
            cboEstado.SelectedValue = 0;
            txtCidade.Text = null;
            lblCodigo2.Text = null;
            txtCidade.Focus();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (acao == "i")
            {
                if (txtCidade.Text != "" && cboEstado.SelectedValue != null)
                {
                    SqlConnection conn = new SqlConnection(strcon);
                    conn.Open();
                    string sql = "select * from TbCidade where NomeCidade = @cid and CodEstado = @cod";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@cid", txtCidade.Text));
                    cmd.Parameters.Add(new SqlParameter("@cod", cboEstado.SelectedValue));
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        MessageBox.Show("Cidade já adicionada", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCidade.Focus();
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
                    MessageBox.Show("Digite uma cidade e selecione um estado", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    cboEstado.Text = null;
                    txtCidade.Focus();
                }
            }

            else if (acao == "a")
            {
                if (txtCidade.Text != "")
                {
                    SqlConnection conn = new SqlConnection(strcon);
                    conn.Open();
                    string sql = "select * from TbCidade where NomeCidade = @cid and CodEstado = @cod";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@cid", txtCidade.Text));
                    cmd.Parameters.Add(new SqlParameter("@cod", cboEstado.SelectedValue));
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        MessageBox.Show("Cidade já adicionada", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtCidade.Focus();
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
                    MessageBox.Show("Digite uma cidade", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtCidade.Focus();
                }
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            acao = "a";

            cboEstado.Enabled = true;
            txtCidade.Enabled = true;
            txtCidade.Focus();
            btnSalvar.Enabled = true;

            dataGridView1.Enabled = false;

            btnNovo.Enabled = false;
            btnExcluir.Enabled = false;
            btnAlterar.Enabled = false;
        }

        private void btmExcluir_Click(object sender, EventArgs e)
        {
            excluir();
            comum();
            selectall();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            frmCidade.ActiveForm.AcceptButton = btnNovo;
            frmCidade.ActiveForm.CancelButton = btnFechar;

            comum();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int linha = dataGridView1.CurrentRow.Index;
            lblCodigo2.Text = dataGridView1.Rows[linha].Cells[0].Value.ToString();
            txtCidade.Text = dataGridView1.Rows[linha].Cells[1].Value.ToString();
            cboEstado.SelectedValue = dataGridView1.Rows[linha].Cells[2].Value;         

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