using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentApi.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string RegisterNo { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Department { get; set; }
        
    }
}
