using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace ExperimentsDataViewer.DataSource
{
    interface IDataSource
    {
        void Start();

        void OnClose();
    }
}
