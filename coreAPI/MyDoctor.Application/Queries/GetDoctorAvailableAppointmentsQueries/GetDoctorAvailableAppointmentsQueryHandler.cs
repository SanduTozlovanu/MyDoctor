using MediatR;
using MyDoctor.Application.Responses;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics;
using System.Data.SqlTypes;

namespace MyDoctor.Application.Queries.GetDoctorAvailableAppointmentsQueries
{
    public class GetDoctorAvailableAppointmentsQueryHandler :
        IRequestHandler<GetDoctorAvailableAppointmentsQuery,
            List<IntervalResponse>>
    {
        private readonly IRepository<ScheduleInterval> scheduleIntervalRepository;
        private readonly IRepository<Appointment> appointmentsRepository;
        private readonly IRepository<AppointmentInterval> appointmentIntervalsRepository;
        private readonly IRepository<Doctor> doctorRepository;

        public GetDoctorAvailableAppointmentsQueryHandler(IRepository<Doctor> doctorRepository,
            IRepository<ScheduleInterval> scheduleIntervalRepository,
            IRepository<Appointment> appointmentsRepository,
            IRepository<AppointmentInterval> appointmentIntervalsRepository)
        {
            this.doctorRepository = doctorRepository;
            this.scheduleIntervalRepository = scheduleIntervalRepository;
            this.appointmentsRepository = appointmentsRepository;
            this.appointmentIntervalsRepository = appointmentIntervalsRepository;
        }
        public async Task<List<IntervalResponse>> Handle(GetDoctorAvailableAppointmentsQuery request, CancellationToken cancellationToken)
        {
            var doctor = await doctorRepository.GetAsync(request.DoctorId);
            if (doctor == null)
            {
                throw new SqlNullValueException();
            }
            var scheduleIntervs = (await scheduleIntervalRepository.FindAsync(si => si.DoctorId == request.DoctorId)).ToList();
            var appointments = (await appointmentsRepository.FindAsync(a => a.DoctorId == request.DoctorId)).ToList();
            var appointmentsIntervs = new List<AppointmentInterval>();
            foreach (var appointment in appointments)
            {
                var appointmentInterval = (await appointmentIntervalsRepository.FindAsync(ai => ai.AppointmentId == appointment.Id)).SingleOrDefault();
                if (appointmentInterval != null)
                {
                    appointmentsIntervs.Add(appointmentInterval);
                }
            }
            var intervals = new List<IntervalResponse>();
            var intervs = Doctor.GetAvailableAppointmentIntervals(request.Date, scheduleIntervs, appointmentsIntervs);
            foreach (var interval in intervs)
            {
                intervals.Add(new IntervalResponse(interval.Item1.ToString("HH:mm"), interval.Item2.ToString("HH:mm")));
            }

            return intervals;
        }
    }
}
