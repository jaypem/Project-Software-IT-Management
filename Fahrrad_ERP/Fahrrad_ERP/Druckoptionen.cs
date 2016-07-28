using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Drawing.Printing;
using System.Windows.Forms;

namespace Fahrrad_ERP
{
    public partial class Druckoptionen : Form
    {
        public Druckoptionen()
        {
            InitializeComponent();
        }
        PrinterSettings Drucker = new PrinterSettings();

        private void Druckoptionen_Load(object sender, EventArgs e)
        {
            Druckeinstellungen ein = new Druckeinstellungen();
            //schreibe alle installierten Drucker in die Auswahlbox
            String drucker;
            for (int i = 0; i < PrinterSettings.InstalledPrinters.Count; i++)
            {
                drucker = PrinterSettings.InstalledPrinters[i];
                comboBox1.Items.Add(drucker);
            }
            //gibt es bereits einen gewünschten Drucker? Exemplare? Farbe?
            if (ein.Name != null)
            {
                comboBox1.SelectedItem = ein.Name;
                numericUpDown1.Value = ein.Exemplare;
                if (ein.Farbe)
                {
                    radioButtonColor.Checked = true;
                }
                else
                {
                    radioButtonMono.Checked = true;
                }
            }
            else
            {
                comboBox1.SelectedIndex = 0;
                Drucker.PrinterName = comboBox1.SelectedItem.ToString();
                validate();
            }
        }

        private void validate()
        {
            //Überprüfen, was der Drucker auch kann (ist es vielleicht ein SW-Drucker?)
            numericUpDown1.Maximum = Drucker.MaximumCopies;
            if (Drucker.SupportsColor)
            {
                radioButtonColor.Enabled = true;
            }
            else
            {
                radioButtonColor.Enabled = false;
                radioButtonMono.Checked = true;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            validate();
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            //Standart Druckeinstellungen werden gespeichert
            Druckeinstellungen ein = new Druckeinstellungen();
            ein.Name = (string)comboBox1.SelectedItem;
            ein.Exemplare = Convert.ToInt16(numericUpDown1.Value);
            ein.Farbe = radioButtonColor.Checked;
            ein.Save();
            this.DialogResult = DialogResult.OK;
        }
    }
}
