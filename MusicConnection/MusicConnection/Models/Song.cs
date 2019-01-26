using System;
using System.Collections.Generic;
using System.Text;

namespace MusicConnection.Models
{
    public class Song
    {
        public int SongId { get; set; }
        public string Name { get; set; }
        public TimeSpan? Size { get; set; }

        public List<Album> AlbumsOfSong { get; set; }
    }
}
