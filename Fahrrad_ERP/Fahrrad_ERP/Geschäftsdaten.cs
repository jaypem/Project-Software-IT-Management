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
    public partial class Geschäftsdaten : Form
    {
        public Geschäftsdaten()
        {
            InitializeComponent();
        }

        private void Geschäftsdaten_Load(object sender, EventArgs e)
        {
            Daten d = new Daten();
            textBoxGeschäftsführer.Text = d.Geschäftsführer;
            textBoxName.Text = d.Name;
            textBoxAdd1.Text = d.Add1;
            textBoxAdd2.Text = d.Add2;
            textBoxPLZ.Text = d.PLZ;
            textBoxOrt.Text = d.Ort;
            textBoxTel.Text = d.Tel;
            textBoxFax.Text = d.Fax;
            textBoxMail.Text = d.Mail;
            textBoxGericht.Text = d.Gericht;
            textBoxRegister.Text = d.Register;
            textBoxUmsatzID.Text = d.UmsatzID;
            textBoxIBAN.Text = d.IBAN;
            textBoxBIC.Text = d.BIC;
            textBoxBank.Text = d.Bank;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Daten d = new Daten();
            d.Geschäftsführer = textBoxGeschäftsführer.Text;
            d.Name = textBoxName.Text;
            d.Add1 = textBoxAdd1.Text;
            d.Add2 = textBoxAdd2.Text;
            d.PLZ = textBoxPLZ.Text;
            d.Ort = textBoxOrt.Text;
            d.Tel = textBoxTel.Text;
            d.Fax = textBoxFax.Text;
            d.Mail = textBoxMail.Text;
            d.Gericht = textBoxGericht.Text;
            d.Register = textBoxRegister.Text;
            d.UmsatzID = textBoxUmsatzID.Text;
            d.IBAN = textBoxIBAN.Text;
            d.BIC = textBoxBIC.Text;
            d.Bank = textBoxBank.Text;
            d.Save();
            ((main)this.MdiParent).Status("Geschäftsdaten wurden erfolgreich gespeichert.");
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
