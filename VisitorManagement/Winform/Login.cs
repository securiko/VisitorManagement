using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisitorManagement.Connection;

namespace VisitorManagement.Winform
{
    public partial class Login : Form
    {
        Main main;
        Connections connection;
        public Login()
        {
            InitializeComponent();
        }

        private void menuConn_Click(object sender, EventArgs e)
        {
            if (panelConn.Width == 50)
            {
                panelConn.Width = 400;
                panelConn.BorderStyle = BorderStyle.Fixed3D;
            }
            else {
                panelConn.Width = 50;
                panelConn.BorderStyle = BorderStyle.None;
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to save this Connection?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // melakukan binding string pada textbox

                connection.setConnection(textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text);
                Properties.Settings.Default.sqlName = textBox3.Text;
                Properties.Settings.Default.dbName = textBox4.Text;
                Properties.Settings.Default.userName = textBox5.Text;
                Properties.Settings.Default.password = textBox6.Text;
                Properties.Settings.Default.Save();

                MessageBoxButtons button = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show("Save Successfully", "Information", button, MessageBoxIcon.Information);
            }
        }

        private void Login_Load(object sender, EventArgs e)
        {
            // melakukan pengecekkan apakan identitas untuk koneksi ke database telah terisi
            // jika telah terisi melakukan set koneksi ke class Connections

            // Get instance koneksi
            connection = Connections.getInstance();
            if (Properties.Settings.Default.sqlName != "" || Properties.Settings.Default.dbName != "" ||
                Properties.Settings.Default.userName != "" || Properties.Settings.Default.password != "")
            {
                connection.setConnection(textBox3.Text, textBox4.Text, textBox5.Text, textBox6.Text);
            }
            panelConn.Width = 50;
            main = new Main();
            this.CenterToScreen();
        }

        private void connBtn_Click(object sender, EventArgs e)
        {
            
            // Buka koneksi sql
            connection.opensql();

            // cek jika telah terbuka
            if (connection.sqlCon.State == ConnectionState.Open)
            {
                MessageBox.Show("Connected to Access Control DB", 
                                        "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                connection.sqlCon.Close();
            }

            // cek status return dari hasil pengecekkan table TVisitor
            if (!checkTable())
            {
                if (MessageBox.Show("The database for Visitor doesn't exists, Would you like to create the database for Visitor? This database for VMS data",
                    "Database Visitor Not Found", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    createTable();
                }
            }
        }

        private bool checkTable()
        {
            Int32 status = 0;
            string query;
            // Melakukan pengecekkan apakah table untuk aplikasi VMS telah terbuat pada database AXData
            // Jika belum terbuat makan akan terdapat pilihan untuk membuat melalui aplikasi ini
            try
            {
                // get status table TVisitor apa sudah terbuat atau belum
                query = "select count(*) as IsExists From dbo.sysobjects where id = object_id('[dbo].[TVisitor]')";
                connection.opensql();
                connection.sqlcmd = new SqlCommand(query, connection.sqlCon);

                // return 0 tidak ada table TVisitor
                // return 1 table TVisitor telah terbuat
                status = (Int32)connection.sqlcmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { connection.sqlCon.Close(); }

            if (status == 0) return false;
            else return true;
        }

        // fungsi digunakan untuk melakukan pengecekkan lowercase / uppercase pada penulisan password
        private string checkAscii(string word)
        {
            List<Int32> ascii = new List<Int32>();
            foreach (char c in word)
            {
                ascii.Add(System.Convert.ToInt32(c));
            }
            word = string.Join("", ascii);
            return word;
        }

        // Fungsi membuat table untuk kebutuhan aplikasi VMS
        private void createTable()
        {
            connection.opensql();

            try
            {
                connection.sqlcmd = new SqlCommand(QueryTable.visitor, connection.sqlCon);
                connection.sqlcmd.ExecuteNonQuery();

                connection.sqlcmd = new SqlCommand(QueryTable.visiting, connection.sqlCon);
                connection.sqlcmd.ExecuteNonQuery();

                connection.sqlcmd = new SqlCommand(QueryTable.createViewVisiting, connection.sqlCon);
                connection.sqlcmd.ExecuteNonQuery();

                MessageBox.Show("Create table successfully", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception e) {
                MessageBox.Show(e.Message);
            }
            connection.sqlCon.Close();
        }

        private void getLocalName_Click(object sender, EventArgs e)
        {
            textBox3.Text = System.Net.Dns.GetHostName() + "\\";
        }

        private void closeBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void loginBtn_Click(object sender, EventArgs e)
        {
            string user, pwd, query, msg;

            // get data user & admin pada table TUser untuk login
            query = "select UserCode, UserPassWord from TUser where UserCode = '" + textBox1.Text + "'";
            user = connection.getDataFromDB(query, "UserCode");
            pwd = connection.getDataFromDB(query, "UserPassWord");

            // Melakukan pengecekkan apakah table TVisitor telah terbuat sewaktu login
            if (checkTable())
            {
                if (textBox1.Text == "" && textBox2.Text == "")
                {
                    MessageBoxButtons button = MessageBoxButtons.OK;
                    DialogResult result = MessageBox.Show("Incorrect Pasword or Username", "Attention", button, MessageBoxIcon.Exclamation);
                }
                else
                {
                    // pengecekkan ascii pada string admin dan password
                    if (checkAscii(user) == checkAscii(textBox1.Text) && checkAscii(pwd) == checkAscii(textBox2.Text))
                    {
                        this.Hide();
                        main.Show();
                        textBox1.Text = "";
                        textBox2.Text = "";
                    }
                    else
                    {
                        MessageBoxButtons button = MessageBoxButtons.OK;
                        DialogResult result = MessageBox.Show("Incorrect Pasword or Username", "Attention", button, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                msg = "Database for visitor not found. Go to [Configure DB -> Check Connection -> Create Database for Visitor]";
                MessageBoxButtons button = MessageBoxButtons.OK;
                DialogResult result = MessageBox.Show(msg, "Information", button, MessageBoxIcon.Exclamation);
            }
        }
    }
}
