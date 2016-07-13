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
    public partial class Auswahl : Form
    {
        public Auswahl(string Version)
        {
            InitializeComponent();
            listView1.ListViewItemSorter = sorter;
            vers = Version;
        }
        private string[] ZeilenInfo = new string[4];
        private string ID;
        private string vers;
        private List<List<string>> dataList = new List<List<string>>();
        private ListViewSortieren sorter = new ListViewSortieren();

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = listView1.SelectedIndices;
            foreach (int i in indexes) set_nummer(1);
        }
        private void set_nummer(int index)
        {
            if (index != 0)
            {
                switch (vers)
                {
                    case "Bestellung":
                        ZeilenInfo[0] = listView1.SelectedItems[0].SubItems[0].Text;
                        ZeilenInfo[1] = listView1.SelectedItems[0].SubItems[1].Text;
                        ZeilenInfo[2] = listView1.SelectedItems[0].SubItems[2].Text;
                        ZeilenInfo[3] = listView1.SelectedItems[0].SubItems[3].Text;
                        ID = listView1.SelectedItems[0].SubItems[4].Text;
                        break;
                    case "Konfiguration":
                        ID = listView1.SelectedItems[0].SubItems[0].Text;
                        break;
                    case "Kunden":
                        ID = listView1.SelectedItems[0].SubItems[0].Text;
                        ZeilenInfo[0] = listView1.SelectedItems[0].SubItems[2].Text + " " + listView1.SelectedItems[0].SubItems[1].Text;
                        break;
                }
            }
        }

        public string[] get_ZeilenInfo()
        {
            return ZeilenInfo;
        }
        public string get_ID()
        {
            return ID;
        }
        private void Bestellung_wählen_Load(object sender, EventArgs e)
        {
            switch (vers)
            {
                case "Bestellung":
                    this.Text = "Bestellung wählen";
                    listView1.Columns.Add("Bestellnr.", 80);
                    listView1.Columns.Add("Name", 160);
                    listView1.Columns.Add("Datum", 70);
                    listView1.Columns.Add("Summe", 80);
                    listView1.Columns.Add("Kundennr.", 0);
                    listview1_Bestellung();
                    break;
                case "Konfiguration":
                    this.Text = "Konfiguration wählen";
                    listView1.Columns.Add("Konfignr.", 80);
                    listView1.Columns.Add("Name", 160);
                    listView1.Columns.Add("Bezeichung", 200);
                    listView1.Columns.Add("Kundennr.", 0);
                    listview1_Konfiguraiton();
                    break;
                case "Kunden":
                    this.Text = "Kunden wählen";
                    listView1.Columns.Add("Kundennr.", 80);
                    listView1.Columns.Add("Name", 120);
                    listView1.Columns.Add("Vorname", 120);
                    listView1.Columns.Add("Ort", 120);
                    listview1_Kunden();
                    break;
            }

        }
        private void listview1_Konfiguraiton()
        {
            string sqlcmd = "SELECT KonfigurationID, kunden.Firma, kunden.Nachname, kunden.Name, Bezeichnung, kunden.Haendler FROM konfigurationen LEFT JOIN kunden ON konfigurationen.KundenID = kunden.KundenID ORDER BY KonfigurationID";
            Database_Fahrrad daten = new Database_Fahrrad();
            dataList = daten.getData(sqlcmd);
            listView1.Items.Clear();
            foreach (List<string> list in dataList)
            {
                if (list[5].ToString() == "1") //für Händler wird der Firmenname eingetragen
                {
                    listView1.Items.Add(new ListViewItem(new string[] { list[0].ToString(), list[1].ToString(), list[4].ToString() }));
                }
                else
                {
                    if (list[2].ToString() == "")
                    {
                        listView1.Items.Add(new ListViewItem(new string[] { list[0].ToString(), "", list[4].ToString() }));
                    }
                    else
                    {
                        listView1.Items.Add(new ListViewItem(new string[] { list[0].ToString(), list[2].ToString() + ", " + list[3].ToString(), list[4].ToString() }));

                    }
                }
            }
            set_nummer(0);
        }
        private void listview1_Bestellung()
        {
            string sqlcmd = "SELECT BestellungID, kunden.Firma, kunden.Nachname, kunden.Name, Datum, Bestellsumme, kunden.KundenID, kunden.Haendler FROM bestellung JOIN kunden ON bestellung.KundenID = kunden.KundenID";
            Database_Fahrrad daten = new Database_Fahrrad();
            dataList = daten.getData(sqlcmd);
            listView1.Items.Clear();
            foreach (List<string> list in dataList)
            {
                if (list[7].ToString() == "1") //für Händler wird der Firmenname eingetragen
                {
                    listView1.Items.Add(new ListViewItem(new string[] { list[0].ToString(), list[1].ToString(), (Convert.ToDateTime(list[4])).ToString("d"), (Convert.ToDecimal(list[5])).ToString("0.00 €"), list[6].ToString() }));
                }
                else //für Privatpersonen wird der Name eingetragen
                {
                    listView1.Items.Add(new ListViewItem(new string[] { list[0].ToString(), list[2].ToString() + ", " + list[3].ToString(), (Convert.ToDateTime(list[4])).ToString("d"), (Convert.ToDecimal(list[5])).ToString("0.00 €"), list[6].ToString() }));
                }
            }
            set_nummer(0);
        }
        private void listview1_Kunden()
        {
            string sqlcmd = "SELECT KundenID, Firma, Nachname, Name, Ort, Haendler FROM kunden";
            Database_Fahrrad daten = new Database_Fahrrad();
            dataList = daten.getData(sqlcmd);
            listView1.Items.Clear();
            foreach (List<string> list in dataList)
            {
                if (list[5].ToString() == "1") //für Händler wird der Firmenname eingetragen
                {
                    listView1.Items.Add(new ListViewItem(new string[] { list[0].ToString(), list[1].ToString(), "", list[4].ToString() }));
                }
                else
                {
                    listView1.Items.Add(new ListViewItem(new string[] { list[0].ToString(), list[2].ToString(), list[3].ToString(), list[4].ToString() }));
                }
            }
            set_nummer(0);
        }
        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // überprüfen ob Spalte bereits sortiert ist
            if (e.Column == sorter.SortColumn)
            {
                // Sortierungsrichtung ändern
                if (sorter.Order == SortOrder.Ascending)
                {
                    sorter.Order = SortOrder.Descending;
                }
                else
                {
                    sorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                sorter.SortColumn = e.Column;
                sorter.Order = SortOrder.Ascending;
            }
            listView1.Sort();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

    }

}
