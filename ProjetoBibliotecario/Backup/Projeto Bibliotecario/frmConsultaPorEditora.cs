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
    public partial class frmConsultaPorEditora : Form
    {
        string strcon = "Data Source=THAYRONE-PC;Initial Catalog=BDBiblio;Integrated Security=SSPI;";
        public frmConsultaPorEditora()
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

        private void frmConsultaPorEditora_Load(object sender, EventArgs e)
        {
            ChangeData();
            if (txtEditora.Text == "")
            {
                SelectAll();
            }
            txtEditora.Focus();
        }

        private void SelectAll()
        {
            SqlConnection conn = new SqlConnection(strcon);
            conn.Open();
            string sql = "select * from TbEditora where NomeEditora like '%'+@edi+'%'";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter("@edi", txtEditora.Text));
            SqlDataReader dr;
            dr = cmd.ExecuteReader();

            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("CodEditora", "Codigo");
            dataGridView1.Columns.Add("NomeEditora", "Editora");

            while (dr.Read())
                dataGridView1.Rows.Add(dr["CodEditora"], dr["NomeEditora"]);
            conn.Close();

            txtEditora.Focus();
        }

        private void txtEditora_KeyUp(object sender, KeyEventArgs e)
        {
            SelectAll();
        }

        private void txtEditora_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            txtEditora.Focus();
        }
    }
}