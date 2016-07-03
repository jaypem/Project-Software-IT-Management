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
    public partial class Mitarbeiter : Form
    {
        public Mitarbeiter()
        {
            InitializeComponent();
        }

        public static string login;
        public static int version;

        private void Mitarbeiter_Load(object sender, EventArgs e)
        {
            //Form kann sowohl zum Anlegen als auch Bearbeiten von Daten genutzt werden. Das Aussehen wird hier gesetzt.
            if (Mitarbeiter.version == 0)
            {
                button1.Text = "Anlegen";
                this.Text = "Mitarbeiter anlegen";
                textBox1.Enabled = true;
            }
            else
            {
                button1.Text = "Speichern";
                this.Text = "Mitarbeiter bearbeiten";
                textBox1.Enabled = false;
                Data_Load();
            }
        }
        private void Data_Load()
        {
            //Daten werden auf Basis des Login's in die einzelnen Felder gesetzt
            List<List<string>> strList = new List<List<string>>();
            string sqlcmd = "SELECT `login`, `Nachname`, `Name`, `abteilung`, `admin`, `ansichtL`, `ansichtV`, `ansichtW` FROM `personal` WHERE `login` LIKE '" + Mitarbeiter.login + "'";
            Database_Fahrrad daten = new Database_Fahrrad();
            strList = daten.getData(sqlcmd);
            string[] str = new string[strList[0].Count];
            strList[0].CopyTo(str, 0);
            textBox1.Text = str[0].ToString();
            textBox2.Text = str[1].ToString();
            textBox3.Text = str[2].ToString();
            textBox4.Text = str[3].ToString();
            if (str[4].ToString() == "1") checkBox1.Checked = true; else checkBox1.Checked = false;
            if (str[5].ToString() == "1") checkBox2.Checked = true; else checkBox2.Checked = false;
            if (str[6].ToString() == "1") checkBox3.Checked = true; else checkBox3.Checked = false;
            if (str[7].ToString() == "1") checkBox4.Checked = true; else checkBox4.Checked = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
