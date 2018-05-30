using Aspose.Cells;
using System.Collections.Generic;

namespace xlsx2proto
{
    class Program
    {
        static void Main(string[] args)
        {
            List<TableInfo> tables = new List<TableInfo>();
            var workbook = ExcelHelper.OpenWorkbook("D:/xlsx2proto/goods_info.xls");
            foreach (Worksheet item in workbook.Worksheets)
            {
                TableInfo add = TableInfo.Create(item);
                if (add != null) tables.Add(add);
            }

            foreach (var item in tables)
            {
                Log.Info(item.name);
            }

            System.Console.ReadKey();
        }
    }
}
