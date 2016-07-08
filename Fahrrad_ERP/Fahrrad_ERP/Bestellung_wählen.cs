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
    public partial class Bestellung_wählen : Form
    {
        public Bestellung_wählen()
        {
            InitializeComponent();
            listView1.ListViewItemSorter = sorter;
        }
        private string[] bestellung = new string[4];
        private string kunde;
        private List<List<string>> dataList = new List<List<string>>();
        private ListViewColumnSorter sorter = new ListViewColumnSorter();

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = listView1.SelectedIndices;
            foreach (int i in indexes) set_nummer(1);
        }
        private void set_nummer(int index)
        {
            if (index != 0)
            {
                bestellung[0] = listView1.SelectedItems[0].SubItems[0].Text;
                bestellung[1] = listView1.SelectedItems[0].SubItems[1].Text;
                bestellung[2] = listView1.SelectedItems[0].SubItems[2].Text;
                bestellung[3] = listView1.SelectedItems[0].SubItems[3].Text;
                kunde = listView1.SelectedItems[0].SubItems[4].Text;
            }
        }

        public string[] get_bestell()
        {
            return bestellung;
        }
        public string get_kunde()
        {
            return kunde;
        }
        private void Bestellung_wählen_Load(object sender, EventArgs e)
        {
            listview1_Refresh();
        }
        private void listview1_Refresh()
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
    }

}
