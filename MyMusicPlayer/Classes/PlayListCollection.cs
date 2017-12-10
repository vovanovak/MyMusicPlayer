using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace MyMusicPlayer
{
    [Serializable]
    public class PlayListCollection
    {
        public List<PlayList> playLists { get; set; }
        public PlayList currentPlayList { get; set; }
        public PlayItem currentPlayItem { get; set; }

        public PlayListCollection()
        {
            try
            {
                playLists = new List<PlayList>();
                playLists.Add(new PlayList());
                currentPlayList = playLists[0];
                currentPlayItem = new PlayItem();
                currentPlayItem = currentPlayList.Items[0];
            }
            catch
            {

            }
        }

        public PlayListCollection(string path)
        {
            try
            {
                BinaryFormatter bf = new BinaryFormatter();
                PlayListCollection col = (PlayListCollection)bf.Deserialize(new System.IO.FileStream(path, System.IO.FileMode.Open, System.IO.FileAccess.Read));
                this.playLists = col.playLists;
                this.currentPlayList = col.currentPlayList;
                this.currentPlayItem = col.currentPlayItem;
            }
            catch
            {
                playLists = new List<PlayList>();
                playLists.Add(new PlayList());
                currentPlayItem = new PlayItem();
                currentPlayList = playLists[0];
            }
        }

        public void Save(string path)
        {
           
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(new System.IO.FileStream(path, System.IO.FileMode.Create, System.IO.FileAccess.Write), this);
        }
    }
}
