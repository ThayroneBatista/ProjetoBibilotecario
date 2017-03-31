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
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }
        string strcon = "Data Source=THAYRONE-PC;Initial Catalog=BDBiblio; Integrated Security=SSPI;";
        // string strcon = "Data Source=MCRF02009;Initial Catalog=BDBiblio; Integrated Security=SSPI;";
        // string strcon = "Data Source=MCRF02009;Initial Catalog=BDBiblio; Integrated Security=SSPI;User ID=Academico\\uii002;Password=boata123;";
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.grpLogin = new System.Windows.Forms.GroupBox();
            this.txtUsuario = new System.Windows.Forms.TextBox();
            this.txtSenha = new System.Windows.Forms.TextBox();
            this.lblSenha = new System.Windows.Forms.Label();
            this.lblUsuario = new System.Windows.Forms.Label();
            this.btnEntrar = new System.Windows.Forms.Button();
            this.grpLogin.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpLogin
            // 
            this.grpLogin.Controls.Add(this.txtUsuario);
            this.grpLogin.Controls.Add(this.txtSenha);
            this.grpLogin.Controls.Add(this.lblSenha);
            this.grpLogin.Controls.Add(this.lblUsuario);
            this.grpLogin.Controls.Add(this.btnEntrar);
            this.grpLogin.Location = new System.Drawing.Point(12, 12);
            this.grpLogin.Name = "grpLogin";
            this.grpLogin.Size = new System.Drawing.Size(207, 138);
            this.grpLogin.TabIndex = 0;
            this.grpLogin.TabStop = false;
            this.grpLogin.Enter += new System.EventHandler(this.grpLogin_Enter);
            // 
            // txtUsuario
            // 
            this.txtUsuario.Location = new System.Drawing.Point(76, 19);
            this.txtUsuario.Name = "txtUsuario";
            this.txtUsuario.Size = new System.Drawing.Size(115, 20);
            this.txtUsuario.TabIndex = 5;
            // 
            // txtSenha
            // 
            this.txtSenha.Location = new System.Drawing.Point(76, 58);
            this.txtSenha.Name = "txtSenha";
            this.txtSenha.PasswordChar = '*';
            this.txtSenha.Size = new System.Drawing.Size(115, 20);
            this.txtSenha.TabIndex = 4;
            // 
            // lblSenha
            // 
            this.lblSenha.AutoSize = true;
            this.lblSenha.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSenha.Location = new System.Drawing.Point(6, 60);
            this.lblSenha.Name = "lblSenha";
            this.lblSenha.Size = new System.Drawing.Size(54, 18);
            this.lblSenha.TabIndex = 2;
            this.lblSenha.Text = "Senha:";
            // 
            // lblUsuario
            // 
            this.lblUsuario.AutoSize = true;
            this.lblUsuario.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUsuario.Location = new System.Drawing.Point(6, 16);
            this.lblUsuario.Name = "lblUsuario";
            this.lblUsuario.Size = new System.Drawing.Size(64, 18);
            this.lblUsuario.TabIndex = 1;
            this.lblUsuario.Text = "Usuário:";
            // 
            // btnEntrar
            // 
            this.btnEntrar.Location = new System.Drawing.Point(76, 95);
            this.btnEntrar.Name = "btnEntrar";
            this.btnEntrar.Size = new System.Drawing.Size(81, 27);
            this.btnEntrar.TabIndex = 0;
            this.btnEntrar.Text = "Entrar";
            this.btnEntrar.UseVisualStyleBackColor = true;
            this.btnEntrar.Click += new System.EventHandler(this.btnEntrar_Click);
            // 
            // frmLogin
            // 
            this.AcceptButton = this.btnEntrar;
            this.ClientSize = new System.Drawing.Size(240, 164);
            this.Controls.Add(this.grpLogin);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Tela de Login";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.grpLogin.ResumeLayout(false);
            this.grpLogin.PerformLayout();
            this.ResumeLayout(false);

        }

        private void btnEntrar_Click(object sender, EventArgs e)
        {
            SqlConnection conexao = new SqlConnection(strcon);
            string sql = "select * from TbUsuario where NomeUsuario=@usu and SenhaUsuario=@sen";

            conexao.Open();

            SqlCommand cmd = new SqlCommand(sql, conexao);

            cmd.Parameters.Add(new SqlParameter("@usu", txtUsuario.Text));
            cmd.Parameters.Add(new SqlParameter("@sen", txtSenha.Text));

            SqlDataReader dr = cmd.ExecuteReader();

            if (dr.Read())
            {
                frmControle frmControle = new frmControle();
                frmControle.Show();
                Hide();
            }
            else
            {
                MessageBox.Show("Usuario/Senha inválidos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtSenha.Text = null;
                txtUsuario.Text = null;
                txtUsuario.Focus();
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            txtUsuario.Focus();
        }

        private void grpLogin_Enter(object sender, EventArgs e)
        {
            txtUsuario.Focus();
        }
    }
}