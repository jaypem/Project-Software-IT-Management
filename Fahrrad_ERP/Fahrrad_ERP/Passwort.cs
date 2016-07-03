using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fahrrad_ERP
{
    public partial class Passwort : Form
    {
        public Passwort()
        {
            InitializeComponent();
        }
        String passwort;
        private void buttonOK_Click(object sender, EventArgs e)
        {
            set_color(0);
            if (textBoxPW.Text == passwort && textBoxPWnew1.Text == textBoxPWnew2.Text)
            {
                set_pass(textBoxPW.Text);
            }
            else
            {
                if (textBoxPW.Text != passwort)
                {
                    set_color(1);
                    textBoxPW.Clear();
                    textBoxPW.Focus();
                    MessageBox.Show("Ihr aktuelles Passwort ist nicht korrekt!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                if (textBoxPWnew1.Text != textBoxPWnew2.Text)
                {
                    set_color(2);
                    textBoxPWnew1.Clear();
                    textBoxPWnew2.Clear();
                    textBoxPWnew1.Focus();
                    MessageBox.Show("Ihr neues Passwort wurde nicht korrekt wiederholt!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private string get_pass()
        {
            string pw = "";
            return pw;
        }

        private void set_pass(String pw)
        {
            MessageBox.Show("Passwort wurde erfolgreich geändert.", "Bestätigung", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void set_color(int i)
        {
            switch (i)
            {
                case 0:
                    textBoxPW.BackColor = Color.White;
                    textBoxPWnew1.BackColor = Color.White;
                    textBoxPWnew2.BackColor = Color.White;
                    break;
                case 1:
                    textBoxPW.BackColor = Color.Red;
                    break;
                case 2:
                    textBoxPWnew1.BackColor = Color.Red;
                    textBoxPWnew2.BackColor = Color.Red;
                    break;
            }
        }

        private void Passwort_Load(object sender, EventArgs e)
        {
            passwort = get_pass();
        }

        private void buttonAB_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
