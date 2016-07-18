namespace Fahrrad_ERP.taskM
{
    partial class newTask
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxBetreff = new System.Windows.Forms.TextBox();
            this.textBoxNachricht = new System.Windows.Forms.RichTextBox();
            this.buttonHin = new System.Windows.Forms.Button();
            this.buttonAb = new System.Windows.Forms.Button();
            this.buttonAn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "An:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 34);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Betreff:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Nachricht:";
            // 
            // textBoxBetreff
            // 
            this.textBoxBetreff.Location = new System.Drawing.Point(9, 50);
            this.textBoxBetreff.Name = "textBoxBetreff";
            this.textBoxBetreff.Size = new System.Drawing.Size(367, 20);
            this.textBoxBetreff.TabIndex = 3;
            // 
            // textBoxNachricht
            // 
            this.textBoxNachricht.Location = new System.Drawing.Point(9, 89);
            this.textBoxNachricht.Name = "textBoxNachricht";
            this.textBoxNachricht.Size = new System.Drawing.Size(367, 96);
            this.textBoxNachricht.TabIndex = 4;
            this.textBoxNachricht.Text = "";
            // 
            // buttonHin
            // 
            this.buttonHin.Location = new System.Drawing.Point(9, 191);
            this.buttonHin.Name = "buttonHin";
            this.buttonHin.Size = new System.Drawing.Size(180, 23);
            this.buttonHin.TabIndex = 6;
            this.buttonHin.Text = "Hinzufügen";
            this.buttonHin.UseVisualStyleBackColor = true;
            this.buttonHin.Click += new System.EventHandler(this.buttonHin_Click);
            // 
            // buttonAb
            // 
            this.buttonAb.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonAb.Location = new System.Drawing.Point(196, 191);
            this.buttonAb.Name = "buttonAb";
            this.buttonAb.Size = new System.Drawing.Size(180, 23);
            this.buttonAb.TabIndex = 7;
            this.buttonAb.Text = "Abbrechen";
            this.buttonAb.UseVisualStyleBackColor = true;
            // 
            // buttonAn
            // 
            this.buttonAn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonAn.Location = new System.Drawing.Point(38, 4);
            this.buttonAn.Name = "buttonAn";
            this.buttonAn.Size = new System.Drawing.Size(338, 23);
            this.buttonAn.TabIndex = 8;
            this.buttonAn.Text = "wählen...";
            this.buttonAn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonAn.UseVisualStyleBackColor = true;
            this.buttonAn.Click += new System.EventHandler(this.buttonAn_Click);
            // 
            // newTask
            // 
            this.AcceptButton = this.buttonHin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonAb;
            this.ClientSize = new System.Drawing.Size(388, 223);
            this.Controls.Add(this.buttonAn);
            this.Controls.Add(this.buttonAb);
            this.Controls.Add(this.buttonHin);
            this.Controls.Add(this.textBoxNachricht);
            this.Controls.Add(this.textBoxBetreff);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(404, 261);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(404, 261);
            this.Name = "newTask";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Neuer Auftrag";
            this.Load += new System.EventHandler(this.newTask_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBoxBetreff;
        private System.Windows.Forms.RichTextBox textBoxNachricht;
        private System.Windows.Forms.Button buttonHin;
        private System.Windows.Forms.Button buttonAb;
        private System.Windows.Forms.Button buttonAn;
    }
}