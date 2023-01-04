using MyDoctorApp.Domain.Helpers;

namespace MyDoctorApp.Domain.Models
{
    public class Patient : User
    {
        public Patient(string email, string password, string firstName, string lastName, string description="", string username="") : 
            base(AccountTypes.Patient, email, password, firstName, lastName, description, username)
        {
            Appointments = new List<Appointment>();
        }
        public virtual List<Appointment> Appointments { get; private set; }
        public virtual SurveyQuestions SurveyQuestions { get; private set; }
        public void RegisterSurveyQuestions(SurveyQuestions surveyQuestions)
        {
            surveyQuestions.AttachToPatient(this);
            SurveyQuestions = surveyQuestions;
        }
        public void RegisterAppointment(Appointment appointment)
        {
            appointment.AttachToPatient(this);
            Appointments.Add(appointment);
        }

        public void Update(Patient patient)
        {
            base.Update(patient);
        }

    }
}
