using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace CulturePathData
{
    public class User
    {

        private static bool CheckUser(string emailadress, SqlConnection cnx)
        {
            if (cnx.State == ConnectionState.Closed)
            {
                cnx.Open();
            }
            SqlCommand cmd = new SqlCommand("SELECT * FROM Users", cnx);
            using (SqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    if ((string)rdr["EmailAdress"] == emailadress)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static bool AddUser(string emailadress, string username, string password, string firstname, string lastname, int age)
        {
            /*SqlConnectionStringBuilder b = new SqlConnectionStringBuilder();
            b["Data Source"] = "(local)";
            b["DataBase"] = @"C:\Users\Bijan\Desktop\CulturePath\CulturePathData\CulturePathData\UsersData\Users.mdf";
            b["integrated Security"] = true;
            b["Trusted_Connection"] = true;*/
            using (SqlConnection sqlCnx = new SqlConnection(ConfigurationManager.ConnectionStrings["UbCnx"].ConnectionString))
            {
                /*if (!CheckUser(emailadress, sqlCnx))
                {
                    return false;
                }*/
                SqlCommand cmd = new SqlCommand("dbo.AddUser", sqlCnx) { CommandType = CommandType.StoredProcedure };
                cmd.Parameters.Add(new SqlParameter("@pEmailAdress", emailadress));
                cmd.Parameters.Add(new SqlParameter("@pUserName", username));
                cmd.Parameters.Add(new SqlParameter("@pPassword", password));
                cmd.Parameters.Add(new SqlParameter("@pFirstName", firstname));
                cmd.Parameters.Add(new SqlParameter("@pLastName", lastname));
                cmd.Parameters.Add(new SqlParameter("@pAge", age));
                if (sqlCnx.State == ConnectionState.Closed)
                {
                    sqlCnx.Open();
                }
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch
                {
                    Console.WriteLine("Hi");
                    return false;
                }
                return true;
            }
        }
    }
}
