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
            showForm(new Passwort());
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
            Status("angemeldeter User: " + User.login);
        }

        public void Status(string str)
        {
            toolStripStatusLabel.Text = str;
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
            showForm(new Rechnung());
        }

        private void produktkonfiguratorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            showForm(new Konfigurator());
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
            if (ActiveMdiChild != null)
            {
                switch (ActiveMdiChild.Text)
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
                switch (ActiveMdiChild.Text)
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
            if (ActiveMdiChild != null)
            {
                switch (ActiveMdiChild.Text)
                {
                    case "Rechnung":
                        ((Rechnung)ActiveMdiChild).buttonWahl_Click(sender, e);
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
            if (ActiveMdiChild != null)
            {
                switch (ActiveMdiChild.Text)
                {
                    case "Konfigurator":
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
            showForm(new Wareneingang());
        }

    }
}
