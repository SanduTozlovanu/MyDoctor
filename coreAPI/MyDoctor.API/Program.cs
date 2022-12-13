using Microsoft.EntityFrameworkCore;
using MyDoctorApp.Domain.Models;
using MyDoctorApp.Infrastructure;
using MyDoctorApp.Infrastructure.Generics;
using MyDoctorApp.Infrastructure.Generics.GenericRepositories;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7244",
                                "http://localhost:3000")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});
builder.Services.AddSwaggerGen(c =>
{
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
// Add services to the container.
builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlite("Data Source = MyDoctorApp.db"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<DatabaseContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IRepository<Appointment>, AppointmentRepository>();
builder.Services.AddScoped<IRepository<AppointmentInterval>, AppointmentIntervalRepository>();
builder.Services.AddScoped<IRepository<ScheduleInterval>, ScheduleIntervalRepository>();
builder.Services.AddScoped<IRepository<Bill>, BillRepository>();
builder.Services.AddScoped<IRepository<Doctor>, DoctorRepository>();
builder.Services.AddScoped<IRepository<Drug>, DrugRepository>();
builder.Services.AddScoped<IRepository<DrugStock>, DrugStockRepository>();
builder.Services.AddScoped<IRepository<MedicalHistory>, MedicalHistoryRepository>();
builder.Services.AddScoped<IRepository<MedicalRoom>, MedicalRoomRepository>();
builder.Services.AddScoped<IRepository<Patient>, PatientRepository>();
builder.Services.AddScoped<IRepository<Prescription>, PrescriptionRepository>();
builder.Services.AddScoped<IRepository<Procedure>, ProcedureRepository>();
builder.Services.AddScoped<IRepository<PrescriptedDrug>, PrescriptedDrugRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(MyAllowSpecificOrigins);
app.UseAuthorization();

app.MapControllers();

app.Run();
public partial class Program { }