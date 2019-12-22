using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer
{
    public static class Pipe
    {
        public static void Init()
        {
            using (NamedPipeClientStream namedPipeClient = new NamedPipeClientStream("test-pipe"))
            {
                namedPipeClient.Connect();
                Console.WriteLine("Pipe Client: Pipe Connected");

                StreamReader sr = new StreamReader(namedPipeClient);

                do
                {
                    try
                    {
                        string Message;
                        Message = sr.ReadLine();
                        if (!String.IsNullOrEmpty(Message))
                        {
                            Console.WriteLine("Pipe Server: " + Message);

                            if(Message == "Start Expt")
                            {
                                TCPManager.StartExpt();
                            }else if(Message =="End Expt")
                            {
                                TCPManager.EndExpt();
                            }
                        }
                    }

                    catch (Exception ex) { throw ex; }
                } while (true);

            }
        }
    }
}
