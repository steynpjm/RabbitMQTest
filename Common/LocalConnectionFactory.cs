using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace SteynPJM.RabbitMQTest.Common
{
  public class LocalConnectionFactory : ConnectionFactory
  {
    public LocalConnectionFactory()
    {
      // These settings should come from a protected and encrypted config file.
      UserName = "admin";
      Password = "admin";
      VirtualHost = "/";
      HostName = "localhost";
    }
  }
}
