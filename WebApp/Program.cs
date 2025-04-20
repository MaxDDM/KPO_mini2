using Infrastructure.RepositoryImplementations;
using System.Runtime.Serialization;
using System;
using WebApp.Controllers;
using Infrastructure;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

if (File.Exists("count.xml"))
{
    XmlSerializer xmlSerializer = new XmlSerializer(typeof(int));

    using (FileStream fs = new FileStream("count.xml", FileMode.OpenOrCreate))
    {
        try
        {
            ObjectsCounter.Count = (int)xmlSerializer.Deserialize(fs);
            if (ObjectsCounter.Count < 0)
            {
                ObjectsCounter.Count = 0;
            }
        } catch (InvalidOperationException)
        {
            ObjectsCounter.Count = 0;
        }
    }
}

AnimalController.repo = new AnimalRepositoryImpl();
EnclosureController.repo = new EnclosureRepositoryImpl();
FeedingController.repo = new FeedingRepositoryImpl();
StatisticsController.repo = new StatisticsRepositoryImpl();

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();