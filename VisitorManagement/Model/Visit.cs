using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VisitorManagement.Connection;

namespace VisitorManagement.Model
{
    public class Visit
    {
        Connections connections;
        private string visitorID, companyID, doorID, cardNumber, dateIn, timeIn, checkinStatus, dateOut, timeOut, checkoutStatus;

        public Visit(string dateIn, string timeIn, string dateOut , string timeOut)
        {
            this.dateIn = dateIn;
            this.timeIn = timeIn;
            this.dateOut = dateOut;
            this.timeOut = timeOut;
            connections = Connections.getInstance();
        }

        public Visit(string visitorID, string companyID, string doorID, string cardNumber, string dateIn, string timeIn, string checkinStatus)
        {
            this.visitorID = visitorID;
            this.companyID = companyID;
            this.doorID = doorID;
            this.dateIn = dateIn;
            this.timeIn = timeIn;
            this.cardNumber = cardNumber;
            this.checkinStatus = checkinStatus;
            connections = Connections.getInstance();
        }

        public string VisitorID { get => visitorID; set => visitorID = value; }
        public string CompanyID { get => companyID; set => companyID = value; }
        public string DoorID { get => doorID; set => doorID = value; }
        public string CardNumber { get => cardNumber; set => cardNumber = value; }
        public string DateIn { get => dateIn; set => dateIn = value; }
        public string TimeIn { get => timeIn; set => timeIn = value; }
        public string CheckinStatus { get => checkinStatus; set => checkinStatus = value; }
        public string DateOut { get => dateOut; set => dateOut = value; }
        public string TimeOut { get => timeOut; set => timeOut = value; }
        public string CheckoutStatus { get => checkoutStatus; set => checkoutStatus = value; }

        public void insert()
        {
            string query = "insert into TVIsiting (VisitorID, CompanyID, DoorID, CardNumber, DateIn, TimeIn, CheckinStatus)" +
                       "values ('" + visitorID + "'," +
                       "'" + companyID + "'," +
                       "'" + doorID + "'," +
                       "'" + cardNumber + "'," +
                       "'" + dateIn + "'," +
                       "'" + timeIn + "'," +
                       "'Yes')";
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
            string query = "update TVisiting set DateOut = '"+ dateOut +"', timeOut = '"+ timeOut + "', CheckoutStatus = 'Yes' where DateIn = '" + dateIn +"' and TimeIn = '"+ timeIn +"'";
            try
            {
                connections.insertSQL(query);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
