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
    public partial class Rechnung : Form
    {
        public Rechnung()
        {
            InitializeComponent();
            listView1.ListViewItemSorter = sorter;
        }
        private string[] bestellung = new string[5];
        private string[] kundendaten = new string[4];
        private string kunde;
        private List<List<string>> dataList = new List<List<string>>(); //für Bestellung
        private List<List<string>> dataList2 = new List<List<string>>(); //für Kunde
        private ListViewSortieren sorter = new ListViewSortieren();
        private void fill()
        {
            labelNummer.Text = bestellung[0];
            labelName.Text = bestellung[1];
            labelDatum.Text = bestellung[2];
            labelSumme.Text = bestellung[3];
            //Bestellungpositionen auslesen
            string sqlcmd = "SELECT Position, produkte.ProduktID, produkte.Bezeichnung, Menge, bestellungpos.Preis, Summe FROM bestellungpos INNER JOIN produkte ON produkte.ProduktID = bestellungpos.ProduktID WHERE BestellungID = '" + bestellung [0]+"'";
            Database_Fahrrad daten = new Database_Fahrrad();
            dataList = daten.getData(sqlcmd);
            //Kunde
            sqlcmd = "SELECT Firma, Nachname, Name, Strasse, Hausnummer, PLZ, Ort, Haendler, Geschlecht FROM kunden WHERE KundenID = '" + kunde + "'";
            dataList2 = daten.getData(sqlcmd);
            //Bestellpositionen einfügen
            listView1.Items.Clear();
            foreach (List<string> list in dataList)
            {
                listView1.Items.Add(new ListViewItem(new string[] { list[0].ToString(), list[1].ToString(), list[2].ToString(), list[3].ToString(), (Convert.ToDecimal(list[4])).ToString("0.00 €"), (Convert.ToDecimal(list[5])).ToString("0.00 €") }));
            }
            listView1.Items.Add(new ListViewItem(new string[] { "", "", "", "", "", "=========" }));
            listView1.Items.Add(new ListViewItem(new string[] { "", "", "", "", "TOTAL", bestellung[3] }));
            listView1.Items.Add(new ListViewItem(new string[] { "", "", "", "", "MwSt. 19%", (Convert.ToDouble(bestellung[3].TrimEnd(' ','€'))*0.19).ToString("0.00 €") }));
            //Kundendaten in String schieben
            List<string> l = new List<string>(dataList2[0]);
            if (l[7].ToString() == "1") //Wenn Händler
            {
                if (l[2].ToString() != "") //mit Ansprechsprechpartner
                {
                    kundendaten[0] = l[0].ToString();
                    kundendaten[1] = "z.H. " + l[2].ToString() + " " + l[1].ToString();
                    kundendaten[2] = l[3].ToString() + " " + l[4].ToString();
                    kundendaten[3] = l[5].ToString() + " " + l[6].ToString();
                }
                else //ohne Ansprechpartner
                {
                    kundendaten[0] = "";
                    kundendaten[1] = l[0].ToString();
                    kundendaten[2] = l[3].ToString() + " " + l[4].ToString();
                    kundendaten[3] = l[5].ToString() + " " + l[6].ToString();
                }
            }
            else //Privatperson
            {
                if (l[8].ToString() == "w") kundendaten[0] = "Frau"; else kundendaten[0] = "Herrn";
                kundendaten[1] = l[2].ToString() + " " + l[1].ToString();
                kundendaten[2] = l[3].ToString() + " " + l[4].ToString();
                kundendaten[3] = l[5].ToString() + " " + l[6].ToString();
            }
        }
        public void buttonWahl_Click(object sender, EventArgs e)
        {
            Auswahl a = new Auswahl("Bestellung");
            if (a.ShowDialog(this) == DialogResult.OK)
            {
                bestellung = a.get_ZeilenInfo();
                kunde = a.get_ID();
                ((main)this.MdiParent).Status("Es wurde die Bestellung " + bestellung[0] + " ausgewählt.");
                fill();
            }
            else
            {
                ((main)this.MdiParent).Status("Auswahl abgebrochen!");
            }
        }
        public void print()
        {
            if (labelNummer.Text != "")
            {
                Druck dr = new Druck();
                printDocument1.DocumentName = "Rechnung" + bestellung[0];
                PrintDialog p = printDialog1;
                p.PrinterSettings = dr.Einstellungen();
                if (p.ShowDialog() == DialogResult.OK)
                {
                    printDocument1.PrinterSettings = p.PrinterSettings;
                    printDocument1.Print();
                }
            }
            else
            {
                MessageBox.Show("Bitte wählen Sie zunächst eine Bestellung aus!", "Keine Bestellung gewählt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    Auswahl a = new Auswahl("Bestellung");
            if (a.ShowDialog(this) == DialogResult.OK)
            {
                bestellung = a.get_ZeilenInfo();
                kunde = a.get_ID();
                ((main)this.MdiParent).Status("Es wurde die Bestellung " + bestellung[0] + " ausgewählt.");
                fill();
            }
            }
        }
        public void printPre()
        {
            if (labelNummer.Text != "")
            {
                printDocument1.DocumentName = "Rechnung" + bestellung[0];
                printPreviewDialog1.Name = "Rechnung" + bestellung[0];
                printPreviewDialog1.ShowDialog();
            }
            else
            {
                MessageBox.Show("Bitte wählen Sie zunächst eine Bestellung aus!", "Keine Bestellung gewählt", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Auswahl b = new Auswahl("Bestellung");
                if (b.ShowDialog(this) == DialogResult.OK)
                {
                    bestellung = b.get_ZeilenInfo();
                    kunde = b.get_ID();
                    ((main)this.MdiParent).Status("Es wurde die Bestellung " + bestellung[0] + " ausgewählt.");
                    fill();
                }
            }
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Druck dr = new Druck();
            dr.Briefkopf(e.Graphics);
            dr.Geschäftsangaben(e.Graphics, 1, 1);
            dr.Adresszeile(e.Graphics, new string[] {kundendaten[0],kundendaten[1],kundendaten[2], kundendaten[3] });
            dr.Postenauflistung(e.Graphics, kunde, bestellung[0], bestellung[3],"Rechnung", bestellung[2], dataList);
        }

        private void Rechnung_Load(object sender, EventArgs e)
        {

        }
    }
}
