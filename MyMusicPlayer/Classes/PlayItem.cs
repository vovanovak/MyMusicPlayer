using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Media;
using System.Threading.Tasks;
using System.Windows;


namespace MyMusicPlayer
{
    [Serializable]
    public class PlayItem
    {
        public string Name { get; set; }
        public string Compositor { get; set; }
        public long Size { get; set; }
        public double Seconds { get; set; }
        public string Extension { get; set; }
        public string Path { get; set; }
        public int AudioBitrate { get; set; }
        public int SampleRate { get; set; }

        public PlayItem()
        {
            this.Name = "Unknown";
            this.Compositor = "Unknown";
            this.Extension = "Unknown"; 
            this.Size = 0;
            this.Seconds = 0;
        }

        public PlayItem(TagLib.File file)
        {
            Name = file.Tag.Title;
            Compositor = file.Tag.FirstPerformer;
            Size = new System.IO.FileInfo(file.Name).Length;
            Seconds = file.Properties.Duration.TotalSeconds;
            Extension = new System.IO.FileInfo(file.Name).Extension;
            Path = file.Name;
            AudioBitrate = file.Properties.AudioBitrate;
            SampleRate = file.Properties.AudioSampleRate;
        }

        public override string ToString()
        {
            return " .:: " + Compositor + " - " + Name + " ::" + Extension.ToUpper().Replace('.', ' ') + " :: " + SampleRate / 1000 + "kHz, " + AudioBitrate + " kbps, " + BytesToString(Size) + "::.          ";
        }

        public static String BytesToString(long byteCount)
        {
            string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" };
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }

        public string ShortToString()
        {
            return Extension.ToUpper().Replace('.', ' ') + " :: " + SampleRate / 1000 + "kHz, " + AudioBitrate + " kbps, " + BytesToString(Size);
        }
    }
}
