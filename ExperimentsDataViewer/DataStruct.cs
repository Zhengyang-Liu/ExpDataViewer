﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExperimentsDataViewer
{
    public class DataStruct
    {
        public string dateTime;
        public string data;
        public DataStruct(string dateTime, string data)
        {
            this.dateTime = dateTime;
            this.data = data;
        }
    }
}