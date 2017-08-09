using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;
using MySql.Data.MySqlClient;
namespace CentralServerApp
    {
    /// <summary>
    /// Summary description for EncryptData
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)] 
    [System.ComponentModel.ToolboxItem(false)] 
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class EncryptData : System.Web.Services.WebService
    {
       MySqlConnection conn1 = new MySqlConnection("server=localhost;database=cloud;userid=root;password=admin;");
       MySqlConnection conn2 = new MySqlConnection("server=localhost;database=cloud1;userid=root;password=admin;");
        
           string f1,f2,g1,g2=string.Empty;
        string res = string.Empty;
        String data1, data2;
        string pwd1, pwd2, pass;
        string mob1, mob2;
        string mob3 = string.Empty;
        string []aa = new string[5];
        string [] bb = new string[5];  

        
        [WebMethod]

        public string InsertData(string name, string add, string gender, string email, string contact, string uname, string pwd, string key)
        {

            int a = 3;
            string name1 = Algorithm.Encrypt(name.Substring(0, a), true, key);
            string name2 = Algorithm.Encrypt(Algorithm.GetLast(name, name.Length - a), true, key);

            int b = 3;
            string add1 = Algorithm.Encrypt(add.Substring(0, b), true, key);
            string add2 = Algorithm.Encrypt(Algorithm.GetLast(add, add.Length - b), true, key);

            int c = 3;
            string gender1 = Algorithm.Encrypt(gender.Substring(0, c), true, key);
            string gender2 = Algorithm.Encrypt(Algorithm.GetLast(gender, gender.Length - c), true, key);

            int d = 3;
            string email1 = Algorithm.Encrypt(email.Substring(0, d), true, key);
            string email2 = Algorithm.Encrypt(Algorithm.GetLast(email, email.Length - d), true, key);

            int e = 3;
            string contact1 = Algorithm.Encrypt(contact.Substring(0, e), true, key);
            string contact2 = Algorithm.Encrypt(Algorithm.GetLast(contact, contact.Length - e), true, key);

            int f = 3;
            string uname1 = uname.Substring(0, f);
            string uname2 = Algorithm.GetLast(uname, uname.Length - f);

            int g = 3;
            string pwd1 = Algorithm.Encrypt(pwd.Substring(0, g), true, key);
            string pwd2 = Algorithm.Encrypt(Algorithm.GetLast(pwd, pwd.Length - g), true, key);

            try
            {

                conn1.Open();
                MySqlCommand cmd = new MySqlCommand("insert into Register values('" + name1 + "','" + add1 + "','" + gender1 + "','" + email1 + "','" + contact1 + "','" + uname1 + "','" + pwd1 + "')", conn1);
                cmd.ExecuteNonQuery();
                conn1.Close();


                conn2.Open();
                MySqlCommand cmd1 = new MySqlCommand("insert into Register values('" + name2 + "','" + add2 + "','" + gender2 + "','" + email2 + "','" + contact2 + "','" + uname2 + "','" + pwd2 + "')", conn2);
                cmd1.ExecuteNonQuery();

                conn2.Close();


                return "success";

            }
            catch (Exception ex)
            {
                return "error";

            }

        }

        [WebMethod]

        public string getData(string uname,string pwd,string key)
        {
            string[] a = new string[10];
            string[] b = new string[10];

            string[] a1 = new string[10];
            string[] b1 = new string[10];
           MySqlConnection conn1 = new MySqlConnection("server=localhost;database=cloud;userid=root;password=admin;");
           MySqlConnection conn2 = new MySqlConnection("server=localhost;database=cloud1;userid=root;password=admin;");
        
            conn1.Open();
            conn2.Open();

            MySqlCommand cmd2 = new MySqlCommand("select * from Register",conn1);

            MySqlDataReader dr1 = cmd2.ExecuteReader();


            MySqlCommand cmd3 = new MySqlCommand("select * from Register", conn2);
            MySqlDataReader dr2 = cmd3.ExecuteReader();

            int kk = 0;

            if (dr1.HasRows)
            {
                int i = 0;
                while (dr1.Read())
                {
                    a[i] = dr1.GetString(5).ToString();
                    b[i] = dr1.GetString(6).ToString();
                    i++;
                    kk++;
                   
                }
            }


            if (dr2.HasRows)
            {
                int j = 0;
                while (dr2.Read())
                {
              
                    a1[j] = dr2.GetString(5).ToString();
                    b1[j] = dr2.GetString(6).ToString();
                    j++;

                }
            }
           
            for (int k = 0; k < kk; k++)
            {


                string uname1 = a[k];
                string uname2 = a1[k];
                string usrname = uname1.ToString() + uname2.ToString();
                if (uname.Equals(usrname))
                {
                     pwd1 = Algorithm.Decrypt(b[k], true, key);
                     pwd2 = Algorithm.Decrypt(b1[k], true, key);
                  pass = pwd1.ToString() + pwd2.ToString();
                }

               
              

                if (uname.Equals(usrname) && pwd.Equals(pass))
                {
                    res = "yes"; break;
                }
                else
                {
                    res = "no";
                }
            }

            
            conn2.Close();
            conn2.Close();

            return res;
        }

        [WebMethod]
        public string SaveData(string uname, string data1, string data2,string path,string key)
        { 
            string result=string.Empty;
            string part = Algorithm.Encrypt(data1, true, key);

            int f = 3;
            string uname1 = uname;
            string uname2 = Algorithm.Encrypt(Algorithm.GetLast(uname, uname.Length - f), true, key);
           // string ppath = path;
            string a = Algorithm.Encrypt(data1, true, key);
            string b = Algorithm.Encrypt(data2, true, key);

            try{
        conn1.Open();
            conn2.Open();
            MySqlCommand objcmd1 = new MySqlCommand("insert into data values('" + uname1 + "','" + a + "','" + path +"')", conn1);
            objcmd1.ExecuteNonQuery();

            MySqlCommand objcmd2 = new MySqlCommand("insert into data values('" + uname1 + "','" + b + "','" + path +"')", conn2);
                objcmd2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            result="no";
            }
            result="yes";
            conn1.Close();
            conn2.Close();
            return result;
            }


        [WebMethod]

        public List<String> getDataFiles(string uname)
        {
            conn1.Open();
            List<string> ls = new List<string>();
            MySqlCommand cmd = new MySqlCommand("select DISTINCT filename from data where user='" + uname +"'", conn1);
            MySqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                while (dr.Read())
                { 
                    //string key="cloud";
                   // string path = Algorithm.Decrypt(dr.GetString(0),true,key);


                    ls.Add(dr.GetString(0));
                
                }
            }
            conn1.Close();
            return ls;
        }


        [WebMethod]

        public string AllContent(string fname,string key)
        {
                        conn1.Open();
            MySqlCommand cmd = new MySqlCommand("select part from data where filename='" + fname +"'",conn1);
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    data1 = dr.GetString(0);
                }
            }
            conn1.Close();
            dr.Close();

            conn2.Open();
            MySqlCommand cmd1 = new MySqlCommand("select part from data where filename='" + fname + "'", conn2);
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.HasRows)
            {
                while (dr1.Read())
                {
                    data2 = dr1.GetString(0);
                }
            }
            conn2.Close();
            dr1.Close();
            string a = Algorithm.Decrypt(data1, true, key);
            string b = Algorithm.Decrypt(data2, true, key);

            return a + b;
        }

        [WebMethod]
        public List<String> AllUsers()
        {
            List<string> users = new List<string>();
            conn1.Open();
            MySqlCommand cmd = new MySqlCommand("select uname from Register");
            cmd.Connection = conn1;
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                int i = 0;
                while (dr.Read())
                {
                    aa[i]=dr.GetString(0);
                    i++;
                }
            }
            conn1.Close();

            conn2.Open();
            MySqlCommand cmd1 = new MySqlCommand("select uname from Register");
            cmd1.Connection = conn2;
            MySqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.HasRows)
            {
                int j = 0;
                while (dr1.Read())
                {
                    bb[j] = dr1.GetString(0);
                    j++;
                }
            }
            for (int k = 0; k < 5; k++)
            {
                users.Add(aa[k] + bb[k]);
            }
                conn1.Close();
            return users;
        }

        [WebMethod]

        public string AccessControl(string fromuser, string filename, string tousers)
        {
            int flg = 0;
            try
            {
                string frmuser = fromuser;
                string fname = filename;
                string touser = tousers;
                conn1.Open();
                MySqlCommand cmd = new MySqlCommand("insert into access values('" + frmuser + "','" + fname + "','" + touser + "')");
                cmd.Connection = conn1;
                cmd.ExecuteNonQuery();
                conn1.Close();

                conn2.Open();
                MySqlCommand cmd2 = new MySqlCommand("insert into access values('" + frmuser + "','" + fname + "','" + touser + "')");
                cmd2.Connection = conn2;
                cmd2.ExecuteNonQuery();
                conn2.Close();
                flg = 1;

            }
            catch (Exception ex)
            {
                return "false";
            }
            if (flg == 1)
                return "true";
            else return "false";
        }

        [WebMethod]

        public List<string> GetAccessList(string uname)
        {
            List<string> lst = new List<string>();
            conn1.Open();
            MySqlCommand cmd = new MySqlCommand("select filename from access where touser='" + uname +"'");
            cmd.Connection = conn1;
            MySqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    lst.Add(dr.GetString(0));
                
                }
            }
            conn1.Close();
            return lst;
        }
        [WebMethod]

        public string GetContact(string uname)
        {
            int f = 3;
            string uname1 = uname.Substring(0, f);
            string uname2 = Algorithm.GetLast(uname, uname.Length - f);

            string[] a = new string[10];
            string[] b = new string[10];
           
            string[] a1 = new string[10];
            string[] b1 = new string[10];
                   MySqlConnection conn1 = new MySqlConnection("server=localhost;database=cloud;userid=root;password=admin;");
           MySqlConnection conn2 = new MySqlConnection("server=localhost;database=cloud1;userid=root;password=admin;");
        
            conn1.Open();
            conn2.Open();

            MySqlCommand cmd2 = new MySqlCommand("select * from Register where uname='" + uname1 +"'", conn1);
            MySqlDataReader dr1 = cmd2.ExecuteReader();


            MySqlCommand cmd3 = new MySqlCommand("select * from Register where uname='" + uname2 +"'", conn2);
            MySqlDataReader dr2 = cmd3.ExecuteReader();



            if (dr1.HasRows)
            {
               
                while (dr1.Read())
                {

                    mob1 = dr1.GetString(4).ToString();

                }
            }


            if (dr2.HasRows)
            {
               
                while (dr2.Read())
                {

                    mob2 = dr2.GetString(4).ToString();
                    
                }
            }


          //  mob3 = string.Concat(mob1,mob2);
                   // mob3 = (mob1.ToString() + mob2.ToString());
            mob3 = Convert.ToString(mob1) + Convert.ToString(mob2);   

            conn1.Close();
            conn2.Close();

            return mob3;
        
        }

        [WebMethod]
        public string DeleteFile(string filename)
        {
                   MySqlConnection conn1 = new MySqlConnection("server=localhost;database=cloud;userid=root;password=admin;");
           MySqlConnection conn2 = new MySqlConnection("server=localhost;database=cloud1;userid=root;password=admin;");
        try
            {
                ////////////// Delete file from data table
                conn1.Open();
                MySqlCommand cmd = new MySqlCommand("delete from data where filename='" + filename +"'", conn1);
                cmd.ExecuteNonQuery();
                conn1.Close();


                conn2.Open();
                MySqlCommand cmd1 = new MySqlCommand("delete from data where filename='" + filename + "'", conn2);
                cmd1.ExecuteNonQuery();

                conn2.Close();
                ///////////////////////// Delete file from access control

                conn1.Open();
                MySqlCommand cmd2 = new MySqlCommand("delete from access where filename='" + filename + "'", conn1);
                var no = cmd2.ExecuteScalar();
                conn1.Close();


                conn2.Open();
                MySqlCommand cmd3 = new MySqlCommand("delete from data where filename='" + filename + "'", conn2);
                var no1 = cmd3.ExecuteScalar();

                conn2.Close();

                //////////////////////////

                return "success";

            }
            catch (Exception ex)
            {
                return "error";

            }
        
        
        }

        [WebMethod]

        public string UpdateData(string uname, string data1, string data2, string path, string key)
        {

            string result = string.Empty;
            string part = Algorithm.Encrypt(data1, true, key);

            int f = 3;
            string uname1 = uname;
            string uname2 = Algorithm.Encrypt(Algorithm.GetLast(uname, uname.Length - f), true, key);
            string ppath = path;
            string a = Algorithm.Encrypt(data1, true, key);
            string b = Algorithm.Encrypt(data2, true, key);

            try
            {
                conn1.Open();
                conn2.Open();
                MySqlCommand objcmd1 = new MySqlCommand("update data set part='" + a +"' where filename='" + path +"' and user='" + uname +"'", conn1);
                objcmd1.ExecuteNonQuery();

                MySqlCommand objcmd2 = new MySqlCommand("update data set part='" + b + "' where filename='" + path + "' and user='" + uname + "'", conn2);
                objcmd2.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                result = "no";
            }
            result = "yes";
            conn1.Close();
            conn2.Close();
            return result;
        }
    }
}
