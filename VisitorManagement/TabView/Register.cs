using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisitorManagement.Connection;
using VisitorManagement.Winform;
using System.Data.SqlClient;
using VisitorManagement.Model;

namespace VisitorManagement.TabView
{
    public partial class Register : UserControl
    {
        Connections connections;
        public string departID;
        Room room;
        DepartmentList departmentList;
        public Register()
        {
            InitializeComponent();
        }

        private void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            gender.SelectedIndex = 0;
            UncheckedAllRoom();
        }

        private void fill_tree()
        {
            string query = "select DoorID, DoorName from TDoor";
            connections.displayDB(query, "TDoor");
            treeView1.Nodes.Clear();
            treeView1.CheckBoxes = true;
            try
            {
                foreach (DataRow dr in connections.sql_d_set.Tables[0].Rows)
                {
                    TreeNode tnParent = new TreeNode();
                    tnParent.Text = dr["DoorName"].ToString();
                    tnParent.Expand();
                    treeView1.Nodes.Add(tnParent);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        private void UncheckedAllRoom()
        {
            foreach (TreeNode unChked in treeView1.Nodes)
            {
                if (unChked.Checked)
                {
                    unChked.Checked = false;
                }
            }
        }

        private void displayDGV()
        {
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            string query = "select * from Vvisiting where DateIn = '"+today+"'";
            connections.displayDB(query, "VisitID");
            dataGridView1.DataSource = connections.sql_d_set;
            dataGridView1.DataMember = "VisitID";
        }

        private void Register_Load(object sender, EventArgs e)
        {
            connections = Connections.getInstance();
            gender.SelectedIndex = 0;
            fill_tree();
            displayDGV();
        }

        private void departBtn_Click(object sender, EventArgs e)
        {
            departmentList = new DepartmentList(this);
            departmentList.Show();
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            insertVisitor();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            string query = "select * from TVisitor where VisitorID = '" + textBox1.Text + "'";
            string visitorID = connections.getDataFromDB(query, "VisitorID");
            if (visitorID == "")
            {
                MessageBox.Show("Could not find Visitor ID", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else {
                Visitor visitor = new Visitor();
                visitor.getData(query);
                textBox2.Text = visitor.Name;
                gender.SelectedIndex = gender.FindString(visitor.Gender);
                textBox4.Text = visitor.Address;
                textBox5.Text = visitor.Phone;
            }
        }

        private void insertVisitor()
        {
            if (MessageBox.Show("Are you sure want to save this?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string query = "select VisitorID from TVisitor where VisitorID = '" + textBox1.Text + "'";
                string tempVisitor = connections.getDataFromDB(query, "VisitorID");
                string tempDoorID = "";
                string tempDoorName = "";
                string cardNo = "";
                string dateIn = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                string dateOut = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                int count = 0;
                if (!tempVisitor.Equals(textBox1.Text))
                {
                    Visitor visitor = new Visitor(textBox1.Text, textBox2.Text, gender.GetItemText(gender.SelectedItem), textBox4.Text, textBox5.Text);
                    visitor.insert();
                }

                foreach (TreeNode trChked in treeView1.Nodes)
                {
                    if (trChked.Checked)
                    {
                        query = "select DoorID from TDoor where DoorName = '" + trChked.Text + "'";
                        tempDoorID = connections.getDataFromDB(query, "DoorID");
                        tempDoorName = trChked.Text;
                        count++;
                    }
                }

                room = new Room();
                cardNo = room.getCardNo(tempDoorName);
                if (count == 0) MessageBox.Show("Please select room", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                else
                {
                    Visit visit = new Visit(textBox1.Text, departID, tempDoorID, cardNo, dateIn, DateTime.Now.ToString("HH:mm"), "Yes", dateOut);
                    visit.insert();
                    clear();
                    MessageBox.Show("Visitor added", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    displayDGV();
                }
            }
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            clear();
        }
    }
}
