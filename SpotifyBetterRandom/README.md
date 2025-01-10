# SpotifyBetterRandom

This is just a project to improve the shitty spotify shuffle algorithm.  
For now there is a lot to work on. but bit by bit.

## Roadmap
### v0.1.0 (The Basics)
- Creates truly random playlists automatically from users created playlists, those playlists will be stored in a custom folder

### v0.2.0 (Now We're talking)
- Implements a custom algorithm so it's not truly random, but based on actual properties of the song (Genre, intensity etc...) instead of original order or popularity. **Truly random is still an option**

### v1.0.0 (Full release)
- The program does the work automagically. (Detection of the playlist, creation of the random playlist and starts playing it)
## Technical TODO List
- Create Song Cache
  - It must persist on the device (A simple Json File should work)

### Technical Debt
- Migration from the Custom implementation of the Spotify API to Spotify.NET (Damn you OAuth)
There is a lot of shit to remove from my testing. 

![](https://brainmade.org/88x31-dark.png)