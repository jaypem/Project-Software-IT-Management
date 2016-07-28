using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Fahrrad_ERP
{
    public partial class Mitarbeiter_ändern : Form
    {
        public Mitarbeiter_ändern(string vers)
        {
            InitializeComponent();
            version = vers;
        }

        public string login;
        string version;

        private void Mitarbeiter_Load(object sender, EventArgs e)
        {
            //Form kann sowohl zum Anlegen als auch Bearbeiten von Daten genutzt werden. Titel und Button Text werden hier gesetzt.
            if (version == "Anlegen")
            {
                buttonOk.Text = "Anlegen";
                this.Text = "Mitarbeiter anlegen";
                textBox1.Enabled = true;
            }
            if (version == "Bearbeiten")
            {
                buttonOk.Text = "Speichern";
                this.Text = "Mitarbeiter bearbeiten";
                textBox1.Enabled = false;
                Data_Load();
            }
        }
        private void Data_Load()
        {
            //Daten werden auf Basis des Login's aus der Datenbank gezogen und in die einzelnen Felder gesetzt
            List<List<string>> strList = new List<List<string>>();
            string sqlcmd = "SELECT `login`, `Nachname`, `Name`, `abteilung`, `admin`, `ansichtL`, `ansichtV`, `ansichtW` FROM `personal` WHERE `login` LIKE '" + login + "'";
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
            //ohne Speicherung geschlossen
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Textdaten einlesen
            string[] str = new string[9];
            str[0] = textBox1.Text;
            str[2] = textBox2.Text;
            str[3] = textBox3.Text;
            str[4] = textBox4.Text;
            if (checkBox1.Checked == true) str[5] = "1"; else str[5] = "";
            if (checkBox2.Checked == true) str[6] = "1"; else str[6] = "";
            if (checkBox3.Checked == true) str[7] = "1"; else str[7] = "";
            if (checkBox4.Checked == true) str[8] = "1"; else str[8] = "";
            //Datensatz vorbereiten
            //Fall 0: Neu anlegen des Mitarbeiters
            if (version == "Anlegen" && valide() == true)
            {
                str[1] = createPasswort();
                string sqlcmd = "INSERT INTO `personal`(`login`, `passwort`, `Nachname`, `Name`, `abteilung`, `admin`, `ansichtL`, `ansichtV`, `ansichtW`) VALUES ('" + 
                    str[0] + "','" + str[1] + "','" + str[2] + "','" + str[3] + "','" + str[4] + "','" + str[5] + "','" + str[6] + "','" + str[7] + "','" + str[8] + "')";
                Database_Fahrrad daten = new Database_Fahrrad();
                daten.setData(sqlcmd);
                MessageBox.Show("Der Mitarbeiter " + str[3] + " " + str[2] + " wurde erfolgreich angelegt. \nBitte teilen Sie dem Mitarbeiter das Initialpasswort >>"
                    + str[1] + " << mit und weisen Sie ihn darauf hin, dass er dieses bei der Erstanmeldung ändern soll.", "Eingabe erfolgreich", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.DialogResult = DialogResult.OK;
            }
            //Fall 1: Bearbeiten des Mitarbeiters
            if (version == "Bearbeiten" && valide() == true)
            {
                string sqlcmd = "UPDATE `personal` SET `Nachname`='" + str[2] + "',`Name`='" + str[3] + "',`abteilung`='" + str[4] + "',`admin`='" + str[5] + "',`ansichtL`='" + str[6] + "',`ansichtV`='" + str[7] + "',`ansichtW`='" + str[8] + "' WHERE `login` LIKE '"+str[0]+"'";
                Database_Fahrrad daten = new Database_Fahrrad();
                daten.setData(sqlcmd);
                this.DialogResult = DialogResult.OK;
            }
        }

        private bool valide()
        {
            bool val = true;
            //Rücksetzen der Felder
            textBox1.BackColor = Color.White;
            textBox1.ForeColor = Color.Black;
            textBox2.BackColor = Color.White;
            textBox2.ForeColor = Color.Black;
            textBox3.BackColor = Color.White;
            textBox3.ForeColor = Color.Black;
            textBox4.BackColor = Color.White;
            textBox4.ForeColor = Color.Black;
            //bei anlegen, wird Login auf Existenz geprüft
            if (textBox1.Enabled == true) {
            if (textBox1.Text.Length < 3 || textBox1.Text.Length > 45)
            {
                val = false;
                MessageBox.Show("Der Login muss aus 3-45 Zeichen bestehen!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox1.BackColor = Color.Red;
            }
            else
            {
                if (Regex.IsMatch(textBox1.Text, "[^a-z]^[A-Z]") == true)
                {
                    val = false;
                    MessageBox.Show("Das Feld Login enthält ungültige Zeichen!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox1.ForeColor = Color.Red;
                } else 
                    //sperren bestimmter Namen!
                    if(textBox1.Text.ToLower() == "admin" || textBox1.Text.ToLower() == "verwaltung" || textBox1.Text.ToLower() == "werkstatt" || textBox1.Text.ToLower() == "laden") {
                        val = false;
                        MessageBox.Show("Der gewählte Login darf nicht verwendet werden!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox1.ForeColor = Color.Red;
                }
                else
                {
                    List<List<string>> strList = new List<List<string>>();
                    string sqlcmd = "SELECT `login` FROM `personal` WHERE `login` LIKE '" + textBox1.Text + "'";
                    Database_Fahrrad daten = new Database_Fahrrad();
                    strList = daten.getData(sqlcmd);
                    if (strList.Count != 0)
                    {
                        val = false;
                        MessageBox.Show("Dieser Login ist bereits vorhanden", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        textBox1.ForeColor = Color.Red;
                    }
                }
            }
            }
            //Name prüfen
            if (textBox2.Text.Length < 2 || textBox2.Text.Length > 45)
            {
                val = false;
                MessageBox.Show("Namen müssen aus 2 - 45 Zeichen bestehen!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox2.BackColor = Color.Red;
            }
            else
            {
                if (Regex.IsMatch(textBox2.Text, "[^a-z]^[A-Z]^[0-9]") == true)
                {
                    val = false;
                    MessageBox.Show("Das Feld Name enthält ungültige Zeichen!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox2.ForeColor = Color.Red;
                }
            }
            //Vorname prüfen
            if (textBox3.Text.Length < 2 || textBox3.Text.Length > 45)
            {
                val = false;
                MessageBox.Show("Vornamen müssen aus 2 - 45 Zeichen bestehen!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox3.BackColor = Color.Red;
            }
            else
            {
                if (Regex.IsMatch(textBox3.Text, "[^a-z]^[A-Z]^[0-9]") == true)
                {
                    val = false;
                    MessageBox.Show("Das Feld Vorname enthält ungültige Zeichen!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox3.ForeColor = Color.Red;
                }
            }
            //Abteilung prüfen
            if (textBox4.Text.Length == 0)
            {
                val = false;
                MessageBox.Show("Es fehlt eine Eingabe für die Abteilung!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBox4.BackColor = Color.Red;
            }
            else
            {
                if (Regex.IsMatch(textBox4.Text, "[^a-z]^[A-Z]") == true)
                {
                    val = false;
                    MessageBox.Show("Das Feld Abteilung enthält ungültige Zeichen!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBox4.ForeColor = Color.Red;
                }
            }
            //mindestens eine Sicht gewählt
            if (checkBox1.Checked == false && checkBox2.Checked == false && checkBox3.Checked == false && checkBox4.Checked == false) 
            { 
                val = false;
                MessageBox.Show("Wählen Sie mindestens eine Ansicht aus, für die der Mitarbeiter berechtigt ist!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            return val;
        }

        private string createPasswort()
        {
            string pw = "";
            //Erstellung eines 10 Zeichen langen Passwortes durch den Zufallsalgorithmus
            char[] grBuch = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
            char[] klBuch = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
            char[] zahl = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            int j,k;
            Random r = new Random();
            for (int i = 1; i <= 10; i++)
            {
                j = r.Next(3);
                switch (j) 
                {
                    case 0:
                        k = r.Next(0,25);
                        pw += grBuch[k];
                        break;
                    case 1:
                        k = r.Next(0,25);
                        pw += klBuch[k];
                        break;
                    case 2:
                        k = r.Next(9);
                        pw += zahl[k];
                        break;
                }
             }
            return pw;
        }

        public string name()
        {
            return textBox3.Text + " " + textBox2.Text;
        }

    }
}
