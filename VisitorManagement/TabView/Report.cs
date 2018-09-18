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

namespace VisitorManagement.TabView
{
    public partial class Report : UserControl
    {
        Connections connections;
        public Report()
        {
            InitializeComponent();
        }

        private void displayVisitor(int choice)
        {
            string query = "";

            switch (choice)
            {
                case 1:
                    query = "select VisitorID as 'Visitor ID', Name, Address from TVisitor";
                    connections.displayDB(query, "VisitorID");
                    dataGridView1.DataSource = connections.sql_d_set;
                    dataGridView1.DataMember = "VisitorID";
                    
                    break;
                case 2:
                    query = "select EmployeeName as 'Door Name', CardNo as 'Card Number' from TEmployee where EmployeeName like '%Ruang%'";
                    connections.displayDB(query, "CardNo");
                    dataGridView1.DataSource = connections.sql_d_set;
                    dataGridView1.DataMember = "CardNo";
                    break;
                default:
                    break;
            }

            DataGridViewCheckBoxColumn checkColumn = new DataGridViewCheckBoxColumn();
            checkColumn.Name = "Select";
            checkColumn.HeaderText = "";
            checkColumn.Width = 50;
            checkColumn.FalseValue = 0;
            checkColumn.TrueValue = 1;
            checkColumn.FillWeight = 10; //if the datagridview is resized (on form resize) the checkbox won't take up too much; value is relative to the other columns' fill values
            dataGridView1.Columns.Insert(0, checkColumn);
        }

        private void Report_Load(object sender, EventArgs e)
        {
            connections = Connections.getInstance();
            displayVisitor(1);
        }

        private void btnVisitor_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            displayVisitor(1);

        }

        private void btnCard_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns.Clear();
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();
            displayVisitor(2);
        }

        
        private void btnExport_Click(object sender, EventArgs e)
        {
            bool check = false;
            int rw = 1, cl = 1;
            string id = "";
            string query = "";
            string header = this.dataGridView1.Columns[2].HeaderText;
            string dateFrom = dateTimePicker1.Value.ToString("yyyy-MM-dd");
            string dateTo = dateTimePicker2.Value.ToString("yyyy-MM-dd");
            string timeFrom = dateTimePicker3.Value.ToString("HH:mm");
            Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                MessageBox.Show("Excel is not properly installed!!");
                return;
            }

            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;

            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);
            for (int i = 0; i < dataGridView1.Rows.Count; i++)
            {
                check = Convert.ToBoolean(dataGridView1.Rows[i].Cells[0].Value);

                if (check)
                {
                    if (header == "Name")
                    {
                        id = Convert.ToString(dataGridView1.Rows[i].Cells[1].Value);
                        query = QueryTable.viewVisiting + " where VisitorID = '" + id + "' and DateIn between '" + dateFrom + "' and '" + dateTo + "'";
                    }
                    else if (header == "Card Number") {
                        id = Convert.ToString(dataGridView1.Rows[i].Cells[2].Value);
                        query = QueryTable.viewVisiting + "where CardNumber = '" + id + "' and DateIn between '" + dateFrom + "' and '" + dateTo + "'";
                    }
                    connections.displayDB(query, "VisitID");
                    DataTable dt = connections.sql_d_set.Tables["VisitID"];
                    foreach (DataRow row in dt.Rows)
                    {
                        foreach (DataColumn col in dt.Columns)
                        {
                            xlWorkSheet.Cells[rw, cl] = row[col].ToString();
                            cl++;
                        }
                        rw++; cl = 1;   
                    }
                }
            }

            xlWorkBook.SaveAs("d:\\csharp-Excel.xls", Excel.XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, Excel.XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);

            MessageBox.Show("Excel file created , you can find the file d:\\csharp-Excel.xls");

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(e.RowIndex.ToString() + " and " + e.ColumnIndex.ToString());
        }
    }
}
