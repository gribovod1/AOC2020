using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnyThings
{
    class Parse
    {
        public static Record parseToRecord(string line, string divider)
        {
            var result = new Record();
            return result;
        }

        public static List<int> parseToList(string line, string divider)
        {
            var result = new List<int>();
            return result;
        }

        public static List<KeyValuePair<string, int>> parseToKeyValue(string line, string keyValueDivider, string divider)
        {
            var result = new List<KeyValuePair<string, int>>();
            int prevEnd = 0;
            int index = line.IndexOf(keyValueDivider);
            while (index >= 0)
            {
                int end = line.IndexOf(divider, index);
                if (end < 0)
                    end = line.Length;
                result.Add(new KeyValuePair<string, int>(line.Substring(prevEnd, index - prevEnd), int.Parse(line.Substring(index+keyValueDivider.Length, end - index + keyValueDivider.Length))));
                prevEnd = end;
                index = line.IndexOf(keyValueDivider, prevEnd);
            }
            return result;
        }

        public struct Record
        {
        }
    }
}
