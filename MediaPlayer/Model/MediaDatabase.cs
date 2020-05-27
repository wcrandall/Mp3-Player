using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SQLitePCL;

namespace MediaPlayer
{
    class MediaDatabase
    {
        public static string path = "";

        public static void updateDatabase()
        {



                if(MediaDatabase.checkExistance() == true)
                {
                    using (var conn = new SQLite.SQLiteConnection(loadConnectionString()))
                    {
                        string query = "DROP TABLE Song";
                        conn.ExecuteScalar<string>(query);
                    }
                    
                }

         

            // if database doesn't exist create
            if (MediaDatabase.checkExistance() != true)
            {
                createTable();
                string[] artists = Directory.GetDirectories(Properties.Settings.Default.databasePath);
                foreach (string oneArtist in artists)
                {
                    string[] albums = Directory.GetDirectories(oneArtist);
                    foreach (string oneAlbum in albums)
                    {
                        DirectoryInfo dir = new DirectoryInfo(oneAlbum);


                        //get files 
                        foreach (FileInfo flinfo in dir.GetFiles())
                        {

                            string name = flinfo.Name;

                            if (name.Contains(".mp3") || name.Contains(".wav"))
                            {



                                string pathAndName = oneAlbum + "\\" + name;
                                var tfile = TagLib.File.Create(@pathAndName);
                                string title = tfile.Tag.Title;
                                //MessageBox.Show(title);
                                string album = tfile.Tag.Album;
                                //MessageBox.Show(album);
                                string artist = tfile.Tag.FirstAlbumArtist;
                                TimeSpan duration = tfile.Properties.Duration;

                                Song song = new Song(name, artist, title, album, duration, pathAndName);

                                insertMp3(song);
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }
            }
        }

        // Gets the path and name of database 
        private static string loadConnectionString()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Mediadb.db");
            return path;
        }

        public static bool createTable()
        {
            using (var conn = new SQLite.SQLiteConnection(loadConnectionString()))
            {
                if (conn.CreateTable<Song>()!= 0)
                {
                    return true;
                }
            }
            return false; 
        }

        public static bool insertMp3(Song song)
        {
            using (var conn = new SQLite.SQLiteConnection(loadConnectionString()))
            {
                if (song.ID == 0)
                {
                    if(conn.Insert(song)!=0)
                    {
                        return true; 
                    }
                }
                else
                {
                    if(conn.Update(song)!=0)
                    {
                        return true;
                    }
                }
            }
            return false; 
        }

        public static bool deleteMp3(Song song)
        {
            using(var conn = new SQLite.SQLiteConnection(loadConnectionString()))
            {
                if(conn.Delete(song)!= 0)
                {
                    return true;
                }
            }

            return false; 
        }

        public static List<Song> getSongs()
        {
            using(var conn = new SQLite.SQLiteConnection(loadConnectionString()))
            {
                return conn.Table<Song>().ToList();
            }
        }

        public static List<string> getArtists()
        {
            using (var conn= new SQLite.SQLiteConnection(loadConnectionString()))
            {
                string query = "SELECT DISTINCT artist FROM Song";
                List<Song> songsWithDifferentArtists = conn.Query<Song>(query);
                List<string> artists = new List<string>();
                foreach(Song song in songsWithDifferentArtists )
                {
                    artists.Add(song.artist);

                }
                return artists; 
            }
            
        }
        public static List<string> getAlbums()
        {
            using(var conn = new SQLite.SQLiteConnection(loadConnectionString()))
            {
                string query = "SELECT DISTINCT album FROM song";
                List<Song> songsWithDifferentAlbums = conn.Query<Song>(query);
                List<string> albums = new List<string>(); 
                foreach(Song song in songsWithDifferentAlbums)
                {
                    albums.Add(song.album); 
                }
                return albums;
            }
        }

        public static List<string> getAlbumsByArtist(string artist)
        {
            using (var conn = new SQLite.SQLiteConnection(loadConnectionString()))
            {
                string query = "SELECT DISTINCT album FROM Song WHERE artist = ?";
                List<Song> songDistinctAlbum = conn.Query<Song>(query, artist);
                List<string> albums = new List<string>(); 
                foreach(Song song in songDistinctAlbum)
                {
                    albums.Add(song.album); 
                }

                return albums; 
            }
        }

        public static List<Song> getSongsByArtist(string artist)
        {
            using (var conn = new SQLite.SQLiteConnection(loadConnectionString()))
            {
                string query = "SELECT * FROM Song WHERE artist=?";
                List<Song> songsByArtist = conn.Query<Song>(query, artist);

                return songsByArtist;
            }
        }

        public static List<Song> getSongsByAlbum(string album)
        {
            using(var conn = new SQLite.SQLiteConnection(loadConnectionString()))
            {
                string query = "SELECT * FROM Song WHERE album = ?";
                List<Song> songsByAlbum = conn.Query<Song>(query, album);
                return songsByAlbum; 
            }

        }
        public static List<Song> getSongsByArtistAndAlbum(string artist, string album)
        {
            using (var conn = new SQLite.SQLiteConnection(loadConnectionString()))
            {
                string query = "SELECT * FROM Song WHERE artist=? AND album=?";
                List<Song> songsByArtistAndAlbum = conn.Query<Song>(query,artist,album);

                return songsByArtistAndAlbum;
            }
        }


        public static bool checkExistance()
        {
            using(var conn = new SQLite.SQLiteConnection(loadConnectionString()))
            {
                var tableExistsQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Song';";
                var result = conn.ExecuteScalar<string>(tableExistsQuery);
                if (result == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public static Song getSong(int id)
        {
            using(var conn = new SQLite.SQLiteConnection(loadConnectionString()))
            {
                return conn.Table<Song>().Where(i => i.ID == id).FirstOrDefault();
            }
        }
    }
}
