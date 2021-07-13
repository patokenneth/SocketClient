using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace SocketClient
{
    class Program
    {
        static int Main(string[] args)
        {
            //Console.WriteLine("Hello World!");
            StartClient();
            return 0;

        }

        public static void StartClient()
        {
            byte[] bytes = new byte[2048];

            IPAddress address = Dns.GetHostAddresses("localhost")[0];

            //Create a remote endpoint
            //Same with the address of the server
            IPEndPoint RemoteendPoint = new IPEndPoint(address, 11500);

            //Create the client object
            Socket sender = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                sender.Connect(RemoteendPoint);
                Console.WriteLine("{0} has connected", sender.RemoteEndPoint.ToString());

                Console.WriteLine("Enter your message");
                string datamessage = Console.ReadLine();

                string encodedmessage = datamessage + "<EOF>";
                //byte[] message = Encoding.ASCII.GetBytes("This is a test<EOF>");

                byte[] message = Encoding.ASCII.GetBytes(encodedmessage);

                int bytesent = sender.Send(message);

                int bytereceived = sender.Receive(bytes);

                Console.WriteLine("Echoed test {0}", Encoding.ASCII.GetString(bytes, 0, bytereceived));
                Console.ReadLine();

                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }

            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }

            catch (SocketException e)
            {
                Console.WriteLine(e.Message.ToString());
                //throw;
            }

            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }
        }
    }
}
