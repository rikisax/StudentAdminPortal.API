using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.DataModels;
using StudentAdminPortal.API.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

//Queste Iniettano (Inject) le dipendenze del DbContext e dei Repository nei services, così li possiamo usare nei nostri controlli.
//Aggiunto per il progetto StudentAdminPortal
builder.Services.AddDbContext<StudentAdminContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("StudentAdminPortalDB"));
});
//Aggiunto per il progetto StudentAdminPortal
builder.Services.AddScoped<IStudentRepository,SqlStudentRepository>();
//Aggiunto per il progetto StudentAdminPortal
builder.Services.AddAutoMapper(typeof(Program).Assembly);




builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
