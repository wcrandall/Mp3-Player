namespace MediaPlayer
{
    partial class SongView
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SongView));
            this.songListView = new System.Windows.Forms.ListView();
            this.playButton = new System.Windows.Forms.Button();
            this.stopButton = new System.Windows.Forms.Button();
            this.pauseButton = new System.Windows.Forms.Button();
            this.nextSongButton = new System.Windows.Forms.Button();
            this.sortByArtistListView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.sortByAlbumListView = new System.Windows.Forms.ListView();
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.currentSongLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.shuffleButton = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setNewFilePathToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // songListView
            // 
            this.songListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.songListView.HideSelection = false;
            this.songListView.Location = new System.Drawing.Point(3, 3);
            this.songListView.Name = "songListView";
            this.songListView.OwnerDraw = true;
            this.tableLayoutPanel1.SetRowSpan(this.songListView, 4);
            this.songListView.Size = new System.Drawing.Size(596, 378);
            this.songListView.TabIndex = 0;
            this.songListView.UseCompatibleStateImageBehavior = false;
            this.songListView.View = System.Windows.Forms.View.Details;
            this.songListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.SongListView_DrawColumnHeader);
            this.songListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.SongListView_DrawItem);
            this.songListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.SongListView_DrawSubItem);
            // 
            // playButton
            // 
            this.playButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playButton.Location = new System.Drawing.Point(1087, 3);
            this.playButton.Name = "playButton";
            this.playButton.Size = new System.Drawing.Size(115, 90);
            this.playButton.TabIndex = 1;
            this.playButton.Text = "Play";
            this.playButton.UseVisualStyleBackColor = true;
            this.playButton.Click += new System.EventHandler(this.PlayButton_Click);
            // 
            // stopButton
            // 
            this.stopButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stopButton.Location = new System.Drawing.Point(1087, 291);
            this.stopButton.Name = "stopButton";
            this.stopButton.Size = new System.Drawing.Size(115, 90);
            this.stopButton.TabIndex = 2;
            this.stopButton.Text = "Stop";
            this.stopButton.UseVisualStyleBackColor = true;
            this.stopButton.Click += new System.EventHandler(this.StopButton_Click);
            // 
            // pauseButton
            // 
            this.pauseButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pauseButton.Location = new System.Drawing.Point(1087, 195);
            this.pauseButton.Name = "pauseButton";
            this.pauseButton.Size = new System.Drawing.Size(115, 90);
            this.pauseButton.TabIndex = 3;
            this.pauseButton.Text = "Pause";
            this.pauseButton.UseVisualStyleBackColor = true;
            this.pauseButton.Click += new System.EventHandler(this.PauseButton_Click);
            // 
            // nextSongButton
            // 
            this.nextSongButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.nextSongButton.Location = new System.Drawing.Point(1087, 99);
            this.nextSongButton.Name = "nextSongButton";
            this.nextSongButton.Size = new System.Drawing.Size(115, 90);
            this.nextSongButton.TabIndex = 4;
            this.nextSongButton.Text = "Next Song";
            this.nextSongButton.UseVisualStyleBackColor = true;
            this.nextSongButton.Click += new System.EventHandler(this.NextSongButton_Click);
            // 
            // sortByArtistListView
            // 
            this.sortByArtistListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.sortByArtistListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sortByArtistListView.HideSelection = false;
            this.sortByArtistListView.Location = new System.Drawing.Point(605, 3);
            this.sortByArtistListView.Name = "sortByArtistListView";
            this.sortByArtistListView.OwnerDraw = true;
            this.tableLayoutPanel1.SetRowSpan(this.sortByArtistListView, 4);
            this.sortByArtistListView.Size = new System.Drawing.Size(235, 378);
            this.sortByArtistListView.TabIndex = 6;
            this.sortByArtistListView.UseCompatibleStateImageBehavior = false;
            this.sortByArtistListView.View = System.Windows.Forms.View.Details;
            this.sortByArtistListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.SortByArtistListView_DrawColumnHeader);
            this.sortByArtistListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.SortByArtistListView_DrawItem);
            this.sortByArtistListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.SortByArtistListView_DrawSubItem);
            this.sortByArtistListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.SortByArtistListView_ItemSelectionChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Sort By Artist";
            this.columnHeader1.Width = 117;
            // 
            // sortByAlbumListView
            // 
            this.sortByAlbumListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader2});
            this.sortByAlbumListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.sortByAlbumListView.HideSelection = false;
            this.sortByAlbumListView.Location = new System.Drawing.Point(846, 3);
            this.sortByAlbumListView.Name = "sortByAlbumListView";
            this.sortByAlbumListView.OwnerDraw = true;
            this.tableLayoutPanel1.SetRowSpan(this.sortByAlbumListView, 4);
            this.sortByAlbumListView.Size = new System.Drawing.Size(235, 378);
            this.sortByAlbumListView.TabIndex = 7;
            this.sortByAlbumListView.UseCompatibleStateImageBehavior = false;
            this.sortByAlbumListView.View = System.Windows.Forms.View.Details;
            this.sortByAlbumListView.DrawColumnHeader += new System.Windows.Forms.DrawListViewColumnHeaderEventHandler(this.SortByAlbumListView_DrawColumnHeader);
            this.sortByAlbumListView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.SortByAlbumListView_DrawItem);
            this.sortByAlbumListView.DrawSubItem += new System.Windows.Forms.DrawListViewSubItemEventHandler(this.SortByAlbumListView_DrawSubItem);
            this.sortByAlbumListView.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.SortByAlbumListView_ItemSelectionChanged);
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Sort By Album";
            this.columnHeader2.Width = 126;
            // 
            // currentSongLabel
            // 
            this.currentSongLabel.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.currentSongLabel, 3);
            this.currentSongLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentSongLabel.Location = new System.Drawing.Point(3, 384);
            this.currentSongLabel.Name = "currentSongLabel";
            this.currentSongLabel.Size = new System.Drawing.Size(1078, 43);
            this.currentSongLabel.TabIndex = 8;
            this.currentSongLabel.Text = "song";
            this.currentSongLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 4;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Controls.Add(this.songListView, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.currentSongLabel, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.pauseButton, 3, 2);
            this.tableLayoutPanel1.Controls.Add(this.nextSongButton, 3, 1);
            this.tableLayoutPanel1.Controls.Add(this.stopButton, 3, 3);
            this.tableLayoutPanel1.Controls.Add(this.playButton, 3, 0);
            this.tableLayoutPanel1.Controls.Add(this.sortByAlbumListView, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.sortByArtistListView, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.shuffleButton, 3, 4);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 24);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 22.5F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1205, 427);
            this.tableLayoutPanel1.TabIndex = 9;
            // 
            // shuffleButton
            // 
            this.shuffleButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.shuffleButton.Location = new System.Drawing.Point(1087, 387);
            this.shuffleButton.Name = "shuffleButton";
            this.shuffleButton.Size = new System.Drawing.Size(115, 37);
            this.shuffleButton.TabIndex = 9;
            this.shuffleButton.Text = "Shuffle Off";
            this.shuffleButton.UseVisualStyleBackColor = true;
            this.shuffleButton.Click += new System.EventHandler(this.ShuffleButton_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.optionsToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1205, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setNewFilePathToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // setNewFilePathToolStripMenuItem
            // 
            this.setNewFilePathToolStripMenuItem.Name = "setNewFilePathToolStripMenuItem";
            this.setNewFilePathToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.setNewFilePathToolStripMenuItem.Text = "Set New File Path";
            this.setNewFilePathToolStripMenuItem.Click += new System.EventHandler(this.SetNewFilePathToolStripMenuItem_Click);
            // 
            // SongView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1205, 451);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SongView";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Song Player";
            this.Load += new System.EventHandler(this.SongView_Load);
            this.Resize += new System.EventHandler(this.SongView_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView songListView;
        private System.Windows.Forms.Button playButton;
        private System.Windows.Forms.Button stopButton;
        private System.Windows.Forms.Button pauseButton;
        private System.Windows.Forms.Button nextSongButton;
        private System.Windows.Forms.ListView sortByArtistListView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ListView sortByAlbumListView;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Label currentSongLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button shuffleButton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setNewFilePathToolStripMenuItem;
    }
}

