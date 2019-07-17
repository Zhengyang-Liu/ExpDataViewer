using System;
using System.Windows;
using System.IO.Ports;
using System.IO;
using System.Text;
using System.Timers;

namespace ExperimentsDataViewer.DataSource
{
    class FakeDataSource : IDataSource
    {
        Timer timer;
        Action<string> appendDataFunction;

        public FakeDataSource(Action<string> appendDataFunction)
        {
            this.appendDataFunction = appendDataFunction;
        }

        public void Start()
        {
            InitDataGeneratorTimer();
        }

        public void InitDataGeneratorTimer()
        {
            timer = new Timer();
            timer.Elapsed += WriteDate;
            timer.Interval = 30;
            timer.AutoReset = true;
            timer.Start();
        }

        private void WriteDate(object sender, EventArgs e)
        {
            string data = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff") + "|0.003\n";
            appendDataFunction(data);
        }

        public void OnClose()
        {
        }
    }
}
