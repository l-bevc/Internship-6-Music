using System;
using System.Collections.Generic;
using System.Text;

namespace MusicConnection.Models
{
    public class Musician
    {
        public int MusicianId { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }

        public List<Album> AlbumsOfMusician { get; set; }
    }
}
