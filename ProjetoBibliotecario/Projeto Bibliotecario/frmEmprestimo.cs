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
    public partial class frmEmprestimo : Form
    {
        string strcon = "Data Source=THAYRONE-PC;Initial Catalog=BDBiblio;Integrated Security=SSPI;";

        public frmEmprestimo()
        {
            InitializeComponent();
        }

        private void frmEmprestimo_Load(object sender, EventArgs e)
        {
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

            if (txtEmpres.Text == "")
            {
                SelectAll();
            }
            txtEmpres.Focus();
        }

        private void SelectAll()
        {
            SqlConnection conn = new SqlConnection(strcon);
            conn.Open();
            string sql = "select CodLivro,Titulo,Emprestado from TbLivro where Emprestado = 1";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("CodLivro", "Codigo");
            dataGridView1.Columns.Add("Titulo", "Titulo");

            while (dr.Read())
                dataGridView1.Rows.Add(dr["CodLivro"], dr["Titulo"]);
            conn.Close();

            txtEmpres.Focus();
        }

        private void txtEmpres_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void frmEmprestimo_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void txtEmpres_KeyUp(object sender, KeyEventArgs e)
        {
            SqlConnection conn = new SqlConnection(strcon);
            conn.Open();
            string sql = "select CodLivro,Titulo from TbLivro where Titulo like '%'+@ti+'%' and Emprestado = 1";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@ti", txtEmpres.Text));
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("CodLivro", "Codigo");
            dataGridView1.Columns.Add("Titulo", "Titulo");

            while (dr.Read())
                dataGridView1.Rows.Add(dr["CodLivro"], dr["Titulo"]);
            conn.Close();

            txtEmpres.Focus();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            txtEmpres.Focus();
        }
    }
}