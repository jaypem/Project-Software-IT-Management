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
        private List<List<string>> dataListGroup = new List<List<string>>(); //für Produktgruppen
        private List<List<string>> dataListProd = new List<List<string>>(); //für Produkte
        private List<List<string>> dataListBlackA = new List<List<string>>(); //für InkompatibleProdukte von A
        private List<List<string>> dataListBlackB = new List<List<string>>(); //für InkompatibleProdukte von B

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
            for (int i = 0; i < treeViewProd.Nodes.Count; i++)
            {
                treeViewProd.Nodes[i].Remove();
            }
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
    }
}
