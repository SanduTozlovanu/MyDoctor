﻿using MyDoctor.Domain.Models;

namespace MyDoctor
{
    public class AppointmentInterval: Interval
    {
        public AppointmentInterval(DateOnly date, TimeOnly startTime, TimeOnly endTime) : base(date, startTime, endTime) {}
        public Appointment Appointment { get; private set; }
        public Guid AppointmentId { get; private set; }

        public void AttachToAppointment(Appointment appointment)
        {
            this.AppointmentId = appointment.Id;
            this.Appointment = appointment;
        }
    }
}