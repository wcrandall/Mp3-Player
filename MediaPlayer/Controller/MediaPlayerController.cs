using MediaPlayer.Model;
using System;
using System.Collections.Generic;
namespace MediaPlayer
{
    public class MediaPlayerController
    {
        // this used for all play, stop, pause, etc. functions
        private PlayerOperations player = new PlayerOperations();

        // goes to the next song
        public Song nextSong( int[] ids, bool isShuffleOn, IntPtr handle)
        {
            return player.nextSong( ids, isShuffleOn, handle);
        }

        // gets songs in selected album
        public List<Song> getSongsByAlbum(string album)
        {
            return MediaDatabase.getSongsByAlbum(album); 
        }

        // gets all albums in database 
        public List<string> getAllAlbums()
        {
            return MediaDatabase.getAlbums();
        }

        // stops the song that is currently playing
        public void stop()
        {
            player.stop();
        }

        // pauses the currently playing song 
        public bool pause()
        {
            return player.pause();
        }
        
        // plays selected song 
        public Song play(Song song, IntPtr hold)
        {
            return player.play(song, hold);
        }
        
        // gets all songs in database
        public List<Song> getAllSongs()
        {
            return MediaDatabase.getSongs();
        }

        // gets all artists in database
        public List<string> getAllArtists()
        {
            return MediaDatabase.getArtists();
        }
        
        // gets all albums for selected artist
        public List<string> getAllAlbumsForArtist(string artist)
        {
            return MediaDatabase.getAlbumsByArtist(artist);
        }

        // updates database
        public Boolean updateDatabase()
        {
            return MediaDatabase.updateDatabase();
        }

        // gets all songs for selected artist 
        public List<Song> getSongsByArtist(string artist)
        {
            return MediaDatabase.getSongsByArtist(artist);
        }

        // gets all songs by artist in a particular album
        public List<Song> getSongsByArtistAndAlbum(string artist, string album)
        {
            return MediaDatabase.getSongsByArtistAndAlbum(artist, album); 
        }

        // gets a song with its id
        public Song getSong(int ID)
        {
            return MediaDatabase.getSong(ID); 
        }
    }
}
