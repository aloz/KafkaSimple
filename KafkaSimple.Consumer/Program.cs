using Confluent.Kafka;
using KafkaSimple.Common;
using KafkaSimple.Common.Types.Messages;
using System.Text.Json;

namespace KafkaSimple.Consumer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConsumer<Ignore, string>? consumer = null;

            try
            {
                Console.WriteLine($"Kafka simple terminal - Consumer");
                Console.WriteLine($"Kafka server {KafkaSimpleConfig.CONST_SERVER} and topic {KafkaSimpleConfig.CONST_TOPIC_keyPressed}");
                Console.WriteLine("Every key is sent from KafkaSimple.Producer");
                Console.WriteLine();

                ConsumerConfig config = new()
                {
                    BootstrapServers = KafkaSimpleConfig.CONST_SERVER,
                    GroupId = $"{KafkaSimpleConfig.CONST_TOPIC_keyPressed}_group",
                    AutoOffsetReset = AutoOffsetReset.Earliest
                };

                consumer = new ConsumerBuilder<Ignore, string>(config).Build();
                consumer.Subscribe(KafkaSimpleConfig.CONST_TOPIC_keyPressed);

                while (true)
                {
                    ConsumeResult<Ignore, string> cresult = consumer.Consume();
                    KeyPressedMessage? KeyPressedMessageDeserialized =  JsonSerializer.Deserialize<KeyPressedMessage>(cresult.Message.Value);
                    char keychar = KeyPressedMessageDeserialized.KeyPressed;
                    Console.Write(keychar);
                    if (keychar == 13)
                        Console.WriteLine();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine($"Exception: {ex}");
                Console.Beep();
                Console.ReadKey();
            }
            finally
            {
                if (consumer != null)
                    try { consumer.Dispose(); } catch (Exception) { }
            }

        }
    }
}