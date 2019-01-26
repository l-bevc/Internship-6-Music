using System;
using System.Collections.Generic;
using System.Text;

namespace MusicConnection.Models
{
    public class Album
    {
        public int AlbumId { get; set; }
        public string Name { get; set; }
        public DateTime? TimeOfPublish { get; set; }
        public int MusicianId { get; set; }

        public List<Song> SongsOnAlbum { get; set; }
    }
}
