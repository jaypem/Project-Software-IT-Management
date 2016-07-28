using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Media;

namespace Fahrrad_ERP
{
    public partial class taskManagement : Form
    {
        public taskManagement()
        {
            InitializeComponent();
        }
        taskM.Management m = new taskM.Management();
        //verschiedene Listen
        List<List<string>> dataListAufgaben = new List<List<string>>();
        List<List<string>> dataListAuftrag = new List<List<string>>();
        List<List<string>> dataListPool = new List<List<string>>();
        List<List<string>> dataListStatus = new List<List<string>>();

        private void taskManagement_Load(object sender, EventArgs e)
        {
            dataListStatus = new Database_Fahrrad().getData("SELECT StatusID, Bezeichnung FROM auftragsstatus");
            //Standartfüllung der ListViews
            fillData();
            //Meldung, dass aktiv und Start des Timers
            notifyIcon1.BalloonTipTitle = "Task Management";
            notifyIcon1.BalloonTipText = "Task Management ist nun aktiv!";
            notifyIcon1.ShowBalloonTip(50000);
            timer1.Start();
        }

        private void fillData()
        {
            //Leeren der Views
            listViewAufgaben.Items.Clear();
            listViewAuftrag.Items.Clear();
            listViewPool.Items.Clear();
            //Füllen mit Daten
            dataListAufgaben = m.getUserTask(User.login);
            foreach (List<string> list in dataListAufgaben)
            {
                listViewAufgaben.Items.Add(new ListViewItem(new string[] { list[0].ToString(), list[1].ToString(), list[2].ToString(), getStatusText(list[3].ToString()) }));
                listViewAufgaben.Items[listViewAufgaben.Items.Count - 1].ForeColor = StatusColor(list[3].ToString());
            }
            dataListAuftrag = m.getUserOrders(User.login);
            foreach (List<string> list in dataListAuftrag)
            {
                listViewAuftrag.Items.Add(new ListViewItem(new string[] { list[0].ToString(), list[1].ToString(), list[2].ToString(), list[3].ToString(), getStatusText(list[4].ToString()) }));
                listViewAuftrag.Items[listViewAuftrag.Items.Count - 1].ForeColor = StatusColor(list[4].ToString());
            }
            dataListPool = m.getOrderPool(User.ansichtL, User.ansichtV, User.ansichtW, User.admin);
            foreach (List<string> list in dataListPool)
            {
                listViewPool.Items.Add(new ListViewItem(new string[] { list[0].ToString(), list[1].ToString(), list[2].ToString(), getStatusText(list[3].ToString()) }));
            }
        }
        private void listViewAufgaben_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Füllen der Informationen zum gewählten Auftrag mittels der RichTextBox
            foreach (ListViewItem it in listViewAufgaben.SelectedItems)
                textBoxAufgaben.Text = getAuftragInfo(it.SubItems[0].Text, dataListAufgaben);
            if (listViewAufgaben.SelectedItems.Count == 0)
                textBoxAufgaben.Clear();
        }

        private string getStatusText(string ID)
        {
            //Suchen des Statustextes zur Status ID
            string str = "";
            bool found = false;
            foreach (List<string> list in dataListStatus)
            {
                found = list.Exists(x => x == ID);
                if (found)
                {
                    str = list[1].ToString();
                    break;
                }
            }
            return str;
        }
        private string getAuftragInfo(string ID, List<List<string>> dataList, string View = "")
        {
            //Suchen der Auftragsinformationen zum Auftrag ID + Auftragshistorie
            string str = "";
            bool found = false;
            foreach (List<string> list in dataList)
            {
                found = list.Exists(x => x == ID);
                if (found)
                {
                    if (View != "Auftrag")
                        str = list[2].ToString() + "\n";
                    else
                        str = list[3].ToString() + "\n";
                    for (int i = 1; i <= list[2].Length; i++) str += "-";
                    if (View != "Auftrag")
                        str += "\n" + list[5].ToString();
                    else
                        str += "\n" + list[6].ToString();
                    str += "\n\nHistorie:\n";
                    List<List<string>> dataListHistorie = new List<List<string>>(m.getStatusHistorie(ID));
                    foreach (List<string> histo in dataListHistorie)
                    {
                        str += histo[0].ToString() + ":\t" + histo[2].ToString() + " um " + histo[3].ToString() + "\tStatus: " + getStatusText(histo[1].ToString()) + "\n";
                    }
                    break;
                }
            }
            return str;
        }

