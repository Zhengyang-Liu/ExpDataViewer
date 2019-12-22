using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer
{
    public static class TCPManager
    {
        static TcpListener server = null;
        static Int32 port = 13000;

        static TcpClient client = null;
        // Get a stream object for reading and writing
        static NetworkStream stream;
        static bool isConnected;

        static string serverMessage = "TCP Server: ";
        public static void Init()
        {
            try
            {
                // TcpListener server = new TcpListener(port);
                server = new TcpListener(IPAddress.Any, port);

                // Start listening for client requests.
                server.Start();

                // Buffer for reading data
                Byte[] bytes = new Byte[256];
                String data = null;

                // Enter the listening loop.
                while (true)
                {
                    Console.WriteLine(serverMessage + "Waiting for a connection... ");

                    // Perform a blocking call to accept requests.
                    // You could also user server.AcceptSocket() here.
                    client = server.AcceptTcpClient();
                    isConnected = true;
                    Console.WriteLine(serverMessage + "Connected!");

                    // Get a stream object for reading and writing
                    stream = client.GetStream();
                    byte[] msg;
                    int i;

                    try
                    {
                        // Loop to receive all the data sent by the client.
                        while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            // Translate data bytes to a ASCII string.
                            data = Encoding.ASCII.GetString(bytes, 0, i);
                            Console.WriteLine(serverMessage + "Received: {0}", data);

                            // Process the data sent by the client.
                            data = data.ToUpper();

                            msg = Encoding.ASCII.GetBytes(data);

                            // Send back a response.
                            stream.Write(msg, 0, msg.Length);
                            Console.WriteLine(serverMessage + "Sent: {0}", data);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(serverMessage + "SocketException: {0}", e);
                    }

                    // Shutdown and end connection
                    client.Close();
                    isConnected = false;
                    Console.WriteLine(serverMessage + "Client Disconnected");
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(serverMessage + "SocketException: {0}", e);
            }
            finally
            {
                // Stop listening for new clients.
                server.Stop();
            }


            Console.WriteLine(serverMessage + "\nHit enter to continue...");
            Console.Read();
        }

        public static void StartExpt()
        {
            if (isConnected)
            {
                string message = "Start";
                byte[] msg = Encoding.ASCII.GetBytes(message);
                stream.Write(msg, 0, msg.Length);
                Console.WriteLine(serverMessage + "Start");
            }
            else
            {
                Console.WriteLine(serverMessage + "Failed to send Start message, client isn't connected");
            }
        }

        public static void EndExpt()
        {
            if (isConnected)
            {
                string message = "End";
                byte[] msg = Encoding.ASCII.GetBytes(message);
                stream.Write(msg, 0, msg.Length);
                Console.WriteLine(serverMessage + "End");
            }
            else
            {
                Console.WriteLine(serverMessage + "Failed to send End message, client isn't connected");
            }
        }
    }
}
