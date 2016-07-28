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
    public partial class Warenlager : Form
    {
        public Warenlager(string version)
        {
            InitializeComponent();
            vers = version;
        }
        string vers = "";
        List<List<string>> dataList = new List<List<string>>();
        List<List<string>> dataShortList = new List<List<string>>(); //Liste mit Itemeinträgen nach denen gefiltert werden kann
        Database_Fahrrad daten = new Database_Fahrrad();
        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = listView1.SelectedIndices;
            foreach (int i in indexes) detail(1);
        }

        private void detail(int index)
        {
            if (index == 0)
            {
                labelID.Text = dataList[0][0].ToString();
                labelBez.Text = dataList[0][1].ToString();
                labelLB.Text = dataList[0][2].ToString();
                if (vers == "Ausgang") numericUpDownMenge.Maximum = Convert.ToInt16(labelLB.Text);
            }
            else
            {
                labelID.Text = listView1.SelectedItems[0].SubItems[0].Text;
                labelBez.Text = listView1.SelectedItems[0].SubItems[1].Text;
                labelLB.Text = listView1.SelectedItems[0].SubItems[2].Text;
                if (vers == "Ausgang") numericUpDownMenge.Maximum = Convert.ToInt16(labelLB.Text);
            }
           
        }
        private void fillData()
        {
            string sqlcmd = "SELECT ProduktID, Bezeichnung, Lagerbestand FROM produkte";
            dataList = daten.getData(sqlcmd);
            listView1.Items.Clear();
            foreach (List<string> list in dataList)
            {
                listView1.Items.Add(new ListViewItem(new string[] { list[0].ToString(), list[1].ToString(), list[2].ToString() }));
            }
            dataShortList = getDataShortList();
            detail(0);             
        }
        private void Wareneingang_Load(object sender, EventArgs e)
        {
            switch (vers)
            {
                case "Eingang":
                    this.Text = "Wareneingang";
                    label4.Text = "Menge Wareneingang";
                    buttonNach.Visible = false;
                    break;
                case "Ausgang":
                    this.Text = "Warenausgang";
                    label4.Text = "Menge Warenausgang";
                    buttonNach.Visible = true;
                    break;
            }
            fillData();
        }

        private void Einbuchen() 
        {
            int menge = Convert.ToInt16(labelLB.Text) + (int)numericUpDownMenge.Value;
            string sqlcmd = "UPDATE produkte SET Lagerbestand = '"+menge+"' WHERE ProduktID = '" + labelID.Text + "'";
            daten.setData(sqlcmd);
            numericUpDownMenge.Value = 1;
            fillData();
        }

        private void Ausbuchen() {
            int menge = Convert.ToInt16(labelLB.Text) - (int)numericUpDownMenge.Value;
            string sqlcmd = "UPDATE produkte SET Lagerbestand = '"+menge+"' WHERE ProduktID = '" + labelID.Text + "'";
            daten.setData(sqlcmd);
            numericUpDownMenge.Value = 1;
            fillData();
        }

        private void btn_buchen_Click(object sender, EventArgs e)
        {
            if (vers == "Eingang") Einbuchen();
            if (vers == "Ausgang") Ausbuchen();
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

        private void buttonNach_Click(object sender, EventArgs e)
        {
            taskM.newTask nt = new taskM.newTask("Verwaltung", "Nachbestellung "+labelID.Text, "Bitte bestellen Sie [ANZAHL EINTRAGEN] Stück von "+labelBez.Text+" (ID: "+labelID.Text+") nach. Vielen Dank!");
            if (nt.ShowDialog() == DialogResult.OK)
            {
                ((main)this.MdiParent).Status("Nachbestellung wurde ausgelöst!");
            }
        }
    }
}
