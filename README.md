# RabbitMQTest

Just a test for playing and having fun with with RabbitMQ.

Clone this repository.

Edit the settings in Common / LocationConnectionFactory.cs to point to an installation of the MQ service.
If you do not have one, use the docker file in the RabbitMQServerImage folder to create a Windows container.

Run the BuildAndTest.ps1 script to build the code and run a quick test on it.

Now you should be able to run the client (publisher) side and then the server (consumer) side:
  dotnet run ClientSide
  dotnet run ServerSide
  
The publisher or consumer will create a queue (whichever runs first). The queue name is "SteynPJMTest".
The publsiher will ask you for a name and post a message to this queue. It will keep on doing it until you type in "quit".
The consumer will keep on reading a message from the queue and display a response until you press any key.

