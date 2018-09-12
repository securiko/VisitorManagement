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
using VisitorManagement.Model;

namespace VisitorManagement.TabView
{
    public partial class Checkout : UserControl
    {
        string dateIn, timeIn, status;
        bool update = false;
        Connections connections;
        Visit visit;
        public Checkout()
        {
            InitializeComponent();
        }

        private void displayDGVToday()
        {
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            string query = QueryTable.viewVisiting + " where 'Date In' = '" + today + "'";
            connections.displayDB(query, "VisitID");
            dataGridView1.DataSource = connections.sql_d_set;
            dataGridView1.DataMember = "VisitID";
        }

        private void displayDGVFilter(string dateFrom, string dateTo)
        {
            string query = QueryTable.viewVisiting + " where DateIn between '"+dateFrom+"' and '"+dateTo+"'";
            connections.displayDB(query, "VisitID");
            dataGridView1.DataSource = connections.sql_d_set;
            dataGridView1.DataMember = "VisitID";
        }

        private void Checkout_Load(object sender, EventArgs e)
        {
            //connections = Connections.getInstance();
            //filterDate.SelectedIndex = 0;
            //displayDGVToday();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
            if (e.RowIndex >= 0)
            {
                textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value + string.Empty;
                textBox2.Text = dataGridView1.CurrentRow.Cells[2].Value + string.Empty;
                textBox3.Text = dataGridView1.CurrentRow.Cells[4].Value + string.Empty;
                textBox4.Text = dataGridView1.CurrentRow.Cells[3].Value + string.Empty;
                dateIn = dataGridView1.CurrentRow.Cells[5].Value + string.Empty;
                timeIn = dataGridView1.CurrentRow.Cells[6].Value + string.Empty;
                string[] split = dateIn.Split(' ');
                dateIn = split[0];
                status = dataGridView1.CurrentRow.Cells[10].Value + string.Empty;
                if (status == "Yes")
                {
                    cmbStatus.SelectedIndex = 1;
                    MessageBox.Show("This visitor has been checkout", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    cmbStatus.SelectedIndex = 0;
                    update = true;
                }
            }
        }

        private void queryBtn_Click(object sender, EventArgs e)
        {
            string dateFrom = "";
            string dateTo = "";
            if(filterDate.SelectedIndex == 1)
            {
                dateFrom = dateTimePicker1.Value.ToString("yyyy-MM-dd");
                dateTo = dateTimePicker2.Value.ToString("yyyy-MM-dd");
                displayDGVFilter(dateFrom, dateTo);
            }
            else
            {
                displayDGVToday();
            }
        }

        private void cmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStatus.SelectedIndex == 0) status = "No";
            else status = "Yes";
        }

        private void filterDate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(filterDate.SelectedIndex == 1)
            {
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;
            }
            else
            {
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;
            }
        }

        private void updateBtn_Click(object sender, EventArgs e)
        {
            string timeOut = "";
            string dateOut = "";
            if (update == true)
            {
                if (MessageBox.Show("Are you sure want to checkout this visitor?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    if (status.Equals("Yes"))
                    {
                        dateOut = DateTime.Now.ToString("yyyy-MM-dd");
                        timeOut = DateTime.Now.ToString("HH:mm");
                        visit = new Visit(dateIn, timeIn, dateOut, timeOut);
                        visit.update();
                        update = false;
                        MessageBox.Show("Checkout visitor done", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        displayDGVToday();
                    }
                    else
                    {
                        MessageBox.Show("Status checkout still not change", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
        }
    }
}
