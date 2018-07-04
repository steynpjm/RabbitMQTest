using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SteynPJM.RabbitMQTest.Common;

namespace ServerSide
{
  class Program
  {
    static void Main(string[] args)
    {
      string queueName = Constants.ChannelName;   // The queue name.

      PrintHeader();

      // Setup the communications to the MQ service.
      LocalConnectionFactory factory = new LocalConnectionFactory();
      IConnection connection = factory.CreateConnection();
      IModel model = connection.CreateModel();
      model.QueueDeclare(queueName, false, false, false, null);

      // Set up the recieving event handler.
      EventingBasicConsumer myconsumer = new EventingBasicConsumer(model);
      myconsumer.Received += Consumer_Received;
      myconsumer.Registered += Consumer_Registered;

      

      model.BasicConsume(queueName, true, myconsumer);

      // Wait for a keypress to end this program.
      Console.ReadKey();


      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine("Shutting down Consumer Side...");

      // Close down the communications.
      model.Close();
      connection.Close();

    }

    /// <summary>
    /// Handles the messages received by this consumer.
    /// </summary>
    /// <remarks>There is no exception handling here. Hanlding invalid responses should be a normal part of the flow.</remarks>
    private static void Consumer_Received(object sender, BasicDeliverEventArgs e)
    {
      var body = e.Body;
      var message = Encoding.UTF8.GetString(body);

      MessageCoder coder = new MessageCoder();

      // Try and decode the message.
      if (coder.Decode(message))
      {
        PrintValidMessageRecieved(coder.Name);
      }
      else
      {
        IndicateInvalidMessageRecieved(message);
      }

    }


    private static void Consumer_Registered(object sender, ConsumerEventArgs e)
    {
      ConsoleColor currentForegroundColor = Console.ForegroundColor;
      Console.ForegroundColor = ConsoleColor.DarkGray;
      Console.WriteLine("Consumer Registered and waiting for messages to be published.");
      Console.ForegroundColor = currentForegroundColor;
    }

    /// <summary>
    /// Indicates that a valid message have been recieved.
    /// </summary>
    /// <param name="recievedName"></param>
    private static void PrintValidMessageRecieved(string recievedName)
    {
      ConsoleColor currentForegroundColor = Console.ForegroundColor;
      Console.ForegroundColor = ConsoleColor.White;
      Console.Write("Valid message : ");
      Console.ForegroundColor = currentForegroundColor;
      Console.WriteLine("Hello {0}, I am your father", recievedName);

    }

    /// <summary>
    /// Indicates that an invalid message was recieved.
    /// </summary>
    /// <param name="message">The message that was recieved.</param>
    private static void IndicateInvalidMessageRecieved(string message)
    {
      ConsoleColor currentForegroundColor = Console.ForegroundColor;
      Console.ForegroundColor = ConsoleColor.Red;
      Console.Write("Invalid message : ");
      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine(message);
      Console.ForegroundColor = currentForegroundColor;
    }

    /// <summary>
    /// Prints the header in the console.
    /// </summary>
    /// <param name="quitValue">The value used to indicate a quit.</param>
    static private void PrintHeader()
    {
      Console.Title = "Consumer Side";

      ConsoleColor currentForegroundColor = Console.ForegroundColor;
      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine("Press any key to end this program.");
      Console.ForegroundColor = currentForegroundColor;
    }

  }
}
