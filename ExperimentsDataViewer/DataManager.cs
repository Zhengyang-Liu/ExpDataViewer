using ExperimentsDataViewer.DataSource;
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
        static IDataSource dataSource = new FakeDataSource(AppendData);
        static StringBuilder sb = new StringBuilder();
        //todo
        //static DatabaseManager = new DatabaseManager(ConfigurationSettings.AppSettings( "conString" ), DatabaseNameTextBox.Text, UserNameTextBox.Text, PasswordTextBox.Text);

        public static void Init()
        {
            dataSource.Start();
            InitDatabase();
        }


        private static void AppendData(string data)
        {
            sb.Append(data);
            Console.WriteLine(sb);
        }

        private static void InitDatabase()
        {
        }
    }
}