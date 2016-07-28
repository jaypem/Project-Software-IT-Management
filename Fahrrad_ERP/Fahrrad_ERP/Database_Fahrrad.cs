using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Sql;
using System.Collections;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Fahrrad_ERP
{
    public class Database_Fahrrad
    {
        //Verbindungszeichenfolge für den Webserver
        string con_string = "SERVER=5.230.134.175; " +
                            "DATABASE=fahrrad_niko; " +
                            "UID=sit_niko; " +
                            "Password = E4ySZA3jCejSnA23";


        public void setData(string command)
        {
            //Der übergebene command wird in die Datenbank geschrieben (es wird keine Rückgabe aus der DB erfolgen)
            MySqlConnection mycon = new MySqlConnection(con_string);
            MySqlCommand comm = mycon.CreateCommand();
            comm.CommandText = command;
            try
            {
                mycon.Open();
                comm.ExecuteNonQuery();
                mycon.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Die Daten konnten nicht in die Datenbank geschrieben werden. Beachten Sie die Fehlermeldung: \n\n" + e.ToString(), "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        public List<List<string>> getData(string command)
        {
            //Der übergebene command wird Daten aus der DB zurückgeben, welche dann als List<List<string>> zurückgegeben werden!
            MySqlConnection mycon = new MySqlConnection(con_string);
            MySqlCommand comm = mycon.CreateCommand();
            comm.CommandText = command;
            MySqlDataReader Reader;
            List<List<string>> back = new List<List<string>>();
            List<string> str = new List<string>();
            try
            {
                mycon.Open();
                Reader = comm.ExecuteReader();
                while (Reader.Read())
                {
                    for (int j = 0; j < Reader.FieldCount; j++)
                        str.Add(Reader.GetValue(j).ToString());
                    back.Add(str.ToList());
                    str.Clear();
                }
                mycon.Close();
                return back;
            }
            catch (Exception e)
            {
                MessageBox.Show("Bei der Datenbank Abfrage ist ein Fehler aufgetreten. Beachten Sie die Fehlermeldung: \n\n" + e.ToString(), "Warnung", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                back.Add(str);
                return back;
            }
        }
    }
}
