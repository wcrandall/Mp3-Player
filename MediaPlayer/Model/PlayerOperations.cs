using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MediaPlayer.Model
{
    class PlayerOperations
    {
        // using windows media player .dll
        [DllImport("winmm.dll")]
        private static extern long mciSendString(string lpstrCommand, StringBuilder lpstrReturnString, int uReturnLength, IntPtr hwndCallback);
        public int currentSongID;
        public bool isPaused; 

        public Song play(Song song,IntPtr handle)
        {
            // stops a song if one is already playing 
            stop();
            // updates current song id
            currentSongID = song.ID;


            // opens song
            string command = "open " + "\"" + song.pathAndName + "\"" + " type MPEGVideo alias Mp3File";
            mciSendString(command, null, 0, IntPtr.Zero);
            // plays song 
            command = "play Mp3File notify";
            mciSendString(command, null, 0, handle);
            // returns song that is now playing 
            return song;
        }



        public Song nextSong( int[] ids, bool isShuffleOn,IntPtr handle)
        {


            // creating variables that will be used to ensure the song doesn't go out of range 
            int highestId = ids.Max();
            int lowestId = ids.Min();
            // if shuffle is on pick a random song using the ids array
            if (isShuffleOn)
            {

                Random random = new Random();
                int randomTrack = random.Next(lowestId, highestId);
                currentSongID = randomTrack;
                Song song = MediaDatabase.getSong(randomTrack);
                play(song,handle);
                return song;
            }
            // else just go to the next song
            else
            {
                // if the highest id is greater than the current song's 
                // just go to next song 
                if (highestId > currentSongID)
                {
                    currentSongID++;

                    Song song = MediaDatabase.getSong(currentSongID);
                    play(song,handle);
                    return song;
                }
                // if highest id is not greater than next song go back to first song 
                else
                {
                    currentSongID = lowestId;
                    Song song = MediaDatabase.getSong(currentSongID);
                    play(song,handle);
                    return song;
                }

            }
        }

        // stop playing songs 
        public void stop()
        {
            string command = "close Mp3File";
            mciSendString(command, null, 0, IntPtr.Zero);
        }

        // pause the currently playing song or resume the one that's paused
        public bool pause()
        {
            // if song isn't paused, pause it. 
            if (!isPaused)
            {
                string command = "pause Mp3File";
                mciSendString(command, null, 0, IntPtr.Zero);
                isPaused = true;
            }
            // if song is paused, resume it. 
            else
            {
                string command = "resume Mp3File";
                mciSendString(command, null, 0, IntPtr.Zero);
                isPaused = false;
            }
            return isPaused;
        }
    }
}
