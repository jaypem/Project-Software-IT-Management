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
                set_pass(textBoxPWnew1.Text);
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

        private string getPasswort(string login)
        {
            List<List<string>> strList = new List<List<string>>();
            string sqlcmd = "SELECT `passwort` FROM `personal` WHERE `login` LIKE '" + login + "'";
            Database_Fahrrad daten = new Database_Fahrrad();
            strList = daten.getData(sqlcmd);
            try
            {
                string[] str = new string[strList[0].Count];
                strList[0].CopyTo(str, 0);
                if (str == null) str[0] = "";
                return str[0];
            }
            catch (Exception)
            {
                return "";
            }
        }

        private void set_pass(String pw)
        {
            string sqlcmd = "UPDATE `personal` SET `Passwort`='" + pw + "' WHERE `login` LIKE '" + User.login + "'";
            Database_Fahrrad daten = new Database_Fahrrad();
            daten.setData(sqlcmd);
            ((main)this.MdiParent).Status("Passwort erfolgreich geändert.");
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
            passwort = getPasswort(User.login);
        }

        private void buttonAB_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
