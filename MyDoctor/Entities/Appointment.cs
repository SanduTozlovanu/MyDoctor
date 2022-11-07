using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDoctor.Bussiness.Entities
{
    public class Appointment
    {
        public int Id { get; set; }
        public Patient Patient { get; set; }
        public Doctor Doctor { get; set; }
        public Hospital Hospital  { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public double Price { get; set; }

    }
}
