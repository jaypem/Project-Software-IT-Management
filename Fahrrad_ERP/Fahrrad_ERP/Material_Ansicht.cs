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
    public partial class Material_Ansicht : Form
    {
        public Material_Ansicht()
        {
            InitializeComponent();
        }

        Database_Fahrrad daten = new Database_Fahrrad();
        List<List<string>> dataList = new List<List<string>>();
        List<List<string>> dataShortList = new List<List<string>>(); //Liste mit Itemeinträgen nach denen gefiltert werden kann
        private void buttonAn_Click(object sender, EventArgs e)
        {
            Kunde_ändern k = new Kunde_ändern("Anlegen");
            if (k.ShowDialog(this) == DialogResult.OK)
            {
                ((main)this.MdiParent).Status("Der Kunde " + "" + " wurde erfolgreich angelegt.");
                fill();
            }
            else
            {
                ((main)this.MdiParent).Status("Anlage abgebrochen!");
            }
        }
        private void fill()
        {
            string sqlcmd = "SELECT ProduktID, p.Bezeichnung, pk.Bezeichnung, Preis FROM produkte " +
                            "p inner join produktkategorie pk ON p.produktkategorie = pk.KategorieID";
            dataList = daten.getData(sqlcmd);
            listView1.Items.Clear();
            foreach (List<string> list in dataList)
            {
                listView1.Items.Add(new ListViewItem(new string[] { list[0].ToString(), list[1].ToString(), list[2].ToString(), Convert.ToDecimal(list[3].ToString()).ToString("0.00")}));
            }
            dataShortList = getDataShortList();
            detail(0);
        }
        private void detail(int index)
        {
            if (index == 0)
            {
                labelID.Text = dataList[0][0].ToString();
            }
            else
            {
                if (listView1.SelectedItems.Count != 0)
                    labelID.Text = listView1.SelectedItems[0].SubItems[0].Text;
            }
            foreach (List<string> list in dataList)
            {
                if (list[0].ToString() == labelID.Text)
                {
                    labelID.Text = list[0].ToString();
                    lb_Bezeichnung.Text = list[1].ToString();
                    lb_Produktkat.Text = list[2].ToString();
                    lb_Preis.Text = list[3].ToString();
                    break;
                }
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            detail(1);
        }

        private void btn_MaterialAnlegen_Click(object sender, EventArgs e)
        {
            Material_ändern m = new Material_ändern("Anlegen");
            if (m.ShowDialog(this) == DialogResult.OK)
            {
                fill();
            }
            else
            {
                ((main)this.MdiParent).Status("Anlage abgebrochen!");
            }
        }

        private void Material_Übersicht_Load(object sender, EventArgs e)
        {
            fill();
        }

        private void btn_Materialbearbeiten_Click(object sender, EventArgs e)
        {
            Material_ändern m = new Material_ändern("Bearbeiten");
            m.ID = labelID.Text;
            if (m.ShowDialog(this) == DialogResult.OK)
            {
                ((main)this.MdiParent).Status("Der Materialdatensatz " + m.ID + " wurde erfolgreich bearbeitet.");
                fill();
            }
            else
            {
                ((main)this.MdiParent).Status("Bearbeitung abgebrochen!");
            }
        }

        private void tb_search_TextChanged(object sender, EventArgs e)
        {
            //Bei jeder Textveränderung in der Filter TextBox wird die Listview gefiltert
            listView1.Items.Clear();
            ListViewFiltern lf = new ListViewFiltern();
            foreach (List<string> list in lf.LookFor(dataShortList, tb_search.Text))
            {
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
