using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExperimentsDataViewer
{
    public class ExpDetail
    {
        public string dateTime;
        public string data;
        public ExpDetail(string dateTime, string data)
        {
            this.dateTime = dateTime;
            this.data = data;
        }
    }
}