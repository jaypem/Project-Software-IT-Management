using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing.Printing;
using System.Drawing;
using System.Text;

namespace Fahrrad_ERP
{
    class Druck
    {
        //Voreinstellungen zu den Schriftarten
        private Font f11r = new Font("Arial", 11, FontStyle.Regular);
        private Font f11b = new Font("Arial", 11, FontStyle.Bold);
        private Font f15b = new Font("Arial", 15, FontStyle.Bold);
        private Font f8r = new Font("Arial", 8, FontStyle.Regular);
        private Font f8i = new Font("Arial", 8, FontStyle.Italic);
        private Font f8b = new Font("Arial", 8, FontStyle.Bold);
        private static Brush b = Brushes.Black;
        private Pen p = new Pen(b, 0.2F);
        //Zeilenabstände
        private float ab11 = 4.4F; //Zeilenabstand Schriftgröße 11
        private float ab8 = 3.2F; //Zeilenabstand Schrifgröße 8
        private float ab13 = 5.0F; //Zeilenabstand Schriftgröße 13
        //Seitenabstände            
        private int randl = 25; //Seitenrand links
        private int randt = 100; //Seitenrand oben für den Textabschnitt
        private int rando = 32; //Seitenrand oben
        private int randu = 266; //Anfang der Geschäftsdaten
        private int randg = 125; //Seitenrand für Geschäftsdaten

