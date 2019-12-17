using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TCPServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Thread PipeTread = new Thread(Pipe.Init);
            PipeTread.Start();

            Thread TCPTread = new Thread(TCPManager.Init);
            TCPTread.Start();
        }

    }
}
