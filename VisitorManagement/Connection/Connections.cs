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

        /**
        * Constructor singleton hanya dapat di instance 1 kali 
        */
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

        // method untuk membuka koneksi sql
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

        /* 
        *   method untuk melakukan insert dan update
        *   @param[query] parameter string yang dapat di isi dengan query insert atau update
        */
        public void insertSQL(string query) 
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

        /*
        *   method ini digunakan untuk menampilkan query database ke datagridview
        *   @param[query] parameter string yang dapat di isi dengan query select
        *   @param[tableView] parameter string yang dapat di isi dengan kolom table database yang akan di mapping
        */
        public void displayDB(string query, string tableView)  
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

        // method untuk melakukan filter pada database
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

        /*
         *  function yang digunakan untuk menarik sebuah data tertentu pada database
         *  @param[query] parameter string yang dapat di isi dengan query select
         *  @param[GetColumnDB] parameter string yang di isi dengan spesifik tabel yang akan ditarik datanya
         *  @return method ini akan mengembalikan nilai dari sebuah kolom tertentu
         */
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
