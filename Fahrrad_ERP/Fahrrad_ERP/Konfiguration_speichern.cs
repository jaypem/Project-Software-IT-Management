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
    public partial class Konfiguration_speichern : Form
    {
        public Konfiguration_speichern()
        {
            InitializeComponent();
        }

        string Kunde;
        string[] Kundeninfo;
        public string KonfID;
        public bool Konf; //true bei Konfiguration, false bei Bestellung
        private List<List<string>> dataList = new List<List<string>>();

        private void button1_Click(object sender, EventArgs e)
        {
            Auswahl a = new Auswahl("Kunden");
            if (a.ShowDialog() == DialogResult.OK)
            {
                Kunde = a.get_ID();
                Kundeninfo = a.get_ZeilenInfo();
                buttonKunde.Text = Kundeninfo[0];
            }
        }

        private void radioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonKunde.Checked || radioButtonBestell.Checked) buttonKunde.Enabled = true; else buttonKunde.Enabled = false;
            if (radioButtonKunde.Checked || radioButtonVorlage.Checked) textBoxBez.Enabled = true; else textBoxBez.Enabled = false;
        }

        private void buttonSpeichern_Click(object sender, EventArgs e)
        {
            Database_Fahrrad daten = new Database_Fahrrad();
            string sqlcmd = "";
            string str;
            if (radioButtonVorlage.Checked)
            {
                if (validate(1))
                {
                    str = textBoxBez.Text;
                    sqlcmd = "INSERT INTO konfigurationen (Bezeichnung) VALUES ('" + str + "')";
                    daten.setData(sqlcmd);
                    sqlcmd = "SELECT KonfigurationID FROM konfigurationen ORDER BY KonfigurationID";
                    dataList = daten.getData(sqlcmd);
                    KonfID = dataList[dataList.Count - 1][0].ToString();
                    Konf = true;
                }
            }
            if (radioButtonKunde.Checked)
            {
                if (validate(2))
                {
                    str = textBoxBez.Text;
                    sqlcmd = "INSERT INTO konfigurationen (Bezeichnung, KundenID) VALUES ('" + str + "', '" + Kunde + "')";
                    daten.setData(sqlcmd);
                    sqlcmd = "SELECT KonfigurationID FROM konfigurationen WHERE Bezeichnung = '"+str+"' AND KundenID = '"+Kunde+"' ORDER BY KonfigurationID";
                    dataList = daten.getData(sqlcmd);
                    KonfID = dataList[dataList.Count - 1][0].ToString();
                    Konf = true;
                }
            }
            if (radioButtonBestell.Checked)
            {
                if (validate(3))
                {
                    string datum = DateTime.Today.ToString("yyyy-MM-dd"); ; 
                    sqlcmd = "INSERT INTO bestellung (KundenID, Datum) VALUES ('" + Kunde + "', '" + datum + "')";
                    daten.setData(sqlcmd);
                    sqlcmd = "SELECT BestellungID FROM bestellung WHERE KundenID = '" + Kunde + "' AND Datum = '" + datum + "' ORDER BY BestellungID";
                    dataList = daten.getData(sqlcmd);
                    KonfID = dataList[dataList.Count - 1][0].ToString();
                    Konf = false;
                }
            }
            this.DialogResult = DialogResult.OK;
        }

        private bool validate(int i)
        {
            bool ok = true;
            textBoxBez.ForeColor = Color.Black;
            buttonKunde.ForeColor = Color.Black;
            switch (i)
            {
                case 1:
                    if (textBoxBez.TextLength > 45)
                    {
                        ok = false;
                        textBoxBez.ForeColor = Color.Red;
                        ((main)this.MdiParent).Status("Die gewählte Bezeichnung ist zu lang.");
                    }
                    if (textBoxBez.TextLength < 3)
                    {
                        ok = false;
                        textBoxBez.ForeColor = Color.Red;
                        ((main)this.MdiParent).Status("Die gewählte Bezeichnung ist zu kurz.");
                    }
                    break;
                case 2:
                    if (textBoxBez.TextLength > 45)
                    {
                        ok = false;
                        textBoxBez.ForeColor = Color.Red;
                        ((main)this.MdiParent).Status("Die gewählte Bezeichnung ist zu lang.");
                    }
                    if (textBoxBez.TextLength < 3)
                    {
                        ok = false;
                        textBoxBez.ForeColor = Color.Red;
                        ((main)this.MdiParent).Status("Die gewählte Bezeichnung ist zu kurz.");
                    }
                    if (Kunde == "")
                    {
                        ok = false;
                        buttonKunde.ForeColor = Color.Red;
                        ((main)this.MdiParent).Status("Es wurde kein Kunde ausgewählt.");
                    }
                    break;
                case 3:
                    if (Kunde == "")
                    {
                        ok = false;
                        buttonKunde.ForeColor = Color.Red;
                        ((main)this.MdiParent).Status("Es wurde kein Kunde ausgewählt.");
                    }
                    break;
            }
            return ok;
        }
    }
}
