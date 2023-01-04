using Microsoft.Extensions.DependencyInjection;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure.Generics.GenericRepositories;
using MyDoctorApp.Infrastructure.Generics;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace MyDoctorApp.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<DatabaseContext>();
            services.AddScoped<IRepository<Appointment>, AppointmentRepository>();
            services.AddScoped<IRepository<AppointmentInterval>, AppointmentIntervalRepository>();
            services.AddScoped<IRepository<ScheduleInterval>, ScheduleIntervalRepository>();
            services.AddScoped<IRepository<Bill>, BillRepository>();
            services.AddScoped<IRepository<Doctor>, DoctorRepository>();
            services.AddScoped<IRepository<Drug>, DrugRepository>();
            services.AddScoped<IRepository<DrugStock>, DrugStockRepository>();
            services.AddScoped<IRepository<SurveyQuestions>, SurveyQuestionsRepository>();
            services.AddScoped<IRepository<MedicalRoom>, MedicalRoomRepository>();
            services.AddScoped<IRepository<Patient>, PatientRepository>();
            services.AddScoped<IRepository<Prescription>, PrescriptionRepository>();
            services.AddScoped<IRepository<Procedure>, ProcedureRepository>();
            services.AddScoped<IRepository<PrescriptedDrug>, PrescriptedDrugRepository>();
            services.AddScoped<IRepository<Speciality>, SpecialityRepository>();
            services.AddDbContext<DatabaseContext>(options => options.UseSqlite(configuration.GetConnectionString("MyDoctorDB")));
            return services;
        }
    }
}
