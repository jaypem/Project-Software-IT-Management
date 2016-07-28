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
    public partial class Material_ändern : Form
    {
        public Material_ändern(string version)
        {
            InitializeComponent();
            this.vers = version;
        }

        public string ID;
        public string vers;
        Database_Fahrrad daten = new Database_Fahrrad();
        List<List<string>> dataList = new List<List<string>>();

        private void btn_save_Click(object sender, EventArgs e)
        {
            //Textdaten einlesen
            string[] str = new string[3];
            str[0] = tb_bezeichnung.Text;
            str[1] = comboBoxProdkat.Text;            
            str[2] = tb_preis.Text;             
            //Datensatz vorbereiten
            string sqlcmd = "";
            if (valide())
            {
                //Fall 0: Neu anlegen des Materials
                if (vers == "Anlegen")
                {
                    sqlcmd = "INSERT INTO produkte (Bezeichnung, Produktkategorie, Lagerbestand, Preis) " +
                        "VALUES ('" + str[0] + "', (SELECT KategorieID FROM produktkategorie WHERE Bezeichnung = '" + str[1] + "'), '" +
                        "0', '" + str[2] + "')";
                    tb_bezeichnung.Clear();
                    tb_preis.Clear();
                    comboBoxProdkat.SelectedIndex = 0;
                    MessageBox.Show("Das Material " + str[0] + " wurde erfolgreich angelegt.", "Neues Material", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                //Fall 1: Bearbeiten des Materials
                if (vers == "Bearbeiten")
                {
                    sqlcmd = "UPDATE produkte SET Bezeichnung= '" + str[0] + "', " +
                        "Produktkategorie=(SELECT KategorieID FROM produktkategorie WHERE Bezeichnung = '" + str[1] + "'), " +
                        "Preis='" + str[2] + "' WHERE ProduktID = '" + ID + "'";
                }
                Database_Fahrrad daten = new Database_Fahrrad();
                daten.setData(sqlcmd);
                this.DialogResult = DialogResult.OK;
            }
        }

        private void Material_ändern_Load(object sender, EventArgs e)
        {   
            string sqlcmd = "SELECT Bezeichnung FROM produktkategorie ORDER BY Bezeichnung";
            List<List<string>> prodkatolist = daten.getData(sqlcmd);
            for (int i = 0; i < prodkatolist.Count; i++)
            {
                comboBoxProdkat.Items.Add(prodkatolist[i][0].ToString());
            }
            //Form kann sowohl zum Anlegen als auch Bearbeiten von Daten genutzt werden. Titel und Button Text werden hier gesetzt.
            if (vers == "Anlegen")
            {
                btn_save.Text = "Anlegen";
                this.Text = "Material anlegen";
            }
            else
            {
                btn_save.Text = "Bearbeiten";
                this.Text = "Material bearbeiten";
                DataLoad();
            }
        }

        private void DataLoad()
        {
            string sqlcmd = "SELECT p.Bezeichnung, pk.Bezeichnung, Preis FROM produkte p inner join " +
                            "produktkategorie pk ON p.produktkategorie = pk.KategorieID WHERE ProduktID = '" + ID + "'";
            dataList = daten.getData(sqlcmd);
            List<string> list = dataList[0];
            tb_bezeichnung.Text = list[0].ToString();
            comboBoxProdkat.Text = list[1].ToString();            
            tb_preis.Text = list[2].ToString();                          
        }

        private bool valide()
        {
            bool val = true;
            //Rücksetzen der Felder
            tb_bezeichnung.BackColor = Color.White;
            tb_preis.BackColor = Color.White;
            tb_preis.ForeColor = Color.Black;
            if (tb_bezeichnung.Text.Length < 3 || tb_bezeichnung.Text.Length > 45)
            {
                tb_bezeichnung.BackColor = Color.Red;
                MessageBox.Show("Die Bezeichnung muss aus 3 - 45 Zeichen bestehen!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                val = false;
            }
            if (comboBoxProdkat.SelectedItem == null)
            {
                MessageBox.Show("Es wurde keine Kategorie ausgewählt.","Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                val = false;
            }
            string preis = tb_preis.Text;
            preis = preis.Replace(',', '.');
            try
            {
                Convert.ToDouble(preis);
            }
            catch
            {
                if (preis.Length != 0) tb_preis.ForeColor = Color.Red;
                else tb_preis.BackColor = Color.Red;
                MessageBox.Show("Der Preis ist nicht korrekt eingetragen!", "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                val = false;
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
