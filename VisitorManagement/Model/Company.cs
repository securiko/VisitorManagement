using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorManagement.Model
{
    class Company
    {
        string companyID, compName, address, phone;

        public Company(string companyID, string compName, string address, string phone)
        {
            this.companyID = companyID;
            this.compName = compName;
            this.address = address;
            this.phone = phone;
        }

        public string CompanyID { get => companyID; set => companyID = value; }
        public string CompName { get => compName; set => compName = value; }
        public string Address { get => address; set => address = value; }
        public string Phone { get => phone; set => phone = value; }
    }
}
