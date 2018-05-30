using System.IO;

namespace xlsx2proto
{
    class ProtoHelper
    {
        public static string CppPath = "../../../../Tmp/cpp/proto/";
        public static string CsPath = "../../../../Tmp/cs/proto/";
        public static string LuaPath = "../../../../Tmp/lua/proto/";

        public const string ProtoHeadString = "syntax = \"proto2\"\n\npackage TableProto\n";
        public const string MessageHeadFmt = @"message {0}";
        public const string MessageBodyFmt = @"{0} {1} {2} = {3};";

        public static void WriteTableProto(TableInfo table)
        {
            if (table.type == TableType.Client)
            {
                WriteTableProtoByType(table, "cs");
                WriteTableProtoByType(table, "lua");
            }
            else if (table.type == TableType.Server)
            {
                WriteTableProtoByType(table, "cpp");
            }
            else if (table.type == TableType.Mix)
            {
                WriteTableProtoByType(table, "cpp");
                WriteTableProtoByType(table, "cs");
                WriteTableProtoByType(table, "lua");
            }
        }

        private static void WriteTableProtoByType(TableInfo table, string type)
        {
            bool someThingToWrite = false;
            foreach (var item in table.fields)
            {
                if (item.language.Contains(type))
                {
                    someThingToWrite = true;
                    break;
                }
            }
            if (!someThingToWrite) return;
            string tableName = table.name + "Table";
            string infoName = table.name + "Info";
            string path = type == "cpp" ? CppPath : (type == "cs" ? CsPath : LuaPath);
            if (!Directory.Exists(path)) Directory.CreateDirectory(path);
            path += ("TableProto_" + tableName + ".proto");
            int indent = 0;
            if (File.Exists(path)) File.Delete(path);
            File.Create(path).Close();
            StreamWriter sw = new StreamWriter(path);
            sw.WriteLine(ProtoHeadString);
            int messageIndex = 1;
            sw.WriteLine(indent, MessageHeadFmt, infoName);
            sw.WriteLine(indent, "{");
            indent++;
            foreach (var field in table.fields)
            {
                sw.WriteLine(indent, MessageBodyFmt, field.requireTypeString, field.type, field.name, messageIndex++);
            }
            indent--;
            sw.WriteLine(indent, "}");
            messageIndex = 1;
            sw.WriteLine(indent, MessageHeadFmt, tableName);
            sw.WriteLine(indent, "{");
            indent++;
            sw.WriteLine(indent, MessageBodyFmt, "optional", "string", "key", messageIndex++);
            sw.WriteLine(indent, MessageBodyFmt, "repeated", infoName, "list", messageIndex++);
            indent--;
            sw.WriteLine(indent, "}");
            sw.Close();
        }
    }

    static class StreamWriterEx
    {
        public static void WriteLine(this StreamWriter sw, int indent, string value)
        {
            string head = "";
            for (int i = 0; i < indent; i++)
            {
                head += "\t";
            }
            sw.WriteLine(head + value);
        }
        public static void WriteLine(this StreamWriter sw, int indent, string fmt, params object[] args)
        {
            string head = "";
            for (int i = 0; i < indent; i++)
            {
                head += "\t";
            }
            sw.WriteLine(head + string.Format(fmt, args));
        }
    }
}
