using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ExperimentsDataViewer
{
    public class DatabaseManager
    {
        SqlConnection connection;

        public DatabaseManager(string serverName, string databaseName, string userName, string password)
        {
            string connectionString = string.Format("Data Source={0}; Initial Catalog={1}; User ID={2}; Password={3}", serverName, databaseName, userName, password);
            connection = new SqlConnection(connectionString);
            connection.Open();
        }

        public void InsertExpDetail(string tableName, List<ExpDetail> dataList)
        {
            DataTable table = new DataTable("ParentTable");

            DataColumn timeColumn = new DataColumn();
            timeColumn.DataType = Type.GetType("System.String");
            timeColumn.ColumnName = "time";
            timeColumn.Unique = true;

            DataColumn accelerationColumn = new DataColumn();
            accelerationColumn.DataType = Type.GetType("System.Double");
            accelerationColumn.ColumnName = "acceleration";

            table.Columns.Add(timeColumn);
            table.Columns.Add(accelerationColumn);

            foreach (ExpDetail item in dataList)
            {
                try
                {
                    DataRow row = table.NewRow();
                    row["time"] = item.dateTime;
                    row["acceleration"] = decimal.Parse(item.data);
                    table.Rows.Add(row);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

            }

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
            {
                bulkCopy.DestinationTableName = "[" + tableName + "]";

                try
                {
                    // Write from the source to the destination.
                    bulkCopy.WriteToServer(table);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
}
