using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PersonDM
    {
        public int id { get; set; }
        public string lastName { get; set; }
        public string firstName { get; set; }
        public string email { get; set; }
        public DateTime dateOfBirth { get; set; }
    }
}
