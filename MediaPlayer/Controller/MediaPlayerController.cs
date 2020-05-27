using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaPlayer.Model;
namespace MediaPlayer
{
    public class MediaPlayerController
    {
        private PlayerOperations player = new PlayerOperations();
        public Song nextSong( int[] ids, bool isShuffleOn, IntPtr handle)
        {
            return player.nextSong( ids, isShuffleOn, handle);
        }
        public List<Song> getSongsByAlbum(string album)
        {
            return MediaDatabase.getSongsByAlbum(album); 
        }
        public List<string> getAllAlbums()
        {
            return MediaDatabase.getAlbums();
        }
        public void stop()
        {
            player.stop();
        }
        public bool pause()
        {
            return player.pause();
        }
        public Song play(Song song, IntPtr hold)
        {
            return player.play(song, hold);
        }
        public List<Song> getAllSongs()
        {
            return MediaDatabase.getSongs();
        }

        public List<string> getAllArtists()
        {
            return MediaDatabase.getArtists();
        }
        
        public List<string> getAllAlbumsForArtist(string artist)
        {
            return MediaDatabase.getAlbumsByArtist(artist);
        }

        public void updateDatabase()
        {
            MediaDatabase.updateDatabase();
        }

        public List<Song> getSongsByArtist(string artist)
        {
            return MediaDatabase.getSongsByArtist(artist);
        }

        public List<Song> getSongsByArtistAndAlbum(string artist, string album)
        {
            return MediaDatabase.getSongsByArtistAndAlbum(artist, album); 
        }

        public Song getSong(int ID)
        {
            return MediaDatabase.getSong(ID); 
        }
    }
}
