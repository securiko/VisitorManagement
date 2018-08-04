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
    public partial class CompanyList : Form
    {
        bool editStatus = false;
        Connections connections;
        Register register;
        public CompanyList(Register register)
        {
            InitializeComponent();
            this.register = register;
        }

        private void clear()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
        }
        private void displayDGV()
        {
            string query = "select CompanyID as 'Company ID', CompName as 'Company Name', Phone from TCompany order by CompName";
            connections.displayDB(query, "CompanyID");
            dataGridView1.DataSource = connections.sql_d_set;
            dataGridView1.DataMember = "CompanyID";
        }

        private void CompanyList_Load(object sender, EventArgs e)
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

        private void addBtn_Click(object sender, EventArgs e)
        {
            if(textBox1.Enabled == false || textBox2.Enabled == false || textBox3.Enabled == false)
            {
                clear();
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                dataGridView1.Enabled = false;
                addBtn.Text = "Save";
            }
            else
            {
                if (addBtn.Text.Equals("Add"))
                {
                    addBtn.Text = "Save";
                    textBox2.Enabled = true;
                    textBox3.Enabled = true;
                    dataGridView1.Enabled = true;
                }
                else
                {
                    if (MessageBox.Show("Are you sure want to save this?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (textBox1.Text == "" || textBox2.Text == "")
                        {
                            MessageBox.Show("Please fill all the data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            string query = "insert into TCompany (CompanyID, CompName, Phone) values ('" + textBox1.Text + "','" + textBox2.Text + "', '" + textBox3.Text + "')";
                            connections.insertSQL(query);
                            MessageBox.Show("Company added", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            displayDGV();
                            clear();
                            addBtn.Text = "Add";
                            textBox1.Enabled = false;
                            textBox2.Enabled = false;
                            textBox3.Enabled = false;
                            dataGridView1.Enabled = true;
                        }
                    }
                }
            }
        }

        private void editBtn_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("Click company list first", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                if (editBtn.Text.Equals("Edit"))
                {
                    editBtn.Text = "Save";
                    textBox2.Enabled = true;
                    textBox3.Enabled = true;
                    dataGridView1.Enabled = true;
                }
                else
                {
                    if (MessageBox.Show("Are you sure want to save this?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        if (textBox2.Text == "")
                        {
                            MessageBox.Show("Please fill all the data", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            string query = "update TCompany set CompName = '" + textBox2.Text + "'," +
                                " Phone = '" + textBox3.Text + "' where CompanyID = '" + textBox1.Text + "'";
                            connections.insertSQL(query);
                            MessageBox.Show("Company added", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            displayDGV();
                            clear();
                            editBtn.Text = "Edit";
                            textBox2.Enabled = false;
                            textBox3.Enabled = false;
                            dataGridView1.Enabled = true;
                        }
                    }
                }
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            register.companyID = dataGridView1.CurrentRow.Cells[0].Value + string.Empty;
            register.textBox3.Text = dataGridView1.CurrentRow.Cells[1].Value + string.Empty;
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            clear();
            textBox1.Enabled = false;
            textBox2.Enabled = false;
            textBox3.Enabled = false;
            dataGridView1.Enabled = true;
            editBtn.Text = "Edit";
            addBtn.Text = "Add";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value + string.Empty;
                textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value + string.Empty;
                textBox3.Text = dataGridView1.CurrentRow.Cells[2].Value + string.Empty;
            }
        }
    }
}
