using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace MediaPlayer
{
    class MediaDatabase
    {

        public static Boolean updateDatabase()
        {

            /* if the path has been changed and the database
             * exists drop all database data in Song table */
            if (Properties.Settings.Default.oldDatabasePath != Properties.Settings.Default.databasePath)
            {
                if (MediaDatabase.checkExistance() == true)
                {
                    using (var conn = new SQLite.SQLiteConnection(loadConnectionString()))
                    {
                        string query = "DROP TABLE Song";
                        conn.ExecuteScalar<string>(query);
                    }

                }
            }



            // if database doesn't exist create
            if (MediaDatabase.checkExistance() != true)
            {
                // in sqlite-net-pcl creating a table will create the database if it doesn't exist
                createTable();
            }


            string[] artists;
            
            // if we cannot get directories, access to one has been denied. IE the update failed
            try
                {
                    artists = Directory.GetDirectories(Properties.Settings.Default.databasePath);
                }
                catch
                {
                    return false;
                }
                    

                    // for each artist found get their albums
                    foreach (string oneArtist in artists)
                    {
                        string[] albums; 
                        // if access to a album directory is denied, the update was unsuccessful
                        try
                        {
                            albums = Directory.GetDirectories(oneArtist);
                        }
                        catch
                        {
                            return false; 
                        }
                        // for each album find the songs 
                        foreach (string oneAlbum in albums)
                        {
                            DirectoryInfo dir = new DirectoryInfo(oneAlbum);
                            
                            // If access to a song in an album directory is denied, the update was unsuccessful 
                            try
                            {
                                Directory.GetFiles(oneAlbum);
                            }
                            catch
                            {
                                return false; 
                            }

                            // going through individual files in album directory and adding them to a database 
                            foreach (FileInfo flinfo in dir.GetFiles())
                            {

                                string name = flinfo.Name;
                                // ensuring the file is a .mp3 or .wav
                                if (name.Contains(".mp3") || name.Contains(".wav"))
                                {



                                    string pathAndName = oneAlbum + "\\" + name;
                                    // if the song isn't in the database, add it.
                                    // if it is, do nothing. 
                                    if (!checkIfSongIsInDatabase(pathAndName))
                                    {
                                        var tfile = TagLib.File.Create(@pathAndName);
                                        string title = tfile.Tag.Title;
                                        string album = tfile.Tag.Album;
                                        string artist = tfile.Tag.FirstAlbumArtist;
                                        TimeSpan duration = tfile.Properties.Duration;

                                        Song song = new Song(name, artist, title, album, duration, pathAndName);
                                        insertMp3(song);
                                    }
                                }
                                else
                                {
                                    continue;
                                }
                            }
                        }
                    }
                
            
            
            return true;
        }

        // Gets the path and name of database 
        private static string loadConnectionString()
        {
            string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Mediadb.db");
            return path;
        }

        // creates tables which in sqlite-net-pcl creates the database if it doesn't already exist
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

        // insert mp3 data into database
        public static bool insertMp3(Song song)
        {
            using (var conn = new SQLite.SQLiteConnection(loadConnectionString()))
            {
                    // if the insert is successful return true
                    if(conn.Insert(song)!=0)
                    {
                        return true; 
                    }
            }
            // if the insert was not successful return false
            return false; 
        }


        // gets all songs from the database
        public static List<Song> getSongs()
        {
            using(var conn = new SQLite.SQLiteConnection(loadConnectionString()))
            {
                return conn.Table<Song>().ToList();
            }
        }

        // gets all Distinct artists from the database
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
        
        // gets all distinct albums from database
        public static List<string> getAlbums()
        {
            using(var conn = new SQLite.SQLiteConnection(loadConnectionString()))
            {
                string query = "SELECT album FROM (SELECT DISTINCT artist, album FROM song)";
                List<Song> songsWithDifferentAlbums = conn.Query<Song>(query);
                List<string> albums = new List<string>(); 
                foreach(Song song in songsWithDifferentAlbums)
                {
                    albums.Add(song.album); 
                }
                return albums;
            }
        }

        // gets all albums for given artist
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

        // gets all songs for given artist
        public static List<Song> getSongsByArtist(string artist)
        {
            using (var conn = new SQLite.SQLiteConnection(loadConnectionString()))
            {
                string query = "SELECT * FROM Song WHERE artist=?";
                List<Song> songsByArtist = conn.Query<Song>(query, artist);

                return songsByArtist;
            }
        }

        // gets all songs for given album
        public static List<Song> getSongsByAlbum(string album)
        {
            using(var conn = new SQLite.SQLiteConnection(loadConnectionString()))
            {
                string query = "SELECT * FROM Song WHERE album = ?";
                List<Song> songsByAlbum = conn.Query<Song>(query, album);
                return songsByAlbum; 
            }

        }
        
        // gets all songs for given artist and album 
        public static List<Song> getSongsByArtistAndAlbum(string artist, string album)
        {
            using (var conn = new SQLite.SQLiteConnection(loadConnectionString()))
            {
                string query = "SELECT * FROM Song WHERE artist=? AND album=?";
                List<Song> songsByArtistAndAlbum = conn.Query<Song>(query,artist,album);

                return songsByArtistAndAlbum;
            }
        }

        // checks if song is in databse with the song's path and name
        public static Boolean checkIfSongIsInDatabase(string pathAndName)
        {
            using (var conn = new SQLite.SQLiteConnection(loadConnectionString()))
            {
                string query = "SELECT EXISTS(SELECT * FROM Song WHERE pathAndName=?)";
                int value = conn.ExecuteScalar<int>(query, pathAndName);
                if (value == 0)
                {

                    return false;
                }
                else
                {
                    return true; 
                }
            }


        }

        // checks if the databas exists 
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

        // gets a song's data with its id 
        public static Song getSong(int id)
        {
            using(var conn = new SQLite.SQLiteConnection(loadConnectionString()))
            {
                return conn.Table<Song>().Where(i => i.ID == id).FirstOrDefault();
            }
        }
    }
}
