using System;
using System.IO;
using System.IO.Pipes;
using System.Diagnostics;

namespace ExperimentsDataViewer
{
    public static class Pipe
    {
        public static NamedPipeServerStream namedPipeServer;
        static StreamWriter sw;
        public static void Init()
        {
            namedPipeServer = new NamedPipeServerStream("test-pipe");
            namedPipeServer.WaitForConnection();

            sw = new StreamWriter(namedPipeServer);
            sw.AutoFlush = true;
            sw.WriteLine("Pipe Connected");
        }

        public static void StartExpt()
        {
            try
            {
                sw.WriteLine("Start Expt");
            }
            // Catch the IOException that is raised if the pipe is broken
            // or disconnected.
            catch (IOException e)
            {
                Debug.WriteLine("ERROR: {0}", e.Message);
            }
        }

        public static void EndExpt()
        {
            try
            {
                sw.WriteLine("End Expt");
            }
            // Catch the IOException that is raised if the pipe is broken
            // or disconnected.
            catch (IOException e)
            {
                Debug.WriteLine("ERROR: {0}", e.Message);
            }
        }

        public static void UnInit()
        {
            namedPipeServer.Dispose();
        }
    }
}