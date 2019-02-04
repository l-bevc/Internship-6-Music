using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Sockets;
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
                var musicians = connection.Query<Musician>("select * from Musicians").ToList();
                var albums = connection.Query<Album>("select * from Albums").ToList();
                var songs = connection.Query<Song>("select * from Songs").ToList();
                var albumsSongs = connection.Query<AlbumSong>("select * from AlbumSong").ToList();
                foreach (var musician in musicians)
                {
                    musician.AlbumsOfMusician = new List<Album>();
                    foreach (var album in albums)
                    {
                        if (album.MusicianId == musician.MusicianId)
                            musician.AlbumsOfMusician.Add(album);
                    }
                }

                foreach (var album in albums)
                {
                    album.SongsOnAlbum = new List<Song>();
                    foreach (var albumSong in albumsSongs)
                    {
                        if (albumSong.AlbumID == album.AlbumId)
                        {
                            foreach (var song in songs)
                            {
                                if (albumSong.SongID == song.SongId)
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
                        if (albumSong.SongID == song.SongId)
                        {
                            foreach (var album in albums)
                            {
                                if (albumSong.AlbumID == album.AlbumId)
                                    song.AlbumsOfSong.Add(album);
                            }
                        }
                    }
                }

                var musicianList = musicians.OrderBy(musician => musician.Name).ToList();
                Console.WriteLine("Izvođači po imenu:");
                musicianList.ForEach(musician => Console.WriteLine(musician.Name));
                Console.WriteLine("Glazbenici određene nacionalnosti:");
                foreach (var musician in musicianList)
                {
                    Console.Write(musician.Nationality + ": ");
                    var musicianOfSomeNationality =
                        musicianList.Where(nationality => nationality.Nationality == musician.Nationality).ToList();
                    musicianOfSomeNationality.ForEach(element => Console.WriteLine(element.Name));
                }

                Console.WriteLine("Albumi grupirani po godini izadanja: ");
                var albumGroups =
                    albums.GroupBy(album => album.TimeOfPublish.GetValueOrDefault().Year)
                        .Select(g => new {Year = g.Key, AlbumList = g});

                foreach (var g in albumGroups)
                {
                    Console.WriteLine("Albumi godine: {0} ", g.Year);
                    foreach (var album in g.AlbumList)
                        Console.WriteLine(album.Name + " " +
                                          (musicianList.FirstOrDefault((id => id.MusicianId == album.MusicianId))).Name);
                }

                Console.WriteLine("Albumi sa zadanim tekstom:");
                var tekst = "the";
                var albumsWithSomeTextInName = albums.Where(album => album.Name.ToLower().Contains(tekst)).ToList();
                albumsWithSomeTextInName.ForEach(element => Console.WriteLine(element.Name));
                Console.WriteLine("Albumi sa ukupnim trajanjem: ");
                var songAlbum =
                    albums.GroupBy(album => album.Name);
                foreach (var album in songAlbum)
                {
                    Console.WriteLine(
                        $"{album.Key} {album.Sum(s => s.SongsOnAlbum.Sum(song => song.Size.Value.Minutes * 60 + song.Size.Value.Seconds))} sekundi");
                }

                var songToFind = songs[0];
                Console.WriteLine($"Pjesma {songToFind.Name} se nalazi na albumima:");
                foreach (var album in songToFind.AlbumsOfSong)
                   Console.WriteLine(album.Name);

                var chosenMusician = musicians[1];
                var chosenYear = 2000;
                var findSongs =
                    albums.Where(album =>
                        album.MusicianId == chosenMusician.MusicianId &&
                        album.TimeOfPublish.GetValueOrDefault().Year > chosenYear).Select(g => new {List=g.SongsOnAlbum});
                Console.WriteLine($"Pjesme glazbenika {chosenMusician.Name} izdane nakon {chosenYear}.:");
                foreach (var song in findSongs)
                {
                    foreach (var songElement in song.List)
                        Console.WriteLine(songElement.Name);
                }
            }
            Console.ReadKey();
        }
    }
}
