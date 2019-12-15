using ExperimentsDataViewer.DataSource;
using ExperimentsDataViewer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Timers;
using System.Web;
using System.Net;

namespace ExperimentsDataViewer
{
    public static class DataManager
    {
        public static bool runningExp = false;
        public static int expNo;

        static IDataSource dataSource = new FakeDataSource();

        public static ExpInfoContext expInfoContextDb = new ExpInfoContext();
        public static RunningExpContext runningExpContextDb = new RunningExpContext();
        public static ExpInfoDetailContext expInfoDetailContext = new ExpInfoDetailContext();

        private static List<ExpInfoDetail> expInfoDetailList = new List<ExpInfoDetail>();
        private static Timer aTimer;

        public static void Init()
        {
            //dataSource.Start();
            if (HasRunningExp())
            {
                runningExp = true;
                expNo = runningExpContextDb.RunningExp.ToArray()[0].ExpNo;
            }

            aTimer = new Timer();
            aTimer.Interval = 1000;
            aTimer.Elapsed += Upload;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        private static bool HasRunningExp()
        {
            return runningExpContextDb.RunningExp.Any();
        }

        public static void ReceiveData(ExpInfoDetail expInfoDetail)
        {
            if (runningExp)
            {
                expInfoDetail.ExpNo = expNo;
                expInfoDetailList.Add(expInfoDetail);
            }
        }

        public static void AddExpDetail(ExpInfoDetail expInfoDetail)
        {
            using (var expInfoDetailContext = new ExpInfoDetailContext())
            {
                expInfoDetailContext.ExpInfoDetails.Add(expInfoDetail);
                expInfoDetailContext.SaveChanges();
            }
        }

        private static void Upload(Object source, ElapsedEventArgs e)
        {
            var uploadArray = expInfoDetailList.ToArray();
            expInfoDetailList.Clear();
            AddExpDetail(uploadArray);
        }

        public static void AddExpDetail(ExpInfoDetail[] expInfoDetails)
        {
            using (var expInfoDetailContext = new ExpInfoDetailContext())
            {
                expInfoDetailContext.ExpInfoDetails.AddRange(expInfoDetails);
                expInfoDetailContext.SaveChanges();
            }
        }
    }
}