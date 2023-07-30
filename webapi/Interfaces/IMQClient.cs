using HiveMQtt.MQTT5.Types;
using System.Xml.Serialization;
using webapi.Model;

namespace webapi.Interfaces
{
    public interface IMQClient
    {
        Task<bool> Connect();

        Task<bool> Disconnect();

        Task<bool> SubscribeTopic(List<Topic> topics);

        List<Message> GetMessages();

    }
}
