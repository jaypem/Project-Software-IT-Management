using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fahrrad_ERP.taskM
{
    public partial class changeStatus : Form
    {
        string aufnr;
        string Status;
        public changeStatus(string Nr, string akStatus)
        {
            aufnr = Nr;
            Status = akStatus;
            InitializeComponent();
        }
        Database_Fahrrad daten = new Database_Fahrrad();
        private void changeStatus_Load(object sender, EventArgs e)
        {
            label2.Text = aufnr;
            string sqlcmd = "SELECT StatusID, Bezeichnung FROM auftragsstatus ORDER BY StatusID";
            List<List<string>> dataList = new List<List<string>>();
            dataList = daten.getData(sqlcmd);
            foreach (List<string> list in dataList)
            {
                comboBox1.Items.Add(list[0].ToString() + " - " + list[1].ToString());
            }
            sqlcmd = "SELECT StatusID FROM auftragsstatus WHERE Bezeichnung = '"+Status+"'";
            dataList.Clear();
            dataList = daten.getData(sqlcmd);
            comboBox1.SelectedIndex = Convert.ToInt16(dataList[0][0].ToString());

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string stat = comboBox1.SelectedIndex.ToString();
            string sqlcmd = "INSERT INTO auftragshistorie (Auftragsnummer, StatusID, Bearbeiter) VALUES ('" + aufnr + "', '"+stat+"', '" + User.login + "')";
            daten.setData(sqlcmd);
            this.DialogResult = DialogResult.OK;
        }
    }
}
