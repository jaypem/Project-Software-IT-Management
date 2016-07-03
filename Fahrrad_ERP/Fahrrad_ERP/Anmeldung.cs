using System;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fahrrad_ERP
{
    public partial class Anmeldung : Form
    {
        public Anmeldung()
        {
            InitializeComponent();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            string pw = getPasswort(textBoxUser.Text);
            if (textBoxPW.Text == pw && pw != "")
            {
                new User().setUserInformation(textBoxUser.Text);
                main m = new main();
                m.UserSettings();
                m.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Zugriff verweigert! \n\nNutzer und Kennwort stimmen nicht überein.", "Achtung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxUser.Clear();
                textBoxPW.Clear();
                textBoxUser.Focus();
            }
        }

        private void buttonAB_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private string getPasswort(string login)
        {
            List<List<string>> strList = new List<List<string>>();
            string sqlcmd = "SELECT `passwort` FROM `personal` WHERE `login` LIKE '"+login+"'";
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
        
        private void Anmeldung_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
        }
    }
}