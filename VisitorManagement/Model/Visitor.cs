using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorManagement.Model
{
    public class Visitor
    {
        private string id, name, birth, phone, address;

        public Visitor(string id, string name, string birth, string phone, string address)
        {
            this.Id = id;
            this.Name = name;
            this.Birth = birth;
            this.Phone = phone;
            this.Address = address;
        }

        public string Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Birth { get => birth; set => birth = value; }
        public string Phone { get => phone; set => phone = value; }
        public string Address { get => address; set => address = value; }
    }
}
