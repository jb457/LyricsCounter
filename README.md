# LyricsCounter
A Simple web application allowing you to search for an artist and find the average number of words in their songs lyrics

# Pre-requisites
You will need .NET Core 2.2 installed

# Running Instructions
Checkout the repository and open in Visual Studio (or an IDE of your choice)
Restore Nuget packages
Run

# Interacting
If all goes well you will load up to the Index page, with a link to take you to a simple search page.
Enter the name of an artist and press Search
Multiple results will likely be found from the initial API lookup so you will be prompted to confirm which artist you wish to investigate
Selecting one of the options will then trigger the work to begin
You will be regularly updated on the progress of the search, and eventually the page will complete where you will be given final results.
You can then choose to go back to the search page and try another artist

# More Details
Entering an artist and pressing search will call the MusicBrainz API to lookup the Artist.
Selecting the artist you wish to use will trigger the main body of work.

The Service class method CountLyricsForArtist will fire, which will:

  Call the MusicBrainz API to get the song for that artist.
  
  It will then loop through each track and call the Lyrics.ovh API to attempt to retreive the lyrics for each song
  
  When lyrics are successfully received it will count the number of words and store the results in a static data repository, updating the IProgress percentage each time.
  
  When all songs have been completed, the method will query the repository and return the results.
  
The UI meanwhile will load an InProgress page.
The View on this page has some javascript which is constantly calling and endpoint which is querying the status of the CountLyricsForArtist Task.
While the task is inprogress, a partial view is returned which has details on how far through the query the task currently is.
When complete, it returns a partial view with final results plus a link back to the search page.


# Improvements
The static Dictionary Repository is obviously not ideal as this will be destroyed when the application shuts down.
The Web UI would not work in a production environment because of a single static Task used to track results.  As such this page is limited to only ever running 1 query at a time.

# Issues

The MusicBrainz API does not consistantly return the same data, and the data it does return does not seem completely correct (for example, I searched for a band and the results said 98 songs, but only the first 22 songs were actually included in the results)
Potentially there is another endpoint I should be using instead of /artist/{MBId}?inc=works

I could not find an actual schema/definition of the format of the results which are returned from MusicBrainz.  
A built some model classes from the results of a query (using http://json2csharp.com/).  Then additional queries would have different field contents and cause errors.
I have resolved all errors I have come across, but potentially more may exist.

Numerous songs cannot be found when querying Lyrics.ovh.  Currently these are discarded.


# Comments
There is definite room for improvement - but time was a factor in getting this completed and working.
As such, some areas have been rushed.



