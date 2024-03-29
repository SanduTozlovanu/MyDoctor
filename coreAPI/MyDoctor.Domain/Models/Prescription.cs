﻿using MyDoctorApp.Domain.Helpers;

namespace MyDoctorApp.Domain.Models
{
    public class Prescription
    {
        private const string EMPTY_PROCEDURES_ERROR = "Add at least one procedure for the current Prescription";
        private const string EMPTY_DRUGS_ERROR = "Add at least one drug for the current Prescription";

        public Prescription(string description, string name)
        {
            Id = Guid.NewGuid();
            Description = description;
            Name = name;
        }
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public List<PrescriptedDrug> PrescriptedDrugs { get; private set; } = new List<PrescriptedDrug>();
        public List<Procedure> Procedures { get; private set; } = new List<Procedure>();
        public virtual Appointment Appointment { get; private set; }
        public Guid AppointmentId { get; private set; }
        public void AttachAppointment(Appointment appointment)
        {
            AppointmentId = appointment.Id;
            Appointment = appointment;
        }
        public Result RegisterProcedures(List<Procedure> procedures)
        {
            if (!procedures.Any())
            {
                return Result.Failure(EMPTY_PROCEDURES_ERROR);
            }

            Procedures.Clear();
            foreach (Procedure procedure in procedures)
            {
                procedure.AttachToPrescription(this);
                Procedures.Add(procedure);
            }

            return Result.Success();
        }

        public Result RegisterPrescriptedDrugs(List<PrescriptedDrug> prescriptedDrugs)
        {
            if (!prescriptedDrugs.Any())
            {
                return Result.Failure(EMPTY_DRUGS_ERROR);
            }

            PrescriptedDrugs.Clear();
            foreach (PrescriptedDrug drug in prescriptedDrugs)
            {
                PrescriptedDrugs.Add(drug);
                drug.AttachPrescription(this);
            }

            return Result.Success();
        }
    }
}