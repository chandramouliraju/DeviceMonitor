using Autofac;
using Autofac.Extras.Moq;
using Microsoft.Extensions.Logging;
using Moq;
using NuGet.Frameworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webapi;
using webapi.Controllers;
using webapi.Interfaces;
using webapi.Model;

namespace DeviceMonitorTests
{
    public class DeviceMonitorControllerTests
    {
        Mock<IMQClient> mock;
        public DeviceMonitorControllerTests() 
        { 
            mock = new Mock<IMQClient>();
            var containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterInstance(mock.Object).As<IMQClient>();
            IOCController._container = containerBuilder.Build();
        }

        [Fact]
        public void ValidateGetMessagesResponsesAreEmpty()
        {
            
            mock.Setup(m => m.GetMessages()).Returns(new List<PublishMessage>());

            var deviceMonitorController = new DeviceMonitorController(It.IsAny<ILogger<DeviceMonitorController>>());
            var actual = deviceMonitorController.GetDeviceMonitor();

            Assert.Equal(0,actual.Count());

        }

        [Fact]
        public void ValidateGetMessagesResponseNotEmpty()
        {
            var message = new PublishMessage
            {
                Topic = "home/sensor/humidity",
                Payload = new Payload
                {
                    DeviceId = "1232234",
                    DateTime = DateTime.Now,
                    Measurement = "25",
                    UnitOfMeasurement = "%"
                }
            };
            List<PublishMessage> messages = new List<PublishMessage>();
            messages.Add(message);
            mock.Setup(m => m.GetMessages()).Returns(messages);

            var deviceMonitorController = new DeviceMonitorController(It.IsAny<ILogger<DeviceMonitorController>>());
            var actual = deviceMonitorController.GetDeviceMonitor();

            Assert.Equal(1, actual.Count());
            Assert.Equal(message.Topic, actual.First().Topic);
            Assert.Equal(message.Payload.DeviceId, actual.First().DeviceId);

        }

        [Fact]
        public void ValidateGetMessagesResponseSortedbyDateTime()
        {
            
            var currentDate = DateTime.Now;
            var message = new PublishMessage
            {
                Topic = "home/sensor/humidity",
                Payload = new Payload
                {
                    DeviceId = "1232234",
                    DateTime = currentDate,
                    Measurement = "25",
                    UnitOfMeasurement = "%"
                }
            };
            List<PublishMessage> messages = new List<PublishMessage>();
            messages.Add(message);
            messages.Add(new PublishMessage
            {
                Topic = "home/sensor/humidity",
                Payload = new Payload
                {
                    DeviceId = "1232235",
                    DateTime = DateTime.Now.AddDays(1),
                    Measurement = "26",
                    UnitOfMeasurement = "%"
                }
            });
            mock.Setup(m => m.GetMessages()).Returns(messages);

            var deviceMonitorController = new DeviceMonitorController(It.IsAny<ILogger<DeviceMonitorController>>());
            var actual = deviceMonitorController.GetDeviceMonitor();

            Assert.Equal(2, actual.Count());
            Assert.Equal(message.Topic, actual.Last().Topic);
            Assert.Equal(message.Payload.DeviceId, actual.Last().DeviceId);
            Assert.Equal(message.Payload.DateTime.ToString(), actual.Last().DateTime);

        }
    }

}
