using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDoctor.Bussiness.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public List<Schedule> WorkingDays{ get; set; }
        public string Speciality { get; set; }
        public List<Hospital> Hospitals { get; set; }

    }
}
