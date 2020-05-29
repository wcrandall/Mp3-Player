//using MediaPlayer.Model;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

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
             
            // if it is the first open, a path to songs must be gotten 
            if (Properties.Settings.Default.isFirstOpen)
            {
                getNewDatabasePath();
                // use the path to set up database
                Boolean wasSuccessful = mediaPlayerController.updateDatabase();
                // if the update was not successful tell the user 
                displayPromptIfDatabaseUpdateUnsuccessful(wasSuccessful);
                // if it was successful make current database path the old database path 
                if(wasSuccessful)
                {
                    Properties.Settings.Default.oldDatabasePath = Properties.Settings.Default.databasePath;
                    Properties.Settings.Default.Save();
                }
            }
            // if it is not the first open just update the already set path 
            else
            {
                Boolean wasSuccessful = mediaPlayerController.updateDatabase();
                // if the update is not successful inform the user 
                displayPromptIfDatabaseUpdateUnsuccessful(wasSuccessful);
                
            }
            initializeListView();

            // creating headers for songListView
            createAndAddHeaderSongListView("Song");
            createAndAddHeaderSongListView("Album");
            createAndAddHeaderSongListView("Artist");
            createAndAddHeaderSongListView("Duration");
            // the whole row in the listviews will be selected rather than just a column 
            songListView.FullRowSelect = true;
            sortByAlbumListView.FullRowSelect = true;
            sortByArtistListView.FullRowSelect = true;
            // update the graphical interface, so it matches data in database
            updatingGUI();
            



        }

        // displays a prompt if database was not updated successfully 
        private void displayPromptIfDatabaseUpdateUnsuccessful(Boolean wasSuccessful)
        {
            // if it is not the first open of the program and the update was not successful tell the user than revert to previously used path
            if (!Properties.Settings.Default.isFirstOpen)
            {
                if (!wasSuccessful)
                {
                    MessageBox.Show("Access denied. Ensure you have rights to access all folders and files in " + Properties.Settings.Default.databasePath + "\n " +
                                   "Reverting to previous directory " + Properties.Settings.Default.oldDatabasePath, "Access Denied");
                    Properties.Settings.Default.databasePath = Properties.Settings.Default.oldDatabasePath;
                    Properties.Settings.Default.Save();
                    mediaPlayerController.updateDatabase();
                }
            }
            else
            {
                // if it is the first open of the program and the update is successful, change the is first open variable to false 
                if (wasSuccessful)
                {
                    Properties.Settings.Default.isFirstOpen = false;
                    Properties.Settings.Default.Save();
                }
                // if it is the first open of the program and the update was not successful tell the user and close the program 
                else
                {
                    MessageBox.Show("Access denied. Ensure you have rights to access all folders and files in " + Properties.Settings.Default.databasePath, "Access Denied");
                    System.Environment.Exit(0);
                }
            }
        }
        
        // updates gui to match database
        private void updatingGUI()
        {
            populateList();
            populateArtistList();
            populateAlbumList();
            // resizes column headers to match contents 
            resizeColumnHeaders();
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
            // removes all items from artisListView
            sortByArtistListView.Items.Clear();
            // gets all artists from database
            List<string> artists = mediaPlayerController.getAllArtists();
            // creating listview item for an all artist selection that will display all songs currently in database
            ListViewItem allArtists = new ListViewItem(new String[] { "All Artists" });
            // adding all artist selection 
            sortByArtistListView.Items.Add(allArtists); 

            // for each artist in database, put the artist in a listview item than add it to the listview
            foreach(string artist in artists)
            {
                if (artist != null)
                {
                    ListViewItem nextItem = new ListViewItem(new String[] { artist });
                    sortByArtistListView.Items.Add(nextItem);
                }
            }
        }
        
        // adds all albums in database to album listview 
        private void populateAlbumList()
        {
            // removing all albums currently being displayed from listiview
            sortByAlbumListView.Items.Clear();
            // getting all albums from database
            List<string> albums = mediaPlayerController.getAllAlbums();
            // creating an option to display albums in database
            ListViewItem allAlbums = new ListViewItem(new String[] { "All Albums" });
            // adding the all albums option to the listview
            sortByAlbumListView.Items.Add(allAlbums); 

            // adding the rest of the albums to the listview
            foreach(string album in albums)
            {
                if (album != null)
                {
                    ListViewItem nextItem = new ListViewItem(new String[] { album });
                    sortByAlbumListView.Items.Add(nextItem);
                }
            }
        }

        // populates album list view with albums by a selected artist 
        private void populateAlbumList(string artist)
        {
            // removes albums currently in listview
            sortByAlbumListView.Items.Clear();
            // gets all albums by the artist
            List<string> albums = mediaPlayerController.getAllAlbumsForArtist(artist);
            // adding the option to display all albums by artist
            ListViewItem allAlbums = new ListViewItem(new String[] { "All Albums" });
            sortByAlbumListView.Items.Add(allAlbums); 

            // adding albums by artist
            foreach(string album in albums)
            {
                if (album != null)
                {
                    ListViewItem nextItem = new ListViewItem(new String[] { album });
                    sortByAlbumListView.Items.Add(nextItem);
                }
            }
        }
        
        // creates and adds a header for song list view with the given string 
        private void createAndAddHeaderSongListView(string headerText)
        {
            ColumnHeader header;
            header = new ColumnHeader();
            header.Text = headerText;
            
            songListView.Columns.Add(header); 
        }

        // populates the song listview with all songs
        private void populateList()
        {
            // removes all songs currently in songlistview 
            songListView.Items.Clear();

            // getting all songs in database 
            List<Song> songs = mediaPlayerController.getAllSongs();

       
            // adding songs to the listview 
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

        // shows song in a given album 
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



        // calls function that plays song upon play button click 
        private void PlayButton_Click(object sender, EventArgs e)
        {
            if (songListView.SelectedItems.Count > 0)
            {
                // getting the selected item form the listview 
                // we've only allowed one item to be selected at a time 
                // so songListView.selectedItems[0] does this 
                ListViewItem item = songListView.SelectedItems[0];

                // getting all song information for the song
                Song song = mediaPlayerController.getSong(Convert.ToInt32(item.SubItems[4].Text));
                //string song = MediaDatabase.path + "\\" + item.SubItems[0].Text;


                // playing the song and updating gui to say it is playing
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

        // changes the text of the label that states which song is currently playing 
        public void updateCurrentSong(Song song)
        {

            // current song display 
            currentSongLabel.Text = song.title + " on " + song.album + " by " + song.artist;

        }

        // changes the text to currentSongLabel to nothing
        // it is called when no song is playing 
        public void updateCurrentSong()
        {
            currentSongLabel.Text = "";
        }




        // Override the WndProc function in the form
        protected override void DefWndProc(ref Message m)
        {

            base.DefWndProc(ref m);

            // if m.msg is equal to the notify we created that signifies the end of a song check if nextsongispressedalready
            if (m.Msg == MM_MCINOTIFY)
            {
                // if next song isn't pressed already go to the next song 
                if (!nextSongIsPressedAlready)
                {
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



        // button that calls function that pauses and resumes 
        private void PauseButton_Click(object sender, EventArgs e)
        {


            bool isPaused = mediaPlayerController.pause();
            // changes the pause buttons text depending on whether song is currently paused 
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
            // If the first song hasn't been played go to the next song 
            if (!isFirstSong)
            {
                
                nextSongIsPressedAlready = false;
                if (!nextSongIsPressedAlready)
                {

                    // setting up variables needed for getting a list of songs
                    // if there is more than one song in the listview for songs get there ids
                    int songsInListView = songListView.Items.Count;
                    if (songsInListView != 0)
                    {
                        ListViewItem[] items = new ListViewItem[songsInListView];
                        songListView.Items.CopyTo(items, 0);
                        int[] ids = new int[songsInListView];
                        int i = 0;

                        foreach (ListViewItem item in items)
                        {
                            ids[i] = Convert.ToInt32(item.SubItems[4].Text);
                            i++;
                        }
                        // use the ids to go to the next song 
                        updateCurrentSong(mediaPlayerController.nextSong(ids, isShuffleOn, this.Handle));



                        nextSongIsPressedAlready = false;
                    }
                }
            }
            nextSongIsPressedAlready = true;
            
        }

        private void SortByArtistListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            // if an artist is selected update the album and song list 
            if (sortByArtistListView.SelectedItems.Count > 0)
            {
                // if the artist isn't all artists get the artist from the listview and use it to populate the song and album listviews
                if (sortByArtistListView.SelectedItems[0].Text != "All Artists")
                {
                    ListViewItem item = sortByArtistListView.SelectedItems[0];

                    string artist = item.SubItems[0].Text;

                    populateList(artist);

                    populateAlbumList(artist);
                }
                // if the artist is all artists simply populate song and album list views
                // populateList and populateAlbumList populate with all albums and all songs by default 
                else
                {
                    populateList();
                    populateAlbumList();
                    
                }
            }
        }

        private void SortByAlbumListView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            // if there is an album selected change the listview for songs
            if (sortByAlbumListView.SelectedItems.Count > 0)
            {
                // if the album selected isn't all abums get there album name populate the song listview accordingly for that album
                if (sortByAlbumListView.SelectedItems[0].Text != "All Albums")
                {
                    ListViewItem item = sortByAlbumListView.SelectedItems[0];
                    string album = item.SubItems[0].Text;
                    populateSongListByAlbum(album);
                }
                // if the selected album is all albums
                else
                {
                    // check to see if there are any artists selected
                    // if an artist is selected that isn't all artists 
                    // populate the songlistview with all that artist's songs
                    if (sortByArtistListView.SelectedItems.Count > 0 && sortByArtistListView.SelectedItems[0].Text!="All Artists")
                    {
                        ListViewItem item = sortByArtistListView.SelectedItems[0];
                        string artist = item.SubItems[0].Text;
                        populateList(artist);
                    }
                    // all artists and all songs is selected populate the songlistview with all songs 
                    // all songs is default for populate lists 
                    else
                    {
                        populateList();
                    }
                }
            }
        }

        // turns shuffle on or off
        private void ShuffleButton_Click(object sender, EventArgs e)
        {
            // if shuffle is on turn shuffle off 
            // and change the text on the button to signify that it is off
            if(isShuffleOn)
            {
                isShuffleOn = false;
                shuffleButton.Text = "Shuffle Off";
            }
            // if shuffle is off turn shuffle on and change the button text signify it is on 
            else
            {
                isShuffleOn = true;
                shuffleButton.Text = "Shuffle On"; 
            }
        }

        



        private void getNewDatabasePath()
        {
            // setting up a dialog to set a folder where music is located
            CommonOpenFileDialog commonOpenFileDialog = new CommonOpenFileDialog();
            // the dialog will start in the users directory
            commonOpenFileDialog.InitialDirectory = "C:\\Users";
            // this makes it so the dialog is folders not files
            commonOpenFileDialog.IsFolderPicker = true;
            // changes the title so user knows what dialog is for 
            commonOpenFileDialog.Title = "Enter Music Folder Directory";
            // shows dialog and stores result in dialog result 
            var dialogResult = commonOpenFileDialog.ShowDialog();
            
            // if the user pressed ok and entered a folder path save it as the new database path 
            if ( dialogResult == CommonFileDialogResult.Ok && !string.IsNullOrWhiteSpace(commonOpenFileDialog.FileName))
            {

                Properties.Settings.Default.databasePath = commonOpenFileDialog.FileName;
                Properties.Settings.Default.Save();
                
            }
            // else if the user canceled and it is the first open of the program exit 
            else if (dialogResult == CommonFileDialogResult.Cancel && Properties.Settings.Default.isFirstOpen)
            {
                System.Environment.Exit(0);
            }
        }

        private void SetNewFilePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // gets a database path from the user 
            getNewDatabasePath();


                // updating databse 
                Boolean wasSuccessful = mediaPlayerController.updateDatabase();
                // tell the user whether the update was successful or not 
                displayPromptIfDatabaseUpdateUnsuccessful(wasSuccessful);
                // if the udpate was successful make the new database path equal to old database path 
                // than update the gui to match the database 
                if (wasSuccessful)
                {
                    Properties.Settings.Default.oldDatabasePath = Properties.Settings.Default.databasePath;
                    Properties.Settings.Default.Save();
                    updatingGUI();
                }
            
        }

        // when the window is resized resize column headers
        private void SongView_Resize(object sender, EventArgs e)
        {
            this.resizeColumnHeaders();
        }

        // centering column header for song list view
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

        // if drawing column headers  items and subitems must be drawn too so I just used the default. 
        private void SongListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        // if drawing column headers  items and subitems must be drawn too so I just used the default. 
        private void SongListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        { 
            e.DrawDefault = true;
        }

        // if drawing column headers  items and subitems must be drawn too so I just used the default. 
        private void SortByArtistListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true; 
        }

        // if drawing column headers  items and subitems must be drawn too so I just used the default. 
        private void SortByArtistListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true;
        }

        // centering column header for artist list view 
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
        // centering column header for album list view
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


        // if drawing column headers  items and subitems must be drawn too so I just used the default. 
        private void SortByAlbumListView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            e.DrawDefault = true; 
        }

        // if drawing column headers  items and subitems must be drawn too so I just used the default. 
        private void SortByAlbumListView_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            e.DrawDefault = true; 
        }
    }
}
