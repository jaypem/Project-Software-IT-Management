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
        private ListViewColumnSorter sorter = new ListViewColumnSorter();
        private void fill()
        {
            labelNummer.Text = bestellung[0];
            labelName.Text = bestellung[1];
            labelDatum.Text = bestellung[2];
            labelSumme.Text = bestellung[3];
            buttonDruck.Enabled = true;
            buttonErstellen.Enabled = true;
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
        private void buttonWahl_Click(object sender, EventArgs e)
        {
            Bestellung_wählen b = new Bestellung_wählen();
            if (b.ShowDialog(this) == DialogResult.OK)
            {
                bestellung = b.get_bestell();
                kunde = b.get_kunde();
                ((main)this.MdiParent).Status("Es wurde die Bestellung " + bestellung[0] + " ausgewählt.");
                fill();
            }
            else
            {
                ((main)this.MdiParent).Status("Auswahl abgebrochen!");
            }
        }
        private void buttonDruck_Click(object sender, EventArgs e)
        {
            print();
        }
        public void print()
        {
            printDocument1.DocumentName = "Rechnung" + bestellung[0];
            DialogResult r = printPreviewDialog1.ShowDialog();
            if (r == DialogResult.OK) MessageBox.Show("DRUCK");
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            Druck dr = new Druck();
            dr.Briefkopf(e.Graphics);
            dr.Geschäftsangaben(e.Graphics, 1, 1);
            dr.Adresszeile(e.Graphics, new string[] {kundendaten[0],kundendaten[1],kundendaten[2], kundendaten[3] });
            dr.Postenauflistung(e.Graphics, kunde, bestellung[0], bestellung[3],"Rechnung", bestellung[2], dataList);
        }

    }
}
