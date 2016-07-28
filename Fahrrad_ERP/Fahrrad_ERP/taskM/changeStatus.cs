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
            //Übergabe der Auftragsnummer und des aktuellen Status
            aufnr = Nr;
            Status = akStatus;
            InitializeComponent();
        }
        Database_Fahrrad daten = new Database_Fahrrad();
        private void changeStatus_Load(object sender, EventArgs e)
        {
            //Datenbankanfrage auf alle möglichen Statusausprägungen
            labelAuf.Text = aufnr;
            string sqlcmd = "SELECT StatusID, Bezeichnung FROM auftragsstatus ORDER BY StatusID";
            List<List<string>> dataList = new List<List<string>>();
            dataList = daten.getData(sqlcmd);
            //setzen der Statusausprägungen in ComboBox
            foreach (List<string> list in dataList)
            {
                comboBox1.Items.Add(list[0].ToString() + " - " + list[1].ToString());
            }
            //StatusID aus aktuellen Status suchen und in ComboBox setzen
            sqlcmd = "SELECT StatusID FROM auftragsstatus WHERE Bezeichnung = '"+Status+"'";
            dataList.Clear();
            dataList = daten.getData(sqlcmd);
            int index = comboBox1.FindString(dataList[0][0].ToString() + " - "+Status);
            comboBox1.SelectedIndex = index;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            //Seperieren der StatusID aus dem ComboBox Feld und anschließendes setzen als neue Aktion
            int index = comboBox1.SelectedItem.ToString().IndexOf('-');
            string stat = comboBox1.SelectedItem.ToString().Remove(index - 1);
            string sqlcmd = "INSERT INTO auftragshistorie (Auftragsnummer, StatusID, Bearbeiter) VALUES ('" + aufnr + "', '"+stat+"', '" + User.login + "')";
            daten.setData(sqlcmd);
            this.DialogResult = DialogResult.OK;
        }
    }
}
