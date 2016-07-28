using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;

namespace Fahrrad_ERP
{
    public partial class Geschäftsdaten : Form
    {
        public Geschäftsdaten()
        {
            InitializeComponent();
        }
        string filename;
        private void Geschäftsdaten_Load(object sender, EventArgs e)
        {
            //Daten in die entsprechenden Felder laden
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
            checkBoxLogo.Checked = d.BildNutzen;
            pictureBox1.Visible = d.BildNutzen;
            FileStream imageStream = new FileStream(d.Bild, FileMode.Open, FileAccess.Read);
            pictureBox1.Image = System.Drawing.Image.FromStream(imageStream);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Daten in die Einstellungsdatei schreiben
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
            d.Bild = filename;
            d.BildNutzen = checkBoxLogo.Checked;
            d.Save();
            ((main)this.MdiParent).Status("Geschäftsdaten wurden erfolgreich gespeichert.");
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonLogo_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "Bild-Datei (*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF";
            openFileDialog1.Title = "Wählen Sie ein Bild";

            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                filename = openFileDialog1.FileName;
                FileStream imageStream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                pictureBox1.Image = System.Drawing.Image.FromStream(imageStream);
            }
        }
        public string ImageToBase64String(Image image, ImageFormat format)
        {
            MemoryStream memory = new MemoryStream();
            image.Save(memory, format);
            string base64 = Convert.ToBase64String(memory.ToArray());
            memory.Close();
            return base64;
        }

        public Image ImageFromBase64String(string base64)
        {
            MemoryStream memory = new MemoryStream(Convert.FromBase64String(base64));
            Image result = Image.FromStream(memory);
            memory.Close();
            return result;
        }

        private void checkBoxLogo_CheckedChanged(object sender, EventArgs e)
        {
            pictureBox1.Visible = checkBoxLogo.Checked;
        }

    }
}
