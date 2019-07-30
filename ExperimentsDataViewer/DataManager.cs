using ExperimentsDataViewer.DataSource;
using ExperimentsDataViewer.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;

namespace ExperimentsDataViewer
{
    public static class DataManager
    {
        public static bool runningExp = false;
        public static int expNo;

        static IDataSource dataSource = new FakeDataSource(AddExpDetail);
        static ExpInfoDetailContext expInfoDetailContext = new ExpInfoDetailContext();
        static RunningExpContext runningExpContextDb = new RunningExpContext();

        public static void Init()
        {
            dataSource.Start();
            if(HasRunningExp())
            {
                runningExp = true;
            }
        }

        private static bool HasRunningExp()
        {
            return runningExpContextDb.RunningExp.Any();
        }

        public static void AddExpDetail(ExpInfoDetail expInfoDetail)
        {
            if(runningExp)
            {
                expInfoDetail.ExpNo = expNo;
                expInfoDetailContext.ExpInfoDetails.Add(expInfoDetail);
                expInfoDetailContext.SaveChanges();
            }
        }

        public static void AddExpDetail(ExpInfoDetail[] expInfoDetails)
        {
            if(runningExp)
            {
                foreach(ExpInfoDetail expInfoDetail in expInfoDetails)
                {
                    expInfoDetail.ExpNo = expNo;
                }
                expInfoDetailContext.ExpInfoDetails.AddRange(expInfoDetails);
                expInfoDetailContext.SaveChanges();
            }
        }
    }
}