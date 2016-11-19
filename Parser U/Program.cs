using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Parser_U
{
    class PartialComparer : IEqualityComparer<string>
    {
        public string GetComparablePart(string s)
        {
            return s.Split('@')[1];
        }
        public bool Equals(string x, string y)
        {
            return GetComparablePart(x).Equals(GetComparablePart(y));
        }

        public int GetHashCode(string obj)
        {
            return GetComparablePart(obj).GetHashCode();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<string> list = new List<string>();

            var files = "inputData.txt";
            list = new List<string>(File.ReadAllLines(files));

            //var result = list.Distinct().ToList();
            var result = list.Distinct(new PartialComparer()).ToList();
            List<int> indexes = new List<int>();
            for (int i = 0; i < list.Count - 1; i++)
            {
                if (i < result.Count && list.Contains(result[i]))
                {
                    indexes.Add(list.IndexOf(result[i]));
                }
            }
            int delIndex = 0;
            foreach (int i in indexes)
            {
                list.RemoveAt(i + delIndex);
                delIndex--;
            }


            foreach (var v in result)
            {
                File.AppendAllText("finalResult.txt", v + "\n");
            }
            File.Delete("inputData.txt");
            foreach (var v in list)
            {
                File.AppendAllText("inputData.txt", v + "\n");
            }
        }
    }
}