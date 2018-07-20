using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VisitorManagement.Connection
{
    public class Connections
    {
        public DataTable s_dt;
        public DataSet sql_d_set;
        public SqlCommand sqlcmd;
        public SqlDataReader sqlrd;
        public SqlDataAdapter sqladptr;
        public SqlConnection sqlCon = new SqlConnection();
        private string serverName, dbName, authUsr, authPwd;
        private static Connections connection = new Connections();

        private Connections() { }
        public static Connections getInstance() {
            return connection;
        }

        public void setConnection(string serverName, string dbName, string authUsr, string authPwd)
        { 
            this.dbName = dbName;
            this.authUsr = authUsr;
            this.authPwd = authPwd;
            this.serverName = serverName;
        }

        // Fungsi untuk membuka koneksi sql
        public void opensql()
        {
            string sqlString;
            sqlString = "Server=" + serverName + ";Database=" + dbName +
                        ";User=" + authUsr + ";Password=" + authPwd;
            sqlCon.ConnectionString = String.Format(sqlString);

            try
            {
                if (sqlCon.State == ConnectionState.Closed) sqlCon.Open();
            }

            catch (Exception ex)
            {
                MessageBox.Show("Connection must be correct. Please check your connection string. Error = " + ex.ToString(),
                                "Connection Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // fungsi untuk melakukan insert dan update
        public void insertSQL(string query)  // C
        {
            try
            {
                opensql();
                sqlcmd = new SqlCommand(query, sqlCon);
                sqlcmd.ExecuteNonQuery();
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
            finally { sqlCon.Close(); }
        }

        // fungsi untuk menampilkan data ke datagridview
        public void displayDB(string query, string tableView)  //R
        {
            try
            {
                opensql();
                sqladptr = new SqlDataAdapter(query, sqlCon);
                sql_d_set = new DataSet();
                sqladptr.Fill(sql_d_set, tableView);
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
            finally { sqlCon.Close(); }
        }

        // fungsi untuk melakukan filter pada database
        public void filterDB(string query)
        {
            try
            {
                opensql();
                sqladptr = new SqlDataAdapter(query, sqlCon);
                sql_d_set.Clear();
                sqladptr.Fill(sql_d_set, "Filter");
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
            finally { sqlCon.Close(); }
        }

        // fungsi untuk get data ke database
        public string getDataFromDB(string query, string GetColumnDB)
        {
            string tempStr = "";
            try
            {
                opensql();
                sqlcmd = new SqlCommand(query, sqlCon);
                sqlrd = sqlcmd.ExecuteReader();
                sqlrd.Read();
                if (sqlrd.HasRows)
                {
                    tempStr = sqlrd[GetColumnDB].ToString();
                    sqlrd.Close();
                }
            }
            catch (Exception e) { MessageBox.Show(e.Message); }
            finally { sqlCon.Close(); }

            return tempStr;
        }

    }
}
