using System;
using System.Collections.Generic;
using System.Text;

namespace SteynPJM.RabbitMQTest.Common
{
  /// <summary>
  /// This class encodes and decodes our message.
  /// The message uses a very sofisticated protocol:
  ///   It must start with 'Hello my name is'.
  ///   Follow by a separator to indicate the start of the Name part.
  ///     The seperator is ', ' (blank included).
  ///   The rest is assumed to be the Name.
  /// </summary>
  public class MessageCoder
  {
    private const string protocolSeparator = ", ";
    private const string frontPart = "Hello my name is";

    public string Name { get; set; }


    public MessageCoder()
    {
      Name = string.Empty;
    }


    /// <summary>
    /// Encodes our message using our sofisticated protocol.
    /// </summary>
    /// <returns>An encoded message.</returns>
    public string Encode()
    {
      string result = string.Empty;

      if(Name != string.Empty)
      {
        // Construct the full message.
        result = string.Format("{0}{1}{2}", frontPart, protocolSeparator, Name);
      }
      else
      {
        // Should not be done, but just to trigger a bad message.
        result = "Bad input message.";
      }

      return result;

    }

    /// <summary>
    /// Decodes our message against our sofisticated protocol.
    /// </summary>
    /// <param name="recievedMessage">The message to be decoded.</param>
    /// <returns>True if the decode was succesful, otherwise fale. The result can be found in Name.</returns>
    public bool Decode(string recievedMessage)
    {
      bool result = false;
      Name = string.Empty;

      // Message must start with a front part.
      if (recievedMessage.StartsWith(frontPart)){
        // we know that this is a valid start, so split up the message usign the separator.
        string[] data = recievedMessage.Split(protocolSeparator);

        // there should now be two parts with the second part containing the name.
        if(data.Length == 2)
        {
          Name = data[1];
          result = true;
        }
      }

      return result;
    }
  }
}
