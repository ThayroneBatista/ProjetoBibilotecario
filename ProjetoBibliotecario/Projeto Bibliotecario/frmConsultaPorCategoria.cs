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
    public partial class frmConsultaPorCategoria : Form
    {
        string strcon = "Data Source=THAYRONE-PC;Initial Catalog=BDBiblio;Integrated Security=SSPI;";

        public frmConsultaPorCategoria()
        {
            InitializeComponent();
        }

        private void ChangeData()
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

        }

        private void frmConsultaPorCategoria_Load(object sender, EventArgs e)
        {

            ChangeData();
            if (txtCategoria.Text == "")
            {
                SelectAll();
            }
            txtCategoria.Focus();
        }

        private void SelectAll()
        {
            SqlConnection conn = new SqlConnection(strcon);
            conn.Open();
            string sql = "select * from TbCategoria where NomeCategoria like '%'+@cate+'%'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@cate", txtCategoria.Text));
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("CodCategoria", "Codigo");
            dataGridView1.Columns.Add("NomeCategoria", "Categoria");

            while (dr.Read())
                dataGridView1.Rows.Add(dr["CodCategoria"], dr["NomeCategoria"]);
            conn.Close();

            txtCategoria.Focus();
        }

        private void txtCategoria_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void txtCategoria_KeyUp(object sender, KeyEventArgs e)
        {
            SelectAll();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            txtCategoria.Focus();
        }
    }
}