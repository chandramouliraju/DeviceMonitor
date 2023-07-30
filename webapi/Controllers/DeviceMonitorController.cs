using Microsoft.AspNetCore.Mvc;
using webapi.Model;
using webapi.Interfaces;

namespace webapi.Controllers;

[ApiController]
[Route("[controller]")]
public class DeviceMonitorController : ControllerBase
{
   
    private readonly ILogger<DeviceMonitorController> _logger;


    public DeviceMonitorController(ILogger<DeviceMonitorController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetDeviceMonitor")]
    public IEnumerable<ViewMessage> GetDeviceMonitor()
    { 
        var mqClient = IOCController.GetResolver<IMQClient>();
        List<PublishMessage> messagesFromMQTT = mqClient.GetMessages();
        List<ViewMessage> messagesToShow = new List<ViewMessage>();

        if(messagesFromMQTT.Count > 0)
        {
            foreach(var message in messagesFromMQTT)
            {
                var messageToShow = new ViewMessage
                {
                    Topic = message.Topic,
                    DeviceId = message.Payload.DeviceId,
                    DateTime = message.Payload.DateTime.ToString(),
                    Measurement = message.Payload.Measurement + message.Payload.UnitOfMeasurement,
                };
                messagesToShow.Add(messageToShow);
            }
            messagesToShow = messagesToShow.OrderBy(m => m.DateTime).ToList();
        }
        return messagesToShow.ToArray();

    }
}
