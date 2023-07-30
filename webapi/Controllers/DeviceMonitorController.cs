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
    public IEnumerable<Message> GetDeviceMonitor()
    {
        IOCController.RegisterImplementations();
        var mqClient = IOCController.GetResolver<IMQClient>();
        List<Message> messages = mqClient.GetMessages();

        if(messages.Count==0)
        {
            var message = new Message
            {
                Topic = "Empty Topic",
                DeviceId = "NA",
                TimeStamp = DateTime.Now.ToString(),
                Payload = "Empty Payload"
            };
            messages.Add(message);
        }

        return mqClient.GetMessages().ToArray();

    }
}
