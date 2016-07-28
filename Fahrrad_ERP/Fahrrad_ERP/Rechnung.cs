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
        public Rechnung(string vers)
        {
            InitializeComponent();
            listView1.ListViewItemSorter = sorter;
            version = vers;
        }
        //Nummern für den Druck
        int aktSeite = 1;
        int lastpage = 1;
        string[] bestellung = new string[5];
        string[] kundendaten = new string[5];
        string kunde;
        string version = "";
        List<List<string>> dataList = new List<List<string>>(); //für Bestellung
        List<List<string>> dataList2 = new List<List<string>>(); //für Kunde
        List<List<string>> dataListInhalt = new List<List<string>>(); //für Druck
        ListViewSortieren sorter = new ListViewSortieren();
        private void fill()
        {
            labelNummer.Text = bestellung[0];
            labelName.Text = bestellung[1];
            labelDatum.Text = bestellung[2];
            labelSumme.Text = bestellung[3];
            //Bestellungpositionen auslesen
            string sqlcmd = "SELECT Position, produkte.ProduktID, produkte.Bezeichnung, Menge, bestellungpos.Preis, Summe FROM bestellungpos INNER JOIN produkte ON produkte.ProduktID = bestellungpos.ProduktID WHERE BestellungID = '" + bestellung[0] + "'";
            Database_Fahrrad daten = new Database_Fahrrad();
            dataList = daten.getData(sqlcmd);
            //Kunde
            sqlcmd = "SELECT Firma, Nachname, Name, Strasse, Hausnummer, PLZ, Ort, Haendler, Geschlecht, EMail FROM kunden WHERE KundenID = '" + kunde + "'";
            dataList2 = daten.getData(sqlcmd);
            //Bestellpositionen einfügen
            listView1.Items.Clear();
            foreach (List<string> list in dataList)
            {
                listView1.Items.Add(new ListViewItem(new string[] { list[0].ToString(), list[1].ToString(), list[2].ToString(), list[3].ToString(), (Convert.ToDecimal(list[4])).ToString("0.00 €"), (Convert.ToDecimal(list[5])).ToString("0.00 €") }));
            }
            if (version == "Rechnung")
            {
                listView1.Items.Add(new ListViewItem(new string[] { "", "", "", "", "", "=========" }));
                listView1.Items.Add(new ListViewItem(new string[] { "", "", "", "", "TOTAL", bestellung[3] }));
                listView1.Items.Add(new ListViewItem(new string[] { "", "", "", "", "MwSt. 19%", (Convert.ToDouble(bestellung[3].TrimEnd(' ', '€')) * 0.19).ToString("0.00 €") }));
            }
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
                    kundendaten[4] = l[9].ToString();
                }
                else //ohne Ansprechpartner
                {
                    kundendaten[0] = "";
                    kundendaten[1] = l[0].ToString();
                    kundendaten[2] = l[3].ToString() + " " + l[4].ToString();
                    kundendaten[3] = l[5].ToString() + " " + l[6].ToString();
                    kundendaten[4] = l[9].ToString();
                }
            }
            else //Privatperson
            {
                if (l[8].ToString() == "w") kundendaten[0] = "Frau"; else kundendaten[0] = "Herrn";
                kundendaten[1] = l[2].ToString() + " " + l[1].ToString();
                kundendaten[2] = l[3].ToString() + " " + l[4].ToString();
                kundendaten[3] = l[5].ToString() + " " + l[6].ToString();
                kundendaten[4] = l[9].ToString();
            }
        }
        private void buttonWahl_Click(object sender, EventArgs e)
        {
            open();
        }
        public void open()
        {
            Auswahl a = new Auswahl("Bestellung");
            if (a.ShowDialog(this) == DialogResult.OK)
            {
                bestellung = a.get_ZeilenInfo();
                kunde = a.get_ZeilenInfo()[4];
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
                aktSeite = 1;
                Druck dr = new Druck();
                printDocument1.DocumentName = version + bestellung[0];
                PrintDialog p = printDialog1;
                p.PrinterSettings = dr.Einstellungen();
                dataListInhalt.Clear();
                dataListInhalt.AddRange(dataList);
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
                aktSeite = 1;
                dataListInhalt.Clear();
                dataListInhalt.AddRange(dataList);
                printDocument1.DocumentName = version + bestellung[0];
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
            //Berechnung der benötigten Seiten für den Druck
            if (aktSeite == 1)
            {
                lastpage = 1;
                int items = dataList.Count - 21;
                while (items > 0)
                {
                    items -= 36;
                    lastpage++;
                }
            }
            switch (version)
            {
                case "Rechnung":
                    //Briefkopf nur auf erster Seite
                    if (aktSeite == 1)
                    {
                        dr.Briefkopf(e.Graphics);
                        dr.Adresszeile(e.Graphics, new string[] { kundendaten[0], kundendaten[1], kundendaten[2], kundendaten[3] });
                    }
                    dr.Logo(e.Graphics);
                    dr.Geschäftsangaben(e.Graphics, aktSeite, lastpage);
                    //Unterscheidung der Fälle, ob die aktuelle Seite auch die letzte ist und eventuell auch die erste
                    if (aktSeite == lastpage)
                    {
                        if (aktSeite == 1)
                            dr.Postenauflistung(e.Graphics, kunde, bestellung[0], bestellung[3], version, bestellung[2], dataListInhalt, true, true);
                        else
                            dr.Postenauflistung(e.Graphics, kunde, bestellung[0], bestellung[3], version, bestellung[2], dataListInhalt, false, true);
                    }
                    else
                    {
                        if (aktSeite == 1)
                            dr.Postenauflistung(e.Graphics, kunde, bestellung[0], bestellung[3], version, bestellung[2], dataListInhalt, true, false);
                        else
                            dr.Postenauflistung(e.Graphics, kunde, bestellung[0], bestellung[3], version, bestellung[2], dataListInhalt, false, false);
                        if (aktSeite == 1)
                        {
                            dataListInhalt.RemoveRange(0, 21);
                        }
                        else
                        {
                            dataListInhalt.RemoveRange(0,36);
                        }
                        aktSeite++;
                        e.HasMorePages = true;
                    }
                    break;
                case "Bestellung":
                    dr.Logo(e.Graphics);
                    dr.Schein(e.Graphics, new string[] { kundendaten[1], kundendaten[2], kundendaten[3], kundendaten[4] }, "Bestellschein", kunde, bestellung[0], bestellung[2]);
                    dr.Postenauflistung(e.Graphics, kunde, bestellung[0], bestellung[3], version, bestellung[2], dataList, true, true);
                    break;
            }

        }

        private void Rechnung_Load(object sender, EventArgs e)
        {
            switch (version)
            {
                case "Rechnung":
                    this.Text = version;                    
                    buttonFertig.Visible = false;
                    buttonBearbeiten.Visible = false;
                    break;
                case "Bestellung":
                    this.Text = "Bestellung - Übersicht";
                    buttonFertig.Visible = User.ansichtW;
                    buttonBearbeiten.Visible = User.ansichtV;
                    break;
            }
            
        }

        private void printPreviewDialog1_Load(object sender, EventArgs e)
        {

        }

        private void buttonFertig_Click(object sender, EventArgs e)
        {
            taskM.newTask nt = new taskM.newTask("Verwaltung", "Bestellung " + labelNummer.Text + " bearbeitet", "Die Bestellung " + labelNummer.Text + " wurde soeben von uns fertiggestellt.\nSie können nun eine Rechnung erstellen.");
            if (nt.ShowDialog() == DialogResult.OK)
            {
                ((main)this.MdiParent).Status("Bestellungsstatus an Verwaltung übermittelt!");
            }
            taskM.newTask nt2 = new taskM.newTask("Laden", "Bestellung " + labelNummer.Text + " bearbeitet", "Die Bestellung " + labelNummer.Text + " wurde soeben von uns fertiggestellt.\nBitte informieren Sie den Kunden, sodass dieser das Fahrrad zeitnah abholen kann.");
            if (nt2.ShowDialog() == DialogResult.OK)
            {
                ((main)this.MdiParent).Status("Bestellungsstatus an Laden übermittelt!");
            }
        }

        private void buttonBearbeiten_Click(object sender, EventArgs e)
        {
            Konfigurator k = new Konfigurator("Bestellung", labelNummer.Text);
            k.MdiParent = this.MdiParent;
            k.Show();
        }

    }
}
