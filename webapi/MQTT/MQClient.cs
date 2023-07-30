using HiveMQtt.Client;
using HiveMQtt.Client.Events;
using HiveMQtt.Client.Options;
using HiveMQtt.MQTT5.Types;
using Newtonsoft.Json;
using System.Threading.Tasks;
using webapi.Interfaces;
using webapi.Model;

namespace webapi.MQTT
{

    public class MQClient : IMQClient
    {
        HiveMQClient _client;

        List<PublishMessage> _messages = new List<PublishMessage>();
        TaskCompletionSource<PublishMessage> _taskCompletionSource = new TaskCompletionSource<PublishMessage>(TaskCreationOptions.RunContinuationsAsynchronously);
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

                var message = new PublishMessage
                {
                    Topic = args.PublishMessage.Topic,
                    Payload = JsonConvert.DeserializeObject<Payload>(args.PublishMessage.PayloadAsString),

                };
                _messages.Add(message);
                Task.Run(() => _taskCompletionSource.TrySetResult(message));
            }
        }

        public List<PublishMessage> GetMessages()
        {
            return _messages;
        }

    }
}
