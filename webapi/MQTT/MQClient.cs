using HiveMQtt.Client;
using HiveMQtt.Client.Events;
using HiveMQtt.Client.Options;
using HiveMQtt.MQTT5.Types;
using System.Threading.Tasks;
using webapi.Interfaces;
using webapi.Model;

namespace webapi.MQTT
{

    public class MQClient : IMQClient
    {
        HiveMQClient _client;

        List<Message> _messages = new List<Message>();
        TaskCompletionSource<Message> _taskCompletionSource = new TaskCompletionSource<Message>(TaskCreationOptions.RunContinuationsAsynchronously);
        public async Task<bool> Connect()
        {
            if (_client == null)
            {
                _client = new HiveMQClient();
            } 
            
            var result = await _client.ConnectAsync().ConfigureAwait(false);

            return Task.FromResult(_client.IsConnected()).Result;        
        }


        public async Task<bool> Disconnect()
        {
            if(_client != null)
            {
                var result = await _client.DisconnectAsync().ConfigureAwait(false);
                return result;
            }
             
            return Task.FromResult(false).Result;
        }

        public async Task<bool> SubscribeTopic(List<Topic> topics)
        {
            var options = new SubscribeOptions();
            foreach(var topic in topics)
            {
                var topicFilter = new TopicFilter(topic.Title, topic.QualityOfService);
                options.TopicFilters.Add(topicFilter);
            }
           
            if (_client != null) {
                var result = await _client.SubscribeAsync(options);
                var areTopicSubscribed = result.Subscriptions.Count == topics.Count;
                if (areTopicSubscribed) {
                    _client.OnMessageReceived += OnMessageReceived;
                    await _taskCompletionSource.Task.WaitAsync(TimeSpan.FromMinutes(120)).ConfigureAwait(false);
                }
                return Task.FromResult(areTopicSubscribed).Result;
            }

            return Task.FromResult(false).Result;
        }

        private void OnMessageReceived(object? sender, OnMessageReceivedEventArgs args)
        {
            if (sender is not null) {

                var message = new Message
                {
                    Topic = args.PublishMessage.Topic,
                    Payload = args.PublishMessage.PayloadAsString,
                    TimeStamp = DateTime.Now.ToString()

                };
                _messages.Add(message);
                Task.Run(() => _taskCompletionSource.TrySetResult(message));
            }
        }

        public List<Message> GetMessages()
        {
            return _messages;
        }

    }
}
