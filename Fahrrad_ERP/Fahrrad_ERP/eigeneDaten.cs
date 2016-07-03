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
    public partial class eigeneDaten : Form
    {
        public eigeneDaten()
        {
            InitializeComponent();
        }

        private void eigeneDaten_Load(object sender, EventArgs e)
        {
            labelLog.Text = User.login;
            labelName.Text = User.Nachname;
            labelVorname.Text = User.Name;
            labelAbteil.Text = User.Abteilung;
            checkBox1.Checked = User.ansichtL;
            checkBox2.Checked = User.ansichtV;
            checkBox3.Checked = User.ansichtW;
            checkBox4.Checked = User.admin;
        }
        
    }
}
