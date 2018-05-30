using Aspose.Cells;
using System.Collections.Generic;

namespace xlsx2proto
{
    class Program
    {
        static void Main(string[] args)
        {
            List<TableInfo> tables = new List<TableInfo>();
            var workbook = ExcelHelper.OpenWorkbook("../../../../goods_info.xls");
            foreach (Worksheet item in workbook.Worksheets)
            {
                TableInfo add = TableInfo.Create(item);
                if (add != null) tables.Add(add);
            }

            //Log.Info("");
            //Log.Info("");
            //Log.Info("");
            //Log.Info("");

            //foreach (var table in tables)
            //{
            //    Log.Info(table.name);
            //    Log.Info("\ttype:" + table.type);
            //    Log.Info("\tfields:" + table.fields.Count);
            //    foreach (var field in table.fields)
            //    {
            //        Log.Info("\t\t" + field.name + "(" + field.col + ")");
            //        Log.Info("\t\t\tlanguage:" + field.language);
            //        Log.Info("\t\t\tRequireType:" + field.requireType);
            //        Log.Info("\t\t\tFieldType:" + field.type);
            //    }
            //}

            foreach (var table in tables)
            {
                ProtoHelper.WriteTableProto(table);
                Log.Info(table.name + "打表完成！");
            }

            System.Console.ReadKey();
        }
    }
}
