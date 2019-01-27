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

            var musiciansList = new SqlConnection(connectionString).Query<Musician>("select * from Musicians").ToList();
            var musicianList= musiciansList.OrderBy(musician => musician.Name);
            Console.WriteLine("Izvođači po imenu:");
            foreach (var musician in musicianList)
                Console.WriteLine($"{musician.Name}");   
            Console.WriteLine("Glazbenici određene nacionalnosti:");
            foreach (var musician in musicianList)
            {
                Console.Write(musician.Nationality+": ");
                var musicianOfSomeNationality =
                    musicianList.Where(nationality => nationality.Nationality == musician.Nationality);
                foreach (var musicianNationality in musicianOfSomeNationality)
                    Console.WriteLine(musicianNationality.Name);
            }

            Console.WriteLine("Albumi grupirani po godini izadanja: ");
            var albumList = new SqlConnection(connectionString).Query<Album>("select * from Albums").ToList();
            var albumGroups =
                from time in albumList
                group time by time.TimeOfPublish.GetValueOrDefault().Year into g
                select new { Year = g.Key, AlbumList = g };

            foreach (var g in albumGroups)
            {
                Console.WriteLine("Albumi godine: {0} ", g.Year);
                foreach (var album in g.AlbumList)
                    Console.WriteLine(album.Name+" "+ (musicianList.FirstOrDefault((id => id.MusicianId == album.MusicianId))).Name);
            }

            Console.WriteLine("Albumi sa zadanim tekstom:");
            var tekst = "the";
            var albumsWithSomeTextInName = albumList.Where(album => album.Name.ToLower().Contains(tekst));
            foreach (var album in albumsWithSomeTextInName)
                Console.WriteLine(album.Name);

            var songList = new SqlConnection(connectionString).Query<Song>("select * from Songs").ToList();
            var albumSongs= new SqlConnection(connectionString).Query<AlbumSong>("select * from AlbumSong").ToList();
            Console.WriteLine("Albumi sa ukupnim trajanjem: ");
            var songAlbum =
                from album in albumList
                join albumWithSong in albumSongs on album.AlbumId equals albumWithSong.AlbumID
                join song in songList on albumWithSong.SongID equals song.SongId into listAlbumsSongs
                group listAlbumsSongs by album;
            foreach (var album in songAlbum)
            {
                Console.WriteLine($"{album.Key.Name} {album.Sum(s=>s.Sum(song =>song.Size.Value.Minutes*60+ song.Size.Value.Seconds))} sekundi");
            }

            var songToFind = songList[0];
            Console.WriteLine($"Pjesma {songToFind.Name} se nalazi na albumima:");
            var findAlbum =
                from song in songList
                join albumWithSong in albumSongs on song.SongId equals albumWithSong.SongID
                join albums in albumList on albumWithSong.AlbumID equals albums.AlbumId into listAlbumsSongs
                select new { name = song.Name, ListOfAlbums=listAlbumsSongs};
            foreach (var album in findAlbum)
            {
                if (album.name == songToFind.Name)
                {
                    foreach (var albumName in album.ListOfAlbums)
                    {
                        Console.WriteLine(albumName.Name);
                    }
                }
            }

            var chosenMusician = musiciansList[1].Name;
            var chosenYear = 2000;
            var findSongs =
                from musician in musiciansList
                join album in albumList on musician.MusicianId equals album.MusicianId into listAlbums
                from albumName in listAlbums.Where(album => album.TimeOfPublish.Value.Year > chosenYear)
                join albumWithSong in albumSongs on albumName.AlbumId equals albumWithSong.AlbumID
                join songs in songList on albumWithSong.SongID equals songs.SongId into listSongs
                select new { name = musician.Name, ListOfSongs = listSongs };
            Console.WriteLine($"Pjesme glazbenika {chosenMusician} izdane nakon {chosenYear}.:");
            foreach (var song in findSongs)
            {
                if (song.name == chosenMusician)
                {
                    foreach (var songName in song.ListOfSongs)
                    {
                        Console.WriteLine(songName.Name);
                    }
                }
            }


            Console.ReadKey();
        }
    }
}
