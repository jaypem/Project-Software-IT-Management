using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Fahrrad_ERP
{
    public partial class Kunde_ändern : Form
    {
        public Kunde_ändern(string version)
        {
            InitializeComponent();
            vers = version;
        }

        public string ID;
        public string vers;
        Database_Fahrrad daten = new Database_Fahrrad();
        List<List<string>> dataList = new List<List<string>>();

        private void Kunde_ändern_Load(object sender, EventArgs e)
        {
            //Form kann sowohl zum Anlegen als auch Bearbeiten von Daten genutzt werden. Titel und Button Text werden hier gesetzt.
            if (vers == "Anlegen")
            {
                btn_save.Text = "Anlegen";
                this.Text = "Kunden anlegen";
            }
            else
            {
                btn_save.Text = "Bearbeiten";
                this.Text = "Kunden bearbeiten";
                DataLoad();
            }
        }
        private void DataLoad()
        {
            string sqlcmd = "SELECT Nachname, Name, Haendler, Firma, PLZ, Ort, Strasse, Hausnummer, Geschlecht, Geburtsdatum, EMail FROM kunden WHERE KundenID = '"+ID+"'";
            dataList = daten.getData(sqlcmd);
            List<string> list = dataList[0];
            textBoxNachname.Text = list[0].ToString();
            textBoxName.Text = list[1].ToString();
            textBoxFirma.Text = list[3].ToString();
            textBoxPlz.Text = list[4].ToString();
            textBoxOrt.Text = list[5].ToString();
            textBoxStr.Text = list[6].ToString();
            textBoxNr.Text = list[7].ToString();
            textBoxGeb.Text = list[9].ToString();
            textBoxMail.Text = list[10].ToString();
            if (list[2].ToString() == "0")
            {
                textBoxFirma.Visible = false;
                checkBoxFirma.Checked = false;
            }
            else
            {
                textBoxFirma.Visible = true;
                checkBoxFirma.Checked = true;
            }
            if (list[8].ToString() == "m")
            {
                comboBoxAnrede.SelectedIndex = 1;
            }
            else
            {
                comboBoxAnrede.SelectedIndex = 0;
            }            
        }

        private void checkBoxFirma_CheckedChanged(object sender, EventArgs e)
        {
            textBoxFirma.Visible = checkBoxFirma.Checked;
            comboBoxAnrede.ResetText();
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            if (this.valide())
            {
                //Textdaten einlesen
                string[] str = new string[11];
                str[0] = textBoxNachname.Text;
                str[1] = textBoxName.Text;
                if (comboBoxAnrede.Text == "Herr")
                    str[2] = "m";
                else
                    str[2] = "w";
                str[3] = Convert.ToDateTime(textBoxGeb.Text).ToString("yyyy-MM-dd");
                str[4] = textBoxMail.Text;
                str[5] = textBoxPlz.Text;
                str[6] = textBoxOrt.Text;
                str[7] = textBoxStr.Text;
                str[8] = textBoxNr.Text;
                if (checkBoxFirma.Checked)
                {
                    str[9] = "1";
                    str[10] = textBoxFirma.Text;
                }
                else if (textBoxFirma.Visible)
                    str[9] = "0";
                //Datensatz vorbereiten
                string sqlcmd = "";
                //Fall 0: Neu anlegen des Kunden
                if (vers == "Anlegen")// && this.valide())   noch hinzufügen
                {
                    sqlcmd = "INSERT INTO kunden(Nachname, Name, Geschlecht, Geburtsdatum, EMail, PLZ, Ort, Strasse, Hausnummer, Haendler, Firma)" +
                                        "VALUES ('" + str[0] + "','" + str[1] + "','" + str[2] + "','" + str[3] + "','" + str[4] + "','" + str[5] +
                                              "','" + str[6] + "','" + str[7] + "','" + str[8] + "','" + str[9] + "','" + str[10] + "')";
                    MessageBox.Show("Der Kunde " + str[0] + str[1] + " wurde erfolgreich angelegt. Eingabe erfolgreich");
                }
                //Fall 1: Bearbeiten eines Kunden
                else if (vers == "Bearbeiten")// && valide())    noch hinzufügen
                {
                    sqlcmd = "UPDATE kunden SET Nachname= '" + str[0] + "',Name='" + str[1] + "',Geschlecht='" + str[2] + "', " +
                             "Geburtsdatum='" + str[3] + "',EMail='" + str[4] + "',PLZ='" + str[5] + "',Ort='" + str[6] + "', " +
                             "Strasse='" + str[7] + "',Hausnummer='" + str[8] + "',Haendler='" + str[9] + "',Firma='" + str[10] + "' " +
                             "WHERE KundenID = '" + ID + "'";
                    MessageBox.Show("Der Kunde " + str[0] + str[1] + " wurde erfolgreich bearbeitet. Eingabe erfolgreich");
                }
                Database_Fahrrad daten = new Database_Fahrrad();
                daten.setData(sqlcmd);
                this.DialogResult = DialogResult.OK;
            }
        }

        private bool valide()
        {
            bool val = true;
            textBoxFirma.BackColor = Color.White;
            textBoxName.BackColor = Color.White;
            textBoxNachname.BackColor = Color.White;
            textBoxOrt.BackColor = Color.White;
            textBoxPlz.BackColor = Color.White;
            textBoxNr.BackColor = Color.White;
            textBoxStr.BackColor = Color.White;
            textBoxMail.BackColor = Color.White;
            //Firma J/N
            if (checkBoxFirma.Checked)
            {
                //Firma darf nicht leer sein
                if (textBoxFirma.Text.Length < 4)
                {
                    val = false;
                    MessageBox.Show("Der Firmenname muss aus mindestens 3 Zeichen bestehen!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxFirma.BackColor = Color.Red;
                }
            }
            else
            {
                if (comboBoxAnrede.Text == "")
                {
                    val = false;
                    MessageBox.Show("Sie müssen eine Anrede wählen!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                //Namen prüfen
                if (textBoxNachname.Text.Length < 4 || Regex.IsMatch(textBoxNachname.Text, "[^A-Z][^a-z]"))
                {
                    val = false;
                    MessageBox.Show("Der Nachname muss aus mindestens 3 Zeichen bestehen!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxNachname.BackColor = Color.Red;
                }
                else if (textBoxName.Text.Length < 4 || Regex.IsMatch(textBoxName.Text, "[^A-Z][^a-z]"))
                {
                    val = false;
                    MessageBox.Show("Der Vorname muss aus mindestens 3 Zeichen bestehen!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    textBoxName.BackColor = Color.Red;
                }
            }
            //PLZ prüfen
            if (textBoxPlz.Text.Length != 5)
            {
                val = false;
                MessageBox.Show("Die Postleitzahl muss aus 5 Zahlen bestehen!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxPlz.BackColor = Color.Red;
            }
            //Ort prüfen
            else if (textBoxOrt.Text.Length < 4 || Regex.IsMatch(textBoxOrt.Text, "[^A-Z][^a-z]"))
            {
                val = false;
                MessageBox.Show("Der Ort muss aus mindestens 3 Zeichen bestehen!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxOrt.BackColor = Color.Red;		 
	        }
            //Straße prüfen
            else if (textBoxStr.Text.Length < 4)
            {
                val = false;
                MessageBox.Show("Die Straße muss aus mindestens 3 Zeichen bestehen!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxStr.BackColor = Color.Red;
            }
            //Haunummer prüfen
            else if (textBoxNr.Text.Length < 2 || Regex.IsMatch(textBoxNr.Text, "[^0-9][^a-z]"))
            {
                val = false;
                MessageBox.Show("Die Hausnummer ist ungültig!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxNr.BackColor = Color.Red;
            }
            //Mail prüfen (unter 6 Zeichen ist keine Mailadresse)
            else if (!Regex.IsMatch(textBoxMail.Text, @"^(?("")("".+?""@)|(([0-9a-zA-Z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-zA-Z])@))" + 
              @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,6}))$"))  {
                val = false;
                MessageBox.Show("Mailadresse ungültig!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                textBoxMail.BackColor = Color.Red;
                }
            return val;
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            //ohne Speicherung geschlossen
            this.Close();
        }
    }
}
