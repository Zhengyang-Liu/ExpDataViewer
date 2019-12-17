using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TCPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            using (NamedPipeClientStream namedPipeClient = new NamedPipeClientStream("test-pipe"))
            {
                namedPipeClient.Connect();
                StreamReader sr = new StreamReader(namedPipeClient);

                do
                {
                    try
                    {
                        string test;
                        test = sr.ReadLine();
                        Console.WriteLine(test);
                    }

                    catch (Exception ex) { throw ex; }
                } while (true);

            }
            //TCPManager.Init();
        }

    }
}
