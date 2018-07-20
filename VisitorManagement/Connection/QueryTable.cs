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

        public static string department = "CREATE TABLE TDepartment ( " +
                            "DepartmentID INT NOT NULL PRIMARY KEY IDENTITY(1, 1), " +
                            "DeptName NVARCHAR(50));";

        public static string visiting = "CREATE TABLE TVisiting ( " +
                            "VisitID INT NOT NULL PRIMARY KEY" +
                            "VisitorID NVARCHAR(50) NOT NULL FOREIGN KEY REFERENCES TVisitor(VisitorID)," +
                            "DepartmentID INT NOT NULL FOREIGN KEY REFERENCES TDepartment(DepartmentID)," +
                            "DoorID INT NOT NULL FOREIGN KEY REFERENCES TDoor(DoorID)," +
                            "DateIn date NOT NULL," +
                            "TimeIn time(7)," +
                            "CheckinStatus NCHAR(3), " +
                            "DateOut date NOT NULL," +
                            "TimeOut time(7)," +
                            "CheckoutStatus NCHAR(3)); ";
        public static string view_visiting = "CREATE VIEW Vvisiting AS select " +
                            "TVisiting.VisitID, " +
                            "TVisitor.Name, " +
                            "TDepartment.DeptName, " +
                            "TDoor.DoorID, " +
                            "TVisiting.DateIn, " +
                            "TVisiting.TimeIn, " +
                            "TVisiting.DateOut, " +
                            "TVisiting.TimeOut " +
                            "from TVisiting " +
                            "inner join TVisitor on TVisiting.VisitorID = TVisitor.VisitorID " +
                            "inner join TDepartment on TVisiting.DepartmentID = TDepartment.DepartmentID " +
                            "inner join TDoor on TVisiting.DoorID = TDoor.DoorID;";
    }
}
