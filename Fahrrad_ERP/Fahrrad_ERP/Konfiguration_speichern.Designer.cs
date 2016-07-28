namespace Fahrrad_ERP
{
    partial class Konfiguration_speichern
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
            this.radioButtonVorlage = new System.Windows.Forms.RadioButton();
            this.radioButtonKunde = new System.Windows.Forms.RadioButton();
            this.radioButtonBestell = new System.Windows.Forms.RadioButton();
            this.label2 = new System.Windows.Forms.Label();
            this.textBoxBez = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.buttonKunde = new System.Windows.Forms.Button();
            this.buttonSpeichern = new System.Windows.Forms.Button();
            this.buttonAb = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Speichern als:";
            // 
            // radioButtonVorlage
            // 
            this.radioButtonVorlage.AutoSize = true;
            this.radioButtonVorlage.Checked = true;
            this.radioButtonVorlage.Location = new System.Drawing.Point(92, 7);
            this.radioButtonVorlage.Name = "radioButtonVorlage";
            this.radioButtonVorlage.Size = new System.Drawing.Size(127, 17);
            this.radioButtonVorlage.TabIndex = 1;
            this.radioButtonVorlage.TabStop = true;
            this.radioButtonVorlage.Text = "Konfigurationsvorlage";
            this.radioButtonVorlage.UseVisualStyleBackColor = true;
            this.radioButtonVorlage.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButtonKunde
            // 
            this.radioButtonKunde.AutoSize = true;
            this.radioButtonKunde.Location = new System.Drawing.Point(92, 30);
            this.radioButtonKunde.Name = "radioButtonKunde";
            this.radioButtonKunde.Size = new System.Drawing.Size(142, 17);
            this.radioButtonKunde.TabIndex = 2;
            this.radioButtonKunde.TabStop = true;
            this.radioButtonKunde.Text = "Konfiguration für Kunden";
            this.radioButtonKunde.UseVisualStyleBackColor = true;
            this.radioButtonKunde.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // radioButtonBestell
            // 
            this.radioButtonBestell.AutoSize = true;
            this.radioButtonBestell.Location = new System.Drawing.Point(92, 53);
            this.radioButtonBestell.Name = "radioButtonBestell";
            this.radioButtonBestell.Size = new System.Drawing.Size(129, 17);
            this.radioButtonBestell.TabIndex = 3;
            this.radioButtonBestell.TabStop = true;
            this.radioButtonBestell.Text = "Bestellung für Kunden";
            this.radioButtonBestell.UseVisualStyleBackColor = true;
            this.radioButtonBestell.CheckedChanged += new System.EventHandler(this.radioButton_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 82);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Bezeichnung:";
            // 
            // textBoxBez
            // 
            this.textBoxBez.Location = new System.Drawing.Point(90, 79);
            this.textBoxBez.Name = "textBoxBez";
            this.textBoxBez.Size = new System.Drawing.Size(182, 20);
            this.textBoxBez.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 110);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Kunde:";
            // 
            // buttonKunde
            // 
            this.buttonKunde.Enabled = false;
            this.buttonKunde.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonKunde.Location = new System.Drawing.Point(90, 105);
            this.buttonKunde.Name = "buttonKunde";
            this.buttonKunde.Size = new System.Drawing.Size(182, 23);
            this.buttonKunde.TabIndex = 7;
            this.buttonKunde.Text = "wählen...";
            this.buttonKunde.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonKunde.UseVisualStyleBackColor = true;
            this.buttonKunde.Click += new System.EventHandler(this.button1_Click);
            // 
            // buttonSpeichern
            // 
            this.buttonSpeichern.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonSpeichern.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonSpeichern.Location = new System.Drawing.Point(25, 142);
            this.buttonSpeichern.Name = "buttonSpeichern";
            this.buttonSpeichern.Size = new System.Drawing.Size(122, 23);
            this.buttonSpeichern.TabIndex = 10;
            this.buttonSpeichern.Text = "Speichern";
            this.buttonSpeichern.UseVisualStyleBackColor = true;
            this.buttonSpeichern.Click += new System.EventHandler(this.buttonSpeichern_Click);
            // 
            // buttonAb
            // 
            this.buttonAb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonAb.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.buttonAb.Location = new System.Drawing.Point(160, 142);
            this.buttonAb.Name = "buttonAb";
            this.buttonAb.Size = new System.Drawing.Size(122, 23);
            this.buttonAb.TabIndex = 11;
            this.buttonAb.Text = "Abbrechen";
            this.buttonAb.UseVisualStyleBackColor = true;
            // 
            // Konfiguration_speichern
            // 
            this.AcceptButton = this.buttonSpeichern;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonAb;
            this.ClientSize = new System.Drawing.Size(294, 187);
            this.Controls.Add(this.buttonAb);
            this.Controls.Add(this.buttonSpeichern);
            this.Controls.Add(this.buttonKunde);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBoxBez);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.radioButtonBestell);
            this.Controls.Add(this.radioButtonKunde);
            this.Controls.Add(this.radioButtonVorlage);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(300, 215);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(300, 215);
            this.Name = "Konfiguration_speichern";
            this.ShowIcon = false;
            this.Text = "Konfiguration speichern";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton radioButtonVorlage;
        private System.Windows.Forms.RadioButton radioButtonKunde;
        private System.Windows.Forms.RadioButton radioButtonBestell;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBoxBez;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button buttonKunde;
        private System.Windows.Forms.Button buttonSpeichern;
        private System.Windows.Forms.Button buttonAb;
    }
}