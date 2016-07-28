using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Fahrrad_ERP
{
    public partial class main : Form
    {
        //private int childFormNumber = 0;

        public main()
        {
            InitializeComponent();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            öffnen(sender, e);
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sichern(sender, e);
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
            this.Close();
        }


        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void eigeneDatenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showForm(new eigeneDaten());
        }

        private void passwortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new Passwort().ShowDialog();
        }

        private void main_Load(object sender, EventArgs e)
        {
            if (User.login == null)
            {
                Anmeldung a = new Anmeldung();
                a.StartPosition = FormStartPosition.CenterScreen;
                a.ShowDialog();
            }
        }
        private void personaldatenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showForm(new Mitarbeiter_Ansicht());
        }
        private void showForm(Form ob)
        {
            ob.MdiParent = this;
            ob.Show();
        }
        public void UserSettings()
        {
            personaldatenToolStripMenuItem.Visible = User.admin;
            geschäftsdatenToolStripMenuItem.Visible = User.admin;
            werkstattMenu.Visible = User.ansichtW;
            ladenMenu.Visible = User.ansichtL;
            verwaltungMenu.Visible = User.ansichtV;
            setEnabledIcon(false);
            Status("angemeldeter User: " + User.login);
        }

        public void Status(string str)
        {
            toolStripStatusLabel.Text = str;
            if (timer1.Enabled)
            {
                timer1.Stop();
            }
            timer1.Start();
        }

        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Möchten Sie das Programm wirklich beenden?", "Beenden", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Application.ExitThread();
            }
            else
            {
                e.Cancel = true;
            }
        }

        private void geschäftsdatenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showForm(new Geschäftsdaten());
        }

        private void rechnungenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showForm(new Rechnung("Rechnung"));
        }

        private void produktkonfiguratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showForm(new Konfigurator("Konfigurator"));
        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Drucken();
        }

        private void printSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Druckoptionen d = new Druckoptionen();
            if (d.ShowDialog() == DialogResult.OK)
            {
                Status("Druckeinstellungen wurden geändert.");
            }
            else
            {
                Status("Druckeinstellungen wurden nicht geändert.");
            }
        }

        private void printPreviewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Druckvorschau();
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            Drucken();
        }
        private void Drucken()
        {
            //bei entsprechender Auswahl einer Form, kann der Druck-Button (Strg+P) verschiedene Methoden auslösen
            if (ActiveMdiChild != null)
            {
                switch (ActiveMdiChild.Name)
                {
                    case "Rechnung":
                        ((Rechnung)ActiveMdiChild).print();
                        break;
                    case "Konfigurator":
                        ((Konfigurator)ActiveMdiChild).print();
                        break;
                    default:

                        break;
                }
            }
        }
        private void Druckvorschau()
        {
            if (ActiveMdiChild != null)
            {
                switch (ActiveMdiChild.Name)
                {
                    case "Rechnung":
                        ((Rechnung)ActiveMdiChild).printPre();
                        break;
                    case "Konfigurator":
                        ((Konfigurator)ActiveMdiChild).preprint();
                        break;
                    default:

                        break;
                }
            }
        }
        private void öffnen(object sender, EventArgs e)
        {
            //bei entsprechender Auswahl einer Form, kann der Öffnen-Button (Strg+O) verschiedene Methoden auslösen
            if (ActiveMdiChild != null)
            {
                switch (ActiveMdiChild.Name)
                {
                    case "Rechnung":
                        ((Rechnung)ActiveMdiChild).open();
                        break;
                    case "Konfigurator":
                        ((Konfigurator)ActiveMdiChild).openConfig();
                        break;
                    default:

                        break;
                }
            }
        }

        private void sichern(object sender, EventArgs e)
        {
            //bei entsprechender Auswahl einer Form, kann der Speicher-Button (Strg+S) verschiedene Methoden auslösen
            if (ActiveMdiChild != null)
            {
                switch (ActiveMdiChild.Name)
                {
                    case "Konfigurator":
                        if (sender.ToString() == "Speichern" || sender.ToString() == "&Speichern")
                            ((Konfigurator)ActiveMdiChild).saveConfig();
                        if (sender.ToString() == "Speichern &unter")
                            ((Konfigurator)ActiveMdiChild).saveConfig();
                        break;
                    default:

                        break;
                }
            }
        }
        private void printPreviewToolStripButton_Click(object sender, EventArgs e)
        {
            Druckvorschau();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            sichern(sender, e);
        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            öffnen(sender, e);
        }

        private void taskManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            taskManagement t = new taskManagement();
            t.Show();
        }

        private void wareneingangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showForm(new Warenlager("Eingang"));
        }

        private void warenausgangToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showForm(new Warenlager("Ausgang"));
        }

        private void warenausgangToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            showForm(new Warenlager("Ausgang"));
        }

        private void kundenstammdatenToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void anlegenToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Kunde_ändern k = new Kunde_ändern("Anlegen");
            if (k.ShowDialog(this) != DialogResult.OK)
            {
                Status("Anlage abgebrochen!");
            }
        }

        private void bearbeitenToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            showForm(new Kunden_Ansicht());
        }

        private void bearbeitenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showForm(new Kunden_Ansicht());
        }

        private void öffnenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showForm(new Konfigurator("Konfigurator"));
        }

        private void einstellungenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showForm(new Konfigurator_Einstellungen());
        }

        private void aufträgeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showForm(new Rechnung("Bestellung"));
        }

        private void übersichtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showForm(new Rechnung("Bestellung"));
        }

        private void aufträgeToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            showForm(new Rechnung("Bestellung"));
        }

        private void main_MdiChildActivate(object sender, EventArgs e)
        {
            //je nach aktiven Mdi-Child werden die Optionen für Drucken, Speichern, Neu etc. aktiviert oder deaktiviert
            if (ActiveMdiChild != null)
            {
                switch (ActiveMdiChild.Name)
                {
                    case "eigeneDaten":
                        setEnabledIcon(false);
                        break;
                    case "Geschäftsdaten":
                        setEnabledIcon(false);
                        break;
                    case "Mitarbeiter_Ansicht":
                        setEnabledIcon(false);
                        newToolStripButton.Enabled = true;
                        newToolStripMenuItem.Enabled = true;
                        break;
                    case "Material_Ansicht":
                        setEnabledIcon(false);
                        newToolStripButton.Enabled = true;
                        newToolStripMenuItem.Enabled = true;
                        break;
                    case "Kunden_Ansicht":
                        setEnabledIcon(false);
                        newToolStripButton.Enabled = true;
                        newToolStripMenuItem.Enabled = true;
                        break;
                    case "Konfigurator":
                        setEnabledIcon(true);
                        break;
                    case "Konfigurator_Einstellungen":
                        setEnabledIcon(false);
                        break;
                    case "Rechnung":
                        setEnabledIcon(true);
                        newToolStripButton.Enabled = false;
                        newToolStripMenuItem.Enabled = false;
                        saveAsToolStripMenuItem.Enabled = false;
                        saveToolStripButton.Enabled = false;
                        saveToolStripMenuItem.Enabled = false;
                        break;
                    case "Warenlager":
                        setEnabledIcon(false);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                setEnabledIcon(false);
            }
        }
        private void setEnabledIcon(bool active)
        {
            newToolStripButton.Enabled = active;
            newToolStripMenuItem.Enabled = active;
            openToolStripButton.Enabled = active;
            openToolStripMenuItem.Enabled = active;
            saveToolStripButton.Enabled = active;
            saveToolStripMenuItem.Enabled = active;
            saveAsToolStripMenuItem.Enabled = active;
            printPreviewToolStripButton.Enabled = active;
            printPreviewToolStripMenuItem.Enabled = active;
            printToolStripButton.Enabled = active;
            printToolStripMenuItem.Enabled = active;
        }

        private void anlegenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            showForm(new Material_ändern("Anlegen"));
        }

        private void bearbeitenToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            showForm(new Material_Ansicht());
        }

        private void anlegenToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            showForm(new Material_ändern("Anlegen"));
        }

        private void bearbeitenToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            showForm(new Material_Ansicht());
        }

        private void anlegenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showForm(new Kunde_ändern("Anlegen"));
        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            Neu();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Neu();
        }
        private void Neu()
        {
            //bei entsprechender Auswahl einer Form, kann der Neu-Button (Strg+N) verschiedene Forms öffnen
            if (ActiveMdiChild != null)
            {
                switch (ActiveMdiChild.Name)
                {
                    case "Kunden_Ansicht":
                        if (new Kunde_ändern("Anlegen").ShowDialog(this) != DialogResult.OK)
                        {
                            Status("Anlage abgebrochen!");
                        }
                        break;
                    case "Mitarbeiter_Ansicht":
                        Mitarbeiter_ändern m = new Mitarbeiter_ändern("Anlegen");
                        if (m.ShowDialog() == DialogResult.OK)
                        {
                            Status("Der Mitarbeier " + m.name() + " wurde erfolgreich angelegt.");
                        }
                        else
                        {
                            Status("Anlage abgebrochen!");
                        }
                        break;
                    case "Material_Ansicht":
                        if (new Material_ändern("Anlegen").ShowDialog(this) != DialogResult.OK)
                        {
                            Status("Anlage abgebrochen!");
                        }
                        break;
                    default:

                        break;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel.Text = "";
            timer1.Stop();
        }

        private void bearbeitenToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            showForm(new Konfigurator("Bestellung"));
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sichern(sender, e);
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showForm(new AboutBox1());
        }
    }
}
