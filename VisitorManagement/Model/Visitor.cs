using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisitorManagement.Connection;

namespace VisitorManagement.Model
{
    public class Visitor
    {
        Connections connections;
        private string visitorID, name, gender, address, phone, photo;

        public Visitor()
        {
            connections = Connections.getInstance();
        }

        public Visitor(string name, string gender, string address, string phone)
        {
            this.name = name;
            this.phone = phone;
            this.gender = gender;
            this.address = address;
            connections = Connections.getInstance();
        }

        public Visitor(string visitorID, string name, string gender, string address, string phone)
        {
            this.phone = phone;
            this.name = name;
            this.gender = gender;
            this.address = address;
            this.visitorID = visitorID;
            connections = Connections.getInstance();
        }

        public string VisitorID { get => visitorID; set => visitorID = value; }
        public string Name { get => name; set => name = value; }
        public string Gender { get => gender; set => gender = value; }
        public string Address { get => address; set => address = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Photo { get => photo; set => photo = value; }

        public void insert()
        {
            string query = "insert into TVisitor (VisitorID, Name, Gender, Address, Phone) values " +
                        "('" + visitorID + "','" + name + "','" + gender + "'" +
                        ",'" + address + "','" + phone + "')";
            try
            {
                connections.insertSQL(query);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
             
        }

        public void update()
        {
            string query = "update TVisitor set Name = '" + name + "'," +
                " Gender = '" + gender + "'," +
                " Address = '" + address + "'," +
                " Phone = '" + phone + "' " +
                "where visitorID = '" + visitorID + "'";
            try
            {
                connections.insertSQL(query);
            } catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void getData(string query)
        {
            try
            {
                connections.opensql();

                connections.sqlcmd = new SqlCommand(query, connections.sqlCon);
                connections.sqlcmd.CommandType = CommandType.Text;
                connections.sqlrd = connections.sqlcmd.ExecuteReader();
                while (connections.sqlrd.Read())
                {
                    name = connections.sqlrd["Name"].ToString();
                    gender = connections.sqlrd["Gender"].ToString();
                    address = connections.sqlrd["Address"].ToString();
                    phone = connections.sqlrd["Phone"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connections.sqlrd.Close();
                connections.sqlCon.Close();
            }
        }
    }
}
