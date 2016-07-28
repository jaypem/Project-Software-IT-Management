using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fahrrad_ERP.taskM
{
    public partial class newTask : Form
    {
        string an;
        string be;
        string na;
        public newTask(string Adressat = "", string Betreff = "", string Nachricht = "")
        {
            InitializeComponent();
            an = Adressat;
            add = Adressat;
            be = Betreff;
            na = Nachricht;
        }
        Database_Fahrrad daten = new Database_Fahrrad();
        string add;
        string[] Zeileninfo;
        private void newTask_Load(object sender, EventArgs e)
        {
            textBoxBetreff.Text = be;
            textBoxNachricht.Text = na;
            if (an != "")
            {
                buttonAn.Text = an;
            }
            else
            {
                buttonAn.Text = "wählen...";
            }
        }

        private void buttonAn_Click(object sender, EventArgs e)
        {
            Auswahl a = new Auswahl("Mitarbeiter");
            if (a.ShowDialog() == DialogResult.OK)
            {
                add = a.get_ID();
                Zeileninfo = a.get_ZeilenInfo();
                buttonAn.Text = Zeileninfo[0];
            }
        }

        private void buttonHin_Click(object sender, EventArgs e)
        {
            if (buttonAn.Text != "wählen...")
            {
                if (textBoxBetreff.TextLength > 1)
                {
                    Management m = new Management();
                    m.newTask(textBoxBetreff.Text, textBoxNachricht.Text, User.login, add);
                    this.DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show("Bitte geben Sie einen Betreff ein", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Bitte wählen Sie zunächst einen Adressaten aus!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

    }
}