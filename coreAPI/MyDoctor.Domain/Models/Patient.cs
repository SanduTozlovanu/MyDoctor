using MyDoctorApp.Domain.Helpers;

namespace MyDoctorApp.Domain.Models
{
    public class Patient : User
    {
        private const string EMPTY_SURVEYQUESTIONS_ERROR = "Add at least one doctor to the current MedicalRoom";

        public Patient(string email, string password, string firstName, string lastName, string description = "", string username = "") :
            base(AccountTypes.Patient, email, password, firstName, lastName, description, username)
        {
            this.Appointments = new List<Appointment>();
            this.SurveyQuestions = new List<SurveyQuestion>();

        }
        public virtual List<Appointment> Appointments { get; private set; }
        public virtual List<SurveyQuestion> SurveyQuestions { get; private set; }
        public Result RegisterSurveyQuestions(List<SurveyQuestion> surveyQuestions)
        {
            if (!surveyQuestions.Any())
            {
                return Result.Failure(EMPTY_SURVEYQUESTIONS_ERROR);
            }

            foreach (SurveyQuestion question in surveyQuestions)
            {
                question.AttachToPatient(this);
                SurveyQuestions.Add(question);
            }

            return Result.Success();
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
