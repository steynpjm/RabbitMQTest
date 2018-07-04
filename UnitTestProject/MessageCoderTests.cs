using SteynPJM.RabbitMQTest.Common;
using System;
using Xunit;

namespace UnitTestProject
{
  public class MessageCoderTests
  {

    const string nameToTest = "test";
    const string validEncodedResult = "Hello my name is, " + nameToTest;
    const string invalidEncodedResult = "Bad input message.";

    /// <summary>
    /// Validate that MessageCoder can be created with default constructor and that name is blank.
    /// </summary>
    [Fact]
    public void CreatedWithEmptyName()
    {
      MessageCoder coder = new MessageCoder();

      Assert.NotNull(coder);
      Assert.Equal(string.Empty, coder.Name);
    }

    /// <summary>
    /// Validates that the Name property is persisted correctly.
    /// </summary>
    [Fact]
    public void NameRemainsSet()
    {
      MessageCoder coder = new MessageCoder();

      coder.Name = nameToTest;

      Assert.Equal(nameToTest, coder.Name);
    }


    /// <summary>
    /// Validates that the MessageCoder can encode a valid name correctly.
    /// </summary>
    [Fact]
    public void EncodeWithValidName()
    {
      MessageCoder coder = new MessageCoder();

      coder.Name = nameToTest;

      string result = coder.Encode();

      Assert.Equal(validEncodedResult, result);
    }


    /// <summary>
    /// Validates that the MessageCoder provides an "invalid" response when an invalid name (blank) is provided.
    /// </summary>
    [Fact]
    public void EncodeWithInvalidName()
    {
      MessageCoder coder = new MessageCoder();

      coder.Name = string.Empty;

      string result = coder.Encode();

      Assert.Equal(invalidEncodedResult, result);
    }


    /// <summary>
    /// Validates that the MessageCoder can decode a valid message correctly.
    /// </summary>
    [Fact]
    public void DecodeWithValidName()
    {
      MessageCoder coder = new MessageCoder();

      bool result = coder.Decode(validEncodedResult);

      Assert.True(result);
      Assert.Equal(nameToTest, coder.Name);
    }

    /// <summary>
    /// Validates that the MessageCoder return false when decoding an invalid message.
    /// </summary>
    [Fact]
    public void DecodeWithInvalidName()
    {
      MessageCoder coder = new MessageCoder();

      bool result = coder.Decode(invalidEncodedResult);

      Assert.False(result);
    }

  }
}
