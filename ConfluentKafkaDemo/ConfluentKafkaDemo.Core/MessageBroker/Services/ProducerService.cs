using ConfluentKafkaDemo.Application.Logger;
using ConfluentKafkaDemo.Application.MessageBroker.Logic.Interfaces;
using ConfluentKafkaDemo.Application.MessageBroker.Services.Interfaces;
using ConfluentKafkaDemo.Application.MessageBroker.Validation.Interfaces;

namespace ConfluentKafkaDemo.Application.MessageBroker.Services
{
    public class ProducerService : IProducerService
    {
        private readonly ILoggerAdapter _logger;
        private readonly IProducerAdapter _producer;
        private readonly IMessageValidator _messageValidator;
        private readonly IGenerateMessage _generateMessage;

        public ProducerService(ILoggerAdapter logger, IProducerAdapter producer,
            IMessageValidator messageValidator, IGenerateMessage generateMessage)
        {
            _logger = logger;
            _producer = producer;
            _messageValidator = messageValidator;
            _generateMessage = generateMessage;
        }

        public async Task Start(string messageType)
        {
            try
            {
                while (true)
                {
                    var message = _generateMessage.Generate();
                    if (_messageValidator.Valid(message.Value) is false) continue;

                    var deliveryResult = await _producer.ProduceAsync(messageType, message);
                    _logger.LogInformation($"Delivery success: {deliveryResult}");
                }
            }
            catch (Exception e)
            {
                _logger.LogError($"Delivery failed: {e.Message}");
            }
        }
    }
}
