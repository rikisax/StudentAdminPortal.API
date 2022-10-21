using Microsoft.EntityFrameworkCore;
using StudentAdminPortal.API.DataModels;
using StudentAdminPortal.API.Repositories;
using System.Net.NetworkInformation;
using System.IO;
using FluentValidation.AspNetCore;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors((options) =>
{
    options.AddPolicy("angularApplication", (builder) =>
    {
        //builder.AllowAnyOrigin();
        builder.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .WithMethods("GET","POST","PUT","DELETE")
        .WithExposedHeaders("*");
    });
});

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
builder.Services.AddScoped<IImageRepository, LocalStorageImageRepository>();
//Aggiunto per il progetto StudentAdminPortal
builder.Services.AddAutoMapper(typeof(Program).Assembly);
//Aggiunto libreria FluentValidation: motodo descritto sul video corso udemy  di Sameer Saini (oggi deprecated)
//builder.Services.AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Program>());
//nuovi statement da: https://github.com/FluentValidation/FluentValidation/issues/1965  e https://github.com/FluentValidation/FluentValidation/issues/1963
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Aggiunto per il progetto StudentAdminPortal: per poter raggiungere la cartella Resources dal Client
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new Microsoft.Extensions.FileProviders.PhysicalFileProvider(Path.Combine(app.Environment.ContentRootPath,"Resources")),
    RequestPath= "/Resources"
});


app.UseCors("angularApplication");

app.UseAuthorization();

app.MapControllers();

app.Run();
