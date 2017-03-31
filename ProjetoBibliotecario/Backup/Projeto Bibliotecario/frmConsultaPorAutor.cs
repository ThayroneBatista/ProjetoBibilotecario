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
    public partial class frmConsultaPorAutor : Form
    {
       //string strcon = "Data Source=MCRF02009;Initial Catalog=BDBiblio; Integrated Security=SSPI;";
        string strcon = "Data Source=THAYRONE-PC;Initial Catalog=BDBiblio; Integrated Security=SSPI;";
        
        public frmConsultaPorAutor()
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

        private void frmConsultaPorAutor_Load(object sender, EventArgs e)
        {

            ChangeData();
            if (txtAutor.Text == "")
            {
                SelectAll();
            }
            txtAutor.Focus();
        }
       
        private void SelectAll()
        {
        SqlConnection conn = new SqlConnection(strcon);
            conn.Open();
            string sql = "select * from TbAutor where NomeAutor like '%'+@autor+'%'";
            SqlCommand cmd  = new SqlCommand(sql, conn);
            cmd.Parameters.Add(new SqlParameter ("@autor", txtAutor.Text));
            SqlDataReader dr;
            dr = cmd.ExecuteReader();
            
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("CodAutor","Codigo");
                dataGridView1.Columns.Add("NomeAutor", "Autor");

            while (dr.Read())
                dataGridView1.Rows.Add(dr["CodAutor"], dr["NomeAutor"]);
                conn.Close();

                txtAutor.Focus();
        }

        private void txtAutor_KeyUp(object sender, KeyEventArgs e)
        {
            SelectAll();
        }

        private void txtAutor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Close();
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            txtAutor.Focus();
        }
    }
}