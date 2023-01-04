﻿using MyDoctorApp.Domain.Helpers;

namespace MyDoctorApp.Domain.Models
{
    public class Doctor : User
    {
        public Doctor(string email, string password, string firstName, string lastName, string description = "", string username = "") :
            base(AccountTypes.Doctor, email, password, firstName, lastName, description, username)
        {
            Appointments = new List<Appointment>();
            ScheduleIntervals = new List<ScheduleInterval>();
        }
        public virtual MedicalRoom MedicalRoom { get; private set; }
        public Guid MedicalRoomId { get; private set; }
        public virtual Speciality Speciality { get; private set; }
        public Guid SpecialityID { get; private set; }
        public virtual List<Appointment> Appointments { get; private set; }
        public virtual List<ScheduleInterval> ScheduleIntervals { get; private set; }

        public void AttachToMedicalRoom(MedicalRoom medicalRoom)
        {
            MedicalRoomId = medicalRoom.Id;
            MedicalRoom = medicalRoom;
        }

        public void AttachToSpeciality(Speciality speciality)
        {
            Speciality = speciality;
            SpecialityID = speciality.Id;
        }

        public void RegisterAppointment(Appointment appointment)
        {
            appointment.AttachToDoctor(this);
            Appointments.Add(appointment);
        }

        public void RegisterScheduleIntervals(List<ScheduleInterval> scheduleIntervals)
        {
            foreach (var si in scheduleIntervals)
            {
                si.AttachToDoctor(this);
                ScheduleIntervals.Add(si);
            }
        }

        public void Update(Doctor doctor)
        {
            base.Update(doctor);
            Speciality = doctor.Speciality;
        }

        public static List<Tuple<TimeOnly, TimeOnly>> GetAvailableAppointmentIntervals(
            DateOnly date,
            List<ScheduleInterval> scheduleIntervals,
            List<AppointmentInterval> appointmentIntervals)
        {
            // Fitlering appointmentIntervals to boost performance, keeping bt date and date-1Day.
            appointmentIntervals = appointmentIntervals.FindAll(a => a.Date == date || a.Date == date.AddDays(-1));

            var weekDay = date.ToString("dddd");
            var appointmentDurationInMins = 30;
            List<Tuple<TimeOnly, TimeOnly>> availableIntervals = new();
            foreach (var interval in scheduleIntervals)
            {
                if (interval.DayOfWeek.ToString() == weekDay)
                {
                    TimeOnly intervalEndTime = interval.StartTime;

                    while ((interval.EndTime - intervalEndTime).TotalMinutes >= appointmentDurationInMins)
                    {
                        ProcessInterval(date, ref intervalEndTime, ref appointmentDurationInMins, ref appointmentIntervals, ref availableIntervals);
                    }
                    break;
                }
            }
            return availableIntervals;
        }

        private static void ProcessInterval(DateOnly date, ref TimeOnly intervalEndTime,
            ref int appointmentDurationInMins, ref List<AppointmentInterval> appointmentIntervals,
            ref List<Tuple<TimeOnly, TimeOnly>> availableIntervals)
        {
            TimeOnly intervalStartTime = intervalEndTime;
            intervalEndTime = intervalStartTime.AddMinutes(appointmentDurationInMins);
            bool skipCurrentInterval = false;
            foreach (var appointmentInterval in appointmentIntervals)
            {
                if (intervalStartTime.IsBetween(appointmentInterval.StartTime, appointmentInterval.EndTime) ||
                    intervalEndTime.AddMinutes(-1).IsBetween(appointmentInterval.StartTime, appointmentInterval.EndTime))
                {
                    // If StartTime is bigger than EndTime (e.x.: "23:30", "00:00")
                    if (appointmentInterval.StartTime.CompareTo(appointmentInterval.EndTime) == 1)
                    {
                        if (date == appointmentInterval.Date)
                        {
                            if (intervalEndTime >= appointmentInterval.StartTime)
                            {
                                skipCurrentInterval = true;
                                break;
                            }
                        }

                        if (date.AddDays(-1) == appointmentInterval.Date)
                        {
                            if (intervalEndTime <= appointmentInterval.EndTime)
                            {
                                skipCurrentInterval = true;
                                break;
                            }
                        }

                    }
                    // If StartTime is less or equal than EndTime (e.x.: "00:00", "00:30")
                    else
                    {
                        if (date == appointmentInterval.Date)
                        {
                            skipCurrentInterval = true;
                            break;
                        }
                    }
                }
            }

            if (!skipCurrentInterval)
            {
                availableIntervals.Add(Tuple.Create(intervalStartTime, intervalEndTime));
            }
        }
    }
}
