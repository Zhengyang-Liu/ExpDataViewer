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
        int counter = 1;

        public void Start()
        {
            InitDataGeneratorTimer();
        }

        public void InitDataGeneratorTimer()
        {
            timer = new Timer();
            timer.Elapsed += WriteDate;
            timer.Interval = 300;
            timer.AutoReset = true;
            timer.Start();
        }

        private void WriteDate(object sender, EventArgs e)
        {
            int deg = 30 * counter;
            double rad = Math.PI * deg / 180.0;

            double value = Math.Sin(rad);

            ExpInfoDetail expInfoDetail = new ExpInfoDetail()
            {
                CollectedTime = DateTime.Now,
                Acceleration = value
            };

            counter++;

            if(counter > 12)
            {
                counter = 1;
            }

            DataManager.ReceiveData(expInfoDetail);
        }

        public void OnClose()
        {
        }
    }
}
