using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using MediaPlayer.Model;
using static System.Windows.Forms.ListView;

namespace MediaPlayer
{
    public partial class SongView : Form
    {
        

        // Declare the nofify constant
        public const int MM_MCINOTIFY = 0x3B9;
        bool nextSongIsPressedAlready = false;
        bool isFirstSong = true;
        bool isShuffleOn = false;
        private MediaPlayerController mediaPlayerController = new MediaPlayerController();


        //PlayerOperations player = new PlayerOperations(); 
        public SongView()
        {
            InitializeComponent();

            if (Properties.Settings.Default.isFirstOpen)
            {
                getNewDatabasePath();
                mediaPlayerController.updateDatabase();
                Properties.Settings.Default.isFirstOpen = false;
                Properties.Settings.Default.Save();
            }
            else if (Properties.Settings.Default.updateDatabase)
            {
                mediaPlayerController.updateDatabase();
                Properties.Settings.Default.updateDatabase = false;
                Properties.Settings.Default.Save();
            }
            initializeListView(); 
            populateList();
            populateArtistList();
            populateAlbumList();

            createAndAddHeaderSongListView("Song");
            createAndAddHeaderSongListView("Album");
            createAndAddHeaderSongListView("Artist");
            createAndAddHeaderSongListView("Duration");
            songListView.FullRowSelect = true;
            sortByAlbumListView.FullRowSelect = true;
            sortByArtistListView.FullRowSelect = true; 


        }
        private void resizeColumnHeaders()
        {
            // stretching all columns to content except last column
            for (int i = 0; i < this.songListView.Columns.Count - 1; i++)
            {
                this.songListView.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent);
                // centering all columns but last
                this.songListView.Columns[i].TextAlign = HorizontalAlignment.Center;
            }
            // last column gets the left over available room
            this.songListView.Columns[this.songListView.Columns.Count - 1].Width = -2;
            // centering last column 
            this.songListView.Columns[this.songListView.Columns.Count - 1].TextAlign = HorizontalAlignment.Center;

            for(int i = 0; i < this.sortByAlbumListView.Columns.Count -1; i++)
            {
                this.sortByAlbumListView.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            this.sortByAlbumListView.Columns[this.sortByAlbumListView.Columns.Count - 1].Width = -2; 

            for(int i = 0; i < this.sortByArtistListView.Columns.Count; i++)
            {
                this.sortByArtistListView.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent); 
            }
            this.sortByArtistListView.Columns[this.sortByArtistListView.Columns.Count - 1].Width = -2;
        }



        private void initializeListView()
        {

           
        }

        // populates artist listview
        private void populateArtistList()
        {
            sortByArtistListView.Items.Clear();
            List<string> artists = mediaPlayerController.getAllArtists();
            ListViewItem allArtists = new ListViewItem(new String[] { "All artists" });
            sortByArtistListView.Items.Add(allArtists); 
            foreach(string artist in artists)
            {
                if (artist != null)
                {
                    ListViewItem nextItem = new ListViewItem(new String[] { artist });
                    sortByArtistListView.Items.Add(nextItem);
                }
            }
        }
        private void populateAlbumList()
        {
            sortByAlbumListView.Items.Clear();
            List<string> albums = mediaPlayerController.getAllAlbums();
            ListViewItem allAlbums = new ListViewItem(new String[] { "All albums" });
            sortByAlbumListView.Items.Add(allAlbums); 
            foreach(string album in albums)
            {
                if (album != null)
                {
                    ListViewItem nextItem = new ListViewItem(new String[] { album });
                    sortByAlbumListView.Items.Add(nextItem);
                }
            }
        }
        // shows albums by selected artist
        private void populateAlbumList(string artist)
        {
            sortByAlbumListView.Items.Clear();
            List<string> albums = mediaPlayerController.getAllAlbumsForArtist(artist);

            ListViewItem allAlbums = new ListViewItem(new String[] { "All albums" });
            sortByAlbumListView.Items.Add(allAlbums); 

            foreach(string album in albums)
            {
                if (album != null)
                {
                    ListViewItem nextItem = new ListViewItem(new String[] { album });
                    sortByAlbumListView.Items.Add(nextItem);
                }
            }
        }
        
        private void createAndAddHeaderSongListView(string headerText)
        {
            ColumnHeader header;
            header = new ColumnHeader();
            header.Text = headerText;
            
            songListView.Columns.Add(header); 
        }
        
        // by default all songs are shown 
        private void populateList()
        {
            songListView.Items.Clear();

            MediaPlayerController mediaPlayerController = new MediaPlayerController();
            List<Song> songs = mediaPlayerController.getAllSongs();

       
            foreach(Song song in songs)
            {
                ListViewItem nextItem = new ListViewItem(new String[]
                    {song.name,song.album,song.artist, string.Format("{0:D2}:{1:D2}", song.duration.Minutes,song.duration.Seconds), song.ID.ToString()});
                songListView.Items.Add(nextItem); 
            }
        }
        

