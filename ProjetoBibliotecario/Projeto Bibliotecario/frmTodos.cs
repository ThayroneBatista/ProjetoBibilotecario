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
    public partial class frmTodos : Form
    {
        string strcon = "Data Source=THAYRONE-PC;Initial Catalog=BDBiblio;Integrated Security=SSPI;";

        public frmTodos()
        {
            InitializeComponent();
        }

        private void frmTodos_Load(object sender, EventArgs e)
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

            if (txtTodos.Text == "")
            {
                SelectAll();
            }
            txtTodos.Focus();
        }

        private void SelectAll()
        {
            SqlConnection conn = new SqlConnection(strcon);
            conn.Open();
            string sql = "select CodLivro,Titulo from TbLivro";
            SqlCommand cmd = new SqlCommand(sql, conn);
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("CodLivro", "Codigo");
            dataGridView1.Columns.Add("Titulo", "Titulo");

            while (dr.Read())
                dataGridView1.Rows.Add(dr["CodLivro"], dr["Titulo"]);
            conn.Close();

            txtTodos.Focus();
        }

        private void txtTodos_KeyUp(object sender, KeyEventArgs e)
        {
            SelectAll();
        }

        private void txtTodos_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }
    }
}