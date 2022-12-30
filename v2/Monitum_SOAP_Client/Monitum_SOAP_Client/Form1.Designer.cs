namespace Monitum_SOAP_Client
{
    partial class Form1
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
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxEmailAdmin = new System.Windows.Forms.TextBox();
            this.textBoxPasswordAdmin = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxPasswordGestor = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxEmailGestor = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.buttonAdd = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(95, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(97, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Email Admin";
            // 
            // textBoxEmailAdmin
            // 
            this.textBoxEmailAdmin.Location = new System.Drawing.Point(234, 42);
            this.textBoxEmailAdmin.Name = "textBoxEmailAdmin";
            this.textBoxEmailAdmin.Size = new System.Drawing.Size(247, 26);
            this.textBoxEmailAdmin.TabIndex = 1;
            // 
            // textBoxPasswordAdmin
            // 
            this.textBoxPasswordAdmin.Location = new System.Drawing.Point(234, 91);
            this.textBoxPasswordAdmin.Name = "textBoxPasswordAdmin";
            this.textBoxPasswordAdmin.Size = new System.Drawing.Size(247, 26);
            this.textBoxPasswordAdmin.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(95, 91);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(127, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Password Admin";
            // 
            // textBoxPasswordGestor
            // 
            this.textBoxPasswordGestor.Location = new System.Drawing.Point(234, 192);
            this.textBoxPasswordGestor.Name = "textBoxPasswordGestor";
            this.textBoxPasswordGestor.Size = new System.Drawing.Size(247, 26);
            this.textBoxPasswordGestor.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(95, 192);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(131, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Password Gestor";
            // 
            // textBoxEmailGestor
            // 
            this.textBoxEmailGestor.Location = new System.Drawing.Point(234, 143);
            this.textBoxEmailGestor.Name = "textBoxEmailGestor";
            this.textBoxEmailGestor.Size = new System.Drawing.Size(247, 26);
            this.textBoxEmailGestor.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(95, 143);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(101, 20);
            this.label4.TabIndex = 4;
            this.label4.Text = "Email Gestor";
            // 
            // buttonAdd
            // 
            this.buttonAdd.Location = new System.Drawing.Point(183, 263);
            this.buttonAdd.Name = "buttonAdd";
            this.buttonAdd.Size = new System.Drawing.Size(141, 75);
            this.buttonAdd.TabIndex = 8;
            this.buttonAdd.Text = "Adicionar Gestor";
            this.buttonAdd.UseVisualStyleBackColor = true;
            this.buttonAdd.Click += new System.EventHandler(this.buttonAdd_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(588, 361);
            this.Controls.Add(this.buttonAdd);
            this.Controls.Add(this.textBoxPasswordGestor);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxEmailGestor);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBoxPasswordAdmin);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBoxEmailAdmin);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxEmailAdmin;
        private System.Windows.Forms.TextBox textBoxPasswordAdmin;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxPasswordGestor;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxEmailGestor;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonAdd;
    }
}

