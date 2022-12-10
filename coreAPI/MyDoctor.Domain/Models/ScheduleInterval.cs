﻿namespace MyDoctorApp.Domain.Models
{
    public class ScheduleInterval : Interval
    {
        public ScheduleInterval(DateOnly date, TimeOnly startTime, TimeOnly endTime, uint appointmentDuration) : base(date, startTime, endTime)
        {
            AppointmentDuration = appointmentDuration;
        }
        public virtual Doctor Doctor { get; private set; }
        public uint AppointmentDuration { get; private set; }
        public Guid DoctorId { get; private set; }

        public void AttachToDoctor(Doctor doctor)
        {
            DoctorId = doctor.Id;
            Doctor = doctor;
        }
    }
}