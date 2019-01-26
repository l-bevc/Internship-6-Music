using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using MusicConnection.Models;

namespace MusicConnection
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString = "Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=MusicDatabase;Integrated Security=true;MultipleActiveResultSets=true;";

            using (var connection = new SqlConnection(connectionString))
            {
                var musicians = connection.Query<Musician>("select * from Musicians");
                var albums= connection.Query<Album>("select * from Albums");
                var songs= connection.Query<Song>("select * from Songs");
                var albumsSongs = connection.Query<AlbumSong>("select * from AlbumSong");

                foreach (var musician in musicians)
                {
                    musician.AlbumsOfMusician=new List<Album>();
                    foreach (var album in albums)
                    {
                        if (album.MusicianId == musician.MusicianId)
                            musician.AlbumsOfMusician.Add(album);
                    }
                }

                foreach (var musician in musicians)
                {
                    Console.WriteLine($"{musician.Name}");
                    foreach (var album in musician.AlbumsOfMusician)
                        Console.WriteLine(album.Name);
                }

                foreach (var album in albums)
                {
                    album.SongsOnAlbum= new List<Song>();
                    foreach (var albumSong in albumsSongs)
                    {
                        if (albumSong.AlbumID == album.AlbumId)
                        {
                            foreach (var song in songs)
                            {
                                if(albumSong.SongID==song.SongId)
                                    album.SongsOnAlbum.Add(song);
                            }
                        }

                    }
                }

                foreach (var song in songs)
                {
                    song.AlbumsOfSong = new List<Album>();
                    foreach (var albumSong in albumsSongs)
                    {
                        if (albumSong.SongID==song.SongId)
                        {
                            foreach (var album in albums)
                            {
                                if (albumSong.AlbumID == album.AlbumId)
                                    song.AlbumsOfSong.Add(album);
                            }
                        }
                    }
                }
                foreach (var album in albums)
                {
                    Console.WriteLine($"Album:{album.Name}");
                    foreach (var song in album.SongsOnAlbum)
                        Console.WriteLine(song.Name);
                }

                foreach (var song in songs)
                {
                    Console.WriteLine($"Pjesma:{song.Name}");
                    foreach (var album in song.AlbumsOfSong)
                        Console.WriteLine(album.Name);
                }
            }



            Console.ReadKey();
        }
    }
}
