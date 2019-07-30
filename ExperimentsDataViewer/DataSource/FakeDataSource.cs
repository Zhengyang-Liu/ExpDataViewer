using System;
using System.Windows;
using System.IO.Ports;
using System.IO;
using System.Text;
using System.Timers;
using ExperimentsDataViewer.Models;

namespace ExperimentsDataViewer.DataSource
{
    class FakeDataSource : IDataSource
    {
        Timer timer;
        Action<ExpInfoDetail> appendDataFunction;
        Random rnd = new Random();

        public FakeDataSource(Action<ExpInfoDetail> appendDataFunction)
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
            ExpInfoDetail expInfoDetail = new ExpInfoDetail()
            {
                CollectedTime = DateTime.Now,
                Acceleration = rnd.NextDouble()
            };

            appendDataFunction(expInfoDetail);
        }

        public void OnClose()
        {
        }
    }
}
