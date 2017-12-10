using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; using System.Windows.Media;
using System.Threading.Tasks;

namespace MyMusicPlayer
{
    [Serializable]
    public class PlayList
    {
        public string Name { get; set; }
        public List<PlayItem> Items { get; set; }

        public PlayList()
        {
            Items = new List<PlayItem>();
            Name = "Unknown";
        }

        public PlayList(string path)
        {
            Items = new List<PlayItem>();

            string[] files = System.IO.Directory.GetFiles(path, "*.mp3", System.IO.SearchOption.AllDirectories);

            foreach (var i in files)
            {
                TagLib.File currentFile = TagLib.File.Create(i);
                Items.Add(new PlayItem(currentFile));
            }

            Name = new System.IO.DirectoryInfo(path).Name;
        }

        public PlayList(List<PlayItem> items)
        {
            Items = items;
        }

        public void RandomTheList()
        {
            Random rand = new Random();
            List<int> array = new List<int>();

            for (int i = 0; i < Items.Count; i++)
            {
                int v = rand.Next(0, Items.Count - 1);

                if (array.Contains(v))
                {
                    while (!array.Contains(v))
                    {
                        v = rand.Next(0, Items.Count - 1);
                    }
                }

                array.Add(v);
                PlayItem item = Items[i];
                Items[i] = Items[array.Last()];
                Items[array.Last()] = item;
            }
        }

        public double TotalTimeInSeconds()
        {
            double length = 0;
            for (int i = 0; i < Items.Count; i++)
            {
                length += Items[i].Seconds;
            }
            return length;
        }

        public long TotalSizeInBytes()
        {
            long length = 0;
            for (int i = 0; i < Items.Count; i++)
            {
                length += Items[i].Size;
            }
            return length;
        }
    }
}
