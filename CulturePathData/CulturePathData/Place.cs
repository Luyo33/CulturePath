using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace CulturePathData
{
    public class Place
    {
        private static bool AddPlace(string s,SqlConnection cnx)
        {
            try
            {
                string[] s_l = s.Split(',');
                SqlCommand cmd = new SqlCommand(String.Format("INSERT INTO Places (Name,Type,AverageTime,Latitude,Longitude) VALUES ('{0}','{1}',{2},{3},{4})", s_l[0], s_l[1], s_l[2], s_l[3], s_l[4]), cnx);
                if (cnx.State == ConnectionState.Closed)
                    cnx.Open();
                cmd.ExecuteNonQuery();
                string command = String.Format("INSERT INTO Schedules (Name,MS1,ME1,MS2,ME2,TuS1,TuE1,TuS2,TuE2,WS1,WE1,WS2,WE2,ThS1,ThE1,ThS2,ThE2,FS1,FE1,FS2,FE2,SaS1,SaE1,SaS2,SaE2,SuS1,SuE1,SuS2,SuE2) VALUES (('{0}'),", s_l[0]);
                int i = 0;
                foreach (string str in s_l[5].Split(';'))
                {
                    i++;
                    command += String.Format("{0}{1}", str == "" ? "NULL" : String.Format("'{0}'",str),i == 28 ? "" : ",");
                }
                command += ")";
                cmd = new SqlCommand(command, cnx);
                if (cnx.State == ConnectionState.Closed)
                    cnx.Open();
                cmd.ExecuteNonQuery();
                string[] s1_l;
                foreach (string str in s_l[6].Split(';'))
                {
                    s1_l = str.Split(':');
                    if (s1_l.Length == 2)
                        command = String.Format("INSERT INTO Prices (Name,Age,Price) VALUES ('{0}',{1},{2})",s_l[0],s1_l[0], s1_l[1]);
                    else
                    {
                        command = String.Format("INSERT INTO Prices (Name,Age,Price) VALUES ('{0}',NULL,{1})",s_l[0],s1_l[0] == "" ? "0" : s1_l[0]);
                    }
                    cmd = new SqlCommand(command,cnx);
                    if (cnx.State == ConnectionState.Closed)
                        cnx.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            catch
            {
            return false;
            }
            return true;
        }
        public static bool AddPlaces(string path)
        {
            using (SqlConnection sqlCnx = new SqlConnection(ConfigurationManager.ConnectionStrings["PbCnx"].ConnectionString))
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    string[] s_l = sr.ReadToEnd().Split('\n');
                    bool b = true;
                    foreach (string s in s_l)
                    {
                        if (!(s == ""))
                        {
                            if (sqlCnx.State == ConnectionState.Closed)
                                sqlCnx.Open();
                            b = AddPlace(s, sqlCnx) && b;
                        }
                    }
                    return b;
                }
            }
        }
    }
}
