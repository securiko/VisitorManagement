using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorManagement.Model
{
    public class Room
    {
        private string id, roomName, cardNo;

        public Room(string id, string roomName, string cardNo)
        {
            this.Id = id;
            this.RoomName = roomName;
            this.CardNo = cardNo;
        }

        public string Id { get => id; set => id = value; }
        public string RoomName { get => roomName; set => roomName = value; }
        public string CardNo { get => cardNo; set => cardNo = value; }
    }
}
