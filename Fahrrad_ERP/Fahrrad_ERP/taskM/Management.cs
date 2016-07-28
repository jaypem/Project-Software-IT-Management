using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Fahrrad_ERP.taskM
{
    class Management
    {
        Database_Fahrrad daten = new Database_Fahrrad();
        public void setStatus(string Auftragsnummer, string Status, string Benutzer)
        {
            //Setzt den Status als neue Aktion
            string sqlcmd = "INSERT INTO auftragshistorie (Auftragsnummer, StatusID, Bearbeiter) VALUES ('"+Auftragsnummer+"','"+Status+"','"+Benutzer+"')";
            daten.setData(sqlcmd);
        }
        public string getStatus(string Auftragsnummer)
        {
            //gibt den Status zur Auftragsnummer zurück, wobei der Status der höchsten Aktionsnummer verwendet wird
            string str = "";
            List<List<string>> dataList = new List<List<string>>();
            string sqlcmd = "SELECT StatusID From auftragshistorie JOIN (SELECT MAX(Aktionsnummer) AS AkNr FROM auftragshistorie GROUP BY Auftragsnummer) AS A ON auftragshistorie.Aktionsnummer = A.AkNr WHERE Auftragsnummer = '" + Auftragsnummer + "'";
            dataList = daten.getData(sqlcmd);
            str = dataList[0][0].ToString();
            return str;
        }
        public List<List<string>> getStatusHistorie(string Auftragsnummer)
        {
            //gibt die Statushistorie zurück
            List<List<string>> dataList = new List<List<string>>();
            string sqlcmd = "SELECT Aktionsnummer, StatusID, Bearbeiter, Zeitstempel FROM auftragshistorie WHERE Auftragsnummer = '" + Auftragsnummer + "'";
            dataList = daten.getData(sqlcmd);
            return dataList;
        }
        public bool isAktionChange(string AufNr, string AkNr)
        {
            //überprüft ob die ggb. Aktion auch die letzte zum Auftrag ist
            string str = "";
            List<List<string>> dataList = new List<List<string>>();
            string sqlcmd = "SELECT MAX(Aktionsnummer) FROM auftragshistorie WHERE Auftragsnummer = '" + AufNr + "'";
            dataList = daten.getData(sqlcmd);
            str = dataList[0][0].ToString();
            if (AkNr != str) return true; else return false;
        }
        public bool isTaskCountChange(string Benutzer, int count)
        {
            //Rückgabe der vom User übernommenen Task oder jener, die direkt an ihn gerichtet sind
            List<List<string>> dataList = new List<List<string>>();
            string sqlcmd = "SELECT COUNT(auftrag.Auftragsnummer) From auftrag Join (SELECT auftragshistorie.Auftragsnummer, StatusID, Bearbeiter From auftragshistorie JOIN (SELECT Auftragsnummer, MAX(Aktionsnummer) As AkNr From auftragshistorie GROUP BY Auftragsnummer) AS A ON A.AkNr = auftragshistorie.Aktionsnummer) AS B ON B.Auftragsnummer = auftrag.Auftragsnummer WHERE (auftrag.An = '"+Benutzer+"' AND B.StatusID < 2) OR (B.Bearbeiter = '"+Benutzer+"' AND B.StatusID = '1')";
            dataList = daten.getData(sqlcmd);
            if (count != Convert.ToInt16(dataList[0][0].ToString())) return true;
            else return false;
        }
        public List<List<string>> getUserTask(string Benutzer)
        {
            //Rückgabe der vom User übernommenen Task oder jener, die direkt an ihn gerichtet sind
            List<List<string>> dataList = new List<List<string>>();
            string sqlcmd = "SELECT auftrag.Auftragsnummer, Von, Betreff, B.StatusID, auftrag.Zeitstempel, Inhalt From auftrag Join (SELECT auftragshistorie.Auftragsnummer, StatusID, Bearbeiter From auftragshistorie JOIN (SELECT Auftragsnummer, MAX(Aktionsnummer) As AkNr From auftragshistorie GROUP BY Auftragsnummer) AS A ON A.AkNr = auftragshistorie.Aktionsnummer) AS B ON B.Auftragsnummer = auftrag.Auftragsnummer WHERE (auftrag.An = '"+Benutzer+"' AND B.StatusID < 2) OR (B.Bearbeiter = '"+Benutzer+"' AND B.StatusID = '1') ORDER BY B.StatusID";
            dataList = daten.getData(sqlcmd);
            return dataList;
        }
        public List<List<string>> getUserOrders(string Benutzer)
        {
            //gibt die vom User in Auftrag gegebenen Tasks zurück
            List<List<string>> dataList = new List<List<string>>();
            string sqlcmd = "SELECT auftrag.Auftragsnummer, Aktionsnummer, An, Betreff, B.StatusID, auftrag.Zeitstempel, Inhalt From auftrag Join (SELECT auftragshistorie.Auftragsnummer, StatusID, Aktionsnummer From auftragshistorie JOIN (SELECT Auftragsnummer, MAX(Aktionsnummer) As AkNr From auftragshistorie GROUP BY Auftragsnummer) AS A ON A.AkNr = auftragshistorie.Aktionsnummer) AS B ON B.Auftragsnummer = auftrag.Auftragsnummer WHERE auftrag.Von = '" + Benutzer + "' ORDER BY B.StatusID";
            dataList = daten.getData(sqlcmd);
            return dataList;
        }
        public List<List<string>> getOrderPool (bool laden, bool verwaltung, bool werkstatt, bool admin)
        {
            //gibt die noch freien Aufträge zurück, die für einen Bearbeiter mit der freigebenen Sicht verfügbar sind
            List<List<string>> dataList = new List<List<string>>();
            string sqlcmd = "SELECT auftrag.Auftragsnummer, Von, Betreff, B.StatusID, auftrag.Zeitstempel, Inhalt From auftrag Join (SELECT auftragshistorie.Auftragsnummer, auftragshistorie.StatusID From auftragshistorie JOIN (SELECT Auftragsnummer, MAX(Aktionsnummer) As AkNr From auftragshistorie GROUP BY Auftragsnummer) AS A ON A.AkNr = auftragshistorie.Aktionsnummer) AS B ON B.Auftragsnummer = auftrag.Auftragsnummer WHERE B.StatusID = 0 AND (auftrag.An = ";
            if (laden && !(verwaltung || werkstatt || admin))
            {
                sqlcmd += "'Laden')";
            }
            else
            {
                sqlcmd += "'Laden' OR auftrag.AN = ";
            }
            if (verwaltung && !(werkstatt || admin))
            {
                sqlcmd += "'Verwaltung')";
            }
            else
            {
                sqlcmd += "'Verwaltung' OR ";
            }
            if (werkstatt && !admin)
            {
                sqlcmd += "'Werkstatt')";
            }
            else
            {
                sqlcmd += "'Werkstatt' OR auftrag.AN = ";
            }
            if (admin)
            {
                sqlcmd += "'admin')";
            }
            sqlcmd += " ORDER BY auftrag.Zeitstempel";
            dataList = daten.getData(sqlcmd);
            return dataList;
        }
        public void newTask(string Betreff, string Nachricht, string Benutzer, string Adressat)
        {
            //Erstellt einen neuen Auftrag und als erste Aktion dazu ein OFFEN
            string sqlcmd = "INSERT INTO auftrag (Von, AN, Betreff, Inhalt) VALUES ('" + Benutzer + "','" + Adressat + "','" + Betreff + "','" + Nachricht + "')";
            daten.setData(sqlcmd);
            List<List<string>> dataList = new List<List<string>>();
            sqlcmd = "SELECT Auftragsnummer FROM auftrag WHERE Von = '" + Benutzer + "' AND An = '" + Adressat + "' AND Betreff = '" + Betreff + "' AND Inhalt = '" + Nachricht + "'";
            dataList = daten.getData(sqlcmd);
            string auftragsnr = dataList[0][0].ToString();
            sqlcmd = "INSERT INTO auftragshistorie (Auftragsnummer, StatusID, Bearbeiter) VALUES ('" + auftragsnr + "', '0', '" + User.login + "')";
            daten.setData(sqlcmd);
        }
    }
}