        //overload populateList 
        // this shows songs by the current selected artist 
        private void populateList(string artist)
        {
            songListView.Items.Clear();
            MediaPlayerController mediaPlayerController = new MediaPlayerController();
            List<Song> songs = mediaPlayerController.getSongsByArtist(artist); 
            
            foreach(Song song in songs)
            {
                ListViewItem nextItem = new ListViewItem(new String[]
                    {song.name,song.album,song.artist, string.Format("{0:D2}:{1:D2}", song.duration.Minutes,song.duration.Seconds), song.ID.ToString()});
                songListView.Items.Add(nextItem);
            }
            
        }
        private void populateSongListByAlbum(string album)
        {
            songListView.Items.Clear();
            List<Song> songs = mediaPlayerController.getSongsByAlbum(album);
            foreach(Song song in songs)
            {
                ListViewItem nextItem = new ListViewItem(new String[]
                    {song.name,song.album,song.artist, string.Format("{0:D2}:{1:D2}", song.duration.Minutes,song.duration.Seconds), song.ID.ToString()});
                songListView.Items.Add(nextItem);
            }
        }



        // calls function that plays song 
        private void PlayButton_Click(object sender, EventArgs e)
        {
            if (songListView.SelectedItems.Count > 0)
            {
                ListViewItem item = songListView.SelectedItems[0];

                Song song = mediaPlayerController.getSong(Convert.ToInt32(item.SubItems[4].Text));
                //string song = MediaDatabase.path + "\\" + item.SubItems[0].Text;



                updateCurrentSong(mediaPlayerController.play(song,this.Handle));

                // if it is the first song there is no need to account for the automatic call of next song at the end of a song
                // if it isn't the first song it must be accounted for 
                if(!isFirstSong)
                {
                    nextSongIsPressedAlready = true; 
                }
                else
                {
                    isFirstSong = false; 
                }
            }
            
             
        }

        // function that plays a song
        public void updateCurrentSong(Song song)
        {

            // current song display 
            currentSongLabel.Text = song.title + " on " + song.album + " by " + song.artist;

        }

        public void updateCurrentSong()
        {
            currentSongLabel.Text = "";
        }




        // Override the WndProc function in the form
        // activates at the end of a song
        protected override void DefWndProc(ref Message m)
        {

            base.DefWndProc(ref m);

            if (m.Msg == MM_MCINOTIFY)
            {
                if (!nextSongIsPressedAlready)
                {
                    //MessageBox.Show("not even called :D ");
                    nextSongButton.PerformClick();
                }

                nextSongIsPressedAlready = false;
                
            }

        }


        // calls function that stops song 
        private void StopButton_Click(object sender, EventArgs e)
        {
            mediaPlayerController.stop();
            nextSongIsPressedAlready = true;
            updateCurrentSong(); 
        }

        // function that stops song currently playing



        // button that calls function that pauses and resumes 
        private void PauseButton_Click(object sender, EventArgs e)
        {


            bool isPaused = mediaPlayerController.pause();
            if (!isPaused)
            {
                pauseButton.Text = "Pause";
            }
            else
            {
                pauseButton.Text = "Resume";
            }

            
        }




        // goes to the next song
        private void NextSongButton_Click(object sender, EventArgs e)
        {
            nextSongIsPressedAlready = false;
            if (!nextSongIsPressedAlready)
            {
                // setting up variables needed for getting a list of songs
                int songsInListView = songListView.Items.Count;
                ListViewItem[] items = new ListViewItem[songsInListView];
                songListView.Items.CopyTo(items, 0);
                int[] ids = new int[songsInListView];
                int i = 0;

                foreach (ListViewItem item in items)
                {
                    ids[i] = Convert.ToInt32(item.SubItems[4].Text);
                    i++;
                }
                updateCurrentSong(mediaPlayerController.nextSong(ids,isShuffleOn,this.Handle));
                


                nextSongIsPressedAlready = false;
            }
            nextSongIsPressedAlready = true;
            
        }

