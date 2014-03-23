using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PersonPhoneDM
    {
        public int id { get; set; }
        public int personId { get; set; }
        public int phoneId { get; set; }
        public int typeId { get; set; }
    }
}
