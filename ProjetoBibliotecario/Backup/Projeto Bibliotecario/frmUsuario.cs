using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Projeto_Bibliotecario
{
    public partial class frmUsuario : Form
    {
        string strcon = "Data Source=THAYRONE-PC;Initial Catalog=BDBiblio;Integrated Security=SSPI;";
        string acao = "";

        public frmUsuario()
        {
            InitializeComponent();
        }

        private void teste()
        {
            dataGridView1.Columns.Add("CodUsuario", "Codigo");
            dataGridView1.Columns.Add("NomeUsuario", "Usuario");
            dataGridView1.Columns.Add("SenhaUsuario", "Senha");
            dataGridView1.Columns.Add("Endereco", "Endereco");
            dataGridView1.Columns.Add("CodCidade", "Cidade");
            dataGridView1.Columns.Add("CodEstado", "Estado");
            dataGridView1.Columns.Add("CEP", "CEP");
            dataGridView1.Columns.Add("Telefone", "Telefone");

            dataGridView1.Rows.Add("1", "Thayrone", "51755", "Casa", "3", "4", "123456", "343483");
            dataGridView1.Rows.Add("2", "Kaique", "*****", "Rua", "4", "6", "654897", "4348348");
            dataGridView1.Rows.Add("3", "Arthur", "*****", "Predio", "7", "5", "486453", "534531");
            dataGridView1.Rows.Add("4", "Rafael", "*****", "Morro", "8", "7", "73873", "5315315");
            dataGridView1.Rows.Add("5", "Landao", "*****", "Ponte", "1", "1", "5738743", "513513153");

        }

        private void selectall()
        {
            SqlConnection conn = new SqlConnection(strcon);
            conn.Open();
            string sql = "Select * from TbUsuario";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("CodUsuario", "Codigo");
            dataGridView1.Columns.Add("NomeUsuario", "Usuario");
            dataGridView1.Columns.Add("SenhaUsuario", "Senha");
            dataGridView1.Columns.Add("Endereco", "Endereco");
            dataGridView1.Columns.Add("CodCidade", "CodCidade");
            dataGridView1.Columns.Add("CodEstado", "CodEstado");
            dataGridView1.Columns.Add("CEP", "CEP");
            dataGridView1.Columns.Add("Telefone", "Telefone");

            while (dr.Read())
                dataGridView1.Rows.Add(dr["CodUsuario"], dr["NomeUsuario"], "*****", dr["Endereco"], dr["CodCidade"], dr["CodEstado"], dr["CEP"], dr["Telefone"]);
            conn.Close();
        }

        private void inserir()
        {
            DialogResult confirmar = MessageBox.Show("Deseja adicionar esse usuario ?", "Adcionar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection(strcon);
                conn.Open();
                string sql = "insert into TbUsuario values (@nome,@senha,@endereco,@cidade,@estado,@cep,@telefone)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@nome", txtUsuario.Text));
                cmd.Parameters.Add(new SqlParameter("@senha", txtSenha.Text));
                cmd.Parameters.Add(new SqlParameter("@endereco", txtEndereço.Text));
                cmd.Parameters.Add(new SqlParameter("@cidade", cboCidade.SelectedValue));
                cmd.Parameters.Add(new SqlParameter("@estado", cboEstado.SelectedValue));
                cmd.Parameters.Add(new SqlParameter("@cep", txtCEP.Text));
                cmd.Parameters.Add(new SqlParameter("@telefone", txtTelefone.Text));

                cmd.ExecuteNonQuery();
            }
        }

        private void alterar()
        {
            int linha = dataGridView1.CurrentRow.Index;
            string codi = dataGridView1.Rows[linha].Cells[0].Value.ToString();

            DialogResult confirmar = MessageBox.Show("Deseja salvar as alteraraçoes acima ?", "Alterar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection(strcon);
                conn.Open();
                string sql = "update TbUsuario set NomeUsuario=@nome, SenhaUsuario=@senha, Endereco=@endereco, CodCidade=@cidade, CodEstado=@estado, CEP=@cep, Telefone=@telefone where CodUsuario = @codi";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.Add(new SqlParameter("@codi", codi));
                cmd.Parameters.Add(new SqlParameter("@nome", txtUsuario.Text));
                cmd.Parameters.Add(new SqlParameter("@senha", txtSenha.Text));
                cmd.Parameters.Add(new SqlParameter("@endereco", txtEndereço.Text));
                cmd.Parameters.Add(new SqlParameter("@cidade", cboCidade.SelectedValue));
                cmd.Parameters.Add(new SqlParameter("@estado", cboEstado.SelectedValue));
                cmd.Parameters.Add(new SqlParameter("@cep", txtCEP.Text));
                cmd.Parameters.Add(new SqlParameter("@telefone", txtTelefone.Text));

                cmd.ExecuteNonQuery();

            }
        }

        private void excluir()
        {
            int linha = dataGridView1.CurrentRow.Index;
            string cod = dataGridView1.Rows[linha].Cells[0].Value.ToString();
            string nome = dataGridView1.Rows[linha].Cells[1].Value.ToString();

            DialogResult confirmar = MessageBox.Show("Deseja excluir usuario " + nome + " ?", "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmar == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection(strcon);
                conn.Open();
                string sql = "delete from TbUsuario where CodUsuario = @cod";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@cod", cod));
                cmd.ExecuteNonQuery();
            }
        }

        protected void carregarestado()
        {
            SqlConnection conn = new SqlConnection(strcon);
            conn.Open();

            try
            {
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

        protected void carregarcidade()
        {
            SqlConnection conn = new SqlConnection(strcon);
            conn.Open();

            try
           {
                string sql = "select C.NomeCidade,C.CodCidade,E.NomeEstado from TbCidade as C inner join TbEstado as E on (E.CodEstado=C.CodEstado) where C.CodEstado = @est";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@est", cboEstado.SelectedIndex + 1));
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(dr);

                this.cboCidade.DataSource = table;
                this.cboCidade.DisplayMember = "NomeCidade";
                this.cboCidade.ValueMember = "CodCidade";

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
            cboCidade.Enabled = false;

            txtUsuario.Enabled = false;
            txtSenha.Enabled = false;
            txtEndereço.Enabled = false;
            txtCEP.Enabled = false;
            txtTelefone.Enabled = false;

            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnNovo.Enabled = true;

            dataGridView1.Enabled = true;

            if (dataGridView1.RowCount > 0)
            {
                DataGridViewRow linha = new DataGridViewRow();
                linha = dataGridView1.CurrentRow;

                lblCodigo2.Text = linha.Cells[0].Value.ToString();
                txtUsuario.Text = linha.Cells[1].Value.ToString();
                txtSenha.Text = linha.Cells[2].Value.ToString();
                txtEndereço.Text = linha.Cells[3].Value.ToString();
                cboCidade.SelectedValue = linha.Cells[4].Value;
                cboEstado.SelectedValue = linha.Cells[5].Value;
                txtCEP.Text = linha.Cells[6].Value.ToString();
                txtTelefone.Text = linha.Cells[7].Value.ToString();
            }
            else
            {
                lblCodigo2.Text = null;
                txtUsuario.Text = null;
                txtSenha.Text = null;
                txtEndereço.Text = null;
                txtCEP.Text = null;
                txtTelefone.Text = null;

                cboEstado.SelectedIndex = -1;
                cboCidade.SelectedIndex = -1;
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmUsuario_Load(object sender, EventArgs e)
        {
            //teste();

            frmUsuario.ActiveForm.AcceptButton = btnNovo;
            frmUsuario.ActiveForm.CancelButton = btnFechar;

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

            txtUsuario.MaxLength = 35;
            txtSenha.MaxLength = 12;
            txtEndereço.MaxLength = 100;
            txtCEP.MaxLength = 9;
            txtTelefone.MaxLength = 15;

            txtUsuario.Enabled = false;
            txtSenha.Enabled = false;
            txtEndereço.Enabled = false;
            txtCEP.Enabled = false;
            txtTelefone.Enabled = false;

            cboEstado.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEstado.Enabled = false;
            cboCidade.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCidade.Enabled = false;

            selectall();
            carregarestado();

            if (dataGridView1.RowCount != 0)
            {
                cboCidade.SelectedValue = dataGridView1.Rows[0].Cells[4].Value;
                cboEstado.SelectedValue = dataGridView1.Rows[0].Cells[5].Value;
            }
            else
            {
                cboEstado.SelectedIndex = -1;
                cboCidade.SelectedIndex = -1;
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewRow linha = new DataGridViewRow();
            linha = dataGridView1.CurrentRow;

            lblCodigo2.Text = linha.Cells[0].Value.ToString();
            txtUsuario.Text = linha.Cells[1].Value.ToString();
            txtSenha.Text = linha.Cells[2].Value.ToString();
            txtEndereço.Text = linha.Cells[3].Value.ToString();
            cboCidade.SelectedValue = linha.Cells[4].Value;
            cboEstado.SelectedValue = linha.Cells[5].Value;
            txtCEP.Text = linha.Cells[6].Value.ToString();
            txtTelefone.Text = linha.Cells[7].Value.ToString();

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

        private void btnNovo_Click(object sender, EventArgs e)
        {
            frmUsuario.ActiveForm.AcceptButton = btnSalvar;
            frmUsuario.ActiveForm.CancelButton = btnCancelar;

            acao = "i";

            dataGridView1.ClearSelection();
            dataGridView1.Enabled = false;

            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;

            txtUsuario.Enabled = true;
            txtSenha.Enabled = true;
            txtEndereço.Enabled = true;
            cboCidade.Enabled = true;
            cboEstado.Enabled = true;
            txtCEP.Enabled = true;
            txtTelefone.Enabled = true;

            cboEstado.SelectedIndex = -1;
            cboEstado.SelectedValue = 0;
            cboCidade.SelectedIndex = -1;
            cboCidade.SelectedValue = 0;

            lblCodigo2.Text = null;
            txtUsuario.Text = null;
            txtSenha.Text = null;
            txtEndereço.Text = null;
            txtCEP.Text = null;
            txtTelefone.Text = null;

            txtUsuario.Focus();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (acao == "i")
            {
                if (txtUsuario.Text != "" && txtSenha.Text != "" && txtCEP.Text != "" && txtTelefone.Text != "" && cboCidade.SelectedIndex != -1 && cboEstado.SelectedIndex != -1)
                {
                    SqlConnection conn = new SqlConnection(strcon);
                    conn.Open();
                    string sql = "select * from TbUsuario where NomeUsuario = @nome";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@nome", txtUsuario.Text));
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        MessageBox.Show("Nome existente, escolha outro", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtUsuario.Focus();
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
                    MessageBox.Show("Preencha todos os campos obrigatorios", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtUsuario.Focus();
                }
            }

            else if (acao == "a")
            {
                if (txtUsuario.Text != "" && txtSenha.Text != "" && txtCEP.Text != "" && txtTelefone.Text != "")
                {
                    SqlConnection conn = new SqlConnection(strcon);
                    conn.Open();
                    string sql = "select * from TbUsuario where NomeUsuario = @nome and CEP = @cep";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@nome", txtUsuario.Text));
                    cmd.Parameters.Add(new SqlParameter("@cep", txtCEP.Text));
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        MessageBox.Show("Nome existente, escolha outro", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtUsuario.Focus();
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
                    MessageBox.Show("Preencha todos os campos obrigatorios", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtUsuario.Focus();
                }
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            acao = "a";

            dataGridView1.Enabled = false;

            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;

            txtUsuario.Enabled = true;
            txtSenha.Enabled = true;
            txtEndereço.Enabled = true;
            cboCidade.Enabled = true;
            cboEstado.Enabled = true;
            txtCEP.Enabled = true;
            txtTelefone.Enabled = true;

            txtUsuario.Focus();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            excluir();
            comum();
            selectall();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            frmUsuario.ActiveForm.AcceptButton = btnNovo;
            frmUsuario.ActiveForm.CancelButton = btnFechar;

            comum();
        }

        private void cboEstado_SelectedValueChanged(object sender, EventArgs e)
        {
            carregarcidade();
        }
    }
}