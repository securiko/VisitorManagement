using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VisitorManagement.Model
{
    class Department
    {
        string departmentID, departName;

        public Department(string departmentID, string departName)
        {
            this.departmentID = departmentID;
            this.departName = departName;
        }

        public string DepartmentID { get => departmentID; set => departmentID = value; }
        public string DepartName { get => departName; set => departName = value; }
    }
}
