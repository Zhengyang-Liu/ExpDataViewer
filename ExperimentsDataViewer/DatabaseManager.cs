using ExperimentsDataViewer.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace ExperimentsDataViewer
{
    public class DatabaseManager
    {
        private ExpInfoDetailContext expInfoDetailContext = new ExpInfoDetailContext();

        public DatabaseManager()
        {
        }

        public void AddExpDetail(ExpInfoDetail expInfoDetail)
        {
            this.expInfoDetailContext.ExpInfoDetails.Add(expInfoDetail);
            this.expInfoDetailContext.SaveChanges();
        }

        public void AddExpDetail(ExpInfoDetail[] expInfoDetails)
        {
            this.expInfoDetailContext.ExpInfoDetails.AddRange(expInfoDetails);
            this.expInfoDetailContext.SaveChanges();
        }

        //public void InsertExpDetail(string tableName, List<ExpDetail> dataList)
        //{
        //    DataTable table = new DataTable("ParentTable");

        //    DataColumn timeColumn = new DataColumn();
        //    timeColumn.DataType = Type.GetType("System.String");
        //    timeColumn.ColumnName = "time";
        //    timeColumn.Unique = true;

        //    DataColumn accelerationColumn = new DataColumn();
        //    accelerationColumn.DataType = Type.GetType("System.Double");
        //    accelerationColumn.ColumnName = "acceleration";

        //    table.Columns.Add(timeColumn);
        //    table.Columns.Add(accelerationColumn);

        //    foreach (ExpDetail item in dataList)
        //    {
        //        try
        //        {
        //            DataRow row = table.NewRow();
        //            row["time"] = item.dateTime;
        //            row["acceleration"] = decimal.Parse(item.data);
        //            table.Rows.Add(row);
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e);
        //        }

        //    }

        //    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
        //    {
        //        bulkCopy.DestinationTableName = "[" + tableName + "]";

        //        try
        //        {
        //            // Write from the source to the destination.
        //            bulkCopy.WriteToServer(table);
        //        }
        //        catch (Exception ex)
        //        {
        //            Console.WriteLine(ex.Message);
        //        }
        //    }
        //}
    }
}
