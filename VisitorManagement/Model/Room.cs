using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorManagement.Connection;

namespace VisitorManagement.Model
{
    public class Room
    {
        Connections connections;
        private string cardNo;

        public Room()
        {
            connections = Connections.getInstance();
        }

        //public string RoomName { get => roomName; set => roomName = value; }
        //public string CardNo { get => cardNo; set => cardNo = value; }

        public string getCardNo(string name)
        {
            string query = "select CardNo from TEmployee where EmployeeName = '" + name + "'";
            cardNo = connections.getDataFromDB(query, "CardNo");
            return cardNo;
        }
    }
}
