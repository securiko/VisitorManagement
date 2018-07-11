using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorManagement.Model
{
    public class Visit
    {
        private string id_room, id_visitor, checkindate, checkintime, checkoutdate, checkouttime, returncard;

        public Visit(string id_room, string id_visitor, string checkindate, string checkintime, string checkoutdate, string checkouttime, string returncard)
        {
            this.Id_room = id_room;
            this.Id_visitor = id_visitor;
            this.Checkindate = checkindate;
            this.Checkintime = checkintime;
            this.Checkoutdate = checkoutdate;
            this.Checkouttime = checkouttime;
            this.Returncard = returncard;
        }

        public string Id_room { get => id_room; set => id_room = value; }
        public string Id_visitor { get => id_visitor; set => id_visitor = value; }
        public string Checkindate { get => checkindate; set => checkindate = value; }
        public string Checkintime { get => checkintime; set => checkintime = value; }
        public string Checkoutdate { get => checkoutdate; set => checkoutdate = value; }
        public string Checkouttime { get => checkouttime; set => checkouttime = value; }
        public string Returncard { get => returncard; set => returncard = value; }
    }
}
