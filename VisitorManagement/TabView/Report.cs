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
using Excel = Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Collections;
using System.IO;

namespace VisitorManagement.TabView
{
    public partial class Report : UserControl
    {
        Connections connections;
        public Report()
        {
            InitializeComponent();
        }

        /* filter report visitor
         * filter dapat dilakukan by tanggal dan (nama / card number / door name )
         */
        private void displayVisitor(int index)
        {
            string dateFrom = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string dateTo = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            string query = "";
            switch(index)
            {
                case 1:
                    query = QueryTable.viewVisiting + "where DateIn between '" + dateFrom + "' and '" + dateTo + "' and CardNumber = '" + textBox1.Text + "'";
                    break;
                case 2:
                    query = QueryTable.viewVisiting + "where DateIn between '" + dateFrom + "' and '" + dateTo + "' and Name = '" + textBox1.Text + "'";
                    break;
                case 3:
                    query = QueryTable.viewVisiting + "where DateIn between '" + dateFrom + "' and '" + dateTo + "' and DoorName = '" + textBox1.Text + "'";
                    break;
                default:
                    query = QueryTable.viewVisiting + "where DateIn between '" + dateFrom + "' and '" + dateTo + "'";
                    break;
            }

            connections.displayDB(query, "VisitorID");
            dataGridView1.DataSource = connections.sql_d_set;
            dataGridView1.DataMember = "VisitorID";
        }

        private void Report_Load(object sender, EventArgs e)
        {
            connections = Connections.getInstance();
            comboBox1.SelectedIndex = 0;
        }

        // event click listener yang digunakan untuk export dari datagridview ke microsoft excel
        private void btnExport_Click(object sender, EventArgs e)
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath);
            string filePath = path + "\\Report.xlsx";
            Excel._Application excel = new Microsoft.Office.Interop.Excel.Application();
            Excel._Workbook workbook = excel.Workbooks.Add(Type.Missing);
            Excel._Worksheet worksheet = null;

            try
            {
                worksheet = workbook.ActiveSheet;
                worksheet.Name = "Report";

                int cellRowIndex = 1;
                int cellColumnIndex = 1;

                for (int i = 0; i < dataGridView1.Rows.Count + 1; i++)
                {
                    for (int j = 0; j < dataGridView1.Columns.Count; j++)
                    {
                        // Excel index starts from 1,1. As first Row would have the Column headers, adding a condition check. 
                        if (cellRowIndex == 1)
                        {
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = dataGridView1.Columns[j].HeaderText;
                        }
                        else
                        {
                            worksheet.Cells[cellRowIndex, cellColumnIndex] = dataGridView1.Rows[i-1].Cells[j].Value.ToString();
                        }
                        cellColumnIndex++;
                    }
                    cellColumnIndex = 1;
                    cellRowIndex++;
                }
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Filter = "Excel files(*.xlsx) | *.xlsx | All files(*.*) | *.* ";
                saveFileDialog.FilterIndex = 2;

                if(saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    workbook.SaveAs(saveFileDialog.FileName);
                    MessageBox.Show("Export Successful");
                }
            }catch(Exception ex) { MessageBox.Show(ex.Message); }
            finally
            {
                excel.Quit();
                workbook = null;
                excel = null;
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(e.RowIndex.ToString() + " and " + e.ColumnIndex.ToString());
        }

        private void btnQuery_Click(object sender, EventArgs e)
        {
            displayVisitor(comboBox1.SelectedIndex);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 0) textBox1.Enabled = false;
            else textBox1.Enabled = true;
        }

    }
}
