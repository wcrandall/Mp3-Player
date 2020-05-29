using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MediaPlayer.Model
{
    class PlayerOperations
    {

        [DllImport("winmm.dll")]
        private static extern long mciSendString(string lpstrCommand, StringBuilder lpstrReturnString, int uReturnLength, IntPtr hwndCallback);
        public int currentSongID;
        public bool isPaused; 

        public Song play(Song song,IntPtr handle)
        {
            stop();
            currentSongID = song.ID;



            string command = "open " + "\"" + song.pathAndName + "\"" + " type MPEGVideo alias Mp3File";
            mciSendString(command, null, 0, IntPtr.Zero);
            command = "play Mp3File notify";
            mciSendString(command, null, 0, handle);
            return song;
        }



        public Song nextSong( int[] ids, bool isShuffleOn,IntPtr handle)
        {



            int highestId = ids.Max();
            int lowestId = ids.Min();
            if (isShuffleOn)
            {

                Random random = new Random();
                int randomTrack = random.Next(lowestId, highestId);
                currentSongID = randomTrack;
                Song song = MediaDatabase.getSong(randomTrack);
                play(song,handle);
                return song;
            }
            else
            {
                if (highestId > currentSongID)
                {
                    currentSongID++;

                    Song song = MediaDatabase.getSong(currentSongID);
                    play(song,handle);
                    return song;
                }
                else
                {
                    currentSongID = lowestId;
                    Song song = MediaDatabase.getSong(currentSongID);
                    play(song,handle);
                    return song;
                }

            }
        }

        public void stop()
        {
            string command = "close Mp3File";
            mciSendString(command, null, 0, IntPtr.Zero);
        }

        public bool pause()
        {
            if (!isPaused)
            {
                string command = "pause Mp3File";
                mciSendString(command, null, 0, IntPtr.Zero);
                isPaused = true;
            }
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
