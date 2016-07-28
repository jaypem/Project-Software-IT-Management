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
    public partial class Kunden_Ansicht : Form
    {
        public Kunden_Ansicht()
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
                fill();
            }
            else
            {
                ((main)this.MdiParent).Status("Anlage abgebrochen!");
            }
        }
        private void fill()
        {
            string sqlcmd = "SELECT KundenID, Nachname, Name, Haendler, Firma, PLZ, Ort, Strasse, Hausnummer, Geschlecht, Geburtsdatum, EMail FROM kunden";
            dataList = daten.getData(sqlcmd);
                            listView1.Items.Clear();
                foreach (List<string> list in dataList)
                {
                    if (list[3].ToString() == "0")
                    {
                        listView1.Items.Add(new ListViewItem(new string[] { list[0].ToString(), getAnrede(list[3].ToString(), list[9].ToString()), list[1].ToString(), list[2].ToString() }));
                    }
                    else
                    {
                        listView1.Items.Add(new ListViewItem(new string[] { list[0].ToString(), getAnrede(list[3].ToString(), list[9].ToString()), list[4].ToString()}));
                    }
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
                    if (list[3].ToString() == "0")
                    {
                        labelName.Text = list[1].ToString();
                        labelVorname.Text = list[2].ToString();
                        labelAdd.Text = list[7].ToString() + " " + list[8].ToString() + "\n" + list[5].ToString() + " " + list[6].ToString();
                    }
                    else
                    {
                        labelName.Text = list[4].ToString();
                        labelVorname.Text = "";
                        labelAdd.Text = list[7].ToString() + " " + list[8].ToString() + "\n" + list[5].ToString() + " " + list[6].ToString();
                    }
                }
            }
        }
        private string getAnrede(string Haendler, string Geschlecht)
        {
            string anr = "";
            if (Haendler == "1") { anr = "Firma"; }
            else
            {
                if (Geschlecht == "m")
                {
                    anr = "Herr";
                }
                else
                {
                    anr = "Frau";
                }
            }
            return anr;
        }

        private void Kunden_Ansicht_Load(object sender, EventArgs e)
        {
            fill();
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            detail(1);
        }

        private void buttonBe_Click(object sender, EventArgs e)
        {
            Kunde_ändern k = new Kunde_ändern("Bearbeiten");
            k.ID = labelID.Text;
            if (k.ShowDialog(this) == DialogResult.OK)
            {
                ((main)this.MdiParent).Status("Der Kundensatz " + k.ID + " wurde erfolgreich bearbeitet.");
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
