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
        private string[] bestellung = new string[4];
        private List<List<string>> dataList = new List<List<string>>();
        private ListViewColumnSorter sorter = new ListViewColumnSorter();
        private void fill()
        {
            labelNummer.Text = bestellung[0];
            labelName.Text = bestellung[1];
            labelDatum.Text = bestellung[2];
            labelSumme.Text = bestellung[3];
            buttonDruck.Enabled = true;
            buttonErstellen.Enabled = true;
            string sqlcmd = "SELECT Position, produkte.ProduktID, produkte.Bezeichnung, Menge, bestellungpos.Preis, Summe FROM bestellungpos INNER JOIN produkte ON produkte.ProduktID = bestellungpos.ProduktID WHERE BestellungID = '" + bestellung [0]+"'";
            Database_Fahrrad daten = new Database_Fahrrad();
            dataList = daten.getData(sqlcmd);
            listView1.Items.Clear();
            foreach (List<string> list in dataList)
            {
                listView1.Items.Add(new ListViewItem(new string[] { list[0].ToString(), list[1].ToString(), list[2].ToString(), list[3].ToString(), (Convert.ToDecimal(list[4])).ToString("0.00 €"), (Convert.ToDecimal(list[5])).ToString("0.00 €") }));
            }
            listView1.Items.Add(new ListViewItem(new string[] { "", "", "", "", "", "=========" }));
            listView1.Items.Add(new ListViewItem(new string[] { "", "", "", "", "TOTAL", bestellung[3] }));
            listView1.Items.Add(new ListViewItem(new string[] { "", "", "", "", "MwSt. 19%", (Convert.ToDouble(bestellung[3].TrimEnd(' ','€'))*0.19).ToString("0.00 €") }));
        }
        private void buttonWahl_Click(object sender, EventArgs e)
        {
            Bestellung_wählen b = new Bestellung_wählen();
            if (b.ShowDialog(this) == DialogResult.OK)
            {
                bestellung = b.get_nummer();
                ((main)this.MdiParent).Status("Es wurde die Bestellung " + bestellung[0] + " ausgewählt.");
                fill();
            }
            else
            {
                ((main)this.MdiParent).Status("Auswahl abgebrochen!");
            }
        }

        private void Rechnung_Load(object sender, EventArgs e)
        {

        }
    }
}