        private void listViewAuftrag_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Füllen der Informationen zum gewählten Auftrag mittels der RichTextBox
            foreach (ListViewItem it in listViewAuftrag.SelectedItems)
                textBoxAuftrag.Text = getAuftragInfo(it.SubItems[0].Text, dataListAuftrag, "Auftrag");
            if (listViewAuftrag.SelectedItems.Count == 0)
                textBoxAuftrag.Clear();
        }

        private void listViewPool_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Füllen der Informationen zum gewählten Auftrag mittels der RichTextBox
            foreach (ListViewItem it in listViewPool.SelectedItems)
                textBoxPool.Text = getAuftragInfo(it.SubItems[0].Text, dataListPool);
            if (listViewPool.SelectedItems.Count == 0)
                textBoxPool.Clear();
        }

        private void buttonÜbernahme_Click(object sender, EventArgs e)
        {
            if (listViewPool.SelectedItems.Count != 0)
            {
                //Prüfung des Status (noch verfügbar)
                string aufnr = listViewPool.SelectedItems[0].SubItems[0].Text;
                if (m.getStatus(aufnr) == "0")
                {
                    //Übernahme bestätigen lassen
                    if (MessageBox.Show("Soll der Auftrag " + aufnr + " übernommen werden?", "Übernahme", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        //hinzufügen der Übernahme in die Historisierung
                        m.setStatus(aufnr, "1", User.login);
                        fillData();
                    }
                }
                else
                {
                    //keine Übernahme, aktualisieren der ListView
                    MessageBox.Show("Der Auftrag wurde bereits übernommen.", "Nicht mehr verfügbar", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fillData();
                }
            }
        }

        private void buttonNeu_Click(object sender, EventArgs e)
        {
            //Neuen Task über Form erstellen lassen und Daten aktualisieren
            taskM.newTask n = new taskM.newTask();
            if (n.ShowDialog() == DialogResult.OK)
            {
                fillData();
            }
        }

        private void buttonChange_Click(object sender, EventArgs e)
        {
            //Status ändern über Form
            if (listViewAufgaben.SelectedItems.Count != 0)
            {
                string Nr = listViewAufgaben.SelectedItems[0].SubItems[0].Text;
                string status = listViewAufgaben.SelectedItems[0].SubItems[3].Text;
                taskM.changeStatus cS = new taskM.changeStatus(Nr, status);
                cS.ShowDialog();
                fillData();
            }
        }

        private Color StatusColor(string ID)
        {
            //Für jeden Status wird eine Farbe zurück gegeben
            Color c = new Color();
            switch (ID)
            {
                case "0": c = Color.Black;
                    break;
                case "1": c = Color.DarkOrange;
                    break;
                case "2": c = Color.DarkGreen;
                    break;
                default: c = Color.Black;
                    break;
            }
            return c;
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //bei Doppelklick auf notifyIcon öffnet sich das Fenster des tastManagers
            this.WindowState = FormWindowState.Normal;
            this.Activate();
        }

        private void taskManagement_KeyDown(object sender, KeyEventArgs e)
        {
            //aktuallisieren bei F5
            if (e.KeyCode == Keys.F5)
            {
                fillData();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Timer überprüft bei jedem "Tick" ob es neue Aufträge gibt für den User oder ob sich der Status geändert hat. Wenn ja, dann wird dies mitgeteilt
            for (int i = 0; i < listViewAuftrag.Items.Count; i++)
            {
                string auftrag = listViewAuftrag.Items[i].SubItems[0].Text;
                string aktion = listViewAuftrag.Items[i].SubItems[1].Text;
                if (m.isAktionChange(auftrag, aktion))
                {
                    notifyIcon1.BalloonTipText = "Zu Ihrem Auftrag " + auftrag + " wurde eine neue Aktion angelegt.";
                    notifyIcon1.ShowBalloonTip(5000);
                    fillData();
                }
            }
            if (m.isTaskCountChange(User.login, listViewAufgaben.Items.Count))
            {
                notifyIcon1.BalloonTipText = "Sie haben eine neue Aufgabe erhalten.";
                SystemSounds.Asterisk.Play();
                notifyIcon1.ShowBalloonTip(5000);
                fillData();
            }
        }

        private void taskManagement_FormClosing(object sender, FormClosingEventArgs e)
        {
            //bei schließen der Form, wird diese nur versteckt
            this.Hide();
            e.Cancel = true;
        }

    }
}
