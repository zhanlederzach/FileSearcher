using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Searcher
{
    class Hearts
    {
        static public List<PictureBox> pics = new List<PictureBox>();

        public void Go(object data)
        {
            Image beat = Image.FromFile(@"D:\heart2.png");
            int cnt = 0;
            //while (true)
            //{
                for(int z=0; z<2; z++)
                {
                    for (int i = 0; i < pics.Count; i++)
                    {
                        if (cnt == 3)
                        {
                            pics.ElementAt(i).Image = beat;
                            cnt = 0;
                        }
                        int j = i - 1;
                        if (j < 0) j += pics.Count;
                        pics.ElementAt(i).Image = beat; cnt++;
                        Thread.Sleep(700);
                    }
                    pics[1].Image = null;
                    pics[2].Image = null;
                    pics[0].Image = null;
                }
            //}
        }

        public void Add(PictureBox p)
        {
            pics.Add(p);
        }
    }
}
