using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Searcher
{
    class FilesSearcher
    {
        public FilesSearcher(String dst)
        {
        }

        public FilesSearcher() { }

        public List<String> Search(Options options)
        {
            DirectoryInfo di = new DirectoryInfo(options.path);

            return Search(di, options);
        }

        private List<string> Search(DirectoryInfo di, Options options)
        {
            List<string> result = new List<String>();
            foreach (FileInfo file in di.GetFiles())
                if (options.matches(file))
                    result.Add(file.FullName);
            foreach (DirectoryInfo dir in di.GetDirectories())
                foreach (string s in Search(dir, options))
                    result.Add(s);
            return result;
        }
    }
}
