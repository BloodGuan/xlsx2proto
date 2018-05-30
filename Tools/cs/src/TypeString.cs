using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xlsx2proto
{
    static class FieldType
    {
        public const string Bool = "bool";
        public const string UInt32 = "uint32";
        public const string Int32 = "int32";
        public const string Float = "float";
        public const string String = "string";

        public static string ErrorMessageFmt
        {
            get
            {
                return ShowError.FieldType_ErrorMessageFmt;
            }
            set
            {
                ShowError.FieldType_ErrorMessageFmt = value;
            }
        }

        static void TypeError(string msg)
        {
            Log.Error(ErrorMessageFmt, string.Format(ShowError.TypeErrorMessage, msg));
        }
        static void ValueError(string msg)
        {
            Log.Error(ErrorMessageFmt, string.Format(ShowError.ValueErrorMessage, msg));
        }

        public static bool ParseType(ref string ret, string str)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(str.Trim()))
            {
                TypeError("NULL");
                return false;
            }
            switch (str.Trim().ToLower())
            {
                case "bool":
                case "boolean":
                    ret = FieldType.Bool;
                    return true;
                case "uint":
                case "uint32":
                    ret = FieldType.UInt32;
                    return true;
                case "int":
                case "int32":
                    ret = FieldType.Int32;
                    return true;
                case "float":
                    ret = FieldType.Float;
                    return true;
                case "string":
                    ret = FieldType.String;
                    return true;
                default:
                    TypeError(str);
                    return false;
            }
        }
        public static bool ParseValue(ref object ret, string typeStr, string valueStr)
        {
            string fieldType = null;
            if (!FieldType.ParseType(ref fieldType, typeStr))
            {
                return false;
            }
            switch (fieldType)
            {
                case FieldType.Bool:
                    return ParseBool(ref ret, valueStr);
                case FieldType.UInt32:
                    return ParseUInt32(ref ret, valueStr);
                case FieldType.Int32:
                    return ParseInt32(ref ret, valueStr);
                case FieldType.Float:
                    return ParseFloat(ref ret, valueStr);
                case FieldType.String:
                    return ParseString(ref ret, valueStr);
                default:
                    TypeError(fieldType);
                    return false;
            }
        }
        static bool ParseBool(ref object ret, string str)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(str.Trim()))
            {
                ValueError("NULL");
                return false;
            }
            switch (str.Trim().ToLower())
            {
                case "yes":
                case "y":
                case "1":
                    ret = true;
                    return true;
                case "no":
                case "n":
                case "0":
                    ret = false;
                    return true;
                default:
                    ValueError(str);
                    return false;
            }
        }
        static bool ParseUInt32(ref object ret, string str)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(str.Trim()))
            {
                ValueError("NULL");
                return false;
            }
            str = str.Trim();
            uint parse;
            if (uint.TryParse(str, out parse))
            {
                ret = parse;
                return true;
            }
            else
            {
                ValueError(str);
                return false;
            }
        }
        static bool ParseInt32(ref object ret, string str)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(str.Trim()))
            {
                ValueError("NULL");
                return false;
            }
            str = str.Trim();
            int parse;
            if (int.TryParse(str, out parse))
            {
                ret = parse;
                return true;
            }
            else
            {
                ValueError(str);
                return false;
            }
        }
        static bool ParseFloat(ref object ret, string str)
        {
            if (string.IsNullOrEmpty(str) || string.IsNullOrEmpty(str.Trim()))
            {
                ValueError("NULL");
                return false;
            }
            str = str.Trim();
            float parse;
            if (float.TryParse(str, out parse))
            {
                ret = parse;
                return true;
            }
            else
            {
                ValueError(str);
                return false;
            }
        }
        static bool ParseString(ref object ret, string str)
        {
            ret = str;
            return true;
        }
    }
}
