using Confluent.Kafka;
using KafkaSimple.Common;
using KafkaSimple.Common.Types.Messages;
using System.Text.Json;

namespace KafkaSimple.Producer
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IProducer<Null, string>? producer = null;

            try
            {
                Console.WriteLine($"Kafka simple terminal - Producer");
                Console.WriteLine($"Kafka server {KafkaSimpleConfig.CONST_SERVER} and topic {KafkaSimpleConfig.CONST_TOPIC_keyPressed}");
                Console.WriteLine("Press any key to send it to KafkaSimple.Consumer");
                Console.WriteLine();

                ProducerConfig pconfig = new() { BootstrapServers = KafkaSimpleConfig.CONST_SERVER };

                producer = new ProducerBuilder<Null, string>(pconfig).Build();

                while (true)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey();
                    char keychar = keyInfo.KeyChar;
                    if (keychar == 13)
                        Console.WriteLine();

                    string KeyPressedMessageSerialized = JsonSerializer.Serialize<KeyPressedMessage>(new KeyPressedMessage(keychar));
                    producer.Produce(
                        KafkaSimpleConfig.CONST_TOPIC_keyPressed,
                        new Message<Null, string> { Value = KeyPressedMessageSerialized }
                        );
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine();
                Console.WriteLine($"Exception: {ex}");
                Console.Beep();
                Console.ReadKey();
            }
            finally
            {
                if (producer != null)
                    try { producer.Dispose(); } catch (Exception) { }
            }
        }
    }
}