        public void Geschäftsangaben(Graphics g, int seite, int gesamt)
        {
            g.PageUnit = GraphicsUnit.Millimeter;
            Daten d = new Daten();
            //Geschäftsangaben unten
            g.DrawString("Rechtsinformationen", f8b, b, randl, randu);
            g.DrawString("Geschäftsführer:", f8r, b, randl, randu + ab8);
            g.DrawString("Handelsregister:", f8r, b, randl, randu + ab8 * 2);
            g.DrawString("Registernr.:", f8r, b, randl, randu + ab8 * 3);
            g.DrawString("UmsatzsteuerID:", f8r, b, randl, randu + ab8 * 4);
            g.DrawString(d.Geschäftsführer, f8r, b, randl + 23, randu + ab8);
            g.DrawString(d.Gericht, f8r, b, randl + 23, randu + ab8 * 2);
            g.DrawString(d.Register, f8r, b, randl + 23, randu + ab8 * 3);
            g.DrawString(d.UmsatzID, f8r, b, randl + 23, randu + ab8 * 4);
            g.DrawString("Bankverbindung", f8b, b, randl + 65, randu);
            g.DrawString("IBAN:", f8r, b, randl + 65, randu + ab8);
            g.DrawString("BIC:", f8r, b, randl + 65, randu + ab8 * 2);
            g.DrawString("Bank:", f8r, b, randl + 65, randu + ab8 * 3);
            g.DrawString(d.IBAN, f8r, b, randl + 76, randu + ab8);
            g.DrawString(d.BIC, f8r, b, randl + 76, randu + ab8 * 2);
            g.DrawString(d.Bank, f8r, b, randl + 76, randu + ab8 * 3);
            g.DrawString("Seite", f8b, b, randl + 140, randu);
            g.DrawString(seite + " / " + gesamt, f8r, b, randl + 140, randu + ab8);
        }
        public void Briefkopf(Graphics g)
        {
            g.PageUnit = GraphicsUnit.Millimeter;
            //Geschäftsdaten holen
            Daten d = new Daten();
            //Faltmarken
            g.DrawLine(p, 7, 87, 12, 87);
            g.DrawLine(p, 7, 192, 12, 192);
            //Absenderinfos
            g.DrawString(d.Name, f11b, b, randg, rando);
            g.DrawString(d.Add1, f11r, b, randg, rando + ab11);
            g.DrawString(d.Add2, f11r, b, randg, rando + ab11 * 2);
            g.DrawString(d.PLZ + " " + d.Ort, f11r, b, randg, rando + ab11 * 3);
            g.DrawString("Bearbeiter: " + User.Name + " " + User.Nachname, f11r, b, randg, rando + ab11 * 5);
            g.DrawString("Tel.:", f11r, b, randg, rando + ab11 * 7);
            g.DrawString("Fax.:", f11r, b, randg, rando + ab11 * 8);
            g.DrawString("Mail:", f11r, b, randg, rando + ab11 * 9);
            g.DrawString(d.Tel, f11r, b, randg + 10, rando + ab11 * 7);
            g.DrawString(d.Fax, f11r, b, randg + 10, rando + ab11 * 8);
            g.DrawString(d.Mail, f11r, b, randg + 10, rando + ab11 * 9);
            g.DrawString("Datum: " + DateTime.Today.ToShortDateString(), f11r, b, randg, rando + ab11 * 11);
        }
        public void Adresszeile(Graphics g, string[] AddList)
        {
            g.PageUnit = GraphicsUnit.Millimeter;
            //Adressblock
            g.DrawString(AddList[0], f11b, b, randl, rando + ab11*3);
            g.DrawString(AddList[1], f11b, b, randl, rando + ab11*4);
            g.DrawString(AddList[2], f11b, b, randl, rando + ab11*5);
            g.DrawString(AddList[3], f11b, b, randl, rando + ab11*6);
        }
        public void Postenauflistung(Graphics g, string kundennr, string bestellnr, string gesamt, string betreff, string datum, List<List<string>> list)
        {            
            g.PageUnit = GraphicsUnit.Millimeter;
            StringFormat rechts = new StringFormat();
            rechts.Alignment = StringAlignment.Far;
            //Spaltenanfänge
            int s1 = 30;
            int s2 = 40;
            int s3 = 50;
            int s4 = 130;
            int s5 = 145;
            int s6 = 165;
            int s7 = 185;
            //Adressblock
            g.DrawString(betreff, f15b, b, randl, randt);
            g.DrawString("Kundennummer", f8i, b, randl, randt + ab11 * 2);
            g.DrawString(kundennr, f8b, b, randl, randt + ab11 * 2 + ab8);
            if (betreff == "Rechnung")
            {
                g.DrawString("Auftragsnummer", f8i, b, randl + 40, randt + ab11 * 2);
                g.DrawString("Auftragsdatum", f8i, b, randl + 80, randt + ab11 * 2);
                g.DrawString(bestellnr, f8b, b, randl + 40, randt + ab11 * 2 + ab8);
                g.DrawString(datum, f8b, b, randl + 80, randt + ab11 * 2 + ab8);
            }
            g.DrawString("Pos", f11b, b, s1, randt + ab11 * 5);
            g.DrawString("ID", f11b, b, s2, randt + ab11 * 5);
            g.DrawString("Bezeichnung", f11b, b, s3, randt + ab11 * 5);
            g.DrawString("Menge", f11b, b, s4, randt + ab11 * 5);
            g.DrawString("Preis", f11b, b, s5, randt + ab11 * 5);
            g.DrawString("Gesamt", f11b, b, s6, randt + ab11 * 5);
            float up = randt + ab11 * 4.9F;
            g.DrawLine(p, s1 - 1, up, s7, up);
            float line = randt + ab11 * 5 + ab13;
            g.DrawLine(p, s1 - 1, line + 0.1F, s7, line + 0.1F);
            foreach (List<string> l in list) {
                g.DrawString(l[0], f11r, b, s1, line);
                g.DrawString(l[1], f11r, b, s2, line);
                g.DrawString(l[2], f11r, b, s3, line);
                g.DrawString(l[3], f11r, b, s4, line);
                g.DrawString((Convert.ToDecimal(l[4])).ToString("0.00 €"), f11r, b, s6 - 1, line, rechts);
                g.DrawString((Convert.ToDecimal(l[5])).ToString("0.00 €"), f11r, b, s7 - 1, line, rechts);
                line += ab13;
                g.DrawLine(p, s1 - 1, line + 0.1F, s7, line + 0.1F);
            }
            g.DrawLine(p, s1 - 1, up, s1 - 1, line + 0.1F);
            g.DrawLine(p, s2 - 1, up, s2 - 1, line + 0.1F);
            g.DrawLine(p, s3 - 1, up, s3 - 1, line + 0.1F);
            g.DrawLine(p, s5 - 1, up, s5 - 1, line + 0.1F);
            g.DrawString("Netto", f11b, b, s4, line);
            g.DrawString((Convert.ToDouble(gesamt.TrimEnd(' ', '€')) / 1.19).ToString("0.00 €"), f11r, b, s7 - 1, line, rechts);
            line += ab13;
            g.DrawLine(p, s4 - 1, line + 0.1F, s7, line + 0.1F);
            g.DrawString("MwSt 19%", f11b, b, s4, line);
            g.DrawString((Convert.ToDouble(gesamt.TrimEnd(' ', '€')) * 0.19).ToString("0.00 €"), f11r, b, s7 - 1, line, rechts);
            line += ab13;
            g.DrawLine(p, s4 - 1, line + 0.1F, s7, line + 0.1F);
            g.DrawString("Gesamt", f11b, b, s4, line);
            g.DrawString((Convert.ToDecimal(gesamt.TrimEnd(' ', '€'))).ToString("0.00 €"), f11b, b, s7 - 1, line, rechts);
            line += ab13;
            g.DrawLine(p, s4 - 1, line + 0.1F, s7, line + 0.1F);
            g.DrawLine(p, s4 - 1, up, s4 - 1, line + 0.1F);
            g.DrawLine(p, s6 - 1, up, s6 - 1, line + 0.1F);
            g.DrawLine(p, s7, up, s7, line + 0.1F);
            if (betreff == "Rechnung")
            {
                line += 2 * ab11;
                g.DrawString("Den Gesamtbetrag i.H.v. " + gesamt + " überweisen Sie bitte binnen 10 Tagen unter Angabe\n" +
                "des Verwendungszwecks: "+ datum.Replace(".","") +"-"+ bestellnr +" auf untenstehenden Konto.\n\nVielen Dank für Ihren Einkauf!", f11r, b, randl, line);
            }
        }
    }
}
