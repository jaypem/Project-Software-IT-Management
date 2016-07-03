using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace Fahrrad_ERP
{
    public class User
    {
        public static string login;
        public static string Nachname;
        public static string Name;
        public static string Abteilung;
        public static bool admin;
        public static bool ansichtL;
        public static bool ansichtV;
        public static bool ansichtW;

        public void setUserInformation(string login)
        {
            List<List<string>> strList = new List<List<string>>();
            string sqlcmd = "SELECT `login`, `Nachname`, `Name`, `abteilung`, `admin`, `ansichtL`, `ansichtV`, `ansichtW` FROM `personal` WHERE `login` LIKE '" + login + "'";
            Database_Fahrrad daten = new Database_Fahrrad();
            strList = daten.getData(sqlcmd);
            List<string> str = strList[0];
            User.login = str[0].ToString();
            User.Nachname = str[1].ToString();
            User.Name = str[2].ToString();
            User.Abteilung = str[3].ToString();
            if (str[4] == "1") User.admin = true;
            if (str[5] == "1") User.ansichtL = true;
            if (str[6] == "1") User.ansichtV = true;
            if (str[7] == "1") User.ansichtW = true;
            }
        }
    }
