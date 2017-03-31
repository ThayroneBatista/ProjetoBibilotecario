using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Projeto_Bibliotecario
{
    public partial class frmControle : Form
    {
        public frmControle()
        {
            InitializeComponent();
        }

        private void autorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAutor frmAutor = new frmAutor();
            frmAutor.Show();
        }

        private void categoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCategoria frmCategoria = new frmCategoria();
            frmCategoria.Show();
        }

        private void editoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditora frmEditora = new frmEditora();
            frmEditora.Show();
        }

        private void sairToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult vsair;
            vsair = MessageBox.Show("Deseja sair ?","Sair",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation);
            if (vsair == DialogResult.Yes)
            Application.Exit();
        }

        private void proAutorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaPorAutor frmConA = new frmConsultaPorAutor();
            frmConA.Show();
        }

        private void porCategoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaPorCategoria frmConC = new frmConsultaPorCategoria();
            frmConC.Show();
        }

        private void porEditoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmConsultaPorEditora frmConE = new frmConsultaPorEditora();
            frmConE.Show();
        }

        private void livrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLivro frmLivro = new frmLivro();
            frmLivro.Show();
        }

        private void usuáriosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUsuario frmU = new frmUsuario();
            frmU.Show();
        }

        private void cidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCidade frmC = new frmCidade();
            frmC.Show();
        }

        private void estadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEstado frmE = new frmEstado();
            frmE.Show();
        }

        private void frmControle_Load(object sender, EventArgs e)
        {
            menuStrip1.BackColor = System.Drawing.Color.LightYellow;
            frmControle.ActiveForm.BackColor = System.Drawing.Color.LightYellow;
        }

        private void emprestadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmprestimo frmEmp = new frmEmprestimo();
            frmEmp.Show();
        }

        private void todosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTodos frmT = new frmTodos();
            frmT.Show();
        }
    }
}