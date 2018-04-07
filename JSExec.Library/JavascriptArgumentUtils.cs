using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSExec.Library
{
    public static class JavascriptArgumentUtils
    {
        public static object ParseArray(object[] dataArray)
        {
            List<object> newObject = new List<object>();

            foreach (object obj in dataArray)
                newObject.Add(Parse(obj));

            return string.Join(", ", newObject);
        }

        public static object Parse(object data)
        {
            if (data == null)
                return null;

            if (data.GetType() == typeof(String))
                return ParseString(data.ToString());

            return data;
        }

        private static string ParseString(string data)
        {
            return "'" + data.Replace("'", "\\'") + "'";
        }
    }
}
