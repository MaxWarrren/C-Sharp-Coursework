namespace Random_Song;

class Program
{       
    static string[] loadSongs(string directory) {
        string songData = File.ReadAllText(directory);
        return songData.Split("\n");
    }

    static void savePlaylist(List<string> playlist) {
        Console.WriteLine("Enter a name for your playlist file: ");
        string fileName = Console.ReadLine();
        if (!fileName.EndsWith(".txt")) {
            fileName += ".txt";
        }
        string filePath = $"playlists\\{fileName}";

        using (StreamWriter sw = File.CreateText(filePath)) {
            foreach (string song in playlist) {
                sw.WriteLine(song);
            }
        }
        Console.WriteLine($"Playlist successfully saved to {fileName}\n ");
    }

    static int getSaveQuitOption() {
        while (true) {
            Console.WriteLine("\nChoose one of the following options\n-----------------------");
            Console.WriteLine("1. Save Playlist & Continue\n2. Save Playlist & Quit\n3. DON'T Save Playlist & Continue\n4. DON'T Save Playlist & Quit");
            try {
                int sqRes = Convert.ToInt32(Console.ReadLine());
                if (1<=sqRes && sqRes<=4) {
                    return sqRes;
                } else {
                    Console.WriteLine("ERROR: Number has to be 1-4");
                    continue;
                }
            } catch (Exception e) {
                Console.WriteLine("ERROR: Enter a number input");
                continue;
            }
           
        }
    }
    static void Main(string[] args)
    {   
        string[] songs = loadSongs("songs.csv");
        int len = songs.Length;
        bool running = true;
        int playlistCnt = 1, playlistLength;

        List<string> newPlaylist = new List<string>();
        Random rd = new Random();

        Console.WriteLine("Random Music Playlist\n----------------------");

        while (running) {
            newPlaylist.Clear();
            
            while (true) {
                Console.WriteLine("Enter the number of songs to add: ");
                playlistLength = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Generating...");
                Console.WriteLine($"Playlist {playlistCnt}\n------------");
                if (1<=playlistLength && playlistLength<=len) {
                    for (int n=1; n<=playlistLength; n++) {
                        int rn = rd.Next(0, len);
                        string song = songs[rn];
                        Console.WriteLine($"Song {n}: {song}");
                        if (!newPlaylist.Contains(song)) {
                            newPlaylist.Add(song);
                        } else {
                            n--;
                        }  
                    }
                    playlistCnt++;
                    break;
                } else {
                    Console.WriteLine($"ERROR: Please enter a number between 1-{len}");
                }
            }
            
            int sqRes = getSaveQuitOption();
            
            switch (sqRes) {
                case 1:
                savePlaylist(newPlaylist);
                break;
                case 2:
                savePlaylist(newPlaylist);
                running = false;
                break;
                case 3:
                break;
                case 4:
                running = false;
                break;
            }
        }
        Console.WriteLine("Ending...");
    }
}
