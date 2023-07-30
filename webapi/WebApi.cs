using Autofac;
using HiveMQtt.MQTT5.Types;
using webapi;
using webapi.Interfaces;
using webapi.Model;
using webapi.MQTT;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


List<Topic> topics = new List<Topic>();
topics.Add(new Topic
{
    Title = "home/sensor/temperature",
    QualityOfService = QualityOfService.AtLeastOnceDelivery
}
);
topics.Add(new Topic
{
    Title = "home/sensor/humidity",
    QualityOfService = QualityOfService.AtLeastOnceDelivery
}
);
topics.Add(new Topic
{
    Title = "home/sensor/brightness",
    QualityOfService = QualityOfService.AtLeastOnceDelivery
}
);
//IOC Controller to execute MQClient
IOCController.RegisterImplementations();
var mqclient = IOCController.GetResolver<IMQClient>();
bool isConnected = await mqclient.Connect();
if (isConnected)
{
    bool areSubscribed = await mqclient.SubscribeTopic(topics);
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseDefaultFiles();
    app.UseStaticFiles();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
