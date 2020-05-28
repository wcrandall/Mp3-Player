<p align="center">
   <img width="200px" height="200px" title="Mp3PlayerLogo" alt="Mp3PlayerLogo" src="MediaPlayer\Images\Mp3PlayerIcon.png">
</p>   


# MP3 Player 

> After the user enters their Mp3's location, they are loaded and able to be played. 

* Versions 
    * 1.0.0
* Dependencies 
    * sqlite-net-pcl
    * TagLibSharp 
    * Microsoft.WindowsAPICodePack.Shell
    
<img title="Mp3PlayerHomescreen" alt="Mp3PlayerHomescreen" src="MediaPlayer\Images\Mp3PlayerHomescreen.PNG">

## Installing/Running the application  
* Option 1 
    * Download the zip, pull, or clone <a href="https://github.com/wcrandall/Mp3-Player">the project from github</a>
    * If zip is downloaded, extract the zip file. 
    * Go into the folder the project is in 
    * Open the .sln file with Visual Studio 
    * Right click on the solution and click manage nuget packages 
    * Search for `Microsoft.WindowsAPICodePack.Shell by Licshee`
    * Install it
    * Click Start
    * Note: If an error is given saying `Couldn't process file SongView.resx due to its being in the Internet or Restricted zone or having the mark of the web on the file. Remove the mark of the web if you want to process these files.`
      * Go to the MediaPlayer folder
      * Right click SongView.resx
      * Click properties
      * Click the General tab
      * At the bottom by where it says `This file came from another computer and might be blocked to help protect this computer` Click 'Unblock' 
      * Close Visual Studio, reopen the project, and click Start
* Option 2 
    * Click on the releases tab of the project in Github
    * Click on the .exe file
    * Save the file 
    * Open the folder it was downloaded into
    * Run the .exe file 

## Note
* Music must be stored in the user's chosen directory in this manner: `chosenDirectory\artistName\albumName\songs.mp3`


## Clone
* Clone this repo to your local machine using `git clone https://github.com/wcrandall/Mp3Player.git`

## Contributors 
* <a href="https://github.com/wcrandall"> Wyatt Crandall </a> 


