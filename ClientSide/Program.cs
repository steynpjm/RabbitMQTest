using System;
using System.Text;
using RabbitMQ.Client;
using SteynPJM.RabbitMQTest.Common;

namespace ClientSide
{
  class Program
  {
    static void Main(string[] args)
    {
      const string quitInput = "quit";  // The value to use to indicate that you want to quit the program.
      string queueName = Constants.ChannelName;   // The queue name.

      PrintHeader(quitInput); // Prints the header and the instuctions.

      // Setup the communications to the MQ service.
      LocalConnectionFactory factory = new LocalConnectionFactory();
      IConnection connection = factory.CreateConnection();
      IModel model = connection.CreateModel();
      model.QueueDeclare(queueName, false, false, false, null);


      // Start the input loop.
      string message;
      byte[] body;
      MessageCoder messageCoder = new MessageCoder();
      string nameInput = GetConsoleInputForName();

      while (nameInput.ToLower() != quitInput)
      {
        // Build the message using our very sofisticated protocol.
        messageCoder.Name = nameInput;
        message = messageCoder.Encode();

        // The body for the communication must be a byte array.
        body = Encoding.UTF8.GetBytes(message);

        // Publish the message to the queue.
        model.BasicPublish("", queueName, null, body);

        // Indicate that the message was send.
        IndicateSend(message);

        nameInput = GetConsoleInputForName();
      }

      Console.ForegroundColor = ConsoleColor.Yellow;
      Console.WriteLine("Shutting down Publisher Side...");

      // Close down the communications.
      model.Close();
      connection.Close();
    }

    /// <summary>
    /// Gets the console input for the Name part.
    /// </summary>
    /// <returns>The value from the console.</returns>
    static private string GetConsoleInputForName()
    {
      string result = string.Empty;

      ConsoleColor currentForegroundColor = Console.ForegroundColor;
      Console.ForegroundColor = ConsoleColor.White;
      Console.Write("Name : ");
      Console.ForegroundColor = currentForegroundColor;

      result = Console.ReadLine();

      return result;
    }

    /// <summary>
    /// Writes the send details to the console.
    /// </summary>
    /// <param name="message">The message that was send.</param>
    static private void IndicateSend(string message)
    {
      ConsoleColor currentForegroundColor = Console.ForegroundColor;
      Console.ForegroundColor = ConsoleColor.DarkGray;
      Console.Write("Send : ");
      Console.ForegroundColor = currentForegroundColor;
      Console.WriteLine(message);
    }

    /// <summary>
    /// Prints the header in the console.
    /// </summary>
    /// <param name="quitValue">The value used to indicate a quit.</param>
    static private void PrintHeader(string quitValue)
    {
      Console.Title = "Publisher Side";

      ConsoleColor currentForegroundColor = Console.ForegroundColor;
      Console.ForegroundColor = ConsoleColor.White;
      Console.WriteLine("Type '" + quitValue + "' to end this program.");
      Console.ForegroundColor = currentForegroundColor;
    }
  }
}
