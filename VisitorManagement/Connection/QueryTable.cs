using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorManagement.Connection
{
    public class QueryTable
    {
        // Query untuk membuat table untuk aplikasi VMS
        
        public static string visitor = "CREATE TABLE TVisitor ( " +
                            "VisitorID NVARCHAR(50) NOT NULL PRIMARY KEY," +
                            "Name NVARCHAR(50) ," +
                            "Gender NCHAR(1)," +
                            "Address NVARCHAR(50)," +
                            "Phone NVARCHAR(50)," +
                            "Photo NVARCHAR(50) default 'No Photo'); ";

        public static string visiting = "CREATE TABLE TVisiting ( " +
                            "VisitID INT NOT NULL PRIMARY KEY IDENTITY(1, 1)," +
                            "VisitorID NVARCHAR(50) NOT NULL FOREIGN KEY REFERENCES TVisitor(VisitorID)," +
                            "CompanyID tinyint," +
                            "DoorID INT NOT NULL FOREIGN KEY REFERENCES TDoor(DoorID)," +
                            "CardNumber NVARCHAR(50)," +
                            "DateIn date ," +
                            "TimeIn time(7)," +
                            "CheckinStatus NCHAR(3), " +
                            "DateOut date ," +
                            "TimeOut time(7)," +
                            "CheckoutStatus NCHAR(3)); ";

        public static string createViewVisiting = "CREATE VIEW Vvisiting AS select " +
                            "TVisiting.VisitID, " +
                            "TVisitor.VisitorID, " +
                            "TVisitor.Name, " +
                            "TCompany.CompName, " +
                            "TDoor.DoorName, " +
                            "TVisiting.CardNumber, " +
                            "TVisiting.DateIn, " +
                            "TVisiting.TimeIn, " +
                            "TVisiting.CheckinStatus, " +
                            "TVisiting.DateOut, " +
                            "TVisiting.TimeOut, " +
                            "TVisiting.CheckoutStatus " +
                            "from TVisiting " +
                            "inner join TVisitor on TVisiting.VisitorID = TVisitor.VisitorID " +
                            "inner join TCompany on TVisiting.CompanyID = TCompany.CompanyID " +
                            "inner join TDoor on TVisiting.DoorID = TDoor.DoorID;";

        public static string viewVisiting = "select VisitID as 'Visit ID', VisitorID as 'Visitor ID', Name," +
                " CompName as 'Company Name'," +
                " DoorName as 'Door'," +
                " CardNumber as 'Card Number'," +
                " DateIn as 'Date In'," +
                " TimeIn as 'In Time'," +
                " CheckinStatus as 'Check in Status'," +
                " DateOut as 'Date Out'," +
                " TimeOut as 'Out Time'," +
                " CheckoutStatus as 'Check out Status' from Vvisiting ";
    }
}
