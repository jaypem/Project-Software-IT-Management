namespace Fahrrad_ERP
{
    partial class Konfigurator_Einstellungen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Konfigurator_Einstellungen));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.listBoxA = new System.Windows.Forms.ListBox();
            this.listBoxB = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.label1, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.listBoxA, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.listBoxB, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(544, 362);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(275, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(264, 20);
            this.label1.TabIndex = 6;
            this.label1.Text = "nicht kompatible Teile anwählen";
            // 
            // listBoxA
            // 
            this.listBoxA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxA.FormattingEnabled = true;
            this.listBoxA.Location = new System.Drawing.Point(3, 38);
            this.listBoxA.Name = "listBoxA";
            this.listBoxA.Size = new System.Drawing.Size(266, 321);
            this.listBoxA.TabIndex = 7;
            this.listBoxA.SelectedIndexChanged += new System.EventHandler(this.listBoxA_SelectedIndexChanged);
            // 
            // listBoxB
            // 
            this.listBoxB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listBoxB.FormattingEnabled = true;
            this.listBoxB.Location = new System.Drawing.Point(275, 38);
            this.listBoxB.Name = "listBoxB";
            this.listBoxB.SelectionMode = System.Windows.Forms.SelectionMode.MultiSimple;
            this.listBoxB.Size = new System.Drawing.Size(266, 321);
            this.listBoxB.TabIndex = 8;
            this.listBoxB.SelectedIndexChanged += new System.EventHandler(this.listBoxB_SelectedIndexChanged);
            // 
            // Konfigurator_Einstellungen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(544, 362);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(560, 400);
            this.Name = "Konfigurator_Einstellungen";
            this.Text = "Konfigurator einstellen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Konfigurator_Einstellungen_FormClosing);
            this.Load += new System.EventHandler(this.Konfigurator_Einstellungen_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox listBoxA;
        private System.Windows.Forms.ListBox listBoxB;
    }
}