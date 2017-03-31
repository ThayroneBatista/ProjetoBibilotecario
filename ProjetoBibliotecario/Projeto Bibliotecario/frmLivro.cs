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
    public partial class frmLivro : Form
    {
        string strcon = "Data Source=THAYRONE-PC;Initial Catalog=BDBiblio;Integrated Security=SSPI;";
        string acao = "";

        public frmLivro()
        {
            InitializeComponent();
        }

        private void selectall()
        {
            SqlConnection conn = new SqlConnection(strcon);
            conn.Open();
            string sql = "Select CodLivro, Titulo, CodAutor, CodEditora, CodCategoria, AcompDVD, Idioma, Observações from TbLivro";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr = cmd.ExecuteReader();

            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("CodLivro", "Codigo");
            dataGridView1.Columns.Add("Titulo", "Titulo");
            dataGridView1.Columns.Add("NomeAutor", "Autor");
            dataGridView1.Columns.Add("NomeEditora", "Editora");
            dataGridView1.Columns.Add("NomeCategoria", "Categoria");
            dataGridView1.Columns.Add("AcompDVD", "AcompDVD");
            dataGridView1.Columns.Add("Idioma", "Idioma");
            dataGridView1.Columns.Add("Observações", "Observações");

            while (dr.Read())
                dataGridView1.Rows.Add(dr["CodLivro"], dr["Titulo"], dr["CodAutor"], dr["CodEditora"], dr["CodCategoria"], dr["AcompDVD"], dr["Idioma"], dr["Observações"]);
            conn.Close();
        }

        private void inserir()
        {
            int chk;
            if (rdbSim.Checked == true)
            { chk = 1; }
            else
            { chk = 0; }

            DialogResult confirmar = MessageBox.Show("Deseja adicionar esse livro ?", "Adcionar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection(strcon);
                conn.Open();
                string sql = "insert into TbLivro (Titulo,CodAutor,CodEditora,CodCategoria,AcompDVD,Idioma,Observações) values (@livro,@aut,@edi,@cat,@dvd,@idi,@obs)";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@livro", txtTitulo.Text));
                cmd.Parameters.Add(new SqlParameter("@aut", cboAutor.SelectedValue));
                cmd.Parameters.Add(new SqlParameter("@edi", cboEditora.SelectedValue));
                cmd.Parameters.Add(new SqlParameter("@cat", cboCategoria.SelectedValue));
                cmd.Parameters.Add(new SqlParameter("@dvd", chk));
                cmd.Parameters.Add(new SqlParameter("@idi", cboIdioma.SelectedValue));
                cmd.Parameters.Add(new SqlParameter("@obs", txtObs.Text));

                cmd.ExecuteNonQuery();
            }
        }

        private void alterar()
        {
            int chk;
            if (rdbSim.Checked == true)
            { chk = 1; }
            else
            { chk = 0; }

            int linha = dataGridView1.CurrentRow.Index;
            string codi = dataGridView1.Rows[linha].Cells[0].Value.ToString();

            DialogResult confirmar = MessageBox.Show("Deseja salvar as alteraçoes acima ?", "Alterar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (confirmar == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection(strcon);
                conn.Open();
                string sql = "update TbLivro set Titulo=@livro, CodAutor=@aut, CodEditora=@edi, CodCategoria=@cat, AcompDVD=@dvd, Idioma=@idi, Observações=@obs where CodLivro = @codi";

                SqlCommand cmd = new SqlCommand(sql, conn);

                cmd.Parameters.Add(new SqlParameter("@codi", codi));
                cmd.Parameters.Add(new SqlParameter("@livro", txtTitulo.Text));
                cmd.Parameters.Add(new SqlParameter("@aut", cboAutor.SelectedValue));
                cmd.Parameters.Add(new SqlParameter("@edi", cboEditora.SelectedValue));
                cmd.Parameters.Add(new SqlParameter("@cat", cboCategoria.SelectedValue));
                cmd.Parameters.Add(new SqlParameter("@dvd", chk));
                cmd.Parameters.Add(new SqlParameter("@idi", cboIdioma.SelectedValue));
                cmd.Parameters.Add(new SqlParameter("@obs", txtObs.Text));

                cmd.ExecuteNonQuery();
            }
        }

        private void excluir()
        {
            int linha = dataGridView1.CurrentRow.Index;
            string cod = dataGridView1.Rows[linha].Cells[0].Value.ToString();
            string nome = dataGridView1.Rows[linha].Cells[1].Value.ToString();

            DialogResult confirmar = MessageBox.Show("Deseja excluir livro " + nome + " ?", "Excluir", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (confirmar == DialogResult.Yes)
            {
                SqlConnection conn = new SqlConnection(strcon);
                conn.Open();
                string sql = "delete from TbLivro where CodLivro = @cod";
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@cod", cod));
                cmd.ExecuteNonQuery();
            }
        }

        protected void carregarautor()
        {
            SqlConnection conn = new SqlConnection(strcon);
            conn.Open();

            try
            {
                string sql = "select CodAutor,NomeAutor from TbAutor";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(dr);

                this.cboAutor.DataSource = table;
                this.cboAutor.DisplayMember = "NomeAutor";
                this.cboAutor.ValueMember = "CodAutor";

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

        protected void carregareditora()
        {
            SqlConnection conn = new SqlConnection(strcon);
            conn.Open();

            try
            {
                string sql = "select CodEditora,NomeEditora from TbEditora";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(dr);

                this.cboEditora.DataSource = table;
                this.cboEditora.DisplayMember = "NomeEditora";
                this.cboEditora.ValueMember = "CodEditora";

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

        protected void carregarcategoria()
        {
            SqlConnection conn = new SqlConnection(strcon);
            conn.Open();

            try
            {
                string sql = "select CodCategoria,NomeCategoria from TbCategoria";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(dr);

                this.cboCategoria.DataSource = table;
                this.cboCategoria.DisplayMember = "NomeCategoria";
                this.cboCategoria.ValueMember = "CodCategoria";

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

        protected void carregaridioma()
        {
            SqlConnection conn = new SqlConnection(strcon);
            conn.Open();

            try
            {
                string sql = "select * from TbIdioma";
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                DataTable table = new DataTable();
                table.Load(dr);

                this.cboIdioma.DataSource = table;
                this.cboIdioma.DisplayMember = "Idioma";
                this.cboIdioma.ValueMember = "CodIdioma";

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

            cboAutor.Enabled = false;
            cboEditora.Enabled = false;
            cboCategoria.Enabled = false;
            cboIdioma.Enabled = false;

            txtObs.Enabled = false;
            txtTitulo.Enabled = false;
            
            btnSalvar.Enabled = false;
            btnCancelar.Enabled = false;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            btnNovo.Enabled = true;

            grpDVD.Enabled = false;

            dataGridView1.Enabled = true;

            if (dataGridView1.RowCount == 0)
            {
                lblCodigo2.Text = null;
                txtTitulo.Text = null;
                txtObs.Text = null;

                cboEditora.SelectedIndex = -1;
                cboCategoria.SelectedIndex = -1;
                cboAutor.SelectedIndex = -1;
                cboIdioma.SelectedIndex = 0;

                rdbNao.Checked = true;
            }
        }

        private void frmLivro_Load(object sender, EventArgs e)
        {
            frmLivro.ActiveForm.AcceptButton = btnNovo;
            frmLivro.ActiveForm.CancelButton = btnFechar;

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

            grpDVD.Enabled = false;

            txtTitulo.MaxLength = 50;
            txtObs.MaxLength = 500;
            
            txtTitulo.Enabled = false;
            txtObs.Enabled = false;
            
            cboEditora.DropDownStyle = ComboBoxStyle.DropDownList;
            cboEditora.Enabled = false;
            cboCategoria.DropDownStyle = ComboBoxStyle.DropDownList;
            cboCategoria.Enabled = false;
            cboAutor.DropDownStyle = ComboBoxStyle.DropDownList;
            cboAutor.Enabled = false;
            cboIdioma.DropDownStyle = ComboBoxStyle.DropDownList;
            cboIdioma.Enabled = false;

            selectall();
            carregareditora();
            carregarautor();
            carregarcategoria();
            carregaridioma();

            if (dataGridView1.RowCount != 0)
            {
                cboCategoria.SelectedValue = dataGridView1.Rows[0].Cells[4].Value;
                cboEditora.SelectedValue = dataGridView1.Rows[0].Cells[3].Value;
                cboAutor.SelectedValue = dataGridView1.Rows[0].Cells[2].Value;
                cboIdioma.SelectedValue = dataGridView1.Rows[0].Cells[6].Value;
            }
            else
            {
                cboEditora.SelectedIndex = -1;
                cboCategoria.SelectedIndex = -1;
                cboAutor.SelectedIndex = -1;
                cboIdioma.SelectedIndex = 0;
            }
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int linha = dataGridView1.CurrentRow.Index;

            lblCodigo2.Text = dataGridView1.Rows[linha].Cells[0].Value.ToString();
            txtTitulo.Text = dataGridView1.Rows[linha].Cells[1].Value.ToString();
            txtObs.Text = dataGridView1.Rows[linha].Cells[7].Value.ToString();
            cboCategoria.SelectedValue = dataGridView1.Rows[linha].Cells[4].Value;
            cboEditora.SelectedValue = dataGridView1.Rows[linha].Cells[3].Value;
            cboAutor.SelectedValue = dataGridView1.Rows[linha].Cells[2].Value;
            cboIdioma.SelectedValue = dataGridView1.Rows[linha].Cells[6].Value;

            if (dataGridView1.Rows[linha].Cells[5].Value.ToString() == "True")
            { rdbSim.Checked = true; }
            else
            { rdbNao.Checked = true; }

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
            frmLivro.ActiveForm.AcceptButton = btnSalvar;
            frmLivro.ActiveForm.CancelButton = btnCancelar;

            acao = "i";

            dataGridView1.ClearSelection();
            dataGridView1.Enabled = false;

            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;
            
            grpDVD.Enabled = true;

            txtTitulo.Enabled = true;
            txtObs.Enabled = true;
            cboCategoria.Enabled = true;
            cboEditora.Enabled = true;
            cboAutor.Enabled = true;
            cboIdioma.Enabled = true;

            cboEditora.SelectedIndex = -1;
            cboEditora.SelectedValue = 0;
            cboCategoria.SelectedIndex = -1;
            cboCategoria.SelectedValue = 0;
            cboAutor.SelectedIndex = -1;
            cboAutor.SelectedValue = 0;
            cboIdioma.SelectedIndex = 0;
            cboIdioma.SelectedValue = 1;

            rdbNao.Checked = true;

            lblCodigo2.Text = null;
            txtTitulo.Text = null;
            txtObs.Text = null;

            txtTitulo.Focus();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            acao = "a";

            dataGridView1.Enabled = false;

            btnSalvar.Enabled = true;
            btnCancelar.Enabled = true;
            btnAlterar.Enabled = false;
            btnExcluir.Enabled = false;

            txtTitulo.Enabled = true;
            txtObs.Enabled = true;
            cboCategoria.Enabled = true;
            cboEditora.Enabled = true;
            cboAutor.Enabled = true;
            cboIdioma.Enabled = true;

            grpDVD.Enabled = true;

            txtTitulo.Focus();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (acao == "i")
            {
                if (txtTitulo.Text != "" && cboAutor.SelectedIndex != -1 && cboEditora.SelectedIndex != -1 && cboCategoria.SelectedIndex != -1)
                {
                    SqlConnection conn = new SqlConnection(strcon);
                    conn.Open();
                    string sql = "select * from TbLivro where Titulo = @titulo";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@titulo", txtTitulo.Text));
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        MessageBox.Show("Livro existente, defina outro", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTitulo.Focus();
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
                    txtTitulo.Focus();
                }
            }

            else if (acao == "a")
            {
                if (txtTitulo.Text != "" && cboAutor.SelectedIndex != -1 && cboEditora.SelectedIndex != -1 && cboCategoria.SelectedIndex != -1)
                {
                    SqlConnection conn = new SqlConnection(strcon);
                    conn.Open();
                    string sql = "select * from TbLivro where Titulo = @livro";
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@livro", txtTitulo.Text));
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.Read())
                    {
                        MessageBox.Show("Livro existente, escolha outro", "Invalido", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtTitulo.Focus();
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
                    txtTitulo.Focus();
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            excluir();
            selectall();
            comum();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            frmLivro.ActiveForm.AcceptButton = btnNovo;
            frmLivro.ActiveForm.CancelButton = btnFechar;

            comum();
        }

    }
}