        private void SortByArtistListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (sortByArtistListView.SelectedItems.Count > 0)
            {
                if (sortByArtistListView.SelectedItems[0].Text != "All artists")
                {
                    ListViewItem item = sortByArtistListView.SelectedItems[0];

                    string artist = item.SubItems[0].Text;

                    populateList(artist);

                    populateAlbumList(artist);
                }
                else
                {
                    populateList();
                    populateAlbumList();
                    
                }
            }
        }

        private void SortByAlbumListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (sortByAlbumListView.SelectedItems.Count > 0)
            {
                if (sortByAlbumListView.SelectedItems[0].Text != "All albums")
                {
                    ListViewItem item = sortByAlbumListView.SelectedItems[0];
                    string album = item.SubItems[0].Text;
                    populateSongListByAlbum(album);
                }
                else
                {
                    if (sortByArtistListView.SelectedItems.Count > 0 && sortByArtistListView.SelectedItems[0].Text!="All artists")
                    {
                        ListViewItem item = sortByArtistListView.SelectedItems[0];
                        string artist = item.SubItems[0].Text;
                        populateList(artist);
                    }
                    else
                    {
                        populateList();
                    }
                }
            }
        }

        private void ShuffleButton_Click(object sender, EventArgs e)
        {
            if(isShuffleOn)
            {
                isShuffleOn = false;
                shuffleButton.Text = "Shuffle Off";
            }
            else
            {
                isShuffleOn = true;
                shuffleButton.Text = "Shuffle On"; 
            }
        }

        private void SongView_Load(object sender, EventArgs e)
        {
            bool isUpdate = Properties.Settings.Default.updateDatabase;
            this.resizeColumnHeaders();

        }

        private void UpdateDatabaseNextOpeningToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.updateDatabase)
            {
                Properties.Settings.Default.updateDatabase = false;
                Properties.Settings.Default.Save();
            }
            else
            {
                Properties.Settings.Default.updateDatabase = true;
                Properties.Settings.Default.Save();
            }
        }
        private void getNewDatabasePath()
        {
            StringBuilder databasePath;

            Properties.Settings.Default.oldDatabasePath = Properties.Settings.Default.databasePath;
            Properties.Settings.Default.Save();

            databasePath = new StringBuilder(Interaction.InputBox("Where is your music?\nNote: Must restart for new music to show", "Database Path", "Enter Directory Here"));

            while (!System.IO.Directory.Exists(databasePath.ToString()) && databasePath.ToString() != "")
            {
                databasePath = new StringBuilder(Interaction.InputBox("Invalid path was entered. Please Enter a valid path or click Cancel. Where is your music?\nNote: Must restart for new music to show", "Database Path", "Enter Directory Here"));
            } 

            if (databasePath.ToString() == "")
            {
                if (Properties.Settings.Default.isFirstOpen)
                {
                    this.Close();
                }
                Properties.Settings.Default.databasePath = Properties.Settings.Default.oldDatabasePath;
            }
            else
            {
                MessageBox.Show(databasePath.ToString());
                Properties.Settings.Default.databasePath = databasePath.ToString();
                Properties.Settings.Default.Save();
            }

        }
        private void SetNewFilePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            getNewDatabasePath();
            if(MediaDatabase.path != Properties.Settings.Default.databasePath)
            {
                Properties.Settings.Default.updateDatabase = true;
                Properties.Settings.Default.Save(); 
            }
        }

        private void SongView_Resize(object sender, EventArgs e)
        {
            this.resizeColumnHeaders();
        }

        private void SongListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (StringFormat sf = new StringFormat())
            {

                sf.Alignment = StringAlignment.Center;
                
                // Draw the standard header background.
                e.DrawBackground();

                // Draw the header text.
                using (Font headerFont =
                            new Font("Helvetica", 10, FontStyle.Bold))
                {
                    e.Graphics.DrawString(e.Header.Text, headerFont,
                        Brushes.Black, e.Bounds, sf);
                }
            }
            return;
        }

        private void SongListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void SongListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        { 
            e.DrawDefault = true;
        }

        private void SortByArtistListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true; 
        }
        
        private void SortByArtistListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        private void SortByArtistListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (StringFormat sf = new StringFormat())
            {

                sf.Alignment = StringAlignment.Center;

                // Draw the standard header background.
                e.DrawBackground();

                // Draw the header text.
                using (Font headerFont =
                            new Font("Helvetica", 10, FontStyle.Bold))
                {
                    e.Graphics.DrawString(e.Header.Text, headerFont,
                        Brushes.Black, e.Bounds, sf);
                }
            }
            return;
        }

        private void SortByAlbumListView_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            using (StringFormat sf = new StringFormat())
            {

                sf.Alignment = StringAlignment.Center;

                // Draw the standard header background.
                e.DrawBackground();

                // Draw the header text.
                using (Font headerFont =
                            new Font("Helvetica", 10, FontStyle.Bold))
                {
                    e.Graphics.DrawString(e.Header.Text, headerFont,
                        Brushes.Black, e.Bounds, sf);
                }
            }
            return;
        }



        private void SortByAlbumListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true; 
        }

        private void SortByAlbumListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true; 
        }
    }
}
