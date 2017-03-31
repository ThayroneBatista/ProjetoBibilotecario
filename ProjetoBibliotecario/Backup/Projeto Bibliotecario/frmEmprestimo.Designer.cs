namespace Projeto_Bibliotecario
{
    partial class frmEmprestimo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmEmprestimo));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblEmprestado = new System.Windows.Forms.Label();
            this.txtEmpres = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 38);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(411, 223);
            this.dataGridView1.TabIndex = 11;
            this.dataGridView1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dataGridView1_KeyDown);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            // 
            // lblEmprestado
            // 
            this.lblEmprestado.AutoSize = true;
            this.lblEmprestado.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmprestado.Location = new System.Drawing.Point(12, 12);
            this.lblEmprestado.Name = "lblEmprestado";
            this.lblEmprestado.Size = new System.Drawing.Size(52, 20);
            this.lblEmprestado.TabIndex = 10;
            this.lblEmprestado.Text = "Livro:";
            // 
            // txtEmpres
            // 
            this.txtEmpres.Location = new System.Drawing.Point(110, 12);
            this.txtEmpres.Name = "txtEmpres";
            this.txtEmpres.Size = new System.Drawing.Size(313, 20);
            this.txtEmpres.TabIndex = 9;
            this.txtEmpres.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtEmpres_KeyDown);
            this.txtEmpres.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtEmpres_KeyUp);
            // 
            // frmEmprestimo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(437, 273);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lblEmprestado);
            this.Controls.Add(this.txtEmpres);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmEmprestimo";
            this.Text = "Consulta por emprestimo";
            this.Load += new System.EventHandler(this.frmEmprestimo_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmEmprestimo_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblEmprestado;
        private System.Windows.Forms.TextBox txtEmpres;
    }
}