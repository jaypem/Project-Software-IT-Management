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
            //übergabe von Version (als welches es geöffnet wird) und hinzufügen des Sorters
            InitializeComponent();
            listView1.ListViewItemSorter = sorter;
            vers = Version;
        }
        //Standart-Auswahl-Fenster
        //
        //Hier kann der Benutzer eine Auswahl aus verschiedenen Informationen treffen, die je nach Füllung an das Parent Fenster zurückgegeben werden
        //Ordnen und Filtern ist möglich

        string[] ZeilenInfo = new string[5];
        string ID = "";
        string vers;
        List<List<string>> dataList = new List<List<string>>(); //Füll-Daten
        ListViewSortieren sorter = new ListViewSortieren();
        List<List<string>> dataShortList = new List<List<string>>(); //Filter-Daten
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Ausgewählte Daten abspeichern, wenn ein Feld gewählt wird.
            if (listView1.SelectedItems.Count != 0)
            {
                set_nummer();
            }
        }
        private void set_nummer()
        {
            //Rückgabe der gewünschten Zeileninformationen je nach Version
                switch (vers)
                {
                    case "Bestellung":
                        ZeilenInfo[0] = listView1.SelectedItems[0].SubItems[0].Text;
                        ZeilenInfo[1] = listView1.SelectedItems[0].SubItems[1].Text;
                        ZeilenInfo[2] = listView1.SelectedItems[0].SubItems[2].Text;
                        ZeilenInfo[3] = listView1.SelectedItems[0].SubItems[3].Text;
                        ZeilenInfo[4] = listView1.SelectedItems[0].SubItems[4].Text;
                        ID = listView1.SelectedItems[0].SubItems[0].Text;
                        break;
                    case "Konfiguration":
                        ID = listView1.SelectedItems[0].SubItems[0].Text;
                        break;
                    case "Kunden":
                        ID = listView1.SelectedItems[0].SubItems[0].Text;
                        ZeilenInfo[0] = listView1.SelectedItems[0].SubItems[2].Text + " " + listView1.SelectedItems[0].SubItems[1].Text;
                        break;
                    case "Mitarbeiter":
                        ID = listView1.SelectedItems[0].SubItems[2].Text;
                        ZeilenInfo[0] = listView1.SelectedItems[0].SubItems[1].Text + " " + listView1.SelectedItems[0].SubItems[0].Text;
                        break;
                }
        }

        public string[] get_ZeilenInfo()
        {
            //Rückgabe der ZeilenInformationen für die ParentForm
            return ZeilenInfo;
        }
        public string get_ID()
        {
            //Rückgabe der ID fü die Parentform
            return ID;
        }
        private void Bestellung_wählen_Load(object sender, EventArgs e)
        {
            //Nach Version werden die Spalten angelegt und mit Daten gefüllt
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
                case "Mitarbeiter":
                    this.Text = "Mitarbeiter wählen";
                    listView1.Columns.Add("Nachname", 120);
                    listView1.Columns.Add("Vorname", 120);
                    listView1.Columns.Add("login", 120);
                    listview1_Mitarbeiter();
                    break;
            }
            //Übergabe der Daten, die in der Listview stehen
            dataShortList = getDataShortList();
        }
        private void listview1_Konfiguraiton()
        {
            //Abfrage und Füllen der Daten für den Fall, dass eine Konfiguration ausgewählt wird
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
        }
        private void listview1_Bestellung()
        {
            //Abfrage und Füllen der Daten für den Fall, dass eine Bestellung ausgewählt wird
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
        }
        private void listview1_Kunden()
        {
            //Abfrage und Füllen der Daten für den Fall, dass ein Kunde ausgewählt wird
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
        }
        private void listview1_Mitarbeiter()
        {
            //Abfrage und Füllen der Daten für den Fall, dass ein Mitarbeiter ausgewählt wird
            string sqlcmd = "SELECT Nachname, Name, login FROM personal ORDER BY Nachname";
            Database_Fahrrad daten = new Database_Fahrrad();
            dataList = daten.getData(sqlcmd);
            listView1.Items.Clear();
            listView1.Items.Add(new ListViewItem(new string[] { "Administrator", "", "Admin" }));
            listView1.Items.Add(new ListViewItem(new string[] { "Ladenmitarbeiter", "alle", "Laden" }));
            listView1.Items.Add(new ListViewItem(new string[] { "Verwaltungsmitarbeiter", "alle", "Verwaltung"}));
            listView1.Items.Add(new ListViewItem(new string[] { "Werkstattmitarbeiter", "alle", "Werkstatt" }));
            foreach (List<string> list in dataList)
            {
               listView1.Items.Add(new ListViewItem(new string[] { list[0].ToString(), list[1].ToString(), list[2].ToString()}));
            }
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
            //Schließt das Fenster und gibt OK zurück, wenn eine ID gewählt wurde, ansonsten wird abbrechen zurückgegeben
            if (ID != "")
            {
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        private void textBoxFilter_TextChanged(object sender, EventArgs e)
        {
            //Bei jeder Textveränderung in der Filter TextBox wird die Listview gefiltert
            listView1.Items.Clear();
            ListViewFiltern lf = new ListViewFiltern();
            foreach (List<string> list in lf.LookFor(dataShortList, textBoxFilter.Text)) {
                listView1.Items.Add(new ListViewItem(list.ToArray()));
            }
        }
        private List<List<string>> getDataShortList()
        {
            //Gibt eine Liste mit den Items aus der ListView zurück
            List<List<string>> Liste = new List<List<string>>();
            foreach (ListViewItem item in listView1.Items)
            {
                List<string> list = new List<string>();
                for (int i = 0; i < item.SubItems.Count; i++)
                {
                    list.Add(item.SubItems[i].Text);                    
                }
                Liste.Add(list.ToList());
                list.Clear();
            }
            return Liste;
        }

    }

}
