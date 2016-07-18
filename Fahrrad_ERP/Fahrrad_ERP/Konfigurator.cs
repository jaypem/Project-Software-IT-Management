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
    public partial class Konfigurator : Form
    {
        public Konfigurator()
        {
            InitializeComponent();
        }
        string konfignr;
        private List<List<string>> dataListGroup = new List<List<string>>(); //für Produktgruppen
        private List<List<string>> dataListProd = new List<List<string>>(); //für Produkte
        private List<List<string>> dataListBlackA = new List<List<string>>(); //für InkompatibleProdukte von A
        private List<List<string>> dataListBlackB = new List<List<string>>(); //für InkompatibleProdukte von B
        private List<List<string>> dataListKonf = new List<List<string>>(); //für Konfigurationspos

        private void Konfigurator_Load(object sender, EventArgs e)
        {
            refreshTree();
        }
        private void refreshTree()
        {
            clearTree();
            Database_Fahrrad daten = new Database_Fahrrad();
            string sqlcmd = "SELECT KategorieID, Bezeichnung FROM produktkategorie";
            dataListGroup = daten.getData(sqlcmd);
            sqlcmd = "SELECT ProduktID, Bezeichnung, Produktkategorie, Preis FROM produkte";
            dataListProd = daten.getData(sqlcmd);
            sqlcmd = "SELECT TeilA, TeilB, produkte.Produktkategorie FROM regelwerk INNER JOIN produkte ON produkte.ProduktID = TeilB ORDER BY TeilA";
            dataListBlackA = daten.getData(sqlcmd);
            sqlcmd = "SELECT TeilB, TeilA, produkte.Produktkategorie FROM regelwerk INNER JOIN produkte ON produkte.ProduktID = TeilA ORDER BY TeilB";
            dataListBlackB = daten.getData(sqlcmd);
            set_Gruppen();
            set_GruppenInhalt();
        }
        private void set_Gruppen()
        {
            foreach (List<string> l in dataListGroup)
            {
                treeViewProd.Nodes.Add(l[0].ToString(), l[1].ToString());
            }
        }
        private void set_GruppenInhalt()
        {
            foreach (List<string> l in dataListProd)
            {
                treeViewProd.Nodes[l[2].ToString()].Nodes.Add(l[0].ToString(), l[1].ToString());
                treeViewProd.Nodes[l[2].ToString()].Nodes[l[0].ToString()].ToolTipText = (Convert.ToDecimal(l[3].ToString())).ToString("0.00 €");
            }
        }
        private void clearTree()
        {
            foreach (List<string> l in dataListGroup)
            {
                treeViewProd.Nodes[l[0].ToString()].Remove();
            }
        }
        private void fill()
        {
            //Konfigurationspos auslesen
            string sqlcmd = "SELECT Position, konfigurationenpos.ProduktID, produkte.Bezeichnung, Menge, produkte.Preis FROM konfigurationenpos INNER JOIN produkte ON produkte.ProduktID = konfigurationenpos.ProduktID WHERE KonfigurationID = '" + konfignr + "'";
            Database_Fahrrad daten = new Database_Fahrrad();
            dataListKonf = daten.getData(sqlcmd);
            refreshTree();
            listViewKonfig.Items.Clear();
            foreach (List<string> list in dataListKonf)
            {
                listViewKonfig.Items.Add(new ListViewItem(new string[] { list[0].ToString(), list[1].ToString(), list[2].ToString(), list[3].ToString(), (Convert.ToDecimal(list[4])).ToString("0.00"), (Convert.ToInt16(list[3])*Convert.ToDecimal(list[4])).ToString("0.00") }));
                blackList(list[1].ToString(), true); //blackliste beachten
            }
            labelSum.Text = "Summe: " + sum().ToString("0.00 €");
            labelKon.Text = "Konfiguration "+konfignr;
        }

        private void treeViewProd_AfterSelect(object sender, TreeViewEventArgs e)
        {
            button1.Text = "Hinzufügen >>";
            if (treeViewProd.SelectedNode.Level == 1) //Produktgruppen werden nicht weiter definiert, nur echte Produkte
            {
                labelID.Text = treeViewProd.SelectedNode.Name;
                labelBezeichnung.Text = treeViewProd.SelectedNode.Text;
                labelPreis.Text = treeViewProd.SelectedNode.ToolTipText;
                button1.Enabled = true;
            }
            else
            {
                labelID.Text = "";
                labelBezeichnung.Text = "";
                labelPreis.Text = "";
                button1.Enabled = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Hinzufügen >>")
            {
                addProd();
            }
            else
            {
                remProd();
            }
        }
        private void addProd()
        {
            //Produkt hinzufügen
            int i = listViewKonfig.Items.Count;
            double p = Convert.ToDouble(labelPreis.Text.TrimEnd(' ', '€'));
            int an = Convert.ToInt16(numericUpDown1.Value);
            string ID = labelID.Text;
            bool found = false;
            //Überprüfen ob Produkt schon in Auswahl enthalten
            for (int j = 0; j < listViewKonfig.Items.Count; j++)
            {
                if (listViewKonfig.Items[j].SubItems[1].Text == ID)
                {
                    an += Convert.ToInt16(listViewKonfig.Items[j].SubItems[3].Text);
                    listViewKonfig.Items[j].SubItems[3].Text = an.ToString();
                    listViewKonfig.Items[j].SubItems[5].Text = (p * an).ToString("0.00");
                    found = true;
                    break;
                }
            }
            if (found == false)
            {
                listViewKonfig.Items.Add(new ListViewItem(new string[] { (i + 1).ToString(), ID, labelBezeichnung.Text, an.ToString(), p.ToString("0.00"), (p * an).ToString("0.00") }));
            }
            numericUpDown1.Value = 1;
            blackList(ID, true);
            labelSum.Text = "Summe: " + sum().ToString("0.00 €");
        }

        private void remProd()
        {
            if (listViewKonfig.SelectedIndices.Count == 1) //Wenn nur Ein Element ausgewählt ist, kann auch Mengenmäßig entfernt werden
            {
                int anz = Convert.ToInt16(numericUpDown1.Value);
                int menge = Convert.ToInt16(listViewKonfig.SelectedItems[0].SubItems[3].Text);
                if (menge - anz <= 0) //ist die zu löschende Menge gleich der aktuellen wird das Element entfernt
                {
                    string ID = listViewKonfig.SelectedItems[0].SubItems[1].Text;
                    listViewKonfig.SelectedItems[0].Remove();
                    blackList(ID, false);
                    numericUpDown1.Value = 1;
                }
                else
                {
                    listViewKonfig.SelectedItems[0].SubItems[3].Text = (menge - anz).ToString();
                    listViewKonfig.SelectedItems[0].SubItems[5].Text = (Convert.ToDouble(listViewKonfig.SelectedItems[0].SubItems[4].Text) * (menge - anz)).ToString("0.00");
                    numericUpDown1.Value = menge - anz;
                }
            }
            else //es wird alles markierte entfernt
            {
                foreach (ListViewItem i in listViewKonfig.SelectedItems)
                {
                    string ID = i.SubItems[1].Text;
                    i.Remove();
                    blackList(ID, false);
                }
            }
            foreach (ListViewItem item in listViewKonfig.Items) //Positionen werden neu gesetzt
            {
                int i = item.Index;
                item.SubItems[0].Text = (i + 1).ToString();
            }
            labelSum.Text = "Summe: " + sum().ToString("0.00 €");
        }
        private double sum()
        {
            //Summenbildung für Preiskalkulation
            double summe = 0;
            for (int i = 0; i < listViewKonfig.Items.Count; i++)
            {
                summe += Convert.ToDouble(listViewKonfig.Items[i].SubItems[5].Text);
            }
            return summe;
        }
        private void treeViewProd_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (treeViewProd.SelectedNode.Level == 1)
                {
                    addProd();
                }
            }
            catch
            {

            }
        }

        private void listViewKonfig_SelectedIndexChanged(object sender, EventArgs e)
        {
            button1.Text = "<< Entfernen";
            button1.Enabled = true;
            try
            {
                labelID.Text = listViewKonfig.SelectedItems[0].SubItems[1].Text;
                labelBezeichnung.Text = listViewKonfig.SelectedItems[0].SubItems[2].Text;
                labelPreis.Text = listViewKonfig.SelectedItems[0].SubItems[4].Text;
                numericUpDown1.Value = Convert.ToInt16(listViewKonfig.SelectedItems[0].SubItems[3].Text);
            }
            catch
            {
            }
        }

        private void blackList(string prod, bool add)
        {
            bool found = false;
            if (add)
            {
                foreach (List<string> list in dataListBlackA)
                {
                    if (list[0].ToString() == prod) //in TeilA Liste gefunden
                    {
                        treeViewProd.Nodes[list[2].ToString()].Nodes[list[1].ToString()].ForeColor = Color.Gray;
                        found = true;
                    }
                    else if (found) break;
                }
                found = false;
                foreach (List<string> list in dataListBlackB)
                {
                    if (list[0].ToString() == prod) //in TeilB Liste gefunden
                    {
                        treeViewProd.Nodes[list[2].ToString()].Nodes[list[1].ToString()].ForeColor = Color.Gray;
                        found = true;
                    }
                    else if (found) break;
                }
            }
            else
            {
                //entferntes Produkt gibt alle blockierten Produkte wieder frei
                foreach (List<string> list in dataListBlackA)
                {
                    if (list[0].ToString() == prod) //in TeilA Liste gefunden
                    {
                        treeViewProd.Nodes[list[2].ToString()].Nodes[list[1].ToString()].ForeColor = Color.Black;
                        found = true;
                    }
                    else if (found) break;
                }
                found = false;
                foreach (List<string> list in dataListBlackB)
                {
                    if (list[0].ToString() == prod) //in TeilB Liste gefunden
                    {
                        treeViewProd.Nodes[list[2].ToString()].Nodes[list[1].ToString()].ForeColor = Color.Black;
                        found = true;
                    }
                    else if (found) break;
                }
                foreach (ListViewItem i in listViewKonfig.Items)
                {
                    found = false;
                    //Überprüfung ob alle Objekte blockiert sind, die in der Auswahlliste sind
                    foreach (List<string> list in dataListBlackA)
                    {
                        if (list[0].ToString() == i.SubItems[1].Text) //in TeilA Liste gefunden
                        {
                            treeViewProd.Nodes[list[2].ToString()].Nodes[list[1].ToString()].ForeColor = Color.Gray;
                            found = true;
                        }
                        else if (found) break;
                    }
                    found = false;
                    foreach (List<string> list in dataListBlackB)
                    {
                        if (list[0].ToString() == i.SubItems[1].Text) //in TeilB Liste gefunden
                        {
                            treeViewProd.Nodes[list[2].ToString()].Nodes[list[1].ToString()].ForeColor = Color.Gray;
                            found = true;
                        }
                        else if (found) break;
                    }
                }
            }
        }

        private void treeViewProd_BeforeSelect(object sender, TreeViewCancelEventArgs e)
        {
            if (e.Node.ForeColor == Color.Gray) e.Cancel = true; //Graue tree Items können nicht verwendet werden
        }

        public void saveConfig()
        {
            string sqlcmd;
            Database_Fahrrad daten = new Database_Fahrrad();
            if (labelKon.Text == "")
            {
                Konfiguration_speichern ks = new Konfiguration_speichern();
                if (ks.ShowDialog() == DialogResult.OK)
                {
                    if (ks.Konf) //ist Konfiguration
                    {
                        sqlcmd = "INSERT INTO konfigurationenpos (KonfigurationID, Position, Menge, ProduktID) VALUES ";
                        for (int j = 0; j < listViewKonfig.Items.Count; j++)
                        {
                            string pos = listViewKonfig.Items[j].SubItems[0].Text;
                            string prod = listViewKonfig.Items[j].SubItems[1].Text;
                            string menge = listViewKonfig.Items[j].SubItems[3].Text;
                            if (j != listViewKonfig.Items.Count - 1) sqlcmd += "('" + ks.KonfID + "', '" + pos + "' , '" + menge + "','" + prod + "'), ";
                            else sqlcmd += "('" + ks.KonfID + "', '" + pos + "' , '" + menge + "','" + prod + "')";
                        }
                        daten.setData(sqlcmd);
                    }
                    else //ist Bestellung
                    {

                    }
                }
            }
            else
            {
                string ID = labelKon.Text;
                ID = ID.Replace("Konfiguration ", " ");
                sqlcmd = "DELETE FROM konfigurationenpos WHERE KonfigurationID = '" + ID + "'";
                daten.setData(sqlcmd);
                sqlcmd = "INSERT INTO konfigurationenpos (KonfigurationID, Position, Menge, ProduktID) VALUES ";
                for (int j = 0; j < listViewKonfig.Items.Count; j++)
                {
                    string pos = listViewKonfig.Items[j].SubItems[0].Text;
                    string prod = listViewKonfig.Items[j].SubItems[1].Text;
                    string menge = listViewKonfig.Items[j].SubItems[3].Text;
                    if (j != listViewKonfig.Items.Count - 1) sqlcmd += "('" + ID + "', '" + pos + "' , '" + menge + "','" + prod + "'), ";
                    else sqlcmd += "('" + ID + "', '" + pos + "' , '" + menge + "','" + prod + "')";
                }
                daten.setData(sqlcmd);
            }
        }

        public void openConfig()
        {
            Auswahl a = new Auswahl("Konfiguration");
            if (a.ShowDialog(this) == DialogResult.OK)
            {
                 konfignr = a.get_ID();
                ((main)this.MdiParent).Status("Es wurde die Konfiguration " + konfignr + " ausgewählt.");
                fill();
            }
            else
            {
                ((main)this.MdiParent).Status("Auswahl abgebrochen!");
            }
        }

        public void print()
        {
            Druck dr = new Druck();
            if (labelKon.Text == "")
            {
                printDocument1.DocumentName = "Konfiguration_ungespeichert";
            }
            else
            {
                printDocument1.DocumentName = labelKon.Text.Trim(' ');
            }
            PrintDialog p = printDialog1;
            p.PrinterSettings = dr.Einstellungen();
            if (p.ShowDialog() == DialogResult.OK)
            {
                printDocument1.PrinterSettings = p.PrinterSettings;
                printDocument1.Print();
            }
        }
        public void preprint()
        {
            Druck dr = new Druck();
            if (labelKon.Text == "")
            {
                printDocument1.DocumentName = "Konfiguration_ungespeichert";
            }
            else
            {
                printDocument1.DocumentName = labelKon.Text.Trim(' ');
            }
            printPreviewDialog1.ShowDialog();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            List<List<string>> dataListInhalt = new List<List<string>>();
            for (int i = 0; i < listViewKonfig.Items.Count; i++)
            {
                ListViewItem item = listViewKonfig.Items[i];
                List<string> zeile = new List<string>();
                for (int j = 0; j < item.SubItems.Count; j++)
                {
                    zeile.Add(item.SubItems[j].Text);
                }
                dataListInhalt.Add(zeile);
            }
            string konfnr = labelKon.Text.Replace("Konfiguraiton ", "");
            string gesamt = sum().ToString("0.00");
            Druck dr = new Druck();
            dr.Briefkopf(e.Graphics);
            dr.Geschäftsangaben(e.Graphics, 1, 1);
            dr.Postenauflistung(e.Graphics, "", konfignr, gesamt, "Konfiguration", DateTime.Now.Date.ToShortDateString(), dataListInhalt);  
        }
    }
}
