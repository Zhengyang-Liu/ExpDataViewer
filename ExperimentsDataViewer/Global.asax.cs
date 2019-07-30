using ExperimentsDataViewer.DataSource;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace ExperimentsDataViewer
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //IDataSource dataSource = new FakeDataSource(AppendData);
        // = new ArduinoDataSource(AppendData, portName, baudRate);

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            //DataManager.Init();
        }
    }
}
