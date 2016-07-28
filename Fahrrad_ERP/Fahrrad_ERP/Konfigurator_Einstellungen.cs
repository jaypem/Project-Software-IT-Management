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
    public partial class Konfigurator_Einstellungen : Form
    {
        public Konfigurator_Einstellungen()
        {
            InitializeComponent();
        }
        Database_Fahrrad daten = new Database_Fahrrad();
        List<List<string>> dataList = new List<List<string>>();
        string aktItem = "";

        private void listBoxA_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBoxA.SelectedItems.Count != 0)
            {
                if (aktItem != "") insertBlacklist(aktItem);
                string item = listBoxA.SelectedItem.ToString();
                aktItem = getID(item);
                setBlacklist(getID(item));
                //Teil A in Teil B Tabelle entfernen (ich kann nicht zu mir selbst inkompatibel werden)
                listBoxB.Items.RemoveAt(listBoxA.SelectedIndex);
            }
        }

        private void listBoxB_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Konfigurator_Einstellungen_Load(object sender, EventArgs e)
        {
            getProdukte();
            listBoxA.SetSelected(0, true);
        }

        private void getProdukte()
        {
            //alle Produkte in beide Listen laden
            string sqlcmd = "SELECT ProduktID, Bezeichnung FROM produkte";
            dataList = daten.getData(sqlcmd);
            foreach (List<string> list in dataList)
            {
                string item = list[0].ToString() + " - " + list[1].ToString();
                listBoxA.Items.Add(item);
                listBoxB.Items.Add(item);
            }
        }

        private void setBlacklist(string ID)
        {
            //setzt inkompatible Teile zur gewählten ID
            List<List<string>> dataTeilB = new List<List<string>>();
            string sqlcmd = "SELECT TeilB FROM regelwerk WHERE TeilA = '" + ID + "'";
            dataTeilB = daten.getData(sqlcmd);
            //Elemente in B neu setzen
            listBoxB.Items.Clear();
            foreach (List<string> list in dataList)
            {
                string item = list[0].ToString() + " - " + list[1].ToString();
                listBoxB.Items.Add(item);
            }
            //Teile B auswählen, die inkompatibel zu A sind
            foreach (List<string> list in dataTeilB)
            {
                string Teil = list[0].ToString();
                for (int i = 0; i < listBoxB.Items.Count; i++)
                {
                    string item = listBoxB.Items[i].ToString();
                    if (getID(item) == Teil) listBoxB.SetSelected(i, true);
                }
            }
            dataTeilB = new List<List<string>>();
            sqlcmd = "SELECT TeilA FROM regelwerk WHERE TeilB = '" + ID + "'";
            dataTeilB = daten.getData(sqlcmd);
            //Teile A auswählen, die inkompatibel zu B sind
            foreach (List<string> list in dataTeilB)
            {
                string Teil = list[0].ToString();
                for (int i = 0; i < listBoxB.Items.Count; i++)
                {
                    string item = listBoxB.Items[i].ToString();
                    if (getID(item) == Teil) listBoxB.SetSelected(i, true);
                }
            }
        }

        private void insertBlacklist(string ID)
        {
            //Alle Datensätze entfernen, wo die ID im Regelwerk vorkommt
            string sqlcmd = "DELETE FROM regelwerk WHERE TeilA = '" + ID +"'";
            daten.setData(sqlcmd);
            sqlcmd = "DELETE FROM regelwerk WHERE TeilB = '" + ID + "'";
            daten.setData(sqlcmd);
            //hinzufügen der Daten, wenn Item gewählt
            int si = listBoxB.SelectedItems.Count;
            if (si != 0)
            {
                sqlcmd = "INSERT INTO regelwerk (TeilA, TeilB) VALUES ";
                for (int i = 0; i < si - 1; i++)
                {
                    sqlcmd += "('" + ID + "', '" + listBoxB.SelectedItems[i] + "'), ";
                }
                sqlcmd += "('" + ID + "', '" + listBoxB.SelectedItems[si - 1] + "')";
                daten.setData(sqlcmd);
            }
        }
        private string getID(string str)
        {
            //abspalten des Textes nach der ProduktID
            int s = str.IndexOf("-");
            str = str.Remove(s - 1);
            return str;
        }

        private void Konfigurator_Einstellungen_FormClosing(object sender, FormClosingEventArgs e)
        {
            insertBlacklist(aktItem);
        }
    }
}
