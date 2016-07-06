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
    public partial class Mitarbeiter_Ansicht : Form
    {
        public Mitarbeiter_Ansicht()
        {
            InitializeComponent();
            listView1.ListViewItemSorter = sorter;
        }
        private List<List<string>> dataList = new List<List<string>>();
        private ListViewColumnSorter sorter = new ListViewColumnSorter();

        private void Mitarbeiter_sehen_Load(object sender, EventArgs e)
        {
            listview1_Refresh();
        }
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = listView1.SelectedIndices;
            foreach(int i in indexes) detail(1);
        }
        private void detail(int index)
        {
            if (index == 0)
            {
                labelLog.Text = dataList[0][0].ToString();
            }
            else
            {
                labelLog.Text = listView1.SelectedItems[0].SubItems[2].Text;
            }
            foreach (List<string> list in dataList)
            {
                if (list[0].ToString() == labelLog.Text)
                {
                    labelName.Text = list[1].ToString();
                    labelVorname.Text = list[2].ToString();
                    labelAbteil.Text = list[3].ToString();
                    if (list[4].ToString() == "1") checkBox1.Checked = true; else checkBox1.Checked = false;
                    if (list[5].ToString() == "1") checkBox2.Checked = true; else checkBox2.Checked = false;
                    if (list[6].ToString() == "1") checkBox3.Checked = true; else checkBox3.Checked = false;
                    if (list[7].ToString() == "1") checkBox4.Checked = true; else checkBox4.Checked = false;
                }
            }
        }        
        private void buttonAn_Click(object sender, EventArgs e)
        {
            Mitarbeiter_ändern m = new Mitarbeiter_ändern();
            m.version = 0;
            if (m.ShowDialog(this) == DialogResult.OK)
            {
                ((main)this.MdiParent).Status("Der Mitarbeier " + m.name() + " wurde erfolgreich angelegt.");
                listview1_Refresh();
            }
            else
            {
                ((main)this.MdiParent).Status("Anlage abgebrochen!");
            }
        }
        private void buttonBe_Click(object sender, EventArgs e)
        {
            string login = labelLog.Text;
            Mitarbeiter_ändern m = new Mitarbeiter_ändern();
            m.version = 1;
            m.login = login;
            if (m.ShowDialog(this) == DialogResult.OK)
            {
                ((main)this.MdiParent).Status("Die Daten von " + m.name() + " wurde erfolgreich geändert.");
                listview1_Refresh();
            }
            else
            {
                ((main)this.MdiParent).Status("Änderung abgebrochen!");
            }
        }
        public void listview1_Refresh()
        {
            string sqlcmd = "SELECT `login`, `Nachname`, `Name`, `abteilung`, `admin`, `ansichtL`, `ansichtV`, `ansichtW` FROM `personal`";
            Database_Fahrrad daten = new Database_Fahrrad();
            dataList = daten.getData(sqlcmd);
            listView1.Items.Clear();
            foreach (List<string> list in dataList)
            {
                listView1.Items.Add(new ListViewItem(new string[] { list[1].ToString(), list[2].ToString(), list[0].ToString() }));
            }
            detail(0);
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
