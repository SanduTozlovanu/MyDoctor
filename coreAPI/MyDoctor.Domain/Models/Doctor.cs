﻿using MyDoctorApp.Domain.Helpers;

namespace MyDoctor.Domain.Models
{
    public class Doctor: User
    {
        public Doctor(string email, string password, string firstName, string lastName, string speciality):
            base(AccountTypes.Doctor, email, password, firstName, lastName)
        {
            this.Speciality = speciality;
        }
        public virtual MedicalRoom MedicalRoom { get; private set; }
        public Guid MedicalRoomId { get; private set; }
        public string Speciality { get; private set; }
        public List<Appointment> Appointments { get; private set; } = new List<Appointment>();
        public List<ScheduleInterval> ScheduleIntervals { get; private set; } = new List<ScheduleInterval>();

        public void AttachToMedicalRoom(MedicalRoom medicalRoom)
        {
            this.MedicalRoomId = medicalRoom.Id;
            this.MedicalRoom = medicalRoom;
        }

        public void RegisterAppointment(Appointment appointment)
        {
            appointment.AttachToDoctor(this);
            this.Appointments.Add(appointment);
        }
        //public Result RegisterAppointmentIntervals(List<AppointmentInterval> appointmentIntervals)
        //{
        //    if (!appointmentIntervals.Any())
        //    {
        //        return Result.Failure("Add at least one appointment to the current Doctor");
        //    }

        //    foreach (AppointmentInterval appointmentInterval in appointmentIntervals)
        //    {
        //        appointmentInterval.AttachDoctor(this);
        //        this.AppointmentIntervals.Add(appointmentInterval);
        //    }

        //    return Result.Success();
        //}
    }
}