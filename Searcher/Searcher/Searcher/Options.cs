using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Searcher
{
    class Options
    {
        public DateTime start, end;
        public bool usesRegularExpression, searchContents;
        public string exp, path;

        public Options()
        {
            start = Options.ConvertToDateTime("01.01.0001");
            end = DateTime.Today;
            end = new DateTime(end.Year, end.Month, end.Day + 1);
            usesRegularExpression = searchContents = false;
            exp = "";
            path = @"C:\work\test";
        }

        public static DateTime ConvertToDateTime(String time)
        {
            string[] s = time.Split('.');
            int d = int.Parse(s[0]);
            int m = int.Parse(s[1]);
            int y = int.Parse(s[2]);
            return new DateTime(y, m, d);
        }

        public Options setStart(string start)
        {
            Regex date = new Regex(@"^\d{2}\.\d{2}\.\d{2}\.$");
            if (date.IsMatch(start))
            {
                this.start = Options.ConvertToDateTime(start);
            }
            return this;
        }

        public Options setEnd(string end)
        {
            Regex date = new Regex(@"^\d{2}\.\d{2}\.\d{2}\.$");
            if (date.IsMatch(end))
                this.end = Options.ConvertToDateTime(end);
            return this;
        }

        public Options setRegExp(bool usesRegularExpression)
        {
            this.usesRegularExpression = usesRegularExpression;
            return this;
        }

        public Options setExpression(string exp)
        {
            this.exp = exp;
            return this;
        }

        public Options setPath(string path)
        {
            this.path = path;
            return this;
        }

        public Options setSearchContents(bool searchContents)
        {
            this.searchContents = searchContents;
            return this;
        }

        public bool matches(FileSystemInfo file1)
        {
            //Console.WriteLine("Checking " + file1.FullName + " with exp=" + exp);
            //Console.WriteLine(file1.CreationTime.ToString() + " " + start.ToString() + " " + end.ToString());
            if (file1.CreationTime.CompareTo(start) < 0 || end.CompareTo(file1.CreationTime) < 0) return false;
            if (usesRegularExpression)
            {
                Regex e = new Regex(exp);
                if (e.IsMatch(file1.Name)) return true;
            } else
            {
                if (file1.Name.Contains(exp)) return true;
            }
            if (searchContents)
            {
                string name = file1.Name;
                FileStream fs = new FileStream(name, FileMode.OpenOrCreate, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                Regex e = new Regex(@"^$");
                string inputLine = sr.ReadToEnd();
                string[] inputs = inputLine.Split(' ');
                sr.Close();
                fs.Close();
                if (inputs.Contains(name)) return true;
            }
            return false;
        }
    }
}
