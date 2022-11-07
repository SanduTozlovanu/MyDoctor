using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDoctor.Bussiness.Entities
{
    public class MedicalHistory
    {
        public int Id { get; set; }

        public List<string> Diseases { get; set; }

        public List<Drug> DrugsEverTaken { get; set; }
    }
}
