using System;

namespace xlsx2proto
{
    static class Log
    {
        public static void Info(object msg)
        {
            Console.WriteLine(msg);
        }
        public static void Info(string fmt, params object[] args)
        {
            Console.WriteLine(fmt, args);
        }

        public static void Error(object msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ResetColor();
        }
        public static void Error(string fmt, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(fmt, args);
            Console.ResetColor();
        }
    }

    static class ShowError
    {
        public static string FieldType_ErrorMessageFmt = "{0}";

        public static string TypeErrorMessage = "意外的字段类型：{0}";
        public static string ValueErrorMessage = "错误的字段值：{0}";
        public static string RequireTypeErrorMessage = "错误的表头：{0}  ,在”{1}“的第 {2} 列";
        public static string FieldNameErrorMessage = "错误的字段名：{0}  ,在”{1}“的第 {2} 列";

        public static void RequireType(string msg, string sheetName, int col)
        {
            Log.Error(RequireTypeErrorMessage, msg, sheetName, col);
        }
        public static void FieldName(string msg, string sheetName, int col)
        {
            Log.Error(FieldNameErrorMessage, msg, sheetName, col);
        }
    }
}
