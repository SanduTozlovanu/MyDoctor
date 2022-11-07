﻿namespace MyDoctor.Bussiness.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public List<Appointment> Apointments { get; set; }
        public MedicalHistory MedicalHistory { get; set; }

    }
}
