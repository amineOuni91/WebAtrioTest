using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using WebAtrioTest.Infrastructure;
using WebAtrioTest.Models;
using WebAtrioTest.Repositories;
using WebAtrioTest.Repositories.Interfaces;
using WebAtrioTest.Services;
using WebAtrioTest.Services.Interfaces;
using WebAtrioTest.Validators;
using WebAtrioTest.ViewModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PersonContext>(options =>
{
    options.UseInMemoryDatabase("PersonDb");
});

builder.Services.AddScoped<IPersonRepository, PersonRepository>();
builder.Services.AddScoped<IJobRepository, JobRepository>();
builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IJobService, JobService>();
builder.Services.AddScoped<IValidator<PersonViewModel>, PersonValidator>();
builder.Services.AddScoped<IValidator<JobViewModel>, JobValidator>();

builder.Services.Configure<JsonOptions>(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGroup("/api/Persons")
      .MapPersonsApi(app.Services)
      .WithTags("Person Api");

app.MapGroup("/api/Jobs")
      .MapJobsApi(app.Services)
      .WithTags("Job Api");



app.Run();


public static class RouteBuilderExtension
{
    public static RouteGroupBuilder MapPersonsApi(this RouteGroupBuilder group, IServiceProvider services)
    {
      
        group.MapGet("", async () =>
        {
            using var scope = services.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IPersonService>();
            return Results.Ok(await service.GetAllAsync());
        });

        group.MapGet("{organisation}", async (string organisation) =>
        {
            using var scope = services.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IPersonService>();
            return Results.Ok(await service.GetByOrganisationAsync(organisation));
        });

        group.MapPost("", async (PersonViewModel PersonVM) =>
        {
            using var scope = services.CreateScope();
            var validator = await scope.ServiceProvider.GetRequiredService<IValidator<PersonViewModel>>().ValidateAsync(PersonVM);

            if (validator.IsValid)
            {
                var service = scope.ServiceProvider.GetRequiredService<IPersonService>();

                var person = await service.AddAsync(new Person
                {
                    FirstName = PersonVM.FirstName,
                    LastName = PersonVM.LastName,
                    BirthDate = PersonVM.BirthDate,
                });

                return Results.Ok(person);
            }

            return Results.BadRequest(validator.Errors);

        });

        return group;
    }

    public static RouteGroupBuilder MapJobsApi(this RouteGroupBuilder group, IServiceProvider services)
    {
        group.MapGet("", async (Guid personId, DateTime startDate, DateTime endDate) =>
        {
            using var scope = services.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<IJobService>();
            var jobs = await service.GetPersonJobsByDateAsync(personId, startDate, endDate);
            return Results.Ok(jobs);
        });
        group.MapPost("", async (JobViewModel jobVM) =>
        {
            using var scope = services.CreateScope();
            var validator = await scope.ServiceProvider.GetRequiredService<IValidator<JobViewModel>>().ValidateAsync(jobVM);

            if (validator.IsValid)
            {
                var service = scope.ServiceProvider.GetRequiredService<IJobService>();

                var job = await service.AddAsync(new Job
                {
                    Organisation = jobVM.Organisation,
                    Position = jobVM.Position,
                    PersonId = jobVM.PersonId,
                    StartDate = jobVM.StartDate,
                    EndDate = jobVM.EndDate
                    
                });

                return Results.Ok(job);
            }
            return Results.BadRequest(validator.Errors);
        });

        return group;
    }

}