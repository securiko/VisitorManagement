using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisitorManagement.Connection;
using VisitorManagement.TabView;

namespace VisitorManagement.Winform
{
    public partial class DepartmentList : Form
    {
        Connections connections;
        Register register;
        public DepartmentList(Register register)
        {
            InitializeComponent();
            this.register = register;
        }

        private void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
        }
        private void displayDGV()
        {
            string query = "select * from TDepartment order by DeptName";
            connections.displayDB(query, "DepartmentID");
            dataGridView1.DataSource = connections.sql_d_set;
            dataGridView1.DataMember = "DepartmentID";
        }

        private void DepartmentList_Load(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            connections = Connections.getInstance();
            displayDGV();
            this.CenterToScreen();
        }

        private void expandBtn_Click(object sender, EventArgs e)
        {
            if (groupBox1.Visible == true)
            {
                groupBox1.Visible = false;
                this.Width = 416;
                expandBtn.Location = new Point(369, 12);
            }
            else
            {
                groupBox1.Visible = true;
                this.Width = 740;
                expandBtn.Location = new Point(689, 12);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure want to save this?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (textBox1.Text == "" || textBox2.Text == "")
                {
                    MessageBox.Show("Please fill all the data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else {
                    string query = "insert into TDepartment (DepartmentID, DeptName) values ('" + textBox1.Text + "','" + textBox2.Text + "')";
                    connections.insertSQL(query);
                    MessageBox.Show("Department added", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    displayDGV();
                    clear();
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            register.departID = dataGridView1.CurrentRow.Cells[0].Value + string.Empty;
            register.textBox3.Text = dataGridView1.CurrentRow.Cells[1].Value + string.Empty;
            this.Close();
        }
    }
}
