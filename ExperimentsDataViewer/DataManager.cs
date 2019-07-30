using ExperimentsDataViewer.DataSource;
using ExperimentsDataViewer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Timers;
using System.Web;

namespace ExperimentsDataViewer
{
    public static class DataManager
    {
        public static bool runningExp = false;
        public static int expNo;

        static IDataSource dataSource = new FakeDataSource(ReceiveData);

        public static ExpInfoContext expInfoContextDb = new ExpInfoContext();
        public static RunningExpContext runningExpContextDb = new RunningExpContext();
        public static ExpInfoDetailContext expInfoDetailContext = new ExpInfoDetailContext();

        private static List<ExpInfoDetail> expInfoDetailList = new List<ExpInfoDetail>();
        private static Timer aTimer;

        public static void Init()
        {
            dataSource.Start();
            if (HasRunningExp())
            {
                runningExp = true;
                expNo = runningExpContextDb.RunningExp.ToArray()[0].ExpNo;
            }

            aTimer = new System.Timers.Timer();
            aTimer.Interval = 5000;
            aTimer.Elapsed += Upload;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static bool HasRunningExp()
        {
            return runningExpContextDb.RunningExp.Any();
        }

        private static void ReceiveData(ExpInfoDetail expInfoDetail)
        {
            if (runningExp)
            {
                expInfoDetail.ExpNo = expNo;
                expInfoDetailList.Add(expInfoDetail);
            }
        }

        public static void AddExpDetail(ExpInfoDetail expInfoDetail)
        {
            expInfoDetail.ExpNo = expNo;
            expInfoDetailContext.ExpInfoDetails.Add(expInfoDetail);
            expInfoDetailContext.SaveChanges();
        }

        public static void AddExpDetail(ExpInfoDetail[] expInfoDetails)
        {
            expInfoDetailContext.ExpInfoDetails.AddRange(expInfoDetails);
            expInfoDetailContext.SaveChanges();
        }

        private static void Upload(Object source, System.Timers.ElapsedEventArgs e)
        {
            AddExpDetail(expInfoDetailList.ToArray());
            expInfoDetailList.Clear();
        }
    }
}