using SQLite;
using System;


namespace MediaPlayer
{

    // a class that models a single row of the Song table in the database 
    public class Song 
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string name { get; set; }
        public string artist { get; set; }
        public string title { get; set; }
        public string album { get; set; }
        public TimeSpan duration { get; set; }
        public string pathAndName { get; set; }

        public Song()
        {
            this.artist = "unknown";
            this.title = "unknown";
            this.album = "unknown"; 
        }
        public Song(string name,string artist, string title, string album, TimeSpan duration, string pathAndName)
        {
            this.name = name; 
            this.artist = artist;
            this.title = title;
            this.album = album;
            this.duration = duration;
            this.pathAndName = pathAndName; 
        }

    }
}
