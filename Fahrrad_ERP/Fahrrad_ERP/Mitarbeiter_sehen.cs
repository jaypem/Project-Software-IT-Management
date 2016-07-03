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
    public partial class Mitarbeiter_sehen : Form
    {
        public Mitarbeiter_sehen()
        {
            InitializeComponent();
        }
        List<List<string>> dataList = new List<List<string>>();

        private void Mitarbeiter_sehen_Load(object sender, EventArgs e)
        {
            string sqlcmd = "SELECT `login`, `Nachname`, `Name`, `abteilung`, `admin`, `ansichtL`, `ansichtV`, `ansichtW` FROM `personal`";
            Database_Fahrrad daten = new Database_Fahrrad();
            dataList = daten.getData(sqlcmd);
            foreach (List<string> list in dataList) {
                listView1.Items.Add(new ListViewItem(new string[]{list[1].ToString(), list[2].ToString(), list[0].ToString()}));
            }
            detail(0);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ListView.SelectedIndexCollection indexes = listView1.SelectedIndices;
            foreach(int i in indexes) detail(i);
        }
        private void detail(int index)
        {
            labelLog.Text = dataList[index][0].ToString();
            labelName.Text = dataList[index][1].ToString();
            labelVorname.Text = dataList[index][2].ToString();
            labelAbteil.Text = dataList[index][3].ToString();
            if (dataList[index][4].ToString() == "1") checkBox1.Checked = true; else checkBox1.Checked = false;
            if (dataList[index][5].ToString() == "1") checkBox2.Checked = true; else checkBox2.Checked = false;
            if (dataList[index][6].ToString() == "1") checkBox3.Checked = true; else checkBox3.Checked = false;
            if (dataList[index][7].ToString() == "1") checkBox4.Checked = true; else checkBox4.Checked = false;
        }
        
        private void checkBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonAn_Click(object sender, EventArgs e)
        {
            Mitarbeiter.version = 0;
            Mitarbeiter m = new Mitarbeiter();
            m.Show();
        }

        private void buttonBe_Click(object sender, EventArgs e)
        {
            Mitarbeiter.version = 1;
            Mitarbeiter.login = labelLog.Text;
            Mitarbeiter m = new Mitarbeiter();
            m.Show();
        }
    }
}
