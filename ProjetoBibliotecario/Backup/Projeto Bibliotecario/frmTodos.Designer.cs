namespace Projeto_Bibliotecario
{
    partial class frmTodos
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTodos));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.lblLivro = new System.Windows.Forms.Label();
            this.txtTodos = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Location = new System.Drawing.Point(12, 38);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.Size = new System.Drawing.Size(411, 223);
            this.dataGridView1.TabIndex = 14;
            // 
            // lblLivro
            // 
            this.lblLivro.AutoSize = true;
            this.lblLivro.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLivro.Location = new System.Drawing.Point(12, 12);
            this.lblLivro.Name = "lblLivro";
            this.lblLivro.Size = new System.Drawing.Size(52, 20);
            this.lblLivro.TabIndex = 13;
            this.lblLivro.Text = "Livro:";
            // 
            // txtTodos
            // 
            this.txtTodos.Location = new System.Drawing.Point(110, 12);
            this.txtTodos.Name = "txtTodos";
            this.txtTodos.Size = new System.Drawing.Size(313, 20);
            this.txtTodos.TabIndex = 12;
            this.txtTodos.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtTodos_KeyDown);
            this.txtTodos.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTodos_KeyUp);
            // 
            // frmTodos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(433, 273);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.lblLivro);
            this.Controls.Add(this.txtTodos);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmTodos";
            this.Text = "Todos";
            this.Load += new System.EventHandler(this.frmTodos_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label lblLivro;
        private System.Windows.Forms.TextBox txtTodos;
    }